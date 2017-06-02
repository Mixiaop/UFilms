<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="UFilm.Console.Movies.MovieTorrents.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="col-sm-12 col-lg-12">
        <form runat="server">
            <!-- Page Header -->
            <div class="content bg-gray-lighter">
                <div class="row items-push">
                    <div class="col-sm-7">
                        <h1 class="page-heading"><%= Model.Movie.FormatName %> <small>的资源列表</small></h1>
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
                                <%--<asp:TextBox runat="server" ID="tbSeachKeywords" placeholder="请输入搜索关键字" CssClass="form-control"></asp:TextBox>--%>
                                <label class="col-xs-pull-1">
                                    <%--<asp:Button ID="btnSearch" CssClass="btn btn-primary btn-sm" runat="server" Text="搜索"></asp:Button>--%>
                                    <a href="Add.aspx?movieId=<%= Model.Movie.Id %>" class="btn btn-primary btn-sm">添加</a>
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
                                      <th class="text-center" width="20%"></th>
                                    <th class="text-center"  >链接</th>
                                    <th class="text-center" width="20%">大小</th>
                                    <th class="text-center" width="10%">时间</th>
                                </tr>
                            </thead>
                            <% foreach(var t in Model.Torrents.Items){ %>
                                    <tbody>
                                        <tr>
                                            <td class="text-center">
                                                <a href="javascript:;" class='btn btn-default btn-xs active-btn btnDelete' data-id="<%= t.Id %>" data-toggle="tooltip" title="删除"><i class="fa fa-remove"></i></a>
                                            </td>
                                            <td class="text-center">
                                                <a href="<%= t.Link %>"><%= t.Name %></a>
                                            </td>
                                            <td class="text-center"><%= t.Size.IsNotNullOrEmpty()?t.Size:"-" %></td>
                                            <td class="text-center"><%= t.CreationTime.ToString("yyyy-MM-dd HH:mm") %></td>
                                        </tr>
                                    </tbody>
                            <%} %>
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
        </form>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script>
        $('.btnDelete').click(function () {
            var torrentId = parseInt($(this).data('id'));
            if (confirm('你确定删除资源，考虑清楚了吗??')) {
                MoviesService.DeleteTorrent(torrentId, function (res) {
                    var result = res.value;
                    if (result.Success) {
                        notify.success('删除成功');
                        page.reload(1000);
                    }
                });
            }

        });
    </script>
</asp:Content>
