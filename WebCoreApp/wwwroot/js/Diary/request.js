$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    table = $("#tblDỉary").DataTable({
        proccessing: true,
        serverSide: true,
        ajax: {
            url: '/Diaries/api/Requests/load',
            type: 'post',
            dataType: 'Json'
        },
        deferRender: true,
        order: [[0, 'desc']],
        columnDefs: [
            { className: 'dt-head-center', targets: '_all' }
        ],
        columns: [
            {
                data: 'Date', render: function (data) {
                    return moment(data).format("DD/MM/YYYY");
                }
            },
            { data: 'FullName' },
            { data: 'TotalJob' },
            {
                data: 'FullName1', render: function (data, type, row) {
                    return data === null ? (row.StatusAutho1 === false ? '<span class="label label-danger">Chưa gửi</span>' : '<span class="label label-warning">Đang chờ đánh giá</span>') : data;
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
    detail(ma.Id);
}

function detail(id) {
    $.ajax({
        url: '/Diaries/Requests/Get',
        data: { id: id },
        type: 'get',
        success: function (data) {
            if (!data) {
                bootbox.alert("Ban Khong co quyen de lam cai nay");
            }
            else {
                $('#myModal').modal('show');
                $('#modalbody').html(data);
                web.formValidation('#frmDiary');
                web.formatDate('.datepicker');
                
            }
        },
        error: function (err) {
            console.log(err.status);
        }
    });
}

function save() {

    if (!$('#frmDiary').valid()) return false;

    var data =$('#frmDiary').serializeJSON();
    data.Date = moment(data.Date, 'DD/MM/YYYY').format('YYYY-MM-DD');
    $('#myModal').modal('hide');
    $.ajax({
        url: '/Diaries/api/Requests/save',
        type: 'post',
        headers: {
            'Content-Type': 'application/json;charset=UTF-8'
        },
        data: JSON.stringify(data),
        success: function (data) {
            if (data.status) {
                web.notify("Lưu thành công", "success"),
                table.ajax.reload();
            }
            else if(!data.status) {
                web.notify("Lỗi lưu", "error");
            }
        },
        error: function (err) {
            web.notify(err.status, "error");
        }
    });
}

function confirmData() {
    var ma = table.row('.selected').data();
    if (ma === undefined) {
        bootbox.alert("Bạn chưa chọn dữ liệu");
        return false;
    }
    web.confirm('Bạn muốn xóa cái này', function () {
        $.ajax({
            url: '/Diaries/api/Requests/delete/' + ma.Id,
            type: 'post',
            headers: {
                'Content-Type': 'application/json;charset=UTF-8'
            },
            
            success: function (data) {
                if (data.status) {
                    web.notify("Xóa thành công", "success"),
                    table.ajax.reload();
                }
                else if (!data.status) {
                    web.notify("Lỗi xóa", "error");
                }
            },
            error: function (err) {
                web.notify(err.status, "error");
            }
        });
    });

}


function getPage() {
    var ma = table.row('.selected').data();
    if (ma === undefined) {
        bootbox.alert("Bạn chưa chọn dữ liệu");
        return false;
    }
    window.location.href = '/Diaries/Evaluates/UserIndex/' + ma.Id;
}

