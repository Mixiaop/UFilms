define(['jquery', 'utils/notify', 'bootstrap'], function ($, notify) {

    var $container, $errorBox, $userList;
    var $keywords, $btnSearch;
    var users = [];

    var vm = {
        committing: false,
        form: {},
        options: {
            teamKey: '',
            callback: function () { }
        }
    };

    function _showError(msg) {
        $errorBox.html(msg);
        $errorBox.removeClass('hide');
    }

    function _hideError() {
        $errorBox.html('');
        $errorBox.addClass('hide');
    }

    //渲染搜索用户
    function _renderSearchUsers() {
        $userList.html('');
        var str = "";
        for (var i = 0; i < users.length; i++) {
            var user = users[i];
            str += '<div class="col-xs-6" style="padding-bottom:5px;">';
            str += user.Username + " （" + user.NickName + "）";
            str += '</div>';
            str += '<div class="col-xs-6 text-right" style="padding-bottom:5px;">';
            if (user.selected == 1) {
                str += '<a href="javascript:;" class="btn btn-default btn-xs" >已添加</a>';
            } else {
                str += '<a href="javascript:;" class="btn btn-primary btn-xs" data-userid="' + user.Id + '">添加</a>';
            }
            str += '</div>';
        }
        $userList.append(str);

        //重新绑定事件
        $userList.find('a.btn-primary').unbind('click');
        $userList.find('a.btn-primary').on('click', function () {
            var $this = $(this);
            //添加用户
            var userid = parseInt($(this).data('userid'));

            for (var i = 0; i < users.length; i++) {
                if (userid == users[i].Id) {
                    users[i].selected = 1;
                    break;
                }
            }
            $this.text('已添加');
            $this.removeClass('btn-primary').addClass('btn-default');
            $this.unbind('click');
        });
    }

    function _existsUser(user) {
        var exists = false;
        for (var i = 0; i < users.length; i++) {
            if (user.Id == users[i].Id) {
                exists = true;
                break;
            }
        }
        return exists;
    }

    function _search() {
        var keywords = $keywords.val();
        if (keywords != "") {
            UserService.Query(keywords, function (res) {
                var json = res.value;
                if (json.Result.length == 0) {
                    alert('搜索不到用户，请检查关键字');
                } else {
                    for (var i = 0; i < json.Result.length; i++) {
                        var user = json.Result[0];
                        if (!_existsUser(user)) {
                            user.selected = 0;
                            users.push(user);
                        }
                    }
                    _renderSearchUsers();
                }
            });
        }
    }

    function _commit() {
        var userIds = "";
        for (var i = 0; i < users.length; i++) {
            if (users[i].selected == 1)
                userIds += users[i].Id + " ";
        }

        TeamService.AddMembers(vm.options.teamKey, userIds, function (res) {
            var json = res.value;
            if (json.Success) {
                notify.success('提交成功了');
                vm.close();
                setTimeout(function () { window.location.href = window.location.href; }, 800);
            } else {
                _showError(json.Error.Message);
            }
        });
        
    }

    function _initialize() {
        $container = $('#users_addMemberDialog');
        $errorBox = $container.find('.alert');
        $userList = $container.find('#userList');

        //form
        $keywords = $container.find('#tbKeywords');
        $btnSearch = $container.find('#btnSearch');

        $keywords.unbind('keydown');
        $keywords.on('keydown', function (e) {
            if (e.keyCode == 13) {
                _search();
            }
        });

        //搜索
        $btnSearch.unbind('click');
        $btnSearch.on('click', function () {
            _search();
        });

        //commit
        $container.find('.btn-primary').unbind('click');
        $container.find('.btn-primary').on('click', function () {
            _commit();
        });
    }


    //exposed methods
    vm.open = function (options) {
        $.extend(vm.options, options);
        if ($container == undefined) {
            $.ajax({
                type: 'get',
                dataType: 'html',
                url: '/js/teams/addMember.dialog.html',
                success: function (data) {
                    $('body').append(data);
                    _initialize();
                    $container.modal({ show: true });
                }
            });
        } else {
            _initialize();
            $container.modal({ show: true });
        }
    }

    vm.close = function () {
        if ($container != undefined) {
            $container.modal('hide');
        }
    }

    return vm;
});

//<div class="col-xs-6"> 15800448791 (海鹏）</div>
//<div class="col-xs-6 text-right">
//                        <a href="" class="btn btn-primary btn-sm">添加</a>
//                  </div>