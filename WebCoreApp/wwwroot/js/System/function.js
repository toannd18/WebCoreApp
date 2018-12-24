$(document).ready(function () {
    loadData();
});

function loadData() {
    $.ajax({
        url: '/System/Functions/GetFunction',
        type: 'get',
        contentType: "application/json; charset=utf-8",
        dataType: 'json'
    }).done(function (data) {
        var roots = [];
        $.each(data, function (k, item) {
            var map = { text: "", id: "", nodes: [] };
            map.text = item.Name;
            map.id = item.Id;
            $.each(item.InverseParent, function (k1, item1) {
                var map1 = { text: "", id: "", nodes: [] };
                map1.text = item1.Name;
                map1.id = item1.Id;
                $.each(item1.InverseParent, function (k2, item2) {
                    map1.nodes.push({ text: item2.Name, id: item2.Id })
                });
                map.nodes.push(map1);
            })
            roots.push(map);
        });
        //data.map((item, index) => {
        //    var map = { text: "",id:"", nodes: [] };
        //    map.text = item.Name;
        //    map.id = item.Id;
        //    item.InverseParent.map((item1, index1) => {
        //        map.nodes.push({ text: item1.Name,id:item1.Id});
        //    });
        //    roots.push(map);

        //});

        $("#tree").treeview({
            levels: 1,
            data: roots
        });
    }).fail(function (jqXHR, textStatus, errorThrown) {
        // If fail
        console.log(textStatus + ': ' + errorThrown + ':' + jqXHR.statusText);
    });
}

function getData(id) {
    if (id === 0) {
        return getDetail("");
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
        $('#modal-header').html(ma === "" ? 'Tạo chức năng' : 'Sửa chức năng');
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

function getRole() {
    var data = $('#tree').treeview('getSelected');
    if (data.length <= 0) {
        return bootbox.alert('Bạn chưa chọn dữ liệu');
    }
    var id = data[0].id;
    $.ajax({
        url: '/System/Functions/GetRole',
        data: { Id: id },
        type: 'get'
    })
    .done(function (data) {
        $('#modalbody').html(data),
         $('#myModal').modal('show');
        $('#Id').val(id);
        $('#role').on('change', function () {
            loadPermission($(this).val());
        })
    })
    .fail(function (err) {
        web.notify(`${err.status} : ${err.responseText}`, 'success');
    })
}

function loadPermission(id) {
    var idFunc = $('#Id').val();
    var tbody = $('#tbl');
    if (id === "") {
        tbody.html("");
        return false;
    }
    $.ajax({
        url: '/System/Functions/LoadPermission',
        data: { id: id, idFunc: idFunc },
        type: 'get'
    })
    .done(function (data) {
        var render = '<tr>' +
            '<td><lable><input type="checkbox" id="View" class="js-switch" > Kích hoạt</label></td>' +
              '<td><lable><input type="checkbox" id="Edit" class="js-switch" > Kích hoạt</label></td>' +
                '<td><lable><input type="checkbox" id="Delete" class="js-switch" > Kích hoạt</label></td>' +
                '</tr>';
        tbody.html(render);
      
        $.each(data, function (index, item) {
            if (item.Value === "View") {
                $('#View').prop('checked', true);
            }
            if (item.Value === "Edit") {
                $('#Edit').prop('checked', true);
            }
            if (item.Value === "Delete") {
                $('#Delete').prop('checked', true);
            }
        });
        var elem = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));
        elem.forEach(function (a) {
            new Switchery(a, { color: "#26B99A" });
        });
      
    })
    .fail(function (err) {
        var render = '<tr class="text-danger"><td>' + err.status + ': ' + err.responseText + '</td></tr>';
        tbody.html(render);
    })
}

function updatePermission(elmt) {
    var role = $('#role').val();
    var id = $('#Id').val();
    if (role === "") {
        return web.notify("Không có role để sửa", "error");
    }
    elmt.disable = true;
    var formData = new FormData();
    var view = document.querySelector('#View').checked;
    var edit = document.querySelector('#Edit').checked;
    var del = document.querySelector('#Delete').checked;
    formData.append("Role", role);
    formData.append("Id", id);
    formData.append("View", view);
    formData.append("Edit", edit);
    formData.append("Delete", del);
    $.ajax({
        url: '/System/Functions/UpdatePermission',
        processData: false,
        contentType: false,
        data: formData,
        type:'post'
    })
    .done((data) => {
        web.notify(data, 'success');
    })
    .fail((err) => {
        web.notify(`${err.status}: ${err.responseText} `, 'error');
    })
    elmt.disable = false;
}