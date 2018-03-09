var modalLevel1Create = $("#modalLevel1Create");
var modalLevel1Update = $("#modalLevel1Update");
var modalLevel2Create = $("#modalLevel2Create");
var modalLevel2Update = $("#modalLevel2Update");
var modalLevel3Create = $("#modalLevel3Create");
var modalLevel3Update = $("#modalLevel3Update");
var permission;
var parentPermission;
var vm = {};

//删除权限
vm.deletePermission = function (pid, name) {
    if (confirm("你确认删除权限 [" + name + "] 吗?")) {
        var msg = PermissionService.Delete(pid);
        if (msg.error != null) {
            $.notify({
                title: '',
                message: msg.error.Message
            }, {
                placement: {
                    from: "top",
                    align: "center"
                },
                type: 'danger'
            });
        } else {
            $.notify({
                title: '',
                message: '删除成功'
            }, {
                placement: {
                    from: "top",
                    align: "center"
                },
                type: 'success'
            });
            setTimeout(function () { window.location.href = window.location.href; }, 500);
        }
    }
}

//添加1级权限 - 提交
vm.modalLevel1CreateCommit = function () {
    var errorBox = modalLevel1Create.find(".alert-danger");
    var input = {
        Name: "",
        Icon: "",
        Url: "",
        ParentId: 0,
        Level: 1,
        Order: 0
    }

    input.Name = modalLevel1Create.find("input[name=txtName]").val();
    input.Icon = modalLevel1Create.find("input[name=txtIcon]").val();
    input.Url = modalLevel1Create.find("input[name=txtUrl]").val();
    input.Order = parseInt(modalLevel1Create.find("input[name=txtOrder]").val());

    if (input.Name == "") {
        errorBox.html("请输入权限名称（菜单名）").removeClass("hide");
        return false;
    }

    if (isNaN(input.Order)) {
        errorBox.html("请输入排序（数字）").removeClass("hide");
        return false;
    }

    errorBox.html("").addClass("hide");
    var msg = PermissionService.Insert(input);
    if (msg.error != null) {
        errorBox.html(msg.error.Message).removeClass("hide");
        return false;
    }

    $.notify({
        title: '',
        message: '添加成功了'
    }, {
        placement: {
            from: "top",
            align: "center"
        },
        type: 'success'
    });
    modalLevel1Create.modal("hide");
    setTimeout(function () { window.location.href = window.location.href; }, 1000);
}

//编辑1级权限 - 提交
vm.modalLevel1UpdateCommit = function () {
    var errorBox = modalLevel1Update.find(".alert-danger");

    permission.Name = modalLevel1Update.find("input[name=txtName]").val();
    permission.Icon = modalLevel1Update.find("input[name=txtIcon]").val();
    permission.Url = modalLevel1Update.find("input[name=txtUrl]").val();
    permission.Order = parseInt(modalLevel1Update.find("input[name=txtOrder]").val());

    if (permission.Name == "") {
        errorBox.html("请输入权限名称（菜单名）").removeClass("hide");
        return false;
    }

    if (isNaN(permission.Order)) {
        errorBox.html("请输入排序（数字）").removeClass("hide");
        return false;
    }

    errorBox.html("").addClass("hide");
    var msg = PermissionService.Update(permission);
    if (msg.error != null) {
        errorBox.html(msg.error.Message).removeClass("hide");
        return false;
    }
    modalLevel1Update.modal("hide");
    $.notify({
        title: '',
        message: '编辑成功了'
    }, {
        placement: {
            from: "top",
            align: "center"
        },
        type: 'success'
    });

    setTimeout(function () { window.location.href = window.location.href; }, 1000);
}

//添加2级权限 - 提交
vm.modalLevel2CreateCommit = function () {
    var $modal = modalLevel2Create;
    var errorBox = $modal.find(".alert-danger");
    if (parentPermission == null || parentPermission == undefined) {
        errorBox.html("父级权限不能为空").removeClass("hide");
        return false;
    }

    var input = {
        Name: "",
        Icon: "",
        Url: "",
        ParentId: parentPermission.Id,
        Level: 2,
        Order: 0
    }

    input.Name = $modal.find("input[name=txtName]").val();
    input.Icon = $modal.find("input[name=txtIcon]").val();
    input.Url = $modal.find("input[name=txtUrl]").val();
    input.Order = parseInt($modal.find("input[name=txtOrder]").val());

    if (input.Name == "") {
        errorBox.html("请输入权限名称（菜单名）").removeClass("hide");
        return false;
    }

    if (isNaN(input.Order)) {
        errorBox.html("请输入排序（数字）").removeClass("hide");
        return false;
    }

    errorBox.html("").addClass("hide");
    var msg = PermissionService.Insert(input);
    if (msg.error != null) {
        errorBox.html(msg.error.Message).removeClass("hide");
        return false;
    }

    $.notify({
        title: '',
        message: '添加成功了'
    }, {
        placement: {
            from: "top",
            align: "center"
        },
        type: 'success'
    });
    $modal.modal("hide");
    setTimeout(function () { window.location.href = window.location.href; }, 1000);
}

