$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    table = $("#tblDỉary").DataTable({
        processing: true,
        serverSide: true,
        ajax: {
            url: '/Diaries/api/Requests/leadload',
            type: 'post',
            data: function (d) {
                d.maTo = $('#maTo').val();
                d.maBp = $('#maBp').val();
            },
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

    $('#maBp').on('change', function () {
        var maBp = $(this).val() === "" ? "null" : $(this).val();
        $.ajax({
            url: '/diaries/api/requests/loadto/' + maBp,
            type: 'get',
            success: function (data) {
                var op = '<option value="">--- Tất cả ---</option>';
                data.map(function (item, index) {
                    op += '<option value="' + item.MaTo + '">' + item.TenTo + '</option>';
                });
                $('#maTo').html(op);
                table.ajax.reload();
            },
            error: function (err) {
                web.notify(err.status, 'error');
            }
        });
       
    });
    $('#maTo').on('change', function () {
        table.ajax.reload();
    });
});


function getPage() {
        var ma = table.row('.selected').data();
        if (ma === undefined) {
            bootbox.alert("Bạn chưa chọn dữ liệu");
            return false;
        }
        window.location.href = '/Diaries/Evaluates/Index/' + ma.Id;
}


