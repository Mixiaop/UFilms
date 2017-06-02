<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="MovieLinkToTask.aspx.cs" Inherits="UFilm.Console.Spiders.Douban.MovieLinkToTask" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <form runat="server">
        <!-- Page Header -->
        <div class="content bg-gray-lighter">
            <div class="row items-push">
                <div class="col-sm-7">
                    <h1 class="page-heading">导入豆瓣电影链接到任务 <small>从豆瓣链接库导入新的任务链接（未使用的链接数：<%=Model.Count %>）</small>
                    </h1>
                </div>
                <div class="col-sm-5 text-right hidden-xs">
                  
                </div>
            </div>
        </div>
        <!-- END Page Header -->
        <!-- Page Content -->
        <div class="content">
            <!-- Dynamic Table Full -->
            <div class="block">
                <div class="block-content block-content-narrow">
                    <asp:Literal runat="server" ID="ltlMessage"></asp:Literal>
                    <div class="row">
                        <div class="col-lg-8">
                    <div class="form-horizontal push-10-t">
                         <div class="form-group">
                            <div class="col-xs-12">
                                <div class="form-material">
                                    <asp:TextBox runat="server" ID="tbCount" CssClass="form-control"></asp:TextBox>
                                    <label class="control-label">多少条</label>
                                    <div class="help-block">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12">
                                <asp:Button runat="server" Text="确定" CssClass="btn btn-primary" ID="btnCommit" />
                            </div>
                        </div>
                              </div>

                        </div>
                        </div>
                </div>
            </div>
            <!-- END Dynamic Table Full -->
        </div>
        <!-- END Page Content -->
        </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
