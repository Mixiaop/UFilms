define(['jquery', 'utils/notify', 'bootstrap'], function ($, notify) {

    var $container;
    var $title, $body, $fileUrl;
    var vm = {
        options: {
            contentId: 0
        }
    };

    function _initialize() {
        $container = $('#nodes_contentInfoDialog');
        $title = $container.find('.block-title');
        $body = $container.find('.block-content');
        $fileUrl = $container.find('.btnFileUrl');

        ContentService.Get(vm.options.contentId, function (res) {
            var json = res.value;
            if (json.Success) {
                var content = json.Result;
                $title.html(content.Title);
                $body.html(content.Body);
                if (content.FileSize > 0) {
                    $fileUrl.removeClass('hidden');
                    $fileUrl.html('<i class="fa fa-download"></i> ' + content.FormatFileSize);
                } else {
                    $fileUrl.addClass('hidden');
                }
            }
        });


    }

    //exposed methods
    vm.open = function (options) {
        $.extend(vm.options, options);
        if ($container == undefined) {
            $.ajax({
                type: 'get',
                dataType: 'html',
                url: '/js/notes/contentInfo.dialog.html',
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