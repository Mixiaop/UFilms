<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Navigation.master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="UFilm.Web.Collection.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCss" runat="server">
     <link rel="stylesheet" href="/css/views/search.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="wrapper found-list">
        <div class="container">
            <div class="row">
                <% foreach (var m in Model.Results.Items)
                   { %>
                <div class="col-xs-6 col-sm-4 col-md-3 col-lg-2 found-list-box" data-id="<%= m.Id %>">
                    <div class="zoom"><i class="si si-magnifier-add"></i></div>
                    <a href="<%= Route.GetRoute("Collection.Subject", m.Id) %>" title="<%= m.Name %>" target="_blank" class="cover">
                        <img src="<%= m.Cover.ThumbUrl %>" class="img-responsive" alt="<%= m.Name %>" title="<%= m.Name %>" data-id="<%= m.Id %>" />
                        <div class="cover-txt">
                                    <span class="pull-right"><%= m.Count %> 部</span>
                                </div>
                    </a>
                    <a href="<%= Route.GetRoute("Collection.Subject", m.Id) %>" title="<%= m.Name %>" class="title" target="_blank"><%= m.Name %></a>
                    <%--<span><%= m.Tags.Replace("/","<em>/</em>") %></span>--%>
                </div>
                <%} %>
            </div>
        </div>

        <div class="col-xs-12 text-center">
            <ul class="pagination">
                <%= Model.PagingHTML %>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">
</asp:Content>
