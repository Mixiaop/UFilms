<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="UZeroConsole.Web.UZero.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
     <!-- Page Header -->
                <div class="content bg-gray-lighter">
                    <div class="row items-push">
                        <div class="col-sm-7">
                            <h1 class="page-heading">
                                工作台 <small>Build Products better，together</small>
                            </h1>
                        </div>
                    </div>
                </div>
                <!-- END Page Header -->
                <!-- Page Content -->
                            <div class="content" style="padding-bottom:50px;">
                                欢迎使用【<%= ConsoleSettings.Title %>】管理后台，您可以点击左侧（顶部）菜单开始管理及操作。
                   
                </div>

                <!-- END Page Content -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
