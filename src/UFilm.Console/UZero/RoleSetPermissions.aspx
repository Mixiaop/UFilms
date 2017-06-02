<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="RoleSetPermissions.aspx.cs" Inherits="UZeroConsole.Web.UZero.RoleSetPermissions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <form runat="server">
        <!-- Page Header -->
        <div class="content bg-gray-lighter">
            <div class="row items-push">
                <div class="col-sm-7">
                    <h1 class="page-heading">设置角色权限<small></small>
                    </h1>
                </div>
                <div class="col-sm-5 text-right hidden-xs">
                            <ol class="breadcrumb push-10-t">
                                <li><a class="link-effect" href="RoleList.aspx">角色列表</a></li>
                                <li>设置角色权限</li>
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
                    <asp:Literal runat="server" ID="ltlErrorMessage"></asp:Literal>
                    <asp:Literal runat="server" ID="ltlSuccessMessage"></asp:Literal>
                    <div class="form-horizontal push-10-t">
                        <div class="form-group">
                            <div class="col-xs-6">
                                <div class="form-material ">
                                    <asp:TextBox runat="server" ID="txtRoleName" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    <label>角色名</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group form-material">
                            <label class="col-xs-12">权限列表</label>
                                            <div class="col-xs-12" id="permissions">
                                                <asp:PlaceHolder runat="server" ID="phPermissions"></asp:PlaceHolder>
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
            <!-- END Dynamic Table Full -->
        </div>
        <!-- END Page Content -->
        <asp:HiddenField runat="server" ID="hfPermissionIds" />
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
     <script>
         $(function () {
             var valueToHidden = function () {
                 var value = '';
                 $('#permissions input[type=checkbox]:checked').each(function () {

                     value += $(this).val() + ',';
                 });
                 if (value != '')
                     value = value.substring(0, value.length - 1);

                 $('#<%= hfPermissionIds.ClientID%>').val(value);
            };

            $('#permissions input[type=checkbox]').click(function () {
                if ($(this).data("level") == "1") {
                    $('#permissions').find('input[parentId=' + $(this).val() + ']').prop('checked', $(this).prop('checked'));
                    $('#permissions').find('input[rootId=' + $(this).val() + ']').prop('checked', $(this).prop('checked'));
                } else if ($(this).data("level") == '2') {
                    $('#permissions').find('input[parentId=' + $(this).val() + ']').prop('checked', $(this).prop('checked'));
                }
                valueToHidden();
            });
        });
    </script>
</asp:Content>
