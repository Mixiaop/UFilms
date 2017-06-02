<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="UFilm.Console.Adults.ActorPhotos.List" %>
<%@ Import Namespace="U" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
     <!-- Page Header -->
    <div class="content bg-gray-lighter">
        <div class="row items-push">
            <div class="col-sm-8">
                <h1 class="page-heading"><%= Model.Actor.CnName %> - 所有照片 <small></small>
                </h1>
            </div>
        </div>
    </div>
    <!-- END Page Header -->
    <!-- Page Content -->
    <div class="content">
        <div class="row items-push">
            <div class="col-xs-12">
                <div class="form-inline">
                    <div class="form-group">
                        <select id="ddlIsX" class="form-control">
                            <option value="0">全部</option>
                            <option value="1">普通</option>
                            <option value="2">限制级</option>
                        </select>
                        <label class="col-xs-pull-1">
                            <button id="btnSearch" class="btn btn-primary btn-sm">搜索</button>
                        </label>
                    </div>
                </div>
            </div>
        </div>
        <div class="row items-push" id="photosContainer">
            <% foreach (var photo in Model.Photos.Items)
               { %>
            <div class="col-xs-6 col-sm-6 col-md-3 col-lg-2 animated fadeIn">
                <div class="img-container">
                    <img class="img-responsive" src="<%= photo.Picture.ThumbUrl %>" alt="<%= photo.Actor.CnName %>的照片" >
                    <div class="img-options">
                        <div class="img-options-content">
                            <h3 class="font-w400 text-white push-5"><%= photo.Actor.CnName %>的照片</h3>
                            <h4 class="h6 font-w400 text-white-op push-15"></h4>
                            <a class="btn btn-sm btn-default" href="<%= photo.Picture.SourceUrl %>" target="_blank"><i class="fa fa-search"></i>原图</a>
                            <a class="btn btn-sm btn-default btnRemovePhoto" href="javascript:void(0)" data-photoid="<%= photo.Id %>"><i class="fa fa-times"></i>删除</a>
                        </div>
                    </div>
                </div>
            </div>
            <%} %>
        </div>
        <div class="text-center">
            <ul class="pagination">
                <%= Model.PagingHTML %>
            </ul>
        </div>
        <!-- END Gallery -->
    </div>
    <!-- END Page Content -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script src="/assets/js/plugins/masonry/masonry.pkgd.min.js"></script>
    <!-- Page JS Code -->
    <script>
        $(function () {
            var $txtKeywords = $('#tbSearchKeywords'),
                $btnSearch = $('#btnSearch'),
                $btnRemovePhotos = $('.btnRemovePhoto');

            //搜索
            var _search = function () {
                var url = "List.aspx?wd=" + $txtKeywords.val();
                window.location.href = url;
            }

            $btnSearch.click(_search);
            $txtKeywords.keydown(function (e) { if (e.keyCode == 13) _search(); });


            var imgTimer;
            var $imgs = $('#photosContainer img');
            var isLoaded = true;

            function imgloaded(callback) {
                $imgs.each(function () {
                    if ($(this).height() == 0) {
                        isLoaded = false;
                        return false;
                    }
                });
                if (isLoaded) {
                    clearTimeout(imgTimer);
                    callback();
                } else {
                    isLoaded = true;
                    imgTimer = setTimeout(function () { imgloaded(callback); }, 500);
                }
            }

            imgloaded(function () {
                new Masonry('#photosContainer', { resize: true }).on('layoutComplete', function () { });
            });

            //图片
            //remove photo
            $btnRemovePhotos.click(function () {
                var photoId = parseInt($(this).data('photoid'));
                if (confirm('你确认删除吗?')) {
                    AdultsService.DeleteActorPhoto(photoId, function (res) {
                        var result = res.value;
                        if (result.Success) {
                            notify.success('删除成功');
                            page.reload(1000);
                        }
                    });
                }

            });
        });
    </script>
</asp:Content>
