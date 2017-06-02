<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="UZeroLogList.aspx.cs" Inherits="UZeroConsole.Web.UZero.UZeroLogList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <form runat="server">
        <!-- Page Header -->
        <div class="content bg-gray-lighter">
            <div class="row items-push">
                <div class="col-sm-7">
                    <h1 class="page-heading">UZeroLog 日志列表 <small></small>
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
                        <div class="form-group ">
                            <label>
                                <asp:DropDownList runat="server" ID="ddlModules" CssClass="form-control"></asp:DropDownList>
                                <asp:DropDownList runat="server" ID="ddlOperateType" CssClass="form-control">
                                    <asp:ListItem Text="操作类型" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="Insert" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="Update" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="Delete" Value="40"></asp:ListItem>
                                    <asp:ListItem Text="Query" Value="30"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList runat="server" ID="ddlIsException" CssClass="form-control">
                                    <asp:ListItem Text="日志类型" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="操作" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="异常" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                <asp:TextBox runat="server" ID="tbKeywords" placeholder="操作者、关键字" Width="300px" CssClass="form-control"></asp:TextBox>
                            </label>
                            <label class="col-xs-pull-1">
                                <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="搜索" OnClick="btnSearch_Click"></asp:Button>
                            </label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="block">
                <div class="block-content table-responsive">
                    <asp:Literal runat="server" ID="ltlMessage"></asp:Literal>
                    <table class="table table-hover js-dataTable-full">
                        <thead>
                            <tr>
                                <th class="text-center" style="width: 8%"></th>
                                <th class="text-center" style="width: 8%">操作者</th>
                                <th class="text-center" style="width: 8%">操作类型</th>
                                <th class="text-center" >应用模块</th>
                                <th class="text-center">内容</th>
                                <th class="text-center">IP</th>
                                <th class="text-center">URL</th>
                                <th class="text-center">时间</th>
                            </tr>
                        </thead>
                        <asp:Repeater runat="server" ID="rptDatas">
                            <ItemTemplate>
                                <tbody>
                                    <tr>
                                        <td class="text-center">
                                            <a class="btn btn-xs btn-default"  data-toggle="tooltip" title="" data-original-title="详情"><i class="fa fa-search"></i></a>
                                        </td>
                                        <td class="text-center"><%# Eval("Operator") %></td>
                                        <td class="text-center"><%# GetOperateTypeAlias((int)Eval("OperateTypeId")) %></td>
                                        <td class="text-center"><%# GetModuleAlias(Eval("App").ToString(), Eval("ModuleName").ToString()) %></td>
                                        <td class="text-center"><%# Eval("ShortMessage") %></td>
                                        <td class="text-center"><%# Eval("IpAddress") %></td>
                                        <td class="text-center"><%# Eval("Url") %></td>
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
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
