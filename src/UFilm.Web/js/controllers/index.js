//
//首页控制器
//
require(['jquery', 'handlebars', 'masonry', 'bootstrap', 'utils/notify'], function ($, handlebars, Masonry, bs, notify) {

    var $movieContainer = $('.page-index .movies');
    //var $more = $('.page-index .btn-more');
    var $loading = $('.page-index .loading');
    var $loadOffset = $('.page-index #loadOffset');
    var $chooseCdi = $('#chooseCdi');
    var $cdiContainer = $('.cdi-container');
    var loading = false;
    var isEnd = false; //电影见底了
    var pageIndex = 0, pageSize = 24;

    var getMovieTypes = $('#hidGetMovieTypes').val();
    var getMovieAreas = $('#hidGetMovieAreas').val();

    //载入电影列表
    var _loadMovies = function () {
        if (isEnd == false) {

            if (!loading) {
                pageIndex++;
                loading = true;
                $loading.removeClass('hide');

                setTimeout(function () {
                    MoviesService.QueryLastActivityMovies(pageIndex, pageSize, '', getMovieTypes, getMovieAreas, function (res) {
                        var result = res.value;
                        if (result.Success) {
                            var value = result.Result;
                            var $html = $('#tempMovieItem').html();
                            var template = handlebars.compile($html);
                            var $data = template({ movies: value.Items });

                            if (value.Items.length == 0) {
                                isEnd = true;
                            }

                            $movieContainer.append($data);

                            setTimeout(function () {
                                //3d翻转
                                $movieContainer.find('.mbox-inner').unbind('mouseenter');
                                $movieContainer.find('.mbox-inner').mouseenter(function () {
                                    $movieContainer.find('.mbox-inner').removeClass('on');
                                    $(this).addClass('on');
                                    //重置背面高度
                                    var height = $(this).height();
                                    $(this).find('.mbox-back').css('height', height + 'px');
                                });

                                //3d翻转-重置背面高度
                                $movieContainer.find('.mbox-inner').each(function () {
                                    var height = $(this).height();
                                    $(this).find('.mbox-back').css('height', height + 'px');
                                });

                                //块点击
                                $movieContainer.find('.movie-item').unbind('mouseenter');
                                $movieContainer.find('.movie-item').mouseenter(function () {
                                    $(this).css('cursor', 'pointer');
                                });
                                $movieContainer.find('.movie-item').unbind('click');
                                $movieContainer.find('.movie-item').on('click', function () {
                                    var url = $(this).data('url');
                                    window.open(url);
                                    //window.location.href = url;
                                });



                                //好像没什么用
                                $(window).unbind('resize');
                                $movieContainer.unbind('resize');
                                new Masonry('.page-index .movies', { resize: true }).on('layoutComplete', function () {
                                    $movieContainer.find('.mbox-inner').removeClass('on');
                                });
                            }, 100);
                        }
                        loading = false;
                        $loading.addClass('hide');
                        //console.log(JSON.stringify(result));
                    });
                }, 300);
            }
        }

    }

    //initialize
    _loadMovies();

    $chooseCdi.on('click', function () {
        $('.cdi-container .items').slideToggle();
    });

    $(window).bind('scroll', function () {

        //load movies
        var offsetTop = $loadOffset.offset().top;
        var clientHeight = document.documentElement.clientHeight;
        var scrollTop = $(window).scrollTop();
        //var scrollStatus = movieContainer.attr('scrollPagination');
        if ((offsetTop - 150) < clientHeight + scrollTop && loading == false) {
            _loadMovies();
        }

        //fix cdi container
        if (scrollTop > 80) {
            $cdiContainer.addClass('cdi-container-fixed');
        } else {
            $cdiContainer.removeClass('cdi-container-fixed');
        }
    });

});