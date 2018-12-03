$(document).ready(function () {
    loadData();
});
function loadData() {
   $.ajax({
        url: '/System/Functions/GetFunction',
        type: 'get',
        contentType: "application/json; charset=utf-8",
        dataType:'json'
   }).done(function (data) {
       var roots = [];
       data.map((item, index) => {
           var map = { text: "",id:"", nodes: [] };
           map.text = item.Name;
           map.id = item.Id;
           item.InverseParent.map((item1, index1) => {
               map.nodes.push({ text: item1.Name,id:item1.Id});
           });
           roots.push(map);

       });
     
       $("#tree").treeview({
           levels:1,
           data: roots
       });
   }).fail(function (jqXHR, textStatus, errorThrown) {
       // If fail
       console.log(textStatus + ': ' + errorThrown + ':' + jqXHR.statusText);
   });
}

function getData(id) {
    if (id === 0) {
      return  getDetail("");
    }
    var data = $('#tree').treeview('getSelected');
    if (data.length > 0) {
        return getDetail(data[0].id);
    }
    return bootbox.alert('Bạn chưa chọn dữ liệu');
}
function getDetail(ma) {
    $.ajax({
        url: '/System/Functions/GetFunctionDetail',
        type: 'get',
     
        data: { Id: ma }

    }).done(function (res) {
        $('#modalbody').html(res),
        $('#modal-header').html(ma==="" ? 'Tạo chức năng' : 'Sửa chức năng');
        $('#myModal').modal('show');
        web.formValidation($('#frmFunction'));
    });
}
function saveData(update) {
    if (!$('#frmFunction').valid()) return false;

    var data = $('#frmFunction').serializeJSON();
    $('#myModal').modal('hide');
    $.ajax({
        url: `/System/Functions/SaveFunction/${update}`,
        type: 'Post',
        contentType: 'application/json;charset=UTF-8',
        data: JSON.stringify(data)
    }).done(function (res) {

        if (res === true) {
            web.notify('Lưu thành công', 'success');
            loadData();
        }
        else if (res === false) {
            web.notify('Lỗi lưu trên Server', 'error');
        } else {
            bootbox.alert('Bạn không có quyền làm điều này');
        }
    }).fail(function (err) {
        web.notify(err.statusText, 'error');
    });
}
function deleteData() {
    let data = $('#tree').treeview('getSelected');
    if (data.length > 0) {
      let ma = data[0].id;
    return web.confirm('Bạn muốn xóa cái này', function () {
            $.ajax({
                url: `/System/Functions/DeleteFunction/${ma}`,
                type: 'post',
                contentType: 'application/json;charset=UTF-8'
            }).done(function (res) {
                if (res === true) {
                    web.notify('Lưu thành công', 'success'); loadData();
                } else if (res === false) {
                    web.notify('Lỗi lưu trên Server', 'error');
                } else {
                    bootbox.alert('Bạn không có quyền làm điều này');
                }
            }).fail(function (err) {
                web.notify(err.statusText, 'error');
            });
    });
    
    }
    return bootbox.alert('Bạn chưa chọn dữ liệu');
}