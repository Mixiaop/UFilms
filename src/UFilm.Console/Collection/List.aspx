<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="UFilm.Console.Collection.List" %>
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
                        <h1 class="page-heading">精选集列表 <small></small>
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
                                <%--<asp:TextBox runat="server" ID="tbSeachKeywords" placeholder="请输入搜索关键字" CssClass="form-control"></asp:TextBox>--%>
                                <label class="col-xs-pull-1">
                                    <%--<asp:Button ID="btnSearch" CssClass="btn btn-primary btn-sm" runat="server" Text="搜索"></asp:Button>--%>
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
                                    <th></th>
                                    <th class="text-center" width="20%">名称</th>
                                    <th class="text-center">简介</th>
                                    <th class="text-center" width="20%">标签</th>
                                    <th class="text-center" width="10%">影片数</th>
                                    <th class="text-center" width="10%">创建时间</th>
                                </tr>
                            </thead>
                            <asp:Repeater runat="server" ID="rptDatas">
                                <ItemTemplate>
                                    <tbody>
                                        <tr>
                                            <td class="text-center">
                                                <a href="/Collection/Items/List.aspx?collectionid=<%# Eval("Id") %>&<%= GetBackUrlEncoded() %>" target="_blank" class='btn btn-default btn-xs active-btn' data-toggle="tooltip" title="影片列表"><i class="fa fa-list"></i></a>
                                                <a class='btn btn-default btn-xs active-btn' href="Edit.aspx?collectionid=<%# Eval("Id") %>&<%= GetBackUrlEncoded() %>" data-toggle="tooltip" title="编辑"><i class="fa fa-pencil"></i></a>
                                                <a class='btn btn-default btn-xs active-btn btnDelete' data-id="<%# Eval("Id") %>" data-toggle="tooltip" title="删除"><i class="fa fa-remove"></i></a>
                                            </td>
                                            <td class="text-left cover" title="<%# Eval("Name") %>"><a href="<%# WebRoutes.GetRoute("Collection.Subject",Eval("Id")) %>" target="_blank"><img src="<%# ((UFilm.Domain.Media.Picture)Eval("Cover")).ThumbUrl %>"  /></a> <a href="<%# WebRoutes.GetRoute("Collection.Subject",Eval("Id")) %>" target="_blank" title="<%# Eval("Name") %>"><%# Eval("Name") %></a>  </td>
                                            <td class="text-center"><%# Eval("Introduction").ToString().GetSubString(0,255,"..") %> </td>
                                            <td class="text-center"><%# Eval("Tags") %></td>
                                            <td class="text-center"><%# Eval("Count") %> </td>
                                            <td class="text-center"><%# Eval("CreationTime","{0:yyyy-MM-dd HH:mm}") %></td>
                                        </tr>
                                    </tbody>
                                </ItemTemplate>
                            </asp:Repeater>
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
        //delete
        $('.btnDelete').click(function () {
            var id = parseInt($(this).data('id'));
            if (confirm('你确认删除吗？')) {
                CollectionService.Delete(id, function (res) {
                    if (res.value.Success) {
                        notify.success('删除成功');
                        redirectService.refresh(1000);
                    }
                });

            }
        });
    </script>
</asp:Content>
