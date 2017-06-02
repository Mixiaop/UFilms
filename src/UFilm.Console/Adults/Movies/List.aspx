<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="UFilm.Console.Adults.Movies.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
     <style type="text/css">
        table .cover img {
            max-width:140px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="col-sm-12 col-lg-12">
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
                                <input type="text" id="txtKeywords" placeholder="番号、关键字" class="form-control" value="<%= Model.GetKeywords %>" />
                                <select class="form-control" id="ddlMovieType">
                                    <option value="">类型</option>
                                    <% foreach(var type in Model.MovieTypes){ %>
                                    <option value="<%= type.Name %>" <%= Model.GetMovieType == type.Name?"selected='selected'":"" %>><%= type.Name %></option>
                                    <%} %>
                                </select>
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
                                    <th class="text-center" width="20%">影片名称</th>
                                    <th class="text-center" width="10%">番号</th>
                                    <th class="text-center" >演员</th>
                                    <th class="text-center" width="10%">类型</th>
                                    <th class="text-center" width="10%">时长</th>
                                    <th class="text-center" >发布日期</th>
                                    <%--
                                    <th class="text-center" width="10%">剧照数</th>
                                    <th class="text-center" width="10%">资源数</th>--%>
                                    <th class="text-center" width="10%">创建时间</th>
                                </tr>
                            </thead>
                                    <tbody>
                                        <% foreach(var movie in Model.Results.Items){ %>
                                        <tr>
                                            <td class="text-center">
                                                <%--<a class='btn btn-default btn-xs active-btn' href="EditMovie.aspx?movieId=<%# Eval("Id") %>&<%= GetBackUrlEncoded() %>" data-toggle="tooltip" title="编辑"><i class="fa fa-pencil"></i></a>
                                                <a class='btn btn-default btn-xs active-btn uploadPhotos' data-id="<%# Eval("Id") %>" data-name="<%# Eval("CnName") %> <%# Eval("EnName") %>" data-toggle="tooltip" title="上传剧照"><i class="fa fa-photo"></i></a>
                                                <a href="../MovieTorrents/Add.aspx?movieid=<%# Eval("Id") %>&<%= GetBackUrlEncoded() %>" target="_blank" class='btn btn-default btn-xs active-btn' data-toggle="tooltip" title="分享资源"><i class="fa fa-share"></i></a>--%>
                                                <a href="http://www.cilibaba.org/search/<%= movie.Code %>/" target="_blank" class='btn btn-default btn-xs active-btn'  data-toggle="tooltip" title="老司机开车"><i class="fa fa-car"></i></a>
                                                <a class='btn btn-default btn-xs active-btn btnDelete' data-name="<%= movie.Code %> <%= movie.CnName %>" data-id="<%= movie.Id %>" href="javascript:void(0);" data-toggle="tooltip" title="删除"><i class="fa fa-remove"></i></a>
                                                
                                            </td>
                                            <td class="text-left cover" ><a href="<%= movie.Cover.SourceUrl %>" target="_blank"><img src="<%= movie.Cover.ThumbUrl%>"  /></a> <a href="<%= movie.Cover.SourceUrl %>" target="_blank" ><%= movie.CnName %></a> <%= movie.Year>0?movie.Year.ToString():"" %> </td>
                                            <td class="text-center"><%= movie.Code %></td>
                                            <td class="text-center"><%= movie.Actors %></td>
                                            <td class="text-center"><%= movie.MovieType.Replace(","," / ") %> </td>
                                            <td class="text-center"><%= movie.MovieLength %></td>
                                            <td class="text-center"><%= movie.OtherPostYear %></td>
                                            <%--<td class="text-center"><a href="../MoviePhotos/List.aspx?wd=<%# Eval("CnName") %>" target="_blank" title="查看剧照"><i class="fa fa-picture-o"></i></a> <%# Eval("PhotoCount") %></td>
                                            <td class="text-center"><a href="../MovieTorrents/List.aspx?movieId=<%# Eval("Id") %>" target="_blank" title="查看资源"><i class="fa fa-car"></i></a> <%# Eval("TorrentCount") %></td>--%>
                                            <td class="text-center"><%= movie.CreationTime.ToString("yyyy-MM-dd HH:mm") %></td>
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
    <script>
        //==========搜索
        var $keywords = $('#txtKeywords');
        var $ddlMovieType = $('#ddlMovieType');

        var search = function () {
            var url = '/Adults/Movies/List.aspx?keywords=' + $keywords.val() + '&movietype=' + $ddlMovieType.val();
            window.location.href = url;
        }
        $keywords.keydown(function (e) {
            if (e.keyCode == 13)
                search();
        });
        $('#btnSearch').click(function () {
            search();
        });

        //==========删除
        $('.btnDelete').click(function () {
            var id = parseInt($(this).data('id'));
            if (confirm('你确定删除电影【' + $(this).data('name') + '】，考虑清楚了吗?')) {

                AdultsService.DeleteMovie(id, function (res) {
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
    </script>
</asp:Content>
