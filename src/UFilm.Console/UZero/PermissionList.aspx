<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="PermissionList.aspx.cs" Inherits="UZeroConsole.Web.UZero.PermissionList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
     <link rel="stylesheet" href="/assets/js/plugins/treeview/treeview.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <form runat="server"></form>
    <!-- Page Header -->
    <div class="content bg-gray-lighter">
        <div class="row items-push">
            <div class="col-sm-7">
                <h1 class="page-heading">控制台权限管理<small> 当前权限共分为 3 级嵌套，权限对应左则菜单。</small>
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
        <div class="block col-xs-12">
            <div class="block-content">
                <div class="row" style="padding-bottom: 10px;">
                    <div class="col-sm-12 col-lg-12">
                        <button class="btn btn-primary" data-toggle="modal" role="dialog" data-target="#modalLevel1Create"><i class="fa fa-plus"></i>添加 1 级权限</button>
                    </div>
                </div>
                <div class="widget flat radius-bordered">
                    <div class="widget-body">
                        <div id="permissions" class="tree tree-plus-minus">
                            <% 
                                var list = permissionService.GetAll();
                                if (list.Count == 0)
                                {
                            %>
                            <div class="alert alert-info">您还未创建权限，请先添加。</div>
                            <%}
                                else
                                {
                                    foreach (var permission in list.Where(x => x.Level == 1))
                                    {
                            %>
                            <div class="tree-folder">
                                <div class="tree-folder-header">
                                    <div class="tree-folder-name">
                                        <%= permission.Name %>
                                        <span>
                                            <button data-toggle="tooltip" title="添加 2 级权限" class="btn btn-default btn-sm" onclick="vm.modalCreateOpen(1,<%= permission.Id %>)"><i class="fa fa-plus"></i></button>
                                            <button data-toggle="tooltip" title="编辑权限" class="btn btn-default btn-sm" onclick="vm.modalUpdateOpen(1,<%= permission.Id %>)"><i class="fa fa-edit"></i></button>
                                            <button data-toggle="tooltip" title="删除权限" class="btn btn-default btn-sm" onclick="vm.deletePermission(<%=permission.Id %>,'<%= permission.Name %>')"><i class="fa fa-trash-o danger"></i></button>
                                        </span>
                                    </div>
                                </div>
                                <%
                                        var level2Childs = list.Where(x => x.ParentId == permission.Id);
                                        if (level2Childs.Count() > 0)
                                        {
                                            foreach (var level2 in level2Childs)
                                            {
                                %>
                                <div class="tree-folder-content">
                                    <div class="tree-folder">
                                        <div class="tree-folder-header">
                                            <div class="tree-folder-name">
                                                <%= level2.Name %>
                                                <span>
                                                    <button data-toggle="tooltip" title="添加 3 级权限" class="btn btn-default btn-sm" onclick="vm.modalCreateOpen(2,<%= level2.Id %>)"><i class="fa fa-plus"></i></button>
                                                    <button data-toggle="tooltip" title="编辑权限" class="btn btn-default btn-sm" onclick="vm.modalUpdateOpen(2,<%= level2.Id %>)"><i class="fa fa-edit"></i></button>
                                                    <button data-toggle="tooltip" title="删除权限" class="btn btn-default btn-sm" onclick="vm.deletePermission(<%=level2.Id %>,'<%= level2.Name %>')"><i class="fa fa-trash-o danger"></i></button>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <% var level3Childs = list.Where(x => x.ParentId == level2.Id);
                                       if (level3Childs.Count() > 0)
                                       {
                                           foreach (var level3 in level3Childs)
                                           {%>
                                    <div class="tree-folder-content">
                                        <div class="tree-folder">
                                            <div class="tree-folder-header">
                                                <div class="tree-folder-name">
                                                    <%= level3.Name %>
                                                    <span>
                                                        <button data-toggle="tooltip" title="编辑权限" class="btn btn-default btn-sm" onclick="vm.modalUpdateOpen(3,<%= level3.Id %>)"><i class="fa fa-edit"></i></button>
                                                        <button data-toggle="tooltip" title="删除权限" class="btn btn-default btn-sm" onclick="vm.deletePermission(<%=level3.Id %>,'<%= level3.Name %>')"><i class="fa fa-trash-o danger"></i></button>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%}
                                       } %>
                                </div>
                                <%}
                                        }%>
                            </div>
                            <%}
                                } %>
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
    <!-- Modal -->
    <!-- Level 1 -->
    <div class="modal fade" id="modalLevel1Create" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-popin">
            <div class="modal-content">
                <div class="block block-themed block-transparent remove-margin-b">
                    <div class="block-header bg-primary-dark">
                        <ul class="block-options">
                            <li>
                                <button data-dismiss="modal" type="button"><i class="si si-close"></i></button>
                            </li>
                        </ul>
                        <h3 class="block-title"><i class="fa fa-plus"></i>添加 1 级权限</h3>
                    </div>
                    <div class="block-content">
                        <div class="form-horizontal push-10-t">
                            <div class="alert alert-danger hide">
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <div class="form-material ">
                                        <input class="form-control" type="text" name="txtName">
                                        <label for="txtName">权限名称</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtIcon">
                                        <label for="txtIcon">Icon</label>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <a href="/OneUI/base_ui_icons.html" target="_blank" class="btn btn-default ">查看Icons</a>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtUrl">
                                        <label for="txtUrl">链接地址</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtOrder">
                                        <label for="txtOrder">排序</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn  btn-primary" type="button" onclick="vm.modalLevel1CreateCommit()">提交</button>
                    <button class="btn  btn-default" type="button" data-dismiss="modal">关闭</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalLevel1Update" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-popin">
            <div class="modal-content">
                <div class="block block-themed block-transparent remove-margin-b">
                    <div class="block-header bg-primary-dark">
                        <ul class="block-options">
                            <li>
                                <button data-dismiss="modal" type="button"><i class="si si-close"></i></button>
                            </li>
                        </ul>
                        <h3 class="block-title"><i class="fa fa-edit"></i>编辑 1 级权限</h3>
                    </div>

                    <div class="block-content">
                        <div class="form-horizontal push-10-t">
                            <div class="alert alert-danger hide">
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtName">
                                        <label for="txtName">权限名称</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtIcon">
                                        <label for="txtIcon">Icon</label>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <a href="/OneUI/base_ui_icons.html" target="_blank" class="btn btn-default ">查看Icons</a>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtUrl">
                                        <label for="txtUrl">链接地址</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtOrder">
                                        <label for="txtOrder">排序</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn  btn-primary" type="button" onclick="vm.modalLevel1UpdateCommit()">提交</button>
                    <button class="btn  btn-default" type="button" data-dismiss="modal">关闭</button>
                </div>
            </div>
        </div>
    </div>
    <!-- END Level 1 -->

    <!-- Level 2 -->
    <div class="modal fade" id="modalLevel2Create" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-popin">
            <div class="modal-content">
                <div class="block block-themed block-transparent remove-margin-b">
                    <div class="block-header bg-primary-dark">
                        <ul class="block-options">
                            <li>
                                <button data-dismiss="modal" type="button"><i class="si si-close"></i></button>
                            </li>
                        </ul>
                        <h3 class="block-title"><i class="fa fa-plus"></i>添加 2 级权限</h3>
                    </div>
                    <div class="block-content">
                        <div class="form-horizontal push-10-t">
                            <div class="alert alert-danger hide">
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <div class="form-material floating">
                                        <input class="form-control" type="text" name="txtParentName" value="无" disabled="disabled">
                                        <label for="txtName">上级权限</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <div class="form-material ">
                                        <input class="form-control" type="text" name="txtName">
                                        <label for="txtName">权限名称</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtIcon">
                                        <label for="txtIcon">Icon</label>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <a href="/OneUI/base_ui_icons.html" target="_blank" class="btn btn-default ">查看Icons</a>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtUrl">
                                        <label for="txtUrl">链接地址</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtOrder">
                                        <label for="txtOrder">排序</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn  btn-primary" type="button" onclick="vm.modalLevel2CreateCommit()">提交</button>
                    <button class="btn  btn-default" type="button" data-dismiss="modal">关闭</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalLevel2Update" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-popin">
            <div class="modal-content">
                <div class="block block-themed block-transparent remove-margin-b">
                    <div class="block-header bg-primary-dark">
                        <ul class="block-options">
                            <li>
                                <button data-dismiss="modal" type="button"><i class="si si-close"></i></button>
                            </li>
                        </ul>
                        <h3 class="block-title"><i class="fa fa-edit"></i>编辑 2 级权限</h3>
                    </div>

                    <div class="block-content">
                        <div class="form-horizontal push-10-t">
                            <div class="alert alert-danger hide">
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <div class="form-material floating">
                                        <input class="form-control" type="text" name="txtParentName" value="无" disabled="disabled">
                                        <label for="txtName">上级权限</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtName">
                                        <label for="txtName">权限名称</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtIcon">
                                        <label for="txtIcon">Icon</label>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <a href="/OneUI/base_ui_icons.html" target="_blank" class="btn btn-default ">查看Icons</a>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtUrl">
                                        <label for="txtUrl">链接地址</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtOrder">
                                        <label for="txtOrder">排序</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn  btn-primary" type="button" onclick="vm.modalLevel2UpdateCommit()">提交</button>
                    <button class="btn  btn-default" type="button" data-dismiss="modal">关闭</button>
                </div>
            </div>
        </div>
    </div>
    <!-- END Level 2 -->

    <!-- Level 3 -->
    <div class="modal fade" id="modalLevel3Create" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-popin">
            <div class="modal-content">
                <div class="block block-themed block-transparent remove-margin-b">
                    <div class="block-header bg-primary-dark">
                        <ul class="block-options">
                            <li>
                                <button data-dismiss="modal" type="button"><i class="si si-close"></i></button>
                            </li>
                        </ul>
                        <h3 class="block-title"><i class="fa fa-plus"></i>添加 3 级权限</h3>
                    </div>
                    <div class="block-content">
                        <div class="form-horizontal push-10-t">
                            <div class="alert alert-danger hide">
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <div class="form-material floating">
                                        <input class="form-control" type="text" name="txtParentName" value="无" disabled="disabled">
                                        <label for="txtName">上级权限</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <div class="form-material ">
                                        <input class="form-control" type="text" name="txtName">
                                        <label for="txtName">权限名称</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtIcon">
                                        <label for="txtIcon">Icon</label>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <a href="/OneUI/base_ui_icons.html" target="_blank" class="btn btn-default ">查看Icons</a>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtUrl">
                                        <label for="txtUrl">链接地址</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtOrder">
                                        <label for="txtOrder">排序</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn  btn-primary" type="button" onclick="vm.modalLevel3CreateCommit()">提交</button>
                    <button class="btn  btn-default" type="button" data-dismiss="modal">关闭</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalLevel3Update" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-popin">
            <div class="modal-content">
                <div class="block block-themed block-transparent remove-margin-b">
                    <div class="block-header bg-primary-dark">
                        <ul class="block-options">
                            <li>
                                <button data-dismiss="modal" type="button"><i class="si si-close"></i></button>
                            </li>
                        </ul>
                        <h3 class="block-title"><i class="fa fa-edit"></i>编辑 3 级权限</h3>
                    </div>

                    <div class="block-content">
                        <div class="form-horizontal push-10-t">
                            <div class="alert alert-danger hide">
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <div class="form-material floating">
                                        <input class="form-control" type="text" name="txtParentName" value="无" disabled="disabled">
                                        <label for="txtName">上级权限</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtName">
                                        <label for="txtName">权限名称</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtIcon">
                                        <label for="txtIcon">Icon</label>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <a href="/OneUI/base_ui_icons.html" target="_blank" class="btn btn-default ">查看Icons</a>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtUrl">
                                        <label for="txtUrl">链接地址</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <div class="form-material">
                                        <input class="form-control" type="text" name="txtOrder">
                                        <label for="txtOrder">排序</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn  btn-primary" type="button" onclick="vm.modalLevel3UpdateCommit()">提交</button>
                    <button class="btn  btn-default" type="button" data-dismiss="modal">关闭</button>
                </div>
            </div>
        </div>
    </div>
    <!-- END Level 3 -->
    <!-- END Modal -->
    <script type="text/javascript" src="permissionList.js"></script>
</asp:Content>
