<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="UFilm.Console.Adults.Actors.List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
    <style type="text/css">
        table .cover img {
            max-width: 140px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="col-sm-12 col-lg-12">
        <!-- Page Header -->
        <div class="content bg-gray-lighter">
            <div class="row items-push">
                <div class="col-sm-7">
                    <h1 class="page-heading">影人列表 <small></small>
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
                            <input type="text" id="txtKeywords" placeholder="名称" class="form-control" value="<%= Model.GetKeywords %>" />
                            <label class="col-xs-pull-1">
                                <input type="button" class="btn btn-primary btn-sm" id="btnSearch" value="搜索" />
                                <a href="Add.aspx?<%= GetBackUrlEncoded() %>" class="btn btn-primary btn-sm">添加</a>
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
                                <th class="text-center" width="20%">名称</th>
                                <th class="text-center" width="10%">拼音</th>
                                <th class="text-center" width="10%">点击量</th>
                                <th class="text-center" width="10%">影片数</th>
                                <th class="text-center" width="10%">照片数</th>
                                <th class="text-center">简介</th>
                                <th class="text-center" width="10%">创建时间</th>
                            </tr>
                        </thead>
                        <tbody>
                            <% foreach (var actor in Model.Results.Items)
                               { %>
                            <tr>
                                <td class="text-center">
                                    <a class='btn btn-default btn-xs active-btn' href="Edit.aspx?actorId=<%= actor.Id %>&<%= GetBackUrlEncoded() %>" data-toggle="tooltip" title="编辑"><i class="fa fa-pencil"></i></a>
                                    <a class='btn btn-default btn-xs active-btn uploadPhotos' data-id="<%= actor.Id%>" data-name="<%= actor.CnName %>" data-toggle="tooltip" title="上传照片"><i class="fa fa-photo"></i></a>
                                    <a class='btn btn-default btn-xs active-btn btnDelete' data-name="<%= actor.CnName %>" data-id="<%= actor.Id %>" href="javascript:void(0);" data-toggle="tooltip" title="删除"><i class="fa fa-remove"></i></a>
                                </td>
                                <td class="text-left cover"><a href="../Movies/List.aspx?keywords=<%= actor.CnName %>">
                                    <img src="<%= actor.Avatar.ThumbUrl%>" /></a> <a href="../Movies/List.aspx?keywords=<%= actor.CnName %>"><%= actor.CnName %></a> </td>
                                <td class="text-center"><%= actor.Pinyin %></td>
                                <td class="text-center"><%= actor.Hits %> </td>
                                <td class="text-center"><%= actor.MovieCount %>  部</td>
                                <td class="text-center"><a href="../ActorPhotos/List.aspx?actorId=<%= actor.Id %>" target="_blank" title="查看照片"><i class="fa fa-picture-o"></i></a> <%= actor.PhotoCount%> 张</td>
                                <td class="text-center"><%= actor.Introduction.GetSubString(0,100,"...") %></td>
                                <td class="text-center"><%= actor.CreationTime.ToString("yyyy-MM-dd HH:mm") %></td>
                            </tr>
                            <%} %>
                        </tbody>
                    </table>
                </div>
                <div class="text-center">
                    <ul class="pagination">
                        <%= Model.PagingHTML %>
                    </ul>
                </div>
            </div>
            <!-- END Dynamic Table Full -->
        </div>
        <!-- END Page Content -->

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
                        <h3 class="block-title"><i class="fa fa-photo"></i> <span></span> - 上传照片</h3>
                    </div>
                    <div class="block-content">
                        <div class="form-horizontal push-10-t">
                            <div class="alert alert-danger hide">
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="form-material ">
                                      <select class="form-control" id="ddlIsX">
                                          <option value="0">普通</option>
                                          <option value="1">限制级</option>
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
        var $keywords = $('#txtKeywords');
        //var $ddlMovieType = $('#ddlMovieType');

        var search = function () {
            var url = '/Adults/Actors/List.aspx?keywords=' + $keywords.val();
            window.location.href = url;
        }
        $keywords.keydown(function (e) {
            if (e.keyCode == 13)
                search();
        });
        $('#btnSearch').click(function () {
            search();
        });

        //delete
        $('.btnDelete').click(function () {
            var actorId = parseInt($(this).data('id'));
            if (confirm('你确定删除影人【' + $(this).data('name') + '】，考虑清楚了吗?')) {

                AdultsService.DeleteActor(actorId, function (res) {
                    var result = res.value;
                    if (result.Success) {
                        notify.success('删除成功');
                        page.reload(1000);
                    } else {
                        notify.error('出错了：' + result.Error.Message);
                    }
                });
            }

        });

        //uploadPhotos
        $modalUploadPhotos = $('#modalUploadPhotos');
        $modalUploadPhotos_Photos = $modalUploadPhotos.find('#photos');
        $modalUploadPhotos_PhotoThumbsBox = $modalUploadPhotos.find('#photosThumbsBox');
        var uploadPhotoIds = [];
        var actorId = 0;
        var committing = false;

        //上传点击
        $('.uploadPhotos').click(function () {
            actorId = parseInt($(this).data('id'));
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
                var $ddlIsX = $modalUploadPhotos.find('#ddlIsX');
                AdultsService.SaveActorPhotos(actorId, uploadPhotoIds, $ddlIsX.val(), function (res) {
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
