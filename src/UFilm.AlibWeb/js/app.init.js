require.config({
    urlArgs: 'v=' + (new Date().getTime()),
    waitSeconds: 30,
    paths: {
        //lib
        'jquery': '/lib/core/jquery.min',
        'bootstrap': '/lib/core/bootstrap.min',
        'underscore': '/lib/core/underscore/underscore-1.6.min',
        'handlebars': '/lib/core/handlebars/handlebars-v2.0.0',
        'cookie': '/lib/core/js.cookie.min',
        //'isotope': '/lib/core/isotope/isotope.pkgd',
        'jquery.placeholder': '/lib/core/jquery.placeholder.min',
        'jquery.slimscroll': '/lib/core/jquery.slimscroll.min',
        'jquery.scrollLock': '/lib/core/jquery.scrollLock.min',
        'jquery.appear': '/lib/core/jquery.appear.min',
        'jquery.countTo': '/lib/core/jquery.countTo.min',
        'masonry': '/lib/plugins/masonry/masonry.pkgd.min',
        'jquery.mousewheel': '/lib/plugins/jquery.mousewheel/mousewheel-3.0.6.pack',
        'jquery.fineuploader': '/lib/plugins/jquery.fineuploader/jquery.fineuploader-3.4.1.min',
        'jquery.tagsinput': '/lib/plugins/jquery-tags-input/jquery.tagsinput.min',
        'jquery.masonry': '/lib/plugins/jquery.masonry.min',
        'jquery.easing': '/lib/plugins/jquery.easing',
        'jquery.montage': '/lib/plugins/jquery.montage/jquery.montage',
        'jquery.fancybox': '/lib/plugins/jquery.fancybox/jquery.fancybox',
        'jquery.fancybox-thumbs': '/lib/plugins/jquery.fancybox/helpers/jquery.fancybox-thumbs',
        'bs.notify': '/lib/plugins/bootstrap-notify/bootstrap-notify.min',
        //utilities
        'utils/notify': '/js/utils/notify',
    },
    shim: {
        'bootstrap': { deps: ['jquery'] },
        'masonry': { deps: ['jquery'] },
        'jquery.scrollLock': { deps: ['jquery'] },
        'jquery.slimscroll': { deps: ['jquery'] },
        'jquery.appear': { deps: ['jquery'] },
        'jquery.fineuploader': { deps: ['jquery'] },
        'jquery.tagsinput': { deps: ['jquery'] },
        'jquery.masonry': { deps: ['jquery'] },
        'jquery.easing': { deps: ['jquery'] },
        'jquery.montage': { deps: ['jquery'] },
        'jquery.mousewheel': { deps: ['jquery'] },
        'jquery.fancybox': { deps: ['jquery'] },
        'jquery.fancybox-thumbs': { deps: ['jquery.fancybox'] }
    }
});

require.onError = function (err) {
    console.log(JSON.stringify(err));
    if (err.requireType === 'timeout') {
        console.log('modules: ' + err.requireModules);
    }
    throw err;
};

