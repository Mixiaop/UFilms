//
//全局控制器
//
require(['jquery', 'jquery.easing'], function ($, $easing) {
    var $backToTop = $('#back-to-top');
    var $topSearchbar = $('#topSearchbar');
    var backToTopShowing = false;
    var navChanged = false;

    $(window).scroll(function () {
        //BackToTop
        if ($(window).scrollTop() > 50) {
            if (backToTopShowing == false) {
                backToTopShowing = true;
                $backToTop.show();
                $backToTop.css({ top: $(window).height() });
                $backToTop.stop(true, false).animate({ top: $(window).height() - 100 }, 300, "easeOutBack");
            }
        } else {
            if (backToTopShowing == true) {
                backToTopShowing = false;
                $backToTop.stop(true, false).animate({ top: $(window).height() }, 300, "easeInBack");
            }
        }
    });



    $backToTop.bind('click', function () {
        $('body,html').animate({ scrollTop: 0 }, 500, "easeInOutQuad");
        return false;
    });

    //Search
    var _search = function (wd) {
        var value = wd;
        var url = $topSearchbar.find('input[type=hidden]').val();
        url = url.replace('{1}', 1);
        url = url.replace('{0}', value);
        window.location.href = url;
    }

    $topSearchbar.find('input').on('keydown', function (e) {
        if (e.keyCode == 13) {
            _search($(this).val());
        }
    });

    //Navigation for mobile
    var $mobileNavi = $('.nav-mb');
    var $mobileNaviMenu = $('#mobileNaviMenu'),
        $mobileNaviSearchBox = $('#mobileNaviSearchBox');

    $mobileNavi.find('.menu').on('click', function () {
        $('body').addClass('open');
    })

    $('.nav-panel-bg').on('click', function () {
        $('body').removeClass('open');
    });
    $mobileNaviMenu.find('.btn-close').on('click', function () {
        $('body').removeClass('open');
    })

    //mobile search
    $mobileNavi.find('.search').on('click', function () {
        $mobileNaviSearchBox.addClass('active');
    });

    $mobileNaviSearchBox.find('input').on('keydown', function (e) {
        if (e.keyCode == 13) {
            _search($(this).val());
        }
    });
    $mobileNaviSearchBox.find('button').on('click', function () {
        _search($mobileNaviSearchBox.find('input').val());
    });

    $mobileNaviSearchBox.find('.btn-close').on('click', function () {
        $('.search-wrap').removeClass('active');
    });

});