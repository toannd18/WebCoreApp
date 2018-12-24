$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    table = $("#tblMaterial").DataTable({
        proccessing: true,
        serverSide: true,
        ajax: {
            url: '/Pipes/Materials/Load',
            type: 'post',
            dataType: 'Json'
        },
        deferRender: true,
        order: [[1, 'asc']],
        columnDefs: [
            { className: 'dt-body-center', targets: '_all' },
            { className: 'dt-head-center', targets: '_all' },
            {
                orderable: false,
                width: '5%',
                targets: 0
            }
        ],
        columns: [
            { data: null },
            { data: 'Name' },
            { data: 'Description' }
        ],
        select: {
            style: 'single'
        }
    });
    table.on('draw.dt', function () {
        var info = table.page.info();
        table.column(0, { search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1 + info.start;
        });
    });
});

function getdata() {
    var ma = table.row('.selected').data();
    if (ma === undefined) {
        bootbox.alert("Bạn chưa chọn dữ liệu");
        return false;
    }
    detail(ma.Name, true);
}
function detail(id, update) {
    $.ajax({
        url: '/Pipes/Materials/Get',
        data: { id: id, update: update },
        type: 'get'
    })
    .done((data) => {
        $('#modal-header').html('Vật liệu');
        $('#modalbody').html(data);
    

        $('#myModal').modal('show');
        web.formValidation('#frmMaterial');
    })
    .fail((err, txtStatus) => {
        web.notify(`${err.status}: ${err.responseText}`, "error");
    });
}

function saveData() {
    if (!$('#frmMaterial').valid()) return false;
    var data = $('#frmMaterial').serializeJSON();
    data.Update = $('#Update').prop('checked');
   
    $('#myModal').modal('hide');
    $.ajax({
        url: '/Pipes/Materials/Save',
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
            url: '/Pipes/Materials/Delete',
            type: 'post',
            data: { id: ma.Name }
        }).done((data) => {
            table.ajax.reload();
            web.notify(data, "success");
        }).fail((err, txtStatus) => {
            web.notify(`${err.status}: ${err.responseText}`, "error");
        });
    });
}

