<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="UFilm.Console.Jobs.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <form runat="server">
        <!-- Page Header -->
        <div class="content bg-gray-lighter">
            <div class="row items-push">
                <div class="col-sm-7">
                    <h1 class="page-heading">后台作业管理列表 <small>所有的后台作业的管理，类型包含：定时、重复</small>
                    </h1>
                </div>
            </div>
            <div class="alert alert-danger">
                注意开启任务必须请在域名【jobs.mbjuan.com】开启
            </div>
        </div>
        <!-- END Page Header -->
        <!-- Page Content -->
        <div class="content">
            <!-- Dynamic Table Full -->
            <div class="block">
                <div class="block-content table-responsive">
                    <asp:Literal runat="server" ID="ltlMessage"></asp:Literal>
                    <table class="table table-hover js-dataTable-full table-bordered">
                        <thead>
                            <tr>
                                <th class="text-center" width="20%">操作</th>
                                <th class="text-center" width="15%">作业名称</th>
                                <th class="text-center" width="10%">作业类型</th>
                                <th class="text-center">描述</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="text-center">
                                    <asp:Button ID="btnDoubanSpiderNow" CssClass="btn btn-default" runat="server" Text="立即运行" OnClientClick="return confirm('您确认立即运行此作业吗？');"></asp:Button>
                                </td>
                                <td class="text-center">豆瓣采集任务</td>
                                <td class="text-center">
                                    <label class="label label-primary">立即运行</label></td>
                                <td class="text-center">根据链接采集豆瓣的电影</td>
                            </tr>
                            <tr>
                                <td class="text-center">
                                    <asp:Button ID="btnKeepAliveDomainJob" CssClass="btn btn-success" runat="server" Text="运行" OnClientClick="return confirm('您确认运行此作业吗？');"></asp:Button>
                                    <asp:Button ID="btnKeepAliveDomainJobRunNow" CssClass="btn btn-default" runat="server" Text="立即运行" OnClientClick="return confirm('您确认立即运行此作业吗？');"></asp:Button>
                                </td>
                                <td class="text-center">保持活动</td>
                                <td class="text-center">
                                    <label class="label label-primary">每5分钟</label></td>
                                <td class="text-center">定时去访问网站应用，让IIS保持活动。（解决IIS默认5-20分钟内没人访问的情况下会进入假死状态）详细查看配置文件</td>
                            </tr>
                            <tr>
                                <td class="text-center">
                                    <asp:Button ID="btnSpiderNH87CN" CssClass="btn btn-default" runat="server" Text="立即运行" OnClientClick="return confirm('您确认立即运行此作业吗？');"></asp:Button>
                                </td>
                                <td class="text-center">男人团采集任务</td>
                                <td class="text-center">
                                    <label class="label label-primary">立即运行</label></td>
                                <td class="text-center">根据数据库所有影人的作品主页（目前500）。（遍历所有的作品，有新作品则添加到数据库）</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="text-center">
                    <ul class="pagination">
                        <%--<%= PagerHtml %>--%>
                    </ul>
                </div>
            </div>
            <!-- END Dynamic Table Full -->
        </div>
        <!-- END Page Content -->
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
