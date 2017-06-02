<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="CollectMovie.aspx.cs" Inherits="UFilm.Console.Spiders.Douban.CollectMovie" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <form runat="server"></form>
        <!-- Page Header -->
        <div class="content bg-gray-lighter">
            <div class="row items-push">
                <div class="col-sm-7">
                    <h1 class="page-heading">豆瓣采集电影 <small>通过链接采集一部电影，图片默认都采集第1页【格式必须不包含请求参数（例：https://movie.douban.com/subject/11598977/）】</small>
                    </h1>
                </div>
                <div class="col-sm-5 text-right hidden-xs">
                   <%-- <ol class="breadcrumb push-10-t">
                        <li><a class="link-effect" href="List.aspx">返回列表</a></li>
                        <li>添加</li>
                    </ol>--%>
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
                                    <input type="text" id="tbName" class="form-control" />
                                    <label class="control-label">名称</label>
                                    <div class="help-block">
                                    </div>
                                </div>
                                <div class="form-material">
                                    <textarea class="form-control" id="tbLinks" rows="20"></textarea>
                                    <label class="control-label">链接</label>
                                    <div class="help-block">如：电影链接，格式必须不包含请求参数（例：https://movie.douban.com/subject/11598977/）
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12">
                                <input type="submit" value="添加任务" class="btn btn-primary" id="btnCommit" /> <%--<input type="submit" value="开启采集任务" class="btn btn-primary" id="btnExecute" />--%> <a href="../tasks/list.aspx" class="btn btn-default">返回任务列表</a>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script>
        $('#btnCommit').click(function () {
            if (confirm('你确定添加到采集任务中吗！')) {
                var $name = $('#tbName');
                var $links = $("#tbLinks");

                if ($name.val() == '') {
                    alert('名称不能为空');
                    return;
                }
                if ($links.val() == '') {
                    alert('链接不能为空');
                    return;
                }
                CurrentApp.Spider($name.val(), $links.val());
                notify.success('添加成功');
                $name.val('');
                $links.val('');
            }
        });

        $('#btnExecute').click(function () {
            if (confirm('你开启执行采集任务吗，一般需要10几分钟以上。【请查看任务后台及日志】')) {
                CurrentApp.Execute();
                notify.success('开启成功');
            }
        });
    </script>
</asp:Content>
