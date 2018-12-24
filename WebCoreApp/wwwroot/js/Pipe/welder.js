$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    table = $("#tblWelder").DataTable({
        proccessing: true,
        serverSide: true,
        ajax: {
            url: '/Pipes/Welders/Load',
            type: 'post',
            dataType: 'Json'
        },
        deferRender: true,
        order: [[0, 'asc']],
        columnDefs: [
            { className: 'dt-head-center', targets: '_all' }
        ],
        columns: [
            { data: 'Id' },
            { data: 'Name' },
            {
                data: 'BrithDay', render: function (data) {
                    return moment(data).format('DD/MM/YYYY');
                }
            },
            {
                data: 'Status', render: function (data) {
                    return data === false ? '<span class="label label-danger">Khóa</span>' : '<span class="label label-warning">Hoạt động</span>';
                }
            }
        ],
        select: {
            style: 'single'
        }
    });
});

function getdata() {
    var ma = table.row('.selected').data();
    if (ma === undefined) {
        bootbox.alert("Bạn chưa chọn dữ liệu");
        return false;
    }
    detail(ma.Id,true);
}
function detail(id,update) {
    $.ajax({
        url: '/Pipes/Welders/Get',
        data: { id: id, update:update },
        type: 'get'
    })
    .done((data) => {
        $('#modal-header').html('Thợ hàn');
        $('#modalbody').html(data);
        formatDate();
        
        $('#myModal').modal('show');
        web.formValidation('#frmWelder');
    })
    .fail((err, txtStatus) => {
        web.notify(`${err.status}: ${err.responseText}`, "error");
    });
}

function saveData() {
    if (!$('#frmWelder').valid()) return false;
    var data = $('#frmWelder').serializeJSON();
    data.Update = $('#Update').prop('checked');
    data.Status = $('#Status').prop('checked');
    $('#myModal').modal('hide');
    $.ajax({
        url: '/Pipes/Welders/Save',
        type: 'post',
        headers: {
            'Content-Type': 'application/json;charset=UTF-8'
        },
        data: JSON.stringify(data)
    })
    .done((data) => {
        table.ajax.reload();
        web.notify(data, "success");
    }).fail((err, txtStatus) => {
        web.notify(`${err.status}: ${err.responseText}`, "error");
    });
}
function deleteData() {
    var ma = table.row('.selected').data();
    if (ma === undefined) {
        bootbox.alert("Bạn chưa chọn dữ liệu");
        return false;
    }
    web.confirm('Bạn muốn xóa cái này', function () {
        $.ajax({
            url: '/Pipes/Welders/Delete',
            type: 'post',
            data:{id:ma.Id}
        }).done((data) => {
            table.ajax.reload();
            web.notify(data, "success");
        }).fail((err, txtStatus) => {
            web.notify(`${err.status}: ${err.responseText}`, "error");
        });
    });
}

function formatDate() {
    $('#Bdate').datepicker({
        dateFormat: 'dd/mm/yy'
    }).on({
        click: function () {
            $('#BrithDay').click();
        },
        change: function () {
            var date = $('#Bdate').val();
            if (moment(date, "DD/MM/YYYY",true).isValid()) {
                $('#BrithDay').val(moment(date, "DD/MM/YYYY").format());
            }
            else {
                $('#BrithDay').val('');
            }
        }
    });
    $('#BrithDay').on("change", function () {
        var date = $(this).val();
        $('#Bdate').val(moment(date).format("DD/MM/YYYY"));
    });
}