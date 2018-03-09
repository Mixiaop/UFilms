<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="UFilm.Console.Spiders.Douban.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <form runat="server">
        <!-- Page Header -->
        <div class="content bg-gray-lighter">
            <div class="row items-push">
                <div class="col-sm-7">
                    <h1 class="page-heading">豆瓣采集测试 <small>针对电影及影人页的测试</small>
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
                                    <asp:TextBox runat="server" ID="tbLinks" CssClass="form-control"></asp:TextBox>
                                    <label class="control-label">链接</label>
                                    <div class="help-block">如：电影或影人页的链接，格式必须不包含请求参数（例：https://movie.douban.com/subject/11598977/）</div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12">
                                <asp:Button runat="server" ID="btnMovieTests" CssClass="btn btn-primary" Text="电影测试" OnClientClick="return confirm('你确认电影链接对的吗？')" />
                                <asp:Button runat="server" ID="btnFilmManTests" CssClass="btn btn-primary" Text="影人测试" OnClientClick="return confirm('你确认影人链接对的吗？')" />
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
