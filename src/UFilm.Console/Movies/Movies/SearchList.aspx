<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="SearchList.aspx.cs" Inherits="UFilm.Console.Movies.Movies.SearchList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
    <style type="text/css">
        table .cover img {
            max-width:140px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="col-sm-12 col-lg-12">
        <form runat="server">
            <!-- Page Header -->
            <div class="content bg-gray-lighter">
                <div class="row items-push">
                    <div class="col-sm-7">
                        <h1 class="page-heading">影片列表 <small></small>
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
                                <asp:TextBox runat="server" ID="tbSeachKeywords" placeholder="请输入搜索关键字" CssClass="form-control"></asp:TextBox>
                                <label class="col-xs-pull-1">
                                    <asp:Button ID="btnSearch" CssClass="btn btn-primary btn-sm" runat="server" Text="搜索"></asp:Button>
                                </label>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Dynamic Table Full -->
                <div class="block">
                    <div class="block-content table-responsive">
                        <asp:Literal runat="server" ID="ltlMessage"></asp:Literal>
                        <table class="table table-hover js-dataTable-full">
                            <thead>
                                <tr>
                                    <td></td>
                                    <th class="text-center" width="20%">影片名称</th>
                                    <th class="text-center" width="10%">类型</th>
                                    <th class="text-center" width="10%">地区</th>
                                    <th class="text-center" width="10%">语言</th>
                                    <th class="text-center" width="15%">豆瓣评分</th>
                                    <th class="text-center">官网</th>
                                    <th class="text-center" width="15%">Imdb编号</th>
                                    <th class="text-center" width="10%">剧照数</th>
                                    <th class="text-center" width="10%">资源数</th>
                                    <th class="text-center" width="10%">创建时间</th>
                                </tr>
                            </thead>
                            <asp:Repeater runat="server" ID="rptDatas">
                                <ItemTemplate>
                                    <tbody>
                                        <tr>
                                            <td class="text-center">
                                                <a class='btn btn-default btn-xs active-btn' href="EditMovie.aspx?movieId=<%# Eval("Id") %>&<%= GetBackUrlEncoded() %>" data-toggle="tooltip" title="编辑"><i class="fa fa-pencil"></i></a>
                                                <a class='btn btn-default btn-xs active-btn uploadPhotos' data-id="<%# Eval("Id") %>" data-name="<%# Eval("CnName") %> <%# Eval("EnName") %>" data-toggle="tooltip" title="上传剧照"><i class="fa fa-photo"></i></a>
                                                <a href="../MovieTorrents/Add.aspx?movieid=<%# Eval("Id") %>&<%= GetBackUrlEncoded() %>" target="_blank" class='btn btn-default btn-xs active-btn' data-toggle="tooltip" title="分享资源"><i class="fa fa-share"></i></a>
                                                <asp:LinkButton CommandArgument='<%# Eval("Id") %>' runat="server" class='btn btn-default btn-xs active-btn ' OnClientClick="return confirm('你确定删除电影，考虑清楚了吗?')" OnClick="DeleteClick" data-toggle="tooltip" title="删除"><i class="fa fa-remove"></i></asp:LinkButton>
                                            </td>
                                            <td class="text-left cover" title="<%# Eval("EnName") %>"><a href="<%# WebRoutes.GetRoute("Movies.Subject",Eval("Id")) %>" target="_blank"><img src="<%# ((UFilm.Services.Media.Dto.PictureDto)Eval("Cover")).ThumbUrl %>"  /></a> <a href="<%# WebRoutes.GetRoute("Movies.Subject",Eval("Id")) %>" target="_blank" title="<%# Eval("EnName") %>"><%# Eval("CnName") %></a> <%# Eval("Year") %> </td>
                                            <td class="text-center"><%# Eval("MovieType") %> </td>
                                            <td class="text-center"><%# Eval("Area") %></td>
                                            <td class="text-center"><%# Eval("Language") %> </td>
                                            <td class="text-center"><%# Eval("DoubanRating")%> <a href="<%# Eval("DoubanLink") %>" target="_blank"><i class="fa fa-link"></i></a> </td>
                                            <td class="text-center"><%# Eval("WebSite") %> </td>
                                            <td class="text-center"><a href="http://www.imdb.com/title/<%# Eval("ImdbCode") %>" target="_blank"><%# Eval("ImdbCode") %></a></td>
                                            <td class="text-center"><a href="../MoviePhotos/List.aspx?wd=<%# Eval("CnName") %>" target="_blank" title="查看剧照"><i class="fa fa-picture-o"></i></a> <%# Eval("PhotoCount") %></td>
                                            <td class="text-center"><a href="../MovieTorrents/List.aspx?movieId=<%# Eval("Id") %>" target="_blank" title="查看资源"><i class="fa fa-car"></i></a> <%# Eval("TorrentCount") %></td>
                                            <td class="text-center"><%# Eval("CreationTime","{0:yyyy-MM-dd HH:mm}") %></td>
                                        </tr>
                                    </tbody>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <div class="text-center">
                        <ul class="pagination">
                             <%= Model.PagiHtml %>
                        </ul>
                    </div>
                </div>
                <!-- END Dynamic Table Full -->
            </div>
            <!-- END Page Content -->
        </form>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <!-- UploadPhotos Modal -->
    <div class="modal fade" id="modalUploadPhotos" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-popin">
            <div class="modal-content">
                <div class="block block-themed block-transparent remove-margin-b">
                    <div class="block-header bg-primary-dark">
                        <ul class="block-options">
                            <li>
                                <button data-dismiss="modal" type="button"><i class="si si-close"></i></button>
                            </li>
                        </ul>
                        <h3 class="block-title"><i class="fa fa-photo"></i> <span></span> - 上传剧照</h3>
                    </div>
                    <div class="block-content">
                        <div class="form-horizontal push-10-t">
                            <div class="alert alert-danger hide">
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="form-material ">
                                      <select class="form-control" id="ddlPhotoType">
                                          <option value="1">剧照</option>
                                          <option value="2">封面</option>
                                      </select>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="form-material ">
                                      <div id="photos"></div>
                                        <div id="photosThumbsBox"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn  btn-primary" type="button" >保存</button>
                    <button class="btn  btn-default" type="button" data-dismiss="modal">关闭</button>
                </div>
            </div>
        </div>
    </div>
    <!-- END UploadPhotos Modal-->
    <link href="/assets/js/plugins/jquery.fineuploader/fineuploader.css" rel="stylesheet" />
    <script src="/assets/js/plugins/jquery.fineuploader/jquery.fineuploader-3.4.1.min.js"></script>
    <script>
        $modalUploadPhotos = $('#modalUploadPhotos');
        $modalUploadPhotos_Photos = $modalUploadPhotos.find('#photos');
        $modalUploadPhotos_PhotoThumbsBox = $modalUploadPhotos.find('#photosThumbsBox');
        var uploadPhotoIds = [];
        var movieId = 0;
        var committing = false;

        //上传点击
        $('.uploadPhotos').click(function () {
            movieId = parseInt($(this).data('id'));
            var name = $(this).data('name');

            $modalUploadPhotos.find('.block-title span').html(name);

            $modalUploadPhotos_Photos.fineUploader({
                request: { endpoint: "/AjaxServices/jQueryFineUploader/MultiUploadPicture.ashx?size=320" },
                multiple: true,
                text: {
                    uploadButton: '上传'
                }
            }).on('complete',
            function (event, id, fileName, responseJSON) {
                if (responseJSON.success) {
                    //console.log(JSON.stringify(responseJSON) + '<br />');
                    <%-- var thumbBox = $("#thumbPicture");
                    thumbBox.html('');
                    thumbBox.append('<img src="' + responseJSON.showthumb + '" >');
                    $("#<%= hfPictureId.ClientID %>").val(responseJSON.pictureId);
                    $("#<%= hfPictureThumb.ClientID %>").val(responseJSON.thumb);--%>
                    $modalUploadPhotos_PhotoThumbsBox.append('<img src="' + responseJSON.showthumb + '" >');
                    uploadPhotoIds.push(responseJSON.pictureId);

                } else {
                    alert(responseJSON.error);
                }
            });

            //reset
            uploadPhotoIds = [];
            $modalUploadPhotos_PhotoThumbsBox.html('');
            $modalUploadPhotos.modal('show');
        });
        
        //保存图片
        $modalUploadPhotos.find('.btn-primary').click(function () {
            if (!committing) {
                committing = true;
                $(this).text('保存中.');
                var $ddlPhotoType = $modalUploadPhotos.find('#ddlPhotoType');
                Movies.MovieService.SavePhotos(movieId, uploadPhotoIds, $ddlPhotoType.val(), function (res) {
                    var result = res.value;
                    if (result.Success) {
                        $modalUploadPhotos.modal('hide');
                        $(this).text('保存');
                        notify.success('保存成功');
                        setTimeout(function () {
                            window.location.reload();
                        }, 500);
                    } else {
                        notify.error(res.Message);
                    }
                });
            }
        });
    </script>
</asp:Content>
