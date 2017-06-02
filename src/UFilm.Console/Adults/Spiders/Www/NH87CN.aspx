<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="NH87CN.aspx.cs" Inherits="UFilm.Console.Adults.Spiders.Www.NH87CN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <form runat="server">
        <div class="container">
            <asp:Button runat="server" ID="btnSpiderListLinks" Text="采集所有影人的主页链接"  />
            <asp:Button runat="server" ID="btnSpiderMovies" Text="测试任务"  />
            <%--<asp:Button runat="server" ID="btnRun" Text="运行任务" />--%>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
