<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Navigation.master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="UFilm.Web.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCss" runat="server">
    <link rel="stylesheet" href="/css/views/search.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="container page-search">
        <% if(Model.GetKeywords.IsNullOrEmpty()){ %>
        <div class="none">
            <p>检索不到相关电影.</p>
        </div>
        <%}else{ %>
        <% if(Model.SearchFilmMan!=null){ %>
        <div class="col-xs-12 list actor">
            <div class="col-xs-3 col-lg-2 pic">
                <a href="<%= Route.GetRoute("Movies.Name",Model.SearchFilmMan.Id) %>" title="<%= Model.SearchFilmMan.ToString() %>">
                    <img src="<%= Model.SearchFilmMan.Avatar.ThumbUrl %>" class="img-responsive" title="<%= Model.SearchFilmMan.ToString() %>" /></a>
            </div>
            <div class="col-xs-9 col-lg-10 info">
                <h2><a href="<%= Route.GetRoute("Movies.Name",Model.SearchFilmMan.Id) %>" title="<%= Model.SearchFilmMan.ToString() %>"><%= Model.SearchFilmMan.ToString() %></a></h2>
                <% if(Model.SearchFilmMan.Job.IsNotNullOrEmpty()){ %>
                <h3><span>职业：</span><%= Model.SearchFilmMan.Job.Replace("/","<em>/</em>") %></h3>
                <%} %>
                <%if(Model.SearchFilmMan.MoreCnName.IsNotNullOrEmpty()){ %>
                <h3><span>更多中文名：</span><%= Model.SearchFilmMan.MoreCnName.Replace("/","<em>/</em>") %></h3>
                <%} %>
                <% if (Model.SearchFilmManMovies != null && Model.SearchFilmManMovies.Items.Count > 0)
                   { %>
                <h3><span>作品：</span>
                    <%
                       int index = 1;
                        foreach(var m in Model.SearchFilmManMovies.Items){ %>
                    <%= m.CnName %> <%= (index == 3?"":" <em>/</em> ") %>
                    <%index++;} %>
                </h3>
                <%} %>
            </div>
            <div class="clearfix"></div>
            <%--<p><a href="searchActor.aspx">所有叫"阿尔"的影人</a></p>--%>
        </div>
        <%} %>
        <% foreach(var m in Model.Movies.Items){ %>
        <div class="col-xs-12 list">
            <div class="col-xs-3 col-lg-2 pic">
                <a href="<%= Route.GetRoute("Movies.Subject", m.Id) %>" title="<%= m.FormatName %>">
                    <img src="<%= m.Cover.ThumbUrl %>" class="img-responsive" alt="<%= m.FormatName %>" /></a>
            </div>
            <div class="col-xs-9 col-lg-10 info">
                <h2><a href="<%= Route.GetRoute("Movies.Subject", m.Id) %>" title="<%= m.FormatName %>"><%= m.CnName %> <%= m.EnName %> <span>(<%= m.Year %>)</span></a></h2>
                <% if(m.Director.IsNotNullOrEmpty()){ %>
                <h3><span>导演：</span><%= m.Director.Replace("/","<em>/</em>") %></h3>
                <%} %>
                <% if(m.Actor.IsNotNullOrEmpty()){ %>
                <h3><span>演员：</span><%= m.Actor.Replace("/","<em>/</em>") %></h3>
                <%} %>
                <div class="ip4">
                    <% if(m.Area.IsNotNullOrEmpty()){ %>
                    <h3><span>地区：</span><%= m.Area.Replace("/","<em>/</em>") %></h3>
                    <%} %>
                    <% if(m.MovieType.IsNotNullOrEmpty()){ %>
                    <h3><span>类型：</span><%= m.MovieType.Replace("/","<em>/</em>") %></h3>
                    <%} %>
                </div>
                <p><%= m.MovieLength.Replace("/","<em>/</em>") %> <%= m.MovieLength.IsNotNullOrEmpty()?"<em>/</em>":"" %> <%= m.OtherPostYear.Replace("/","<em>/</em>") %> <%= m.OtherPostYear.IsNotNullOrEmpty()?"<em>/</em>":"" %> <%= m.OtherEnName.Replace("/","<em>/</em>") %>  </p>
            </div>
        </div>
        <%} %>
        <div class="row">
            <div class="col-xs-12 text-center">
                <ul class="pagination">
                    <%= Model.PagiHTML %>
                </ul>
            </div>
        </div>
        <%} %>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">
</asp:Content>
