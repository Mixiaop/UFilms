var modalCreateRole = $("#modalCreateRole");
var modalUpdateRole = $("#modalUpdateRole");
var role;
var vm = {};

vm.deleteRole = function (roleId, name) {
    if (confirm("你确认删除权限 [" + name + "] 吗?")) {
        var msg = RoleService.Delete(roleId);

        if (msg.error != null) {
            notifyService.error("", msg.error.Message);
        } else {
            notifyService.success("", "删除成功了");
            redirectService.refresh(500);
        }
    }
}

vm.modalCreateRoleCommit = function () {
    var $modal = modalCreateRole;
    var errorBox = $modal.find(".alert-danger");
    var input = {
        Name: "",
        Remark: ""
    }

    input.Name = $modal.find("input[name=name]").val();
    input.Remark = $modal.find("textarea[name=remark]").val();

    if (input.Name == "") {
        errorBox.html("请输入角色名称").removeClass("hide");
        return false;
    }

    errorBox.html("").addClass("hide");
    var msg = RoleService.Insert(input);
    if (msg.error != null) {
        errorBox.html(msg.error.Message).removeClass("hide");
        return false;
    }
    notifyService.success("", "添加成功了");
    $modal.modal("hide");
    redirectService.refresh(1000);
}

vm.modalUpdateRoleCommit = function () {
    var $modal = modalUpdateRole;
    var errorBox = $modal.find(".alert-danger");
    role.Name = $modal.find("input[name=name]").val();
    role.Remark = $modal.find("textarea[name=remark]").val();

    if (role.Name == "") {
        errorBox.html("请输入角色名称").removeClass("hide");
        return false;
    }

    errorBox.html("").addClass("hide");
    var msg = RoleService.Update(role);
    if (msg.error != null) {
        errorBox.html(msg.error.Message).removeClass("hide");
        return false;
    }
    notifyService.success("", "编辑成功了");
    $modal.modal("hide");
    redirectService.refresh(1000);
}

//点击添加子权限
vm.modalUpdateOpen = function (roleId) {
    role = RoleService.Get(roleId).value;
    modalUpdateRole.find("input[name=name]").val(role.Name);
    modalUpdateRole.find("textarea[name=remark]").val(role.Remark);
    modalUpdateRole.modal("show");
}