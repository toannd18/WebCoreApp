$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    table = $('#tblIso').DataTable({
        proccessing: true,
        serverSide: true,
        dom: '<"row"l><"row"f>rt<"bottom"ip><"clear">',
        ajax: {
            url: '/Pipes/Isometrics/Load',
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
                targets: [0]
            }
        ],
        columns: [
            { data: null },
            { data: 'DrawName' }
        ],
        select: {
            style: 'single'
        }
    })

    table.on('draw.dt', function () {
        var info = table.page.info();
        table.column(0, { search: 'applied', order: 'applied', page: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1 + info.start;
        });
    });
    table.on('select', function (e, dt, type, indexes) {
        if (type === 'row') {
            var data = table.row('.selected').data();
            $('#iso').val(data.DrawName);
            if ($('#tab_content1').hasClass('active')) {
                tblWelding.ajax.reload();
            }
            else {
                tblWelded.ajax.reload();
            }
        }
    });
    loadWelding();
    activeTab();
    $('#tblIso_filter').css('float', 'left');
    web.formatDate('#WeldingDate');
    autoWelder('#Welder1');
    autoWelder('#Welder2');
    autoWelder('#Welder3');
    autoWelder('#Welder4');
});

function loadWelding() {
    tblWelding = $("#tblWelding").DataTable({
        ajax: {
            url: '/Pipes/Weldings/WeldedLoad',
            data: function (d) {
                d.isoName = $('#iso').val();
                d.welding = false;
            },
            type: 'get',
            dataType: 'Json'
        },
        deferRender: true,
        columnDefs: [
        { className: 'dt-body-center', targets: '_all' },
        { className: 'dt-head-center', targets: '_all' },
        ],
        columns: [
          { data: 'Joint' },
          { data: 'Size' },
          { data: 'TypeJoint' },
          { data: 'WeldingDate' },
          { data: 'Welder1' },
          { data: 'Welder2' },
          { data: 'Welder3' },
          { data: 'Welder4' }
        ],
        select: {
            style: 'single'
        }
    });

    tblWelded = $("#tblWelded").DataTable({
        ajax: {
            url: '/Pipes/Weldings/WeldedLoad',
            data: function (d) {
                d.isoName = $('#iso').val();
                d.welding = true;
            },
            type: 'get',
            dataType: 'Json'
        },
        deferRender: true,
        columnDefs: [
        { className: 'dt-body-center', targets: '_all' },
        { className: 'dt-head-center', targets: '_all' },
        ],
        columns: [
          { data: 'Joint' },
          { data: 'Size' },
          { data: 'TypeJoint' },
          {
              data: 'WeldingDate', render: function (data) {
                  return moment(data).format('DD/MM/YYYY');
              }
          },
          { data: 'Welder1' },
          { data: 'Welder2' },
          { data: 'Welder3' },
          { data: 'Welder4' }
        ],
        select: {
            style: 'single'
        }
    });

    tblWelding.on('select', function (e, dt, type, indexes) {
        if (type === 'row') {
            var data = tblWelding.row('.selected').data();
            selectData(data);
            web.formValidation($("#frmWelding"));
        }
    });

    tblWelded.on('select', function (e, dt, type, indexes) {
        if (type === 'row') {
            var data = tblWelded.row('.selected').data();
            data.WeldingDate = moment(data.WeldingDate).format('DD/MM/YYYY');
            selectData(data);
            web.formValidation($("#frmWelding"));
        }
    })
}

function activeTab() {
    $('#home-tab').on('click', function () {
        tblWelding.ajax.reload();
    });
    $('#profile-tab').on('click', function () {
        tblWelded.ajax.reload();
    })
}

function selectData(data) {
    $('#Id').val(data.Id);
    $('#txtjoint').val(data.Joint);
    $('#txtsize').val(data.Size);
    $('#tJoint').val(data.TypeJoint);
    $('#WeldingDate').val(data.WeldingDate);
    $('#Heate1').val(data.Heate1);
    $('#Heate2').val(data.Heate2);
    $('#Welder1').val(data.Welder1);
    $('#Welder2').val(data.Welder2);
    $('#Welder3').val(data.Welder3);
    $('#Welder4').val(data.Welder4);
}

function saveData(elmt) {
    if (!$('#frmWelding').valid()) {
        return false;
    }
    elmt.disabled = true;
    var data = $('#frmWelding').serializeJSON();
    data.WeldingDate = moment(data.WeldingDate, 'DD/MM/YYYY').format('MM/DD/YYYY');
    $.ajax({
        url: '/Pipes/Weldings/Save',
        type: 'post',
        headers: {
            'Content-Type': 'application/json;charset=UTF-8'
        },
        data: JSON.stringify(data)
    })
    .done((data) => {
        web.notify(data, "success");
        if ($('#tab_content1').hasClass('active')) {
            tblWelding.ajax.reload();
        }
        else {
            tblWelded.ajax.reload();
        }
    })
    .fail((err) => {
             
        web.notify(`${err.status}: ${err.responseText}`, "error");
    });
    elmt.disabled = false;
}

function removeData(elmt) {
    var joint = $('#txtjoint').val();
    var weld = $('#WeldingDate').val();
    if (joint === null || weld === null) {
        bootbox.alert("Bạn chưa chọn dữ liệu");
        return false;
    }
    
    web.confirm('Bạn muốn xóa ngày này', function () {
        elmt.disabled = true;
        $.ajax({
            url: '/Pipes/Weldings/ClearWelding',
            type: 'post',
            data: { id: $('#Id').val()}
        }).done((data) => {
            web.notify(data, "success");
            resetData();
            if ($('#tab_content1').hasClass('active')) {
                tblWelding.ajax.reload();
            }
            else {
                tblWelded.ajax.reload();
            }
        }).fail((err, txtStatus) => {
            web.notify(`${err.status}: ${err.responseText}`, "error");
        });
        elmt.disabled = false;
    });
}

function autoWelder(id) {
    $(id).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Pipes/Welders/Find',
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
            $(id).val(ui.item.Id);
            return false;
        },
        select: function (event, ui) {
            $(id).val(ui.item.Id);

            return false;
        },
        change: function (event, ui) {
        },
        appendTo: '#frmWelding'
    }).autocomplete("instance")._renderItem = function (ul, item) {
        return $("<li>")
            .append("<div>Mã: " + item.Id + "</div>")
            .appendTo(ul);
    };
}

function resetData() {
    $('#WeldingDate').val('');
    $('#Heate1').val('');
    $('#Heate2').val('');
    $('#Welder1').val('');
    $('#Welder2').val('');
    $('#Welder3').val('');
    $('#Welder4').val('');
}