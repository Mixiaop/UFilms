<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="UFilm.Console.Collection.Items.List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
    <style type="text/css">
        table .cover img {
            max-width: 140px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="col-sm-12 col-lg-12">
        <form runat="server">
            <!-- Page Header -->
            <div class="content bg-gray-lighter">
                <div class="row items-push">
                    <div class="col-sm-7">
                        <h1 class="page-heading"><%= Model.Collection.Name %> <small>精选集列表</small>
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
                                <%--<asp:TextBox runat="server" ID="tbSeachKeywords" placeholder="请输入搜索关键字" CssClass="form-control"></asp:TextBox>--%>
                                <label class="col-xs-pull-1">
                                    <%--<asp:Button ID="btnSearch" CssClass="btn btn-primary btn-sm" runat="server" Text="搜索"></asp:Button>--%>
                                    <a href="javascript:;" class="btn btn-primary btn-sm btnAdd">添加</a>
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
                                    <th width="20%"></th>
                                    <th class="text-center" width="20%">影片</th>
                                    <th class="text-center">副标题</th>
                                    <th class="text-center" width="10%">排序</th>
                                    <th class="text-center" width="10%">创建时间</th>
                                </tr>
                            </thead>
                            <tbody>
                                <% foreach (var info in Model.Results.Items)
                                   { %>
                                <tr>
                                    <td class="text-center">
                                        <a class='btn btn-default btn-xs active-btn btnEdit' data-id="<%= info.Id %>" href="javascript:;" data-toggle="tooltip" title="编辑"><i class="fa fa-pencil"></i></a>
                                        <a class='btn btn-default btn-xs active-btn btnDelete' data-id="<%= info.Id %>" data-toggle="tooltip" title="删除"><i class="fa fa-remove"></i></a>
                                    </td>
                                    <td class="text-center cover" title="><%= info.Movie.CnName %>"><a href="<%= WebRoutes.GetRoute("Movies.Subject", info.MovieId) %>" target="_blank">
                                        <img src="<%= info.Movie.Cover.ThumbUrl %>" /></a> <a href="<%= WebRoutes.GetRoute("Movies.Subject", info.MovieId) %>" target="_blank" title="<%= info.Movie.CnName%>"><%= info.Movie.CnName%></a>  </td>
                                    <td class="text-center"><%= info.Title%> </td>
                                    <td class="text-center"><%= info.Order %></td>
                                    <td class="text-center"><%= info.CreationTime.ToString("yyyy-MM-dd HH:mm") %></td>
                                </tr>
                                <%} %>
                            </tbody>
                        </table>
                    </div>
                    <div class="text-center">
                        <ul class="pagination">
                            <%= Model.PagingHTML %>
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
    <link href="/assets/js/plugins/jquery-auto-complete/jquery.auto-complete.css" rel="stylesheet" />
    <script src="/assets/js/plugins/jquery-auto-complete/jquery.auto-complete.js"></script>
    <!-- Add Modal -->
    <div class="modal fade" id="modalAdd" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-popin">
            <div class="modal-content">
                <div class="block block-themed block-transparent remove-margin-b">
                    <div class="block-header bg-primary-dark">
                        <ul class="block-options">
                            <li>
                                <button data-dismiss="modal" type="button"><i class="si si-close"></i></button>
                            </li>
                        </ul>
                        <h3 class="block-title"><i class="fa fa-plus"></i>添加</h3>
                    </div>
                    <div class="block-content">
                        <div class="form-horizontal push-10-t">
                            <div class="alert alert-danger hide">
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="form-material ">
                                        <input type="text" name="tbName" class="form-control" />
                                        <label>影片</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="form-material ">
                                        <input type="text" name="tbAlias" class="form-control" />
                                        <label>副标题</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn  btn-primary" type="button">提交</button>
                    <button class="btn  btn-default" type="button" data-dismiss="modal">关闭</button>
                </div>
            </div>
        </div>
    </div>
    <!-- END Add Modal-->
    <!-- Edit Modal -->
    <div class="modal fade" id="modalEdit" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-popin">
            <div class="modal-content">
                <div class="block block-themed block-transparent remove-margin-b">
                    <div class="block-header bg-primary-dark">
                        <ul class="block-options">
                            <li>
                                <button data-dismiss="modal" type="button"><i class="si si-close"></i></button>
                            </li>
                        </ul>
                        <h3 class="block-title"><i class="fa fa-plus"></i>编辑</h3>
                    </div>
                    <div class="block-content">
                        <div class="form-horizontal push-10-t">
                            <div class="alert alert-danger hide">
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="form-material ">
                                        <input type="text" name="tbTitle" class="form-control" />
                                        <label>副标题</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="form-material ">
                                        <input type="text" name="tbOrder" class="form-control" />
                                        <label>排序</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn  btn-primary" type="button">保存</button>
                    <button class="btn  btn-default" type="button" data-dismiss="modal">关闭</button>
                </div>
            </div>
        </div>
    </div>
    <!-- END Edit Modal-->
    <script>
        var collectionId = parseInt('<%= Model.Collection.Id%>');
        //add
        var $modalAdd = $('#modalAdd');
        var $modalAdd_Name = $modalAdd.find('input[name=tbName]');
        var $modalAdd_Alias = $modalAdd.find('input[name=tbAlias]');
        var addMovieId = 0;
        $modalAdd_Name.autoComplete(
                {
                    minChars: 1,
                    source: function (term, response) {
                        term = term.toLowerCase();
                        term = $.trim(term);
                        MoviesService.Search(term, 10, function (res) {
                            var json = res.value;
                            if (json.Success) {
                                response(json.Result);
                            }
                        });
                    },
                    renderItem: function (item, search) {
                        return '<div class="autocomplete-suggestion" data-movieid="' + item.Id + '"  data-val="' + (item.Year + ' ' + item.CnName) + '">' + (item.Year + ' ' + item.CnName) + '</div>';
                    },
                    onSelect: function (e, term, item) {
                        addMovieId = parseInt(item.data('movieid'));
                    },
                    delay: 300
                }
            );
        var _addItem = function () {
            if (addMovieId == 0) {
                alert('请先选择一部电影');
                return;
            }
            CollectionService.AddItem(collectionId, addMovieId, $modalAdd_Alias.val(), function (res) {
                var json = res.value;
                if (json.Success) {
                    notify.success('添加成功');
                    addMovieId = 0;
                    $modalAdd_Alias.val('');
                    $modalAdd_Name.val('');
                    $modalAdd.modal('hide');
                    redirectService.refresh(1000);
                }
            });
        }
        $modalAdd.find('.btn-primary').click(function () {
            _addItem();
        });
        $modalAdd_Alias.keydown(function (e) {
            if (e.keyCode == 13) {
                _addItem();
            }
        });
        $modalAdd_Name.keydown(function (e) {
            if (e.keyCode == 13) {
                _addItem();
            }
        });

        $('.btnAdd').click(function () {
            addMovieId = 0;
            $modalAdd.modal('show');
        });

        //edit
        var $modalEdit = $('#modalEdit');
        var $modalEdit_Title = $modalEdit.find('input[name=tbTitle]');
        var $modalEdit_Order = $modalEdit.find('input[name=tbOrder]');
        var editItemId = 0;
        $('.btnEdit').click(function () {
            editItemId = parseInt($(this).data('id'));

            CollectionService.GetItem(editItemId, function (res) {
                if (res.value.Success) {
                    var item = res.value.Result;
                    $modalEdit_Title.val(item.Title);
                    $modalEdit_Order.val(item.Order);
                    $modalEdit.modal('show');
                }
            });
            
        });

        $modalEdit.find('.btn-primary').click(function () {
            var title = $modalEdit_Title.val();
            var order = $modalEdit_Order.val();
            if (order == '')
            {
                alert('请输入排序');
                return;
            }

            CollectionService.UpdateItem(editItemId, title, parseInt(order), function (res) {
                $modalEdit.modal('hide');
                notify.success('保存成功');
                addMovieId = 0;
                
                redirectService.refresh(1000);
            });
        });

        //delete
        $('.btnDelete').click(function () {
            var itemId = parseInt($(this).data('id'));
            if (confirm('你确认删除吗？')) {
                CollectionService.DeleteItem(itemId, function (res) {
                    if (res.value.Success) {
                        notify.success('删除成功');
                        redirectService.refresh(1000);
                    }
                });

            }
        });
    </script>
</asp:Content>
