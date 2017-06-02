define(['jquery', 'utils/notify', 'bootstrap'], function ($, notify) {

    var $container, $errorBox, $blockIcons, $blockParent;
    var $parentName, $parentId, $name, $icon, $des, $order, $public,$types;
    var vm = {
        committing: false,
        form: {},
        options: {
            parentId: 0,
            parentName: '',
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

    function _initialize() {
        $container = $('#nodes_addNodeDialog');
        $blockIcons = $container.find('#blockIcon');
        $blockParent = $container.find('#blockParent');
        $errorBox = $container.find('.alert');
        //form
        $parentName = $container.find('#tbParentName');
        $parentId = $container.find('#hidParentId');
        $name = $container.find('#tbName');
        $icon = $container.find('#tbIcon');
        $des = $container.find('#tbDescription');
        $order = $container.find('#tbOrder');
        $types = $container.find('#ddlNodeTypes');
        //$public = $container.find('#ddlPublic');

        if (vm.options.parentId == 0) {
            $container.find('.block-title').html('<i class="fa fa-plus"></i> 添加主目录');
            $blockParent.addClass('hide');
            $blockIcons.removeClass('hide');
        } else {
            $container.find('.block-title').html('<i class="fa fa-plus"></i> 添加子目录');
            $blockParent.removeClass('hide');
            $blockIcons.addClass('hide');

            $parentName.val(vm.options.parentName);
            $parentName.attr('disabled', 'disabled');
            $parentId.val(vm.options.parentId);
        }

        //commit
        $container.find('.btn-primary').on('click', function () {
            vm.form.parentName = $parentName.val();
            vm.form.parentId = $parentId.val();
            vm.form.name = $name.val();
            vm.form.icon = $icon.val();
            vm.form.des = $des.val();
            vm.form.order = $order.val();
            vm.form.public = 0; //$public.val();
            vm.form.nodeType = $types.val();

            if (vm.form.name == '') {
                _showError('请输入名称');
                return;
            }
            _hideError();


            if (vm.form.order == '') {
                _showError('请输入排序');
                return;
            }
            _hideError();

            if (isNaN(vm.form.order)) {
                _showError('排序格式为数字');
                return;
            }
            _hideError();

            var input = {};
            input.ParentId = vm.form.parentId;
            input.Name = vm.form.name;
            input.Icon = vm.form.icon;
            input.Des = vm.form.des;
            input.Order = vm.form.order;
            input.Public = vm.form.public == 1 ? true : false;
            input.TeamKey = vm.options.teamKey;
            input.NodeTypeId = vm.form.nodeType;

            NodeService.Insert(input, function (json) {
                var result = json.value;
                console.log(JSON.stringify(json));
                //console.log(JSON.stringify(result));
                if (result.Success) {
                    notify.success('添加成功了')
                    vm.options.callback(result.Result);
                    vm.close();
                } else {
                    _showError(result.Error.Message);
                }
            });
        });
    }


    //exposed methods
    vm.open = function (options) {
        $.extend(vm.options, options);
        if ($container == undefined) {
            $.ajax({
                type: 'get',
                dataType: 'html',
                url: '/js/notes/addNode.dialog.html?t=' + new Date().getTime(),
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