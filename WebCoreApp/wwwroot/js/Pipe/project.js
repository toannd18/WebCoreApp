$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    table = $('#tblProject').DataTable({
        proccessing: true,
        serverSide: true,
        ajax: {
            url: '/Pipes/Projects/Load',
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
})

function getdata() {
    var ma = table.row('.selected').data();
    if (ma === undefined) {
        bootbox.alert("Bạn chưa chọn dữ liệu");
        return false;
    }
    detail(ma.Id, true);
}
function detail(id, update) {
    $.ajax({
        url: '/Pipes/Projects/Get',
        data: { id: id, update: update },
        type: 'get'
    })
    .done((data) => {
        $('#modal-header').html('Dự án');
        $('#modalbody').html(data);

        $('#myModal').modal('show');
        web.formValidation('#frmProject');
    })
    .fail((err, txtStatus) => {
        web.notify(`${err.status}: ${err.responseText}`, "error");
    });
}

function saveData(elmt) {
    elmt.disable = true;
    if (!$('#frmProject').valid()) {
        elmt.disable = false;
        return false;
    }
    var data = $('#frmProject').serializeJSON();
    data.Update = $('#Update').prop('checked');


    $('#myModal').modal('hide');
    $.ajax({
        url: '/Pipes/Projects/Save',
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
    elmt.disable = false;
}
function deleteData(elmt) {
    var ma = table.row('.selected').data();
    if (ma === undefined) {
        bootbox.alert("Bạn chưa chọn dữ liệu");
        return false;
    }
    web.confirm('Bạn muốn xóa cái này', function () {
        elmt.disable = true;
        $.ajax({
            url: '/Pipes/Projects/Delete',
            type: 'post',
            data: { id: ma.Id }
        }).done((data) => {
            table.ajax.reload();
            web.notify(data, "success");
        }).fail((err, txtStatus) => {
            web.notify(`${err.status}: ${err.responseText}`, "error");
        });
        elmt.disable = false;
    });
}