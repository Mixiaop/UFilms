<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="UFilm.Console.Collection.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
     <form runat="server">
        <!-- Page Header -->
        <div class="content bg-gray-lighter">
            <div class="row items-push">
                <div class="col-sm-7">
                    <h1 class="page-heading">添加精选集 <small></small>
                    </h1>
                </div>
                <div class="col-sm-5 text-right hidden-xs">
                    <ol class="breadcrumb push-10-t">
                        <li><a class="link-effect" href="<%= GetBackUrlDecoded() %>">返回列表</a></li>
                        <li>编辑精选集</li>
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
                                            <asp:TextBox runat="server" ID="tbName" CssClass="form-control"></asp:TextBox>
                                            <label>名称</label>
                                        </div>
                                        <div class="help-block">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox ID="tbCount" CssClass="form-control" runat="server"></asp:TextBox>
                                            <label>影片数</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox ID="tbIntroduction" CssClass="editor form-control" TextMode="MultiLine" Rows="15" runat="server"></asp:TextBox>
                                            <label>简介</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbTags" CssClass="form-control"></asp:TextBox>
                                            <label>标签</label>
                                        </div>
                                        <div class="help-block">
                                            按“回车”添加新标签
                                        </div>
                                        <% if (Model.Tags.Count > 0)
                                           { %>
                                        <div class="tags">
                                            <% foreach (var tag in Model.Tags)
                                               { %>
                                            <a href="javascript:;"><%= tag.Name %></a> <%} %>
                                        </div>
                                        <%} %>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-horizontal push-10-t">
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                           <div id="fuCover"></div>
                                            <div id="fuCoverPreview"></div>
                                            <input type="hidden" id="hidCoverUrl" name="hidCoverUrl" runat="server" />
                                            <input type="hidden" id="hidCoverId" name="hideCoverId" runat="server" />
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
    <link href="/assets/js/plugins/jquery-tags-input/jquery.tagsinput.css" rel="stylesheet" />
    <script src="/assets/js/plugins/jquery.fineuploader/jquery.fineuploader.js"></script>
    <script src="/assets/js/plugins/jquery-tags-input/jquery.tagsinput.js"></script>
    <script>
        var $fuCover = $('#fuCover'),
            $fuCoverPreview = $('#fuCoverPreview'),
            $hidCoverId = $('#<%= hidCoverId.ClientID%>'),
            $hidCoverUrl = $('#<%= hidCoverUrl.ClientID%>');

        if ($hidCoverUrl.val() != '')
            $fuCoverPreview.html('<img src="' + $hidCoverUrl.val() + '" />');

        $fuCover.fineUploader({
            request: { endpoint: "/AjaxServices/jQueryFineUploader/UploadPicture.ashx?size=450" },
            multiple: false,
            text: {
                uploadButton: '上传封面'
            }
        }).on('complete',
           function (event, id, fileName, responseJSON) {
               if (responseJSON.success) {
                   $fuCoverPreview.html('');
                   $fuCoverPreview.html('<img src="' + responseJSON.showthumb + '" />');
                   $hidCoverId.val(responseJSON.pictureId);
                   $hidCoverUrl.val(responseJSON.showthumb);
               }
           });

        //tags
        var $tags = $('.tags');
        var $tbTag = $('#<%= tbTags.ClientID %>');

        $tbTag.tagsInput({
            height: '36px',
            width: '100%',
            defaultText: '添加标签',
            removeWithBackspace: true,
            delimiter: [',']
        });

        $tags.find('a').on('click', function () {
            var val = $tbTag.val();
            var tag = $(this).text();

            if (val == '') {
                $tbTag.addTag(tag);
            } else {
                if (val.indexOf(tag) == -1) {
                    $tbTag.addTag(tag);
                } else {
                    $tbTag.removeTag(tag);
                }
            }
        });
    </script>
</asp:Content>
