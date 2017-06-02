<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="AdminList.aspx.cs" Inherits="UZeroConsole.Web.UZero.AdminList" %>
<%@ Import Namespace="UZeroConsole.Services.Dto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <form runat="server">
        <!-- Page Header -->
        <div class="content bg-gray-lighter">
            <div class="row items-push">
                <div class="col-sm-7">
                    <h1 class="page-heading">管理员列表<small></small>
                    </h1>
                </div>
                <div class="col-sm-5 text-right hidden-xs">
                </div>
            </div>
        </div>
        <!-- END Page Header -->
        <!-- Page Content -->
        <div class="content">
            <div class="row items-push">
                <div class="col-xs-12">
                    <div class="form-inline">
                        <div class="form-group ">
                            <label>
                                <asp:DropDownList ID="ddlRole" runat="server" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList></label>
                            <label>
                                <asp:TextBox runat="server" ID="tbSearchKeywords" placeholder="输入名称或关键字" CssClass="form-control"></asp:TextBox></label>
                            <label class="col-xs-pull-1">
                                <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="搜索"></asp:Button>
                                &nbsp;
                            <button type="button" class="btn btn-primary" onclick="window.location.href='AdminCreate.aspx'"><i class="fa fa-plus"></i>添加管理员</button>
                            </label>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Dynamic Table Full -->
            <div class="block">
                <div class="block-content">
                    <asp:Literal runat="server" ID="ltlMessage"></asp:Literal>
                    <!-- DataTables init on table by adding .js-dataTable-full class, functionality initialized in js/pages/base_tables_datatables.js -->
                    <table class="table table-hover js-dataTable-full">
                        <thead>
                            <tr>
                                <th class="text-center">操作</th>
                                <th class="text-center">用户名</th>
                                <th class="text-center">姓名（昵称）</th>
                                <th class="text-center">角色</th>
                                <th class="text-center">最后一次登录</th>
                                <th class="text-center">创建时间</th>
                            </tr>
                        </thead>
                        <asp:Repeater runat="server" ID="rptDatas">
                            <ItemTemplate>
                                <tbody>
                                    <tr>
                                        <td class="text-center">
                                            <asp:LinkButton runat="server" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-xs btn-default" data-toggle="tooltip" title="重置密码" OnClientClick="return confirm('你确定要重置密码吗?');" OnClick="btnResetPassword"><i class="fa fa-key"></i></asp:LinkButton>
                                            <a class='btn btn-default btn-xs active-btn' href="AdminUpdate.aspx?adminId=<%# Eval("Id") %>" data-toggle="tooltip" title="编辑"><i class="fa fa-pencil"></i></a>
                                            <asp:LinkButton runat="server" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-xs btn-default" data-toggle="tooltip" title="删除" OnClientClick="return confirm('你确定要删除吗?');" OnClick="btnDelete_Click"><i class="fa fa-close"></i></asp:LinkButton>

                                        </td>
                                        <td class="text-center"><%# Eval("Username") %></td>
                                        <td class="text-center"><%#Eval("Name")!=null?Eval("Name"):"-" %></td>
                                        <td class="text-center"><%# ((RoleDto)Eval("Role"))!=null?((RoleDto)Eval("Role")).Name:"-" %></td>
                                        <td class="text-center"><%# ((DateTime?)Eval("LastLoginTime")).HasValue?((DateTime?)Eval("LastLoginTime")).Value.ToString("yyyy-MM-dd HH:mm"):"-" %></td>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
