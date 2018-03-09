<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="UFilm.Console.Movies.MovieTorrents.Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <form runat="server">
        <!-- Page Header -->
        <div class="content bg-gray-lighter">
            <div class="row items-push">
                <div class="col-sm-7">
                    <h1 class="page-heading">分享资源 <small><%= Model.Movie.ToString() %> </small>
                    </h1>
                </div>
                <div class="col-sm-5 text-right hidden-xs">
                    <ol class="breadcrumb push-10-t">
                        <li><a class="link-effect" href="List.aspx?movieid=<%= Model.Movie.Id %>">返回列表</a></li>
                        <li>分享资源</li>
                    </ol>
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
                        <div class="col-lg-12">
                            <div class="form-horizontal push-10-t">
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <select id="ddlFormat" class="form-control">
                                                <option value="">默认</option>
                                                <option value="1080P">1080P高清</option>
                                                <option value="720P">720P高清</option>
                                                <option value="BD1280MKV">BD1280MKV</option>
                                                <option value="BD1024MKV">BD1024MKV</option>
                                                <option value="BD1280">BD1280</option>
                                                <option value="BD1024">BD1024</option>
                                                <option value="HD1024">HD1024</option>
                                                <option value="HD960">HD960</option>
                                                <option value="DVD-RMVB">DVD-RMVB</option>
                                                <option value="DVD-MP4">DVD-RMVB</option>
                                                <option value="MKV">MKV</option>
                                                <option value="RMVB">RMVB</option>
                                                <option value="AVI">AVI</option>
                                                <option value="MP4">MP4</option>
                                            </select>
                                            <label>格式</label>
                                        </div>
                                        <div class="help-block">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbName"  CssClass="form-control"></asp:TextBox>
                                            <label>名称</label>
                                        </div>
                                        <div class="help-block">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:DropDownList runat="server" ID="ddlType" CssClass="form-control">
                                                <asp:ListItem Value="">默认</asp:ListItem>
                                                <asp:ListItem Value="磁力链接"></asp:ListItem>
                                                <asp:ListItem Value="电驴链接"></asp:ListItem>
                                                <asp:ListItem Value="迅雷链接"></asp:ListItem>
                                                <asp:ListItem Value="迅雷快传"></asp:ListItem>
                                                <asp:ListItem Value="种子文件"></asp:ListItem>
                                                <asp:ListItem Value="字幕文件"></asp:ListItem>
                                                <asp:ListItem Value="FTP链接"></asp:ListItem>
                                            </asp:DropDownList>
                                            <label>类型</label>
                                        </div>
                                        <div class="help-block">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbSize" CssClass="form-control"></asp:TextBox>
                                            <label>资源大小</label>
                                        </div>
                                        <div class="help-block">
                                            如 690MB
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbLink" CssClass="form-control"></asp:TextBox>
                                            <label>链接</label>
                                        </div>
                                        <div class="help-block">
                                            如 磁力、电驴、迅雷
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group hide">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <label>资源文件</label>
                                        </div>
                                        <div class="help-block">
                                            如 .torrent 等资源下载
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary" Text="提交" />

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
    <script>
        var $ddlFormat = $('#ddlFormat');
        var $tbName = $('#<%=tbName.ClientID%>');
        var movieCnName = '<%=Model.Movie.CnName%>';
        $ddlFormat.change(function () {
            $tbName.val(movieCnName + " " + $(this).val());
        });
    </script>
</asp:Content>
