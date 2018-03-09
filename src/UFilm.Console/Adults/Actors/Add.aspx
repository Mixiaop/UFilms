<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="UFilm.Console.Adults.Actors.Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <form runat="server">
        <!-- Page Header -->
        <div class="content bg-gray-lighter">
            <div class="row items-push">
                <div class="col-sm-7">
                    <h1 class="page-heading">添加影人 <small></small>
                    </h1>
                </div>
                <div class="col-sm-5 text-right hidden-xs">
                    <ol class="breadcrumb push-10-t">
                        <li><a class="link-effect" href="<%= GetBackUrlDecoded("List.aspx") %>">所有影人</a></li>
                        <li>添加影人</li>
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
                                            <asp:TextBox runat="server" ID="tbCnName" CssClass="form-control"></asp:TextBox>
                                            <label>中文名</label>
                                        </div>
                                        <div class="help-block">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbEnName" CssClass="form-control"></asp:TextBox>
                                            <label>外文名</label>
                                        </div>
                                        <div class="help-block">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbPinyin" CssClass="form-control"></asp:TextBox>
                                            <label>拼音</label>
                                        </div>
                                        <div class="help-block">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:DropDownList runat="server" ID="ddlSex" CssClass="form-control">
                                                <asp:ListItem Value="-1" Text="选择"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="男"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="女"></asp:ListItem>
                                            </asp:DropDownList>
                                            <label>姓别</label>
                                        </div>
                                        <div class="help-block">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbConstellation" CssClass="form-control"></asp:TextBox>
                                            <label>星座</label>
                                        </div>
                                        <div class="help-block">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbBirthday" CssClass="form-control"></asp:TextBox>
                                            <label>出生日期</label>
                                        </div>
                                        <div class="help-block">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbPlaceOfBirth" CssClass="form-control"></asp:TextBox>
                                            <label>出生地</label>
                                        </div>
                                        <div class="help-block">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox ID="tbIntroduction" CssClass="kindeditor form-control" TextMode="MultiLine" Rows="15" runat="server"></asp:TextBox>
                                            <label>个人简介</label>
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
                                            <label>头像</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbJob" CssClass="form-control"></asp:TextBox>
                                            <label>职业</label>
                                        </div>
                                        <div class="help-block">
                                            逗号“,”分割，如：导演、女优
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbMoreCnName" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                            <label>更多中文名</label>
                                        </div>
                                        <div class="help-block">
                                        </div>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbMoreEnName" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                            <label>更多外文名</label>
                                        </div>
                                        <div class="help-block">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbWebSite" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                            <label>个人网站</label>
                                        </div>
                                        <div class="help-block">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary" Text="提交" />
                                        <a href="<%= GetBackUrlDecoded("List.aspx") %>" class="btn btn-default" >返回</a>

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
    <link href="/editors/kindeditor/css/default.css" rel="stylesheet" />
    <link href="/assets/js/plugins/jquery.fineuploader/fineuploader.css" rel="stylesheet" />
    <script src="/editors/kindeditor/kindeditor.js"></script>
    <script src="/assets/js/plugins/jquery.fineuploader/jquery.fineuploader-3.4.1.min.js"></script>
    <script>
        var arrEditor = new Array();
        var editor;
        $('.kindeditor').each(function () {
            editor = KindEditor.create('#' + $(this).attr('id'), {
                width: '100%',
                items: ['source', '|',
                        'plainpaste', 'wordpaste', '|', 'justifyleft', 'justifycenter', 'justifyright',
                        'justifyfull', 'insertorderedlist', 'insertunorderedlist', 'clearhtml', '|', 'table', 'hr', 'fullscreen', '/',
                        'formatblock', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold',
                        'italic', 'underline', 'strikethrough', '|', 'image', 'multiimage',
                       'insertfile', 'link', 'unlink'],
                uploadJson: '/editors/kindeditor/handler/upload_json.ashx'
            });
            arrEditor[arrEditor.length] = editor;
        });

        $('#<%= btnSave.ClientID%>').on('click', function () {
                if (arrEditor.length != 0) {
                    for (var i = 0; i < arrEditor.length; i++) {
                        arrEditor[i].sync();
                    }
                }
                return true;
        });

        //avatar
        $('#uploadPicture').fineUploader({
            request: { endpoint: "/AjaxServices/jQueryFineUploader/UploadPicture.ashx?size=280" },
            multiple: false,
            text: {
                uploadButton: '上传头像'
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
