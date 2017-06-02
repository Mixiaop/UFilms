
function resizeFrame() {
    var hWindows = $(window).height(), hTopbar = $("#header-navbar").height();
    var hBody = hWindows - hTopbar;
    var wWindows = $(window).width(), wLeftMenu = $('#sidebar').width();
    var wBody = wWindows - wLeftMenu;
    if (wWindows < 1000) {
        $('#main-container').width(wWindows);
        $("#mainFrame").width(wWindows);
    } else {
        $('#main-container').width(wBody);
        $("#mainFrame").width(wBody);
    }
    
    //大屏自定义高度
    if (window.screen.width > 500) {
        $('#main-container').height(hBody);
        $("#mainFrame").height(hBody);
    }
}

$(function () {
    resizeFrame();
    $(window).resize(function (e) {
        resizeFrame();
    });

    $("#leftmenu li a").click(function () {
        var url = $(this).data("url");
        if (url != undefined && url != '') {
            $("#mainFrame").attr("src", url + "?time=" + Math.random());
            if ($(window).width() < 500) {
                App.layout('sidebar_close');
            }
            resizeFrame();
        }
    });

    $('#hiddenMenu').click(function () {
        var hWindows = $(window).height(), hTopbar = $("#header-navbar").height();
        var hBody = hWindows - hTopbar;
        var wWindows = $(window).width(), wLeftMenu = $('#sidebar').width();
        var wBody = wWindows - (wLeftMenu - 170);
        if (wWindows < 1000) {
            $('#main-container').width(wWindows);
            $("#mainFrame").width(wWindows);
        } else {
            $('#main-container').width(wBody);
            $("#mainFrame").width(wBody);
        }

        //大屏自定义高度
        if (window.screen.width > 500) {
            $('#main-container').height(hBody);
            $("#mainFrame").height(hBody);
        }
    });

    //确认修改密码
    $("#modalChangePassword .btn-primary").click(function () {
        var modal = $("#modalChangePassword");

        var newPassword2 = modal.find("input[name=newPassword2]").val();
        var input = {
            AdminId: parseInt($("#hfAdminId").val()),
            OldPassword: modal.find("input[name=oldPassword]").val(),
            NewPassword: modal.find("input[name=newPassword]").val()
        }
        var errorBox = modal.find(".alert-danger");

        if (input.OldPassword == "") {
            errorBox.html("请输入原密码").removeClass("hide");
            return false;
        }

        if (input.NewPassword == "") {
            errorBox.html("请输入新密码").removeClass("hide");
            return false;
        }

        if (input.NewPassword != newPassword2) {
            errorBox.html("两次输入的密码不一致").removeClass("hide");
            return false;
        }

        errorBox.html("").addClass("hide");

        var msg = AdminService.ChangePassword(input).value;
        if (!msg.Success) {
            errorBox.html(msg.ErrorMessage).removeClass("hide");
            return false;
        }

        modal.modal("hide");
        notifyService.success("", "密码修改成功");
        setTimeout(function () {
            window.location.href = "/Logout.aspx";
        }, 1000);
    });
});
