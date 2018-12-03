$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    table = $("#dailytbl").DataTable({
        processing: true,
        serverSide: true,
        ajax: {
            url: '/Diaries/api/evaluates/load',
            data:function(d){
                d.id= $("#daily_id").val();
            },
            type: 'post',
            dateType: 'json'
        },
        deferRender: true,
        rder: [[0, 'desc']],
        columnDefs: [
            {
                className: 'dt-head-center', targets: '_all'
                
            }
        ],
        columns: [
            {
                data: 'FromTime', render: function (data) {
                    return moment(data,'HH:mm:ss',true).format("HH:mm");
                }
            },
            {
                data: 'ToTime', render: function (data) {
                    return moment(data, 'HH:mm:ss', true).format("HH:mm");
                }
            },
            {
                data: 'Content_Job', width: '15%'
            },
            {
                data: 'Method', render: function (data, type) {
                    return data !== null ? '<p style="white-space:pre-wrap">' + data + '</p>' : data;
                }, width: '20%', orderable: false
            },
            {
                data: 'Result', width: '8%', orderable: false
            },
            {
                data: 'Total_Job', width: '15%', orderable: false
            },
            {
                data: 'Comment1', render: function (data, type, row) {
                    return (row.Level_1 === null ? "" : "- Mức " + row.Level_1) + (data === null ? "" : (data === "" ? "" : " (" + data + ")"));
                }, width: '10%', orderable: false

            },
            {
                data: 'Comment2', render: function (data, type, row) {
                    return (row.Level_2 === null ? "" : "- Mức " + row.Level_2) + (data === null ? "" : (data === "" ? "" : " (" + data + ")"));
                }, width: '10%', orderable: false
            },
            {
                data: 'Comment3', render: function (data, type, row) {
                    return (row.Level_3 === null ? "" : "- Mức " + row.Level_3) + (data === null ? "" : (data === "" ? "" : " (" + data + ")"));
                }, width: '10%', orderable: false
            }
        ],
        searching:false,
        select: {
            style: 'single'
        }
    });

    jQuery.validator.addMethod("timeValidator",
      function (value, element, params) {
          var val = new Date('1/1/1991' + ' ' + value);
          var par = new Date('1/1/1991' + ' ' + $(params).val());
          return val > par;

      }, 'Thời gian đến phải lớn hơn thời gian bắt đầu');
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
        url: '/Diaries/Evaluates/userget/' + id,
        type: 'get',
        success: function (data) {
            $('#modalbody').html(data);
            $('#myModal').modal('show');
            $('#DailyId').val($('#daily_id').val());
            web.formValidation('#frmUserInp');
            $("#ToTime").rules('add', { timeValidator: "#FormTime" });

        },
        error: function (err) {
            web.notify(err.status, 'error');
        }
    });
}


function save() {
    if (!$('#frmUserInp').valid()) return false;

    var data = $('#frmUserInp').serializeJSON();
    $('#myModal').modal('hide');
    $.ajax({
        url: '/diaries/api/evaluates/savediary',
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
            else if (!data.status) {
                web.notify("Lỗi lưu", "error");
            }
        },
        error: function (err) {
            web.notify(err.status, "error");
        }
    });
}

function confirmDelete() {
    var ma = table.row('.selected').data();
    if (ma === undefined) {
        bootbox.alert("Bạn chưa chọn dữ liệu");
        return false;
    }
    web.confirm('Bạn muốn xóa cái này', function () {
        $.ajax({
            url: '/Diaries/api/evaluates/delete/' + ma.Id,
            type: 'post',
            headers: {
                'Content-Type': 'application/json;charset=UTF-8'
            },

            success: function (data) {
                if (data.status) {
                    web.notify("Xóa thành công", "success");
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

// Đánh giá 
function comment(id)
{
    if (id === 0) {
        var ma = table.row('.selected').data();
        if (ma === undefined) {
            bootbox.alert("Bạn chưa chọn dữ liệu");
            return false;
        }

        $.ajax({
            url: '/Diaries/Evaluates/Comment/' + ma.Id,
            type: 'get',
            success: function (data) {
                $('#modalbody').html(data);
                $('#modal-header').html("Đánh giá");
                $('#myModal').modal('show');

            },
            error: function (err) {
                web.notify(err.status, 'error');
            }

        });
    }
    else {
        $('#levelAll').val('3');
        $('#commentAll').val('');
        $('#modal-comment-all').modal('show');
    }
}

function savecomment(code) {
    if (code === 0) {
        var data = JSON.stringify($('#frmcomment').serializeJSON());
        $('#myModal').modal('hide');
        $.ajax({
            url: '/Diaries/api/evaluates/savecomment',
            type: 'post',
            headers: {
                'Content-Type': 'application/json;charset=UTF-8'
            },
            data: data,
            success: function (data) {
                if (data.status) {
                    web.notify("Lưu thành công", "success"),
                    table.ajax.reload();
                }
                else if (!data.status) {
                    web.notify("Lỗi lưu", "error");
                }
            },
            error: function (err) {
                web.notify(err.status, "error");
            }
        });

    }
    else {
        var data_comment = {
            comment: $('#commentAll').val(),
            level: $('#levelAll').val(),
            idDaily: $('#daily_id').val()
        };
        $('#modal-comment-all').modal('hide');
        $.ajax({
            url: '/Diaries/api/evaluates/savecommentall',
            type: 'post',
            headers: {
                'Content-Type': 'application/json;charset=UTF-8'
            },
            data: JSON.stringify(data_comment),
            success: function (data) {
                if (data.status) {
                    web.notify("Lưu thành công", "success"),
                    table.ajax.reload();
                }
                else if (!data.status) {
                    web.notify("Lỗi lưu", "error");
                }
            },
            error: function (err) {
                web.notify(err.status, "error");
            }
        });

    }
}

function sendnotification() {
    let id = $('#daily_id').val();
    $.ajax({
        url: '/Diaries/api/evaluates/SendNotification/' + id,
        dataType: 'json',
        type: 'post',
        success: function (data) {
            if (data.status) {
                web.notify("Gửi thành công", "success");
            }
            else if (!data.status) {
                web.notify("Lỗi xóa", "error");
            }
        },
        error: function (err) {
            web.notify(err.status, "error");
        }

    });
}