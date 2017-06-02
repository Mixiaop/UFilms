<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="UFilm.Console.Spiders.TaskLogs.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="col-sm-12 col-lg-12">
        <form runat="server">
            <!-- Page Header -->
            <div class="content bg-gray-lighter">
                <div class="row items-push">
                    <div class="col-sm-7">
                        <h1 class="page-heading"><%= Model.Task.Name %> <small>的任务日志列表</small></h1>
                        <div>链接：<%= Model.Task.Links %></div>
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
                                    <a href="List.aspx" target="_blank" class="btn btn-default btn-sm">弹出页</a>
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
                                      <th class="text-center"></th>
                                    <th class="text-center" width="20%">任务</th>
                                    <th class="text-center" >日志</th>
                                    <th class="text-center" width="10%">时间</th>
                                </tr>
                            </thead>
                            <asp:Repeater runat="server" ID="rptDatas">
                                <ItemTemplate>
                                    <tbody>
                                        <tr>
                                            <td class="text-center">
                                                <a href="<%# WebRoutes.GetRoute("Movies.Subject",(((UFilm.Domain.Spiders.SpiderTask)Eval("Task"))!=null?((UFilm.Domain.Spiders.SpiderTask)Eval("Task")).ObjectId:0))  %>" target="_blank" class='btn btn-default btn-xs active-btn '  data-toggle="tooltip" title="查看电影"><i class="fa fa-camera"></i></a>
                                            </td>
                                            <td class="text-center"><%# ((UFilm.Domain.Spiders.SpiderTask)Eval("Task"))!=null?((UFilm.Domain.Spiders.SpiderTask)Eval("Task")).Name:"" %></td>
                                            <td class="text-left"><%# Eval("Message") %></td>
                                            <td class="text-center"><%# Eval("CreationTime","{0:yyyy-MM-dd HH:mm}") %></td>
                                        </tr>
                                    </tbody>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <div class="text-center">
                        <ul class="pagination">
                             <%= PagerHtml %>
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
