<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="RoleList.aspx.cs" Inherits="UZeroConsole.Web.UZero.RoleList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <form runat="server">
        <!-- Page Header -->
        <div class="content bg-gray-lighter">
            <div class="row items-push">
                <div class="col-sm-7">
                    <h1 class="page-heading">角色列表 <small>角色管理权限</small>
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
                        <div class="form-group ">
                            <label class="col-xs-pull-1">
                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalCreateRole"><i class="fa fa-plus"></i>添加角色</button>
                            </label>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Dynamic Table Full -->
            <div class="block">
                <div class="block-content">
                    <!-- DataTables init on table by adding .js-dataTable-full class, functionality initialized in js/pages/base_tables_datatables.js -->
                    <table class="table table-hover js-dataTable-full">
                        <thead>
                            <tr>
                                <th class="text-center" width="10%">操作</th>
                                <th class="text-center" width="30%">权限名称</th>
                                <th class="text-center">描述</th>
                            </tr>
                        </thead>
                        <% if (roles != null)
                           {
                               foreach (var role in roles)
                               {
                        %>
                        <tr>
                            <td class="text-center">
                                <a href="RoleSetPermissions.aspx?roleId=<%= role.Id %>" class='btn btn-xs' data-toggle="tooltip" title="设置权限"><i class="fa fa-key"></i></a>
                                <a class='btn btn-xs' data-toggle="tooltip" title="编辑" onclick="vm.modalUpdateOpen(<%= role.Id %>)"><i class="fa fa-pencil"></i></a>
                                <a class='btn btn-xs' data-toggle="tooltip" title="删除" onclick="vm.deleteRole(<%= role.Id %>,'<%= role.Name %>')"><i class="fa fa-close"></i></a>
                            </td>
                            <td class="text-center"><%= role.Name %></td>
                            <td class="text-center"><%= role.Remark %></td>
                        </tr>
                        <%}
                           } %>
                    </table>
                </div>
            </div>
            <!-- END Dynamic Table Full -->

        </div>
        <!-- END Page Content -->
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
     <!-- Modal -->
    <!-- Level 1 -->
    <div class="modal fade" id="modalCreateRole" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-popin">
            <div class="modal-content">
                <div class="block block-themed block-transparent remove-margin-b">
                    <div class="block-header bg-primary-dark">
                        <ul class="block-options">
                            <li>
                                <button data-dismiss="modal" type="button"><i class="si si-close"></i></button>
                            </li>
                        </ul>
                        <h3 class="block-title"><i class="fa fa-plus"></i>添加角色</h3>
                    </div>
                    <div class="block-content">
                        <div class="form-horizontal push-10-t">
                            <div class="alert alert-danger hide">
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <div class="form-material ">
                                        <input class="form-control" type="text" name="name">
                                        <label for="txtName">角色名称</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <div class="form-material">
                                        <textarea class="form-control" name="remark" cols="5" rows="3"></textarea>
                                        <label for="txtUrl">角色描述</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn  btn-primary" type="button" onclick="vm.modalCreateRoleCommit()">提交</button>
                    <button class="btn  btn-default" type="button" data-dismiss="modal">关闭</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalUpdateRole" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-popin">
            <div class="modal-content">
                <div class="block block-themed block-transparent remove-margin-b">
                    <div class="block-header bg-primary-dark">
                        <ul class="block-options">
                            <li>
                                <button data-dismiss="modal" type="button"><i class="si si-close"></i></button>
                            </li>
                        </ul>
                        <h3 class="block-title"><i class="fa fa-edit"></i>编辑角色</h3>
                    </div>
                    <div class="block-content">
                        <div class="form-horizontal push-10-t">
                            <div class="alert alert-danger hide">
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <div class="form-material ">
                                        <input class="form-control" type="text" name="name">
                                        <label for="txtName">角色名称</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <div class="form-material">
                                        <textarea class="form-control" name="remark" cols="5" rows="3"></textarea>
                                        <label for="txtUrl">角色描述</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button class="btn  btn-primary" type="button" onclick="vm.modalUpdateRoleCommit()">提交</button>
                    <button class="btn  btn-default" type="button" data-dismiss="modal">关闭</button>
                </div>
            </div>
        </div>
    </div>
    <!-- END Modal-->
    <script type="text/javascript" src="roleList.js"></script>
</asp:Content>
