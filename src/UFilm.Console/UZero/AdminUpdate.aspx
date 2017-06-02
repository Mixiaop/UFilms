<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/UZero.Master" AutoEventWireup="true" CodeBehind="AdminUpdate.aspx.cs" Inherits="UZeroConsole.Web.UZero.AdminUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
<form runat="server">
        <!-- Page Header -->
        <div class="content bg-gray-lighter">
            <div class="row items-push">
                <div class="col-sm-7">
                    <h1 class="page-heading">添加管理员<small></small>
                    </h1>
                </div>
                  <div class="col-sm-5 text-right hidden-xs">
                            <ol class="breadcrumb push-10-t">
                                <li><a class="link-effect" href="AdminList.aspx">管理员列表</a></li>
                                <li>编辑管理员信息</li>
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
                    <div class="form-horizontal push-10-t">
                        <div class="form-group">
                            <div class="col-xs-12">
                                <div class="form-material">
                                    <asp:TextBox runat="server" ID="tbUsername" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    <label >用户名</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12">
                                <div class="form-material">
                                    <asp:TextBox runat="server" ID="tbName" CssClass="form-control"></asp:TextBox>
                                    <label >姓名（昵称）</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="roles">
                            <div class="col-xs-12">
                                <div class="form-material">
                                    <asp:PlaceHolder runat="server" ID="phRoles"></asp:PlaceHolder>
                                    <asp:HiddenField runat="server" ID="hfRoleId" />
                                    <label>角色</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12">
                                <div class="form-material">
                                    <asp:TextBox runat="server" ID="tbRemark" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    <label >备注</label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12">
                                <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary"  Text="保存"   />
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
    <script>
        $(function () {
            $('#roles input[type=radio]').click(function () {
                $('#<%= hfRoleId.ClientID%>').val($(this).val());
            });
        });
    </script>
</asp:Content>
