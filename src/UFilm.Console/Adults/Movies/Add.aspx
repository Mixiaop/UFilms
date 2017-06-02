<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="UFilm.Console.Adults.Movies.Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <form runat="server" >
        <!-- Page Header -->
        <div class="content bg-gray-lighter">
            <div class="row items-push">
                <div class="col-sm-7">
                    <h1 class="page-heading">添加电影 <small></small>
                    </h1>
                </div>
                <div class="col-sm-5 text-right hidden-xs">
                    <ol class="breadcrumb push-10-t">
                        <li><a class="link-effect" href="<%= GetBackUrlDecoded("List.aspx") %>">所有电影</a></li>
                        <li>添加电影</li>
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
                                            <asp:TextBox runat="server" ID="tbCode" CssClass="form-control"></asp:TextBox>
                                            <label>番号</label>
                                        </div>
                                        <div class="help-block">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbCnName" CssClass="form-control"></asp:TextBox>
                                            <label>标题</label>
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
                                            如 2016
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
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbMovieType" CssClass="form-control"></asp:TextBox>
                                            <label>类型</label>
                                        </div>
                                        <div class="help-block">
                                            如 动作,爱情
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbMovieLength" CssClass="form-control"></asp:TextBox>
                                            <label>片长</label>
                                        </div>
                                        <div class="help-block">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbArea" CssClass="form-control"></asp:TextBox>
                                            <label>地区</label>
                                        </div>
                                        <div class="help-block">
                                            如 美国,日本
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbLanguage" CssClass="form-control"></asp:TextBox>
                                            <label>语言</label>
                                        </div>
                                        <div class="help-block">
                                            如 日语 英语
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox ID="tbIntroduction" CssClass="kindeditor form-control" TextMode="MultiLine" Rows="15" runat="server"></asp:TextBox>
                                            <label>简介</label>
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
                                    <div class="col-xs-12" id="actorsWrapper">
                                        <asp:HiddenField runat="server" ID="hfActors" />
                                        <div class="form-material">
                                            <input type="text" class="form-control actor" />
                                            <label>演员 <a href="../Actors/Add.aspx" target="_blank" data-toggle="tooltip" title="不存在演员，去添加" class='btn btn-default btn-xs active-btn'><i class="fa fa-plus"></i></a></label>
                                        </div>
                                        <div class="help-block">“演员”联想出来并选择后才会添加成功</div>
                                        <div class="form-material" id="actorItems">
                                            <%--<div class="radio">
                                                <label>
                                                    松 <a href="javascript:;" id="btnAddActorItem" data-toggle="tooltip" title="移除" class='btn btn-default btn-xs active-btn'><i class="fa fa-remove"></i></a>
                                                </label>
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbOtherEnName" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                            <label>其他外文名</label>
                                        </div>
                                        <div class="help-block">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbHits" Text="0" CssClass="form-control"></asp:TextBox>
                                            <label>热度</label>
                                        </div>
                                        <div class="help-block">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="form-material">
                                            <asp:TextBox runat="server" ID="tbPhotoCount" Text="0" CssClass="form-control"></asp:TextBox>
                                            <label>图片数量</label>
                                        </div>
                                        <div class="help-block">
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary" Text="提交" />
                                        <a href="<%= GetBackUrlDecoded("List.aspx") %>" class="btn btn-default">返回</a>

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
    <link href="/assets/js/plugins/jquery-auto-complete/jquery.auto-complete.css" rel="stylesheet" />
    <script src="/editors/kindeditor/kindeditor.js"></script>
    <script src="/assets/js/plugins/jquery.fineuploader/jquery.fineuploader-3.4.1.min.js"></script>
    <script src="/assets/js/plugins/jquery-auto-complete/jquery.auto-complete.js"></script>
    <script>
        $(document).keydown(function () {
            if (event.keyCode == 13) {
                return false;
            }
        });
        //==========上传封面
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

            //==========演员信息
            var $actorsWrapper = $('#actorsWrapper');
            var $btnAddActor = $('#btnAddActorItem');
            var $hfActors = $('#<%= hfActors.ClientID %>');
            var $actorItems = $('#actorItems');
            var $tbActor = $actorsWrapper.find('input.actor');
            var actors = []; //ex:{Id:1,Name:'123'}

            var addActor = function (id, name) {
                var exists = false;
                for (var i = 0; i < actors.length; i++) {
                    if (actors[i].Id == id) {
                        exists = true;
                        break;
                    }
                }
                if (!exists) {
                    actors.push({ Id: id, Name: name });
                }

                $renderActorItems();
            }

            var removeActor = function (id) {
                var tempActors = actors;
                actors = [];
                for (var i = 0; i < tempActors.length; i++) {
                    if (tempActors[i].Id != id)
                        actors.push(tempActors[i]);
                }
                $renderActorItems();
            }

            var $renderActorItems = function () {
                $actorItems.html('');
                for (var i = 0; i < actors.length; i++) {
                    var actor = actors[i];
                    var $html = '<div class="radio"><label>';
                    $html += actor.Name + ' <a href="javascript:;" onclick="removeActor(' + actor.Id + ')" data-toggle="tooltip" title="移除" class="btn btn-default btn-xs active-btn"><i class="fa fa-remove"></i></a>';
                    $html += '</label></div>';
                    $actorItems.append($html);
                }

            }

            var $initActorsEvents = function () {
                $actorsWrapper.find('input.actor').autoComplete(
                    {
                        minChars: 1,
                        source: function (term, response) {
                            term = term.toLowerCase();
                            term = $.trim(term);
                            AdultsService.SearchActors(term, 10, function (res) {
                                var json = res.value;
                                if (json.Success) {
                                    response(json.Result);
                                }
                            });
                        },
                        renderItem: function (item, search) {
                            var name = item.CnName;
                            return '<div class="autocomplete-suggestion" data-actorid="' + item.Id + '"  data-val="' + name + '">' + name + '</div>';
                        },
                        onSelect: function (e, term, item) {
                            var id = parseInt(item.data('actorid'));
                            var name = item.data('val');
                            name = name.trim();
                            addActor(id, name);
                            $tbActor.val('');
                        },
                        delay: 100
                    }
                );

                if ($hfActors.val() != '') {
                    var items = $hfActors.val().split(',');
                    for (var i = 0; i < items.length; i++) {
                        var actor = items[i].split(':');
                        addActor(actor[0], actor[1]);
                    }
                    $renderActorItems();
                }
            }
            $initActorsEvents();


            //==========提交
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
                //editor
                if (arrEditor.length != 0) {
                    for (var i = 0; i < arrEditor.length; i++) {
                        arrEditor[i].sync();
                    }
                }

                //actors
                var actorIds = '';
                for (var i = 0; i < actors.length; i++) {
                    actorIds += actors[i].Id + ':' + actors[i].Name + ',';
                }
                if (actorIds != '')
                    actorIds = actorIds.substr(0, actorIds.length - 1);
                $hfActors.val(actorIds);
            });


    </script>
</asp:Content>
