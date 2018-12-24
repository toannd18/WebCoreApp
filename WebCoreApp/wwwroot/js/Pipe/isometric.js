$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    table = $("#tblIsometric").DataTable({
        proccessing: true,
        serverSide: true,
        ajax: {
            url: '/Pipes/Isometrics/Load',
            type: 'post',
            dataType: 'Json'
        },

        deferRender: true,
        order: [[0, 'asc']],
        columnDefs: [
            { className: 'dt-body-center', targets: '_all' },
            { className: 'dt-head-center', targets: '_all' },
            {
                orderable: false,
                width: '5%',
                targets: [1, 2]
            }
        ],
        columns: [
            { data: 'DrawName' },
            { data: 'Rev' },
            { data: 'Size' },
            { data: 'Unit' },
            { data: 'PipeClass' },
            { data: 'Line' },
            { data: 'Type' },
            { data: 'Material' }
        ],
        select: {
            style: 'single'
        }
    });
    loadData();
    //table.on('draw.dt', function () {
    //    var info = table.page.info();
    //    table.column(0, { search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
    //        cell.innerHTML = i + 1 + info.start;
    //    });
    //});
});

function getdata() {
    var ma = table.row('.selected').data();
    if (ma === undefined) {
        bootbox.alert("Bạn chưa chọn dữ liệu");
        return false;
    }
    detail(ma.DrawName, true);
}
function detail(id, update) {
    $.ajax({
        url: '/Pipes/Isometrics/Get',
        data: { id: id, update: update },
        type: 'get'
    })
    .done((data) => {
        $('#modal-header').html('Bản vẽ');
        $('#modalbody').html(data);
        autocomplete();
        $('#myModal').modal('show');
        web.formValidation('#frmIsometric');
    })
    .fail((err, txtStatus) => {
        web.notify(`${err.status}: ${err.responseText}`, "error");
    });
}

function saveData() {
    if (!$('#frmIsometric').valid()) return false;
    var data = $('#frmIsometric').serializeJSON();
    data.Update = $('#Update').prop('checked');

    $('#myModal').modal('hide');
    var dialog = web.isLoading();
    $.ajax({
        url: '/Pipes/Isometrics/Save',
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
    dialog.modal('hide');
}

function deleteData() {
    var ma = table.row('.selected').data();
    if (ma === undefined) {
        bootbox.alert("Bạn chưa chọn dữ liệu");
        return false;
    }
    web.confirm('Bạn muốn xóa cái này', function () {
        var dialog = web.isLoading();
        $.ajax({
            url: '/Pipes/Isometrics/Delete',
            type: 'post',
            data: { id: ma.DrawName }
        }).done((data) => {
            table.ajax.reload();
            web.notify(data, "success");
        }).fail((err, txtStatus) => {
            web.notify(`${err.status}: ${err.responseText}`, "error");
        });
        dialog.modal('hide');
    });
}

function autocomplete() {
    var material = $('#Material');
    var type = $('#Type');
    material.autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Pipes/Materials/Find',
                dataType: 'JSON',
                data: { id: request.term },
                success: function (data) {
                    response(data);
                    //response($.map(data, function (item) {
                    //    return { lable: item.FullName, value: item.UserName };

                    //}));
                }
            });
        },
        focus: function (event, ui) {
            material.val(ui.item.Name);
            return false;
        },
        select: function (event, ui) {
            material.val(ui.item.Name);

            return false;
        },
        change: function (event, ui) {
            if (ui.item) {
                return false;
            }
            else {
                alert("Yêu cầu lựa chọn trong danh sách");
                material.val("");
                material.focus();
                return false;
            }
        },
        appendTo: "#modalbody"
    }).autocomplete("instance")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<div>Mã: " + item.Name + "</div>")
            .appendTo(ul);
    };

    type.autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Pipes/TypeJoints/Find',
                dataType: 'JSON',
                data: { id: request.term },
                success: function (data) {
                    response(data);
                    //response($.map(data, function (item) {
                    //    return { lable: item.FullName, value: item.UserName };

                    //}));
                }
            });
        },
        focus: function (event, ui) {
            type.val(ui.item.Type);
            return false;
        },
        select: function (event, ui) {
            type.val(ui.item.Type);

            return false;
        },
        change: function (event, ui) {
            if (ui.item) {
                return false;
            }
            else {
                alert("Yêu cầu lựa chọn trong danh sách");
                type.val("");
                type.focus();
                return false;
            }
        },
        appendTo: "#modalbody"
    }).autocomplete("instance")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<div>Mã: " + item.Type + "</div>")
            .appendTo(ul);
    };
}