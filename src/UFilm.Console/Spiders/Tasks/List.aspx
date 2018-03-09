<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="UFilm.Console.Spiders.Tasks.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
     <div class="col-sm-12 col-lg-12">
        <form runat="server">
            <!-- Page Header -->
            <div class="content bg-gray-lighter">
                <div class="row items-push">
                    <div class="col-sm-7">
                        <h1 class="page-heading">采集任务列表 <small>采集电影或影人都会创建任务用于监控进度</small>
                        </h1>
                    </div>
                </div>
            </div>
            <!-- END Page Header -->
            <!-- Page Content -->
            <div class="content">
                <div class="row items-push">
                    <div class="col-xs-12">
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:TextBox runat="server" ID="tbSeachKeywords" placeholder="请输入搜索关键字" CssClass="form-control"></asp:TextBox>
                                <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                                    <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="采集中" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="未完成" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="已完成" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                                <label class="col-xs-pull-1">
                                    <asp:Button ID="btnSearch" CssClass="btn btn-primary btn-sm" runat="server" Text="搜索"></asp:Button>
                                    <a href="../douban/collectmovie.aspx"  class="btn btn-primary btn-sm">添加豆瓣任务</a>
                                    <%--<asp:Button ID="btnRunJob" OnClientClick="return confirm('请你确认Hangfire监控未有采集的任务，并开始运行采集任务。')" CssClass="btn btn-danger btn-sm" runat="server" Text="运行采集任务"></asp:Button>--%>
                                    <a href="/jobs/jobs/enqueued" target="_blank" class="btn btn-default btn-sm">Hangfire监控</a>
                                    <a href="List.aspx" target="_blank" class="btn btn-default btn-sm">弹出页</a>
                                </label>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Dynamic Table Full -->
                <div class="block">
                    <div class="block-content table-responsive">
                        <asp:Literal runat="server" ID="ltlMessage"></asp:Literal>
                        <table class="table table-hover js-dataTable-full">
                            <thead>
                                <tr>
                                    <th class="text-center" ></th>
                                    <th class="text-center" width="20%" >名称</th>
                                    <th class="text-center" >消息</th>
                                    <th class="text-center" width="10%">图片数（解析）</th>
                                    <th class="text-center" >是否完成</th>
                                    <th class="text-center" >完成时间</th>
                                    <th class="text-center" width="10%">创建时间</th>
                                </tr>
                            </thead>
                            <asp:Repeater runat="server" ID="rptDatas">
                                <ItemTemplate>
                                    <tbody>
                                        <tr>
                                            <td class="text-center">
                                                <a href="<%# WebRoutes.GetRoute("Movies.Subject",Eval("ObjectId").ToInt())  %>" target="_blank" class='btn btn-default btn-xs active-btn '  data-toggle="tooltip" title="查看电影"><i class="fa fa-camera"></i></a>
                                                <a href="javascript:void(0);" data-id="<%# Eval("Id") %>"  class='btn btn-default btn-xs active-btn btn-loglist'  data-toggle="tooltip" title="日志"><i class="fa fa-list"></i></a>
                                                <asp:LinkButton CommandArgument='<%# Eval("Id") %>' runat="server" class='btn btn-default btn-xs active-btn ' OnClientClick="return confirm('你确认重新开始任务吗?')" OnClick="RestartClick" data-toggle="tooltip" title="重新开始任务"><i class="fa fa-refresh"></i></asp:LinkButton>
                                                <asp:LinkButton CommandArgument='<%# Eval("Id") %>' runat="server" class='btn btn-default btn-xs active-btn ' OnClientClick="return confirm('你确认处理异常了吗?')" OnClick="HandleExceptionClick" data-toggle="tooltip" title="已处理异常"><i class="fa fa-lightbulb-o"></i></asp:LinkButton>
                                                <asp:LinkButton CommandArgument='<%# Eval("Id") %>' runat="server" class='btn btn-default btn-xs active-btn ' OnClientClick="return confirm('你确认完成任务吗，强行改变状态?')" OnClick="FinishClick" data-toggle="tooltip" title="完成任务"><i class="fa fa-check"></i></asp:LinkButton>
                                                <asp:LinkButton CommandArgument='<%# Eval("Id") %>' runat="server" class='btn btn-default btn-xs active-btn ' OnClientClick="return confirm('你确定删除吗?')" OnClick="DeleteClick" data-toggle="tooltip" title="删除"><i class="fa fa-remove"></i></asp:LinkButton>
                                            </td>
                                            <td class="text-center"><%# Eval("Name") %> <%# (bool)Eval("Spidering")?"<span class=\"label-success label\">采集中</span>":"" %><br />
                                                <%--<%# Eval("Links") %>--%>
                                            </td>
                                            <td class="text-center"><%# Eval("Content").ToString().GetSubString(0,100,".") %> </td>
                                            <td class="text-center"><%# Eval("ImageCount") %> </td>
                                          <td class="text-center"><%# (bool)Eval("Finished")?"<span class=\"label-success label\">已完成</span>":"-" %> </td>
                                            <td class="text-center"><%# (bool)Eval("Finished")?(((DateTime?)Eval("FinishedTime")).Value - ((DateTime)Eval("CreationTime"))).Minutes +" 分钟":"-" %> </td>
                                            <td class="text-center"><%# Eval("CreationTime","{0:yyyy-MM-dd HH:mm}") %></td>
                                        </tr>
                                    </tbody>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <div class="text-center">
                        <ul class="pagination">
                             <%= PagerHtml %>
                        </ul>
                    </div>
                </div>
                <!-- END Dynamic Table Full -->
            </div>
            <!-- END Page Content -->
        </form>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
     <!-- LogList-->
    <div class="modal fade" id="modalLogList" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-popin">
            <div class="modal-content">
                <div class="block block-themed block-transparent remove-margin-b">
                    <div class="block-header bg-primary-dark">
                        <ul class="block-options">
                            <li>
                                <button data-dismiss="modal" type="button"><i class="si si-close"></i></button>
                            </li>
                        </ul>
                        <h3 class="block-title"><span></span> 的任务日志列表</h3>
                    </div>
                    <div class="block-content">
                        <div class="form-horizontal push-10-t">
                            <div class="alert alert-danger hide">
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="form-material ">
                                       <input type="text" value="影片信息 》 影人信息 》 影片海报 》 影片剧照" class="form-control" disabled="disabled" />
                                    <label class="control-label">解析流程</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="form-material ">
                                       <input type="text" id="tbLinks" class="form-control" disabled="disabled" />
                                    <label class="control-label">链接</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                   <table width="100%" >
                                   </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <a href="" target="_blank" class="btn btn-primary">详细</a>
                    <button class="btn  btn-default" type="button" data-dismiss="modal">关闭</button>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            var $btnLogList = $('.btn-loglist');
            var $modalLogList = $('#modalLogList');

            var $logTable = $modalLogList.find('table');
            var logTimer = null;
            var logCount = 0;
            var logNow = "";
            var logList = [];
            

            $btnLogList.click(function () {
                var $title = $modalLogList.find('.block-title span');
                var $links = $modalLogList.find('#tbLinks');
                var taskId = parseInt($(this).data('id'));
                $modalLogList.find('.btn-primary').attr('href', '../TaskLogs/List.aspx?taskid=' + taskId);

                clearTimeout(logTimer);
                logCount = 0;
                logNow = '';
                logList = [];
                $logTable.html('');

                SpidersService.GetTask(taskId, function (res) {
                    var result = res.value;
                    if (result.Success) {
                        var task = result.Result;
                        $title.text(task.Name);
                        $links.val(task.Links);
                        $modalLogList.modal('show');

                        loadLogs(taskId);
                    } 
                });
            });

            var existsLog = function (log) {
                var exists = false;
                for (var i = 0; i < logList.length; i++) {
                    if (logList[i].Id == log.Id) {
                        exists = true;
                        break;
                    }
                }
                return exists;
            }

            var loadLogs = function (taskId) {
                logTimer = setTimeout(function () {
                    if (logCount >= 15) {
                        logCount = 0;
                        logList = [];
                        $logTable.html('');
                    }
                    
                    SpidersService.GetLastLogs(taskId, logNow, function (res) {
                        //console.log('时间：' + logNow);
                        var model = res.value;

                        if (model.Success) {
                            for (var i = 0; i < model.Result.length; i++) {
                                var log = model.Result[i];
                                if (i == 0)
                                    logNow = log.Time;

                                if (!existsLog(log)) {
                                    $logTable.prepend('<tr><td class="text-left">' + log.Message + '</td><td class="text-center">' + log.Time + '</td></tr>');
                                    logList.push(log);
                                    logCount++;
                                    break;
                                }
                            }
                            loadLogs(taskId);
                        }
                    });
                    
                }, 2000);
            }

        });
    </script>
</asp:Content>
