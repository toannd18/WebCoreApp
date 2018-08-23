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
        ]
    });
});