//编辑2级权限 - 提交
vm.modalLevel2UpdateCommit = function () {
    var $modal = modalLevel2Update;
    var errorBox = $modal.find(".alert-danger");

    permission.Name = $modal.find("input[name=txtName]").val();
    permission.Icon = $modal.find("input[name=txtIcon]").val();
    permission.Url = $modal.find("input[name=txtUrl]").val();
    permission.Order = parseInt($modal.find("input[name=txtOrder]").val());

    if (permission.Name == "") {
        errorBox.html("请输入权限名称（菜单名）").removeClass("hide");
        return false;
    }

    if (isNaN(permission.Order)) {
        errorBox.html("请输入排序（数字）").removeClass("hide");
        return false;
    }

    errorBox.html("").addClass("hide");
    var msg = PermissionService.Update(permission);
    if (msg.error != null) {
        errorBox.html(msg.error.Message).removeClass("hide");
        return false;
    }
    $modal.modal("hide");
    $.notify({
        title: '',
        message: '编辑成功了'
    }, {
        placement: {
            from: "top",
            align: "center"
        },
        type: 'success'
    });

    setTimeout(function () { window.location.href = window.location.href; }, 1000);
}

//添加3级权限 - 提交
vm.modalLevel3CreateCommit = function () {
    var $modal = modalLevel3Create;
    var errorBox = $modal.find(".alert-danger");
    if (parentPermission == null || parentPermission == undefined) {
        errorBox.html("父级权限不能为空").removeClass("hide");
        return false;
    }

    var input = {
        Name: "",
        Icon: "",
        Url: "",
        ParentId: parentPermission.Id,
        Level: 3,
        Order: 0
    }

    input.Name = $modal.find("input[name=txtName]").val();
    input.Icon = $modal.find("input[name=txtIcon]").val();
    input.Url = $modal.find("input[name=txtUrl]").val();
    input.Order = parseInt($modal.find("input[name=txtOrder]").val());

    if (input.Name == "") {
        errorBox.html("请输入权限名称（菜单名）").removeClass("hide");
        return false;
    }

    if (isNaN(input.Order)) {
        errorBox.html("请输入排序（数字）").removeClass("hide");
        return false;
    }

    errorBox.html("").addClass("hide");
    var msg = PermissionService.Insert(input);
    if (msg.error != null) {
        errorBox.html(msg.error.Message).removeClass("hide");
        return false;
    }

    $.notify({
        title: '',
        message: '添加成功了'
    }, {
        placement: {
            from: "top",
            align: "center"
        },
        type: 'success'
    });
    $modal.modal("hide");
    setTimeout(function () { window.location.href = window.location.href; }, 1000);
}

//编辑3级权限 - 提交
vm.modalLevel3UpdateCommit = function () {
    var $modal = modalLevel3Update;
    var errorBox = $modal.find(".alert-danger");

    permission.Name = $modal.find("input[name=txtName]").val();
    permission.Icon = $modal.find("input[name=txtIcon]").val();
    permission.Url = $modal.find("input[name=txtUrl]").val();
    permission.Order = parseInt($modal.find("input[name=txtOrder]").val());

    if (permission.Name == "") {
        errorBox.html("请输入权限名称（菜单名）").removeClass("hide");
        return false;
    }

    if (isNaN(permission.Order)) {
        errorBox.html("请输入排序（数字）").removeClass("hide");
        return false;
    }

    errorBox.html("").addClass("hide");
    var msg = PermissionService.Update(permission);
    if (msg.error != null) {
        errorBox.html(msg.error.Message).removeClass("hide");
        return false;
    }
    $modal.modal("hide");
    $.notify({
        title: '',
        message: '编辑成功了'
    }, {
        placement: {
            from: "top",
            align: "center"
        },
        type: 'success'
    });

    setTimeout(function () { window.location.href = window.location.href; }, 1000);
}

//点击添加子权限
vm.modalCreateOpen = function (level, pid) {
    if (level == 1) {
        parentPermission = PermissionService.Get(pid).value;
        modalLevel2Create.find("input[name=txtParentName]").val(parentPermission.Name);
        modalLevel2Create.modal("show");
    } else if (level == 2) {
        parentPermission = PermissionService.Get(pid).value;
        modalLevel3Create.find("input[name=txtParentName]").val(parentPermission.Name);
        modalLevel3Create.modal("show");
    }
}

//点击编辑权限
vm.modalUpdateOpen = function (level, pid) {
    permission = PermissionService.Get(pid).value;
    if (permission != null) {
        if (level == 1) {
            modalLevel1Update.find("input[name=txtName]").val(permission.Name);
            modalLevel1Update.find("input[name=txtIcon]").val(permission.Icon);
            modalLevel1Update.find("input[name=txtUrl]").val(permission.Url);
            modalLevel1Update.find("input[name=txtOrder]").val(permission.Order);
            modalLevel1Update.modal("show");
        } else if (level == 2) {
            if (permission.Parent != null)
                modalLevel2Update.find("input[name=txtParentName]").val(permission.Parent.Name);
            modalLevel2Update.find("input[name=txtName]").val(permission.Name);
            modalLevel2Update.find("input[name=txtIcon]").val(permission.Icon);
            modalLevel2Update.find("input[name=txtUrl]").val(permission.Url);
            modalLevel2Update.find("input[name=txtOrder]").val(permission.Order);
            modalLevel2Update.modal("show");
        } else if (level == 3) {
            if (permission.Parent != null)
                modalLevel3Update.find("input[name=txtParentName]").val(permission.Parent.Name);
            modalLevel3Update.find("input[name=txtName]").val(permission.Name);
            modalLevel3Update.find("input[name=txtIcon]").val(permission.Icon);
            modalLevel3Update.find("input[name=txtUrl]").val(permission.Url);
            modalLevel3Update.find("input[name=txtOrder]").val(permission.Order);
            modalLevel3Update.modal("show");
        }
    }
}
