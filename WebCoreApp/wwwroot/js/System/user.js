$(document).ready(function () {
    table = $("#tblUser").DataTable({
        proccessing: true,
        serverSide: true,
        ajax: {
            url: '/System/Api/Users/Load',
            type: 'post',
            dataType: 'Json'
        },
        order: [[0, 'asc']],
        columnDefs: [
            { className: 'dt-body-center', targets: '_all' }
        ],
        columns: [
            { data: 'UserName'},
            { data: 'FullName'},
            {
                data: 'Gender', render: function (data) {
                    return data ? 'Nam' : 'Nữ';
                }
            },
            { data: 'Email'},
            {
                data: 'Avatar', render: function (data) {
                    return '<img src="' + data + '"height="50" width="50"/>';
                }
            }
        ],
        select: {
            style: 'single'
        }
    });
    $('[data-toggle="tooltip"]').tooltip();

});

function getdata() {
    var ma = table.row('.selected').data();
    if (ma === undefined) {
        bootbox.alert("Bạn chưa chọn dữ liệu");
        return false;
    }
    return ma.Id;
}

function reset() {
    var ma = table.row('.selected').data();
    if (ma === undefined) {
        bootbox.alert("Bạn chưa chọn dữ liệu");
        return false;
    }
    var id = ma.Id.toString();
    web.confirm(`Bạn muốn reset tài khoản này: ${ma.UserName}`, () => {
        $.ajax({
            url: '/System/Users/RestorePass',
            data: { id: id },
            type: 'Post'
        })
        .done((data) => {
            if (data) {
                return web.notify("Khôi phục thành công", "success");
            }
            return web.notify("Khôi phục thất bại", "error");
        })
        .fail((err, txtStatus) => { web.notify(`${er}: ${txtStatus}`, "error"); });

    });
}

function getRole() {
    var ma = table.row('.selected').data();
    if (ma === undefined) {
        bootbox.alert("Bạn chưa chọn dữ liệu");
        return false;
    }
    $.ajax({
        url: '/System/Users/GetRole',
        data: { id: ma.Id },
        type: 'get'
    })
    .done((data) => {
        $('#modal-header').html('Bảng phân quyền');
        $('#modalbody').html(data);
        $('#myModal').modal('show');
        $('#roletbl').on('click', 'tr', function () {
            
            $(this).addClass('active').siblings().removeClass('active');
        });
    })
    .fail((err, txtStatus) => {
        web.notify(`${err}: ${txtStatus}`, "error");
    });
}

function addRole() {
    var roleName = $('#roleName').val();
    if (roleName === "") {
     
        getAlert("Chưa chọn quyền", "alert-danger");
        return false;
    }
    var ma = table.row('.selected').data();
    var role = $('#roletbl');

    $.ajax({
        url: '/System/Users/AddRole',
        data: { userId: ma.Id, roleName: roleName },
        type: 'post'
    }).done((data, txtStatus, jqXRH) => {
        console.log(txtStatus + jqXRH);
        if (data.status) {
            var render = `<tr> <td> ${data.role.Name}</td>` +
                        `<td> ${data.role.Description}</td> </tr>`;
            role.append(render);
            return getAlert("Thêm thành công", "alert-success");
        }
        return getAlert("Thêm thất bại", "alert-danger");
    }).fail((err, txtStatus, errorThrown) => {
        if (err.status === 400) {
            console.log(err);
            return getAlert(`${err.status} ${err.responseText}`, "alert-danger");
        }
        getAlert(`${err.status} ${txtStatus}`, "alert-danger");
    });
}

function getAlert(alert, nclass) {
    $('.alert').show(function () {
        var $notify = $(this);
        $notify.html(alert).removeClass('alert-danger alert-info').addClass(nclass);
        setTimeout(function () { $notify.hide(1000); }, 5000);
    });
}

function deleteRole() {
    var role = $('#roletbl').find('tr.active');
    var roleName = $('#roletbl').find('tr.active td:eq(0)').text();
    if (roleName === "") {

        getAlert("Chưa chọn quyền", "alert-danger");
        return false;
    }
    var ma = table.row('.selected').data();
    $.ajax({
        url: '/System/Users/RemoveRole',
        data: { userId: ma.Id, roleName: roleName },
        type: 'post'
    }).done((data, txtStatus, jqXRH) => {
        if (data) {
            role.remove();
            return getAlert("Xóa thành công", "alert-success");
        }
        return getAlert("Xóa thất bại", "alert-danger");
    }).fail((err, txtStatus, errorThrown) => {
        getAlert(`${err.status} ${txtStatus}`, "alert-danger");
    });
}