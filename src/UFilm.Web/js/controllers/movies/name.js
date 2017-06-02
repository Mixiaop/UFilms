require(['jquery', 'bootstrap', 'jquery.mousewheel', 'utils/notify', 'jquery.montage', 'jquery.fancybox', 'jquery.fancybox-thumbs'], function ($, bs, $mousewheel, notify, $montage) {
    var $photosContainer = $('#photo-items'),
        $imgs = $photosContainer.find('img').hide(),
        totalImgs = $imgs.length,
        cnt = 0;

    var _init = function () {
        $imgs.each(function (i) {
            var $img = $(this);
            $('<img/>').load(function () {
                ++cnt;
                if (cnt === totalImgs) {
                    $imgs.show();
                    $photosContainer.montage({
                        fillLastRow: false,
                        alternateHeight: true,
                        alternateHeightRange: {
                            min: 120,
                            max: 180
                        }
                    });
                }
            }).attr('src', $img.attr('src'));
        });

        setTimeout(function () {
            //弹出框
            $(".fancybox-thumbs").fancybox({
                prevEffect: 'none',                nextEffect: 'none',                closeBtn: false,                arrows: false,                nextClick: true,                helpers: {
                    thumbs: {
                        width: 50,                        height: 50
                    }
                },
                afterLoad: function () {
                    this.title = (this.index + 1) + ' / ' + this.group.length;
                }
            });
        }, 500);
    }

    _init();
});