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
                var pie = document.getElementById('pie1').getContext('2d');
                pieChart(labels, datasets,pie);
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
function getdatato() {
    var dateFrom = $("#date_Fto").val();
    var dateTo = $("#date_Tto").val();
    var MaBP = $("#maBP").val();
    var dialog = web.isLoading();
    $.ajax({
        url: '/Reports/Diary/ByTo',
        data: { dateFrom: dateFrom, dateTo: dateTo, MaBp: MaBP },
        type: 'Post',
        success: function (data) {
            if (data.length > 0) {
                var render;
                var labels = [];
                var datasets = [];
                data.map(function (item, index) {
                    var rate = (item.Total_Work / (item.Total_Date * 4.8)).toFixed(2);
                    render += '<tr>' +
                                '<td> ' + item.Ten_To + '</td>' +
                                '<td> ' + item.Total_Date + '</td>' +
                                '<td> ' + item.Total_Work + '</td>' +
                                '<td> ' + rate + '% </td>' +
                                '</tr>';
                    labels.push(item.Ten_To);
                    datasets.push(rate);
                });
                $('#toTbl').html(render);
                var pie = document.getElementById('pie2').getContext('2d');
                pieChart(labels, datasets, pie);
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
function pieChart(labels, dataset,pie) {
   
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