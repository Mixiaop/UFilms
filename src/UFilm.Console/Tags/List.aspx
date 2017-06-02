<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="UFilm.Console.Tags.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
     <div class="col-sm-12 col-lg-12">
        <form runat="server">
            <!-- Page Header -->
            <div class="content bg-gray-lighter">
                <div class="row items-push">
                    <div class="col-sm-7">
                        <h1 class="page-heading">标签列表 <small></small>
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
                                <select class="form-control">
                                    <option value="-1">类型</option>
                                </select>
                                <input type="text" id="tbSeachKeywords" class="form-control" placeholder="名称" />
                                <label class="col-xs-pull-1">
                                    <button type="button" class="btn btn-primary btn-sm" id="btnSearch">搜索</button>
                                    <%--<a href="javascript:;" class="btn btn-primary btn-sm btnAdd">添加</a>--%>
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
                                    <th width="20%"></th>
                                    <th class="text-center" width="20%">影片</th>
                                    <th class="text-center">副标题</th>
                                    <th class="text-center" width="10%">排序</th>
                                    <th class="text-center" width="10%">创建时间</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%--<% foreach (var info in Model.Results.Items)
                                   { %>
                                <tr>
                                    <td class="text-center">
                                        <a class='btn btn-default btn-xs active-btn btnEdit' data-id="<%= info.Id %>" href="javascript:;" data-toggle="tooltip" title="编辑"><i class="fa fa-pencil"></i></a>
                                        <a class='btn btn-default btn-xs active-btn btnDelete' data-id="<%= info.Id %>" data-toggle="tooltip" title="删除"><i class="fa fa-remove"></i></a>
                                    </td>
                                    <td class="text-center cover" title="><%= info.Movie.CnName %>"><a href="<%= WebRoutes.GetRoute("Movies.Subject", info.MovieId) %>" target="_blank">
                                        <img src="<%= info.Movie.Cover.ThumbUrl %>" /></a> <a href="<%= WebRoutes.GetRoute("Movies.Subject", info.MovieId) %>" target="_blank" title="<%= info.Movie.CnName%>"><%= info.Movie.CnName%></a>  </td>
                                    <td class="text-center"><%= info.Title%> </td>
                                    <td class="text-center"><%= info.Order %></td>
                                    <td class="text-center"><%= info.CreationTime.ToString("yyyy-MM-dd HH:mm") %></td>
                                </tr>
                                <%} %>--%>
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
        </form>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
