function getdata() {
    var dateFrom = $("#dateFrom").val();
    var dateTo = $("#dateTo").val();
    var dialog = web.isLoading();
    $.ajax({
        url: '/Reports/Diary/ByBP',
        data: { dateFrom: dateFrom, dateTo: dateTo },
        type: 'Post',
        success: function (data) {
            if (data.length > 0) {
                var render;
                var labels = [];
                var datasets = [];
                data.map(function (item, index) {
                    var rate = (item.Total_Work / (item.Total_Date * 4.8)).toFixed(2);
                    render += '<tr>' +
                                '<td> ' + item.Ten_Phong + '</td>' +
                                '<td> ' + item.Total_Date + '</td>' +
                                '<td> ' + item.Total_Work + '</td>' +
                                '<td> ' + rate + '% </td>' +
                                '</tr>';
                    labels.push(item.Ten_Phong);
                   datasets.push(rate);
                });
                $('#bdTbl').html(render);
                pieChart(labels, datasets);
            }
          
        },
        error: function (err) {
            web.notify(err.status, 'error');
        },
        complete: function () {
            dialog.modal('hide');
        }
    });
    
}

function pieChart(labels, dataset) {
    var pie = document.getElementById('pie1').getContext('2d');
    var color = ['#ff0000','#ffd700','#00ffff','#40e0d0','#ff7373','#0000ff','#ffa500','#800000'];
    var config = {
        type: 'pie',
        data: {
            datasets: [{
                data: dataset,
                backgroundColor: color,
                label: 'Dataset 1'
            }],
            labels: labels
        },
        options: {
            reponsive: true
        }

    };
    new Chart(pie, config);
}