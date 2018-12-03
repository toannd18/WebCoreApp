var web = {
    notify: function (message, type) {
        $.notify(message, type);
    },

    confirm: function (message, okCallback) {
        bootbox.confirm({
            message: message,
            buttons: {
                confirm: {
                    label: 'Không',
                    className: 'btn-danger'
                },
                cancel: {
                    label: 'có',
                    className: 'btn-success'
                }
            },
            callback: function (result) {
                if (result === false) {
                    okCallback();
                }
            }
        });
    },

    formatNumber: function (number, precision) {
        if (!isFinite(number)) {
            return number.toString();
        }

        var a = number.toFixed(precision).split('.');
        a[0] = a[0].replace(/\d(?=(\d{3})+$)/g, '$&,');
        return a.join('.');
    },

    formatDate: function (id){
        $(id).datepicker({
            dateFormat: 'dd/mm/yy'
        }).on('blur', function () {
            var newDate = $(this).val();
            if (!moment(newDate, 'DD/MM/YYYY', true).isValid()) {
                $(this).val('');
              
            }
        });
    },

    formValidation: function (id) {
        
        $(id).removeData('validator');
        $(id).removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse($(id));
        
    },

    isLoading: function (){
        return bootbox.dialog({
            message: '<div class="loader text-center"></div>',
            closeButton: false
        });
    }
};
$(document).ajaxSend(function (e, xhr, options) {
    if (options.type.toUpperCase() === "POST" || options.type.toUpperCase() === "PUT") {
        var token = $('form').find("input[name='__RequestVerificationToken']").val();
        xhr.setRequestHeader("RequestVerificationToken", token);
    }
});
$(document).ready(function () {
    if ($("body").hasClass("nav-md")) {
        $("#menu_toggle").click();
    }
});