define(['jquery', 'utils/notify', 'bootstrap'], function ($, notify) {

    var $container, $errorBox;
    var $title, $node, $targetNodes;

    var vm = {
        committing: false,
        form: {},
        options: {
            contentId: 0,
            nodeId: 0,
            title: '',
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
        $container = $('#nodes_moveContentDialog');
        $errorBox = $container.find('.alert');
        
    }


    //exposed methods
    vm.open = function (options) {
        $.extend(vm.options, options);
        if ($container == undefined) {
            $.ajax({
                type: 'get',
                dataType: 'html',
                url: '/js/notes/moveContent.dialog.html?t=' + new Date().getTime(),
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