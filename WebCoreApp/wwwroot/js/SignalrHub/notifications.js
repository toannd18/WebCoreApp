
var connection = new signalR.HubConnection("/ptscHub", { logger: signalR.LogLevel.Information });

    connection.on("ReceiveMessage", function (user, message) {
        return console.log(message);
    });
    connection.on("GetNotification", function () {
        $.ajax({
            url: '/Home/Notification',
            type: 'get',
            success: function (data) {
                return $('#_notificationPush').html(data);
            },
            error: function (err) {
                return console.log(err.status);
            }
        })
    });


    connection.start().catch(function (err) {
        return console.error(err.toString());
    });

