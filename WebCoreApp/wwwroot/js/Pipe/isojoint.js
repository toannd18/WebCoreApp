function openModal() {
    var data = table.row('.selected').data();
    if (data === undefined) {
        bootbox.alert("Bạn chưa chọn dữ liệu");
        return false;
    }

    
    $('#txtIso').val(data.DrawName);
    $('#txtLine').val(data.Line);
    $('#txtRev').val(data.Rev);
    $('#txtSize').val(data.Size);

    $('#txtUnit').val(data.Unit);
    $('#txtPipe').val(data.PipeClass);
    $('#txtType').val(data.Type);
    $('#txtMaterial').val(data.Material);
    $('#txtRemarkl').val(data.Remark);

    $('#IsoName').val(data.DrawName);

    $('#modalJoint').modal('show');
    $('#txtSearch').focus();
    
    tableJoint.ajax.reload();
}

function loadData() {
    tableJoint = $('#tblIsoJoint').DataTable({
        searching: false,
        paging: false,
        ajax: {
            url: '/Pipes/Isometrics/LoadJoint',
            dataType: 'Json',
            data: function (d) {
                d.search = $('txtSearch').val();
                d.isoName = $('#txtIso').val();
            }
        },
        deferRender: true,
        order: [[0, 'asc']],
        columnDefs: [
            
            { className: 'dt-head-center', targets: '_all' },
            {
                orderable: false,
                width: '1%',
                targets: [0]
            }
        ],
        columns: [
            { data: null },
            { data: 'Joint' },
            { data: 'Rev' },
            { data: 'TypeJoint' },
            { data: 'Size' },
            { data: 'SF' },
            { data: 'Status' }
        ],
        select: {
            style: 'single'
        }
    });

    tableJoint.on('draw.dt', function () {
      tableJoint.column(0, { order: 'applied' }).nodes().each(function (cell, i) {
           cell.innerHTML = i + 1;
      });
    });

    tableJoint.on('select', function (e, dt, type, indexes) {
        if (type === 'row') {
            var data = tableJoint.row('.selected').data();
            $('#Id').val(data.Id);
            $('#Joint').val(data.Joint);
            $('#Rev').val(data.Rev);
            $('#Size').val(data.Size);
            $('#TypeJoint').val(data.TypeJoint);
            $('#SF').val(data.SF);
            $('#Status').val(data.Status);
            $('#UpdateJoint').prop('checked', true);
            web.formValidation($('#frmIsoJoint'));
        }
            
    });
}

function saveJoint(elmt) {
    web.formValidation($('#frmIsoJoint'));
    if (!$('#frmIsoJoint').valid())
    {
        return false;
    }
    var update = $('#UpdateJoint').prop('checked');
    if (update === false) {
        var result = existJoint();
        if (result >= 0) {
            $('#txtAlert').show();
            return false;
        }
        $('#txtAlert').hide();
    }
   
    var data = $('#frmIsoJoint').serializeJSON();
    data.UpdateJoint = update;
    elmt.disabled=true;
    
    $.ajax({
        url: '/Pipes/Isometrics/SaveJoint',
        type: 'post',
        headers: {
            'Content-Type': 'application/json;charset=UTF-8'
        },
        data: JSON.stringify(data)
    })
    .done((data) => {
        tableJoint.ajax.reload();
        web.notify(data, "success");
    }).fail((err, txtStatus) => {
        web.notify(`${err.status}: ${err.responseText}`, "error");
    });
    elmt.disabled = false;
    $('#Joint').prop('readonly', true);
    resetJoint();
}

function deleteJoint(elmt) {
    var ma = tableJoint.row('.selected').data();
    if (ma === undefined) {
        bootbox.alert("Bạn chưa chọn dữ liệu");
        return false;
    }
    
    web.confirm('Bạn muốn xóa cái này', function () {
        elmt.disabled = true;
        $.ajax({
            url: '/Pipes/Isometrics/DeleteJoint',
            type: 'post',
            data: { id: ma.Id }
        }).done((data) => {
            tableJoint.ajax.reload();
            web.notify(data, "success");
        }).fail((err, txtStatus) => {
            web.notify(`${err.status}: ${err.responseText}`, "error");
        });
        elmt.disabled = false;
    });
}

function addJoint() {
    $('#Joint').prop('readonly', false);
    $('#Joint').val('');
    $('#Rev').val($('#txtRev').val());
    $('#Size').val($('#txtSize').val());
    $('#TypeJoint').val($('#txtType').val());
    $('#SF').val('S');
    $('#Status').val('');
    $('#UpdateJoint').prop('checked', false);
}

function resetJoint() {
    $('#Joint').val('');
    $('#Rev').val('');
    $('#Size').val('');
    $('#TypeJoint').val('');
    $('#SF').val('S');
    $('#Status').val('');
}

function existJoint() {
   var ma= $('#Joint').val();
  var result=  tableJoint
        .column(1)
        .data()
        .indexOf(ma)
  return result;
}