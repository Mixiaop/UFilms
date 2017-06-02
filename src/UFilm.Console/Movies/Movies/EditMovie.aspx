<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="EditMovie.aspx.cs" Inherits="UFilm.Console.Movies.Movies.EditMovie" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <form runat="server">
        <!-- Page Header -->
        <div class="content bg-gray-lighter">
            <div class="row items-push">
                <div class="col-sm-7">
                    <h1 class="page-heading">编辑电影 <small><%= Model.Movie.ToString() %></small>
                    </h1>
                </div>
                <div class="col-sm-5 text-right hidden-xs">
                    <ol class="breadcrumb push-10-t">
                        <li><a class="link-effect" href="<%= GetBackUrlDecoded() %>">所有电影</a></li>
                        <li>编辑电影</li>
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
                        <div class="col-lg-8">
                    <div class="form-horizontal push-10-t">
                        <div class="form-group">
                            <div class="col-xs-12">
                                <div class="form-material">
                                    <asp:TextBox runat="server" ID="tbMoreEnName" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    <label>又名</label>
                                </div>
                                 <div class="help-block">
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                         <div class="col-xs-12">
                                <div class="form-material">
                                    <asp:TextBox runat="server" ID="tbArea" CssClass="form-control"></asp:TextBox>
                                    <label>制片国家/地区</label>
                                </div>
                                 <div class="help-block">
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12">
                                <div class="form-material">
                                    <asp:TextBox runat="server" ID="tbYear" CssClass="form-control"></asp:TextBox>
                                    <label>年份</label>
                                </div>
                                 <div class="help-block">
                                    如 2007
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12">
                                <div class="form-material">
                                    <asp:TextBox runat="server" ID="tbOtherPostYear" CssClass="form-control"></asp:TextBox>
                                    <label>上映日期</label>
                                </div>
                                 <div class="help-block">
                                    如 2010-09-12(中国大陆)
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12">
                                <div class="form-material">
                                    <asp:TextBox runat="server" ID="tbLength" CssClass="form-control"></asp:TextBox>
                                    <label>片长</label>
                                </div>
                                 <div class="help-block">
                                   如 116分钟(韩国) 122分钟(导演剪辑版)
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-xs-12">
                                <div class="form-material">
                                    <asp:TextBox ID="tbIntroduction" CssClass="editor form-control" TextMode="MultiLine" Rows="15" runat="server"></asp:TextBox>
                                    <label>剧情简介</label>
                                </div>
                            </div>
                        </div>
                        
                    </div>
                            </div>
                        <div class="col-lg-4">
                            <div class="form-horizontal push-10-t">
                                <div class="form-group">
                            <div class="col-xs-12">
                                <div class="form-material">
                                    <div id="uploadPicture"></div>
                                    <div id="thumbPicture"></div>
                                    <asp:HiddenField runat="server" ID="hfPictureId" />
                                    <asp:HiddenField runat="server" ID="hfPictureThumb" />
                                    <label>封面</label>
                                </div>
                            </div>
                        </div>
                                <div class="form-group">
                            <div class="col-xs-12">
                                <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary" Text="保存" />

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
    <link href="/assets/js/plugins/jquery.fineuploader/fineuploader.css" rel="stylesheet" />
    <script src="/assets/js/plugins/jquery.fineuploader/jquery.fineuploader-3.4.1.min.js"></script>
    <script>
        $('#thumbPicture').html('<img src="' + $('#<%= hfPictureThumb.ClientID%>').val() + '" />');
        $('#uploadPicture').fineUploader({
            request: { endpoint: "/AjaxServices/jQueryFineUploader/UploadPicture.ashx?size=280" },
            multiple: false,
            text: {
                uploadButton: '上传封面'
            }
        }).on('complete',
            function (event, id, fileName, responseJSON) {
                if (responseJSON.success) {
                    var thumbBox = $("#thumbPicture");
                    thumbBox.html('');
                    thumbBox.append('<img src="' + responseJSON.showthumb + '" >');
                    $("#<%= hfPictureId.ClientID %>").val(responseJSON.pictureId);
                   $("#<%= hfPictureThumb.ClientID %>").val(responseJSON.thumb);

                } else {
                    alert(responseJSON.error);
                }
            });
    </script>
</asp:Content>
