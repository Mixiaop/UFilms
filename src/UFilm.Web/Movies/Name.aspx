<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Navigation.master" AutoEventWireup="true" CodeBehind="Name.aspx.cs" Inherits="UFilm.Web.Movies.Name" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCss" runat="server">
    <link rel="stylesheet" href="/css/views/movies/name.css" />
    <link rel="stylesheet" href="/lib/plugins/jquery.fancybox/jquery.fancybox.css" />
    <link rel="stylesheet" href="/lib/plugins/jquery.fancybox/helpers/jquery.fancybox-thumbs.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div id="page-name">
        <div class="container">
            <div class="row">
                <div class="col-xs-12 col-lg-6">
                    <div class="row remove-margin">
                        <div id="title" class="col-xs-12 remove-padding">
                            <%= Model.FilmMan.ToString() %>
                        </div>
                    </div>
                    <div id="info" class="row remove-margin">
                        <div class="col-xs-4 col-lg-4 remove-padding">
                            <a href="<%= Model.FilmMan.Avatar.SourceUrl %>" target="_blank" title="<%= Model.FilmMan.ToString() %>">
                                <img src="<%= Model.FilmMan.Avatar.ThumbUrl %>" alt="<%= Model.FilmMan.ToString() %>" class="img-responsive" /></a>
                        </div>
                        <div class="col-xs-8 col-lg-8 remove-padding">
                            <ul>
                                <%if (Model.FilmMan.FormatGender != "")
                                  { %>
                                <li><span>性别：</span> <%= Model.FilmMan.FormatGender%></li>
                                <%} %>
                                <% if (Model.FilmMan.Constellation.IsNotNullOrEmpty())
                                   { %>
                                <li><span>星座：</span> <%= Model.FilmMan.Constellation%></li>
                                <%} %>
                                <%if (Model.FilmMan.Birthday.IsNotNullOrEmpty())
                                  { %>
                                <li><span>出生日期：</span> <%= Model.FilmMan.Birthday%></li>
                                <%} %>
                                <%if (Model.FilmMan.PlaceOfBirth.IsNotNullOrEmpty())
                                  { %>
                                <li><span>出生地：</span> <%= Model.FilmMan.PlaceOfBirth.Replace("/","<em>/</em>")%></li>
                                <%} %>
                                <%if (Model.FilmMan.Job.IsNotNullOrEmpty())
                                  { %>
                                <li><span>职业：</span> <%= Model.FilmMan.Job.Replace("/","<em>/</em>")%></li>
                                <%} %>
                                <%if (Model.FilmMan.MoreCnName.IsNotNullOrEmpty())
                                  { %>
                                <li><span>更多中文名：</span> <%= Model.FilmMan.MoreCnName.Replace("/","<em>/</em>")%></li>
                                <%} %>
                                <%if (Model.FilmMan.MoreEnName.IsNotNullOrEmpty())
                                  { %>
                                <li><span>更多外文名：</span> <%= Model.FilmMan.MoreEnName.Replace("/","<em>/</em>")%></li>
                                <%} %>
                                <%if (Model.FilmMan.FamilyInfo.IsNotNullOrEmpty())
                                  { %>
                                <li><span>家庭信息：</span> <%= Model.FilmMan.FamilyInfo.Replace("/","<em>/</em>")%></li>
                                <%} %>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div id="summary">
                        <div id="summary-title">
                            简介 introduction
                        </div>
                        <div id="summary-body">
                            <%= Model.FilmMan.Introduction %>
                        </div>
                    </div>
                </div>
            </div>
            <div class="container">
            <div class="row">
                <% if(Model.JoinMovies.TotalCount>0){ %>
                <div class="col-lg-12 remove-padding" id="works">
                    <div class="title">
                        作品 works
                    </div>
                    <% foreach(var m in Model.JoinMovies.Items){ %>
                    <div class="workitems remove-padding col-xs-6 col-lg-2">
                        <div class="workitem">
                            <a href="<%= Route.GetRoute("Movies.Subject", m.Id) %>"  class="img" title="<%= m.FormatName %>"><img src="<%= m.Cover.ThumbUrl %>" alt="<%= m.FormatName %>"></a>
                            <a href="<%= Route.GetRoute("Movies.Subject", m.Id) %>" class="title" title="<%= m.FormatName %>"><%= m.CnName %></a>
                            <span><%= m.Year %></span>
                        </div>
                    </div>
                    <%} %>
                </div>
                <%} %>
                <% if(Model.Photos.TotalCount>0){ %>
                <div class="col-xs-12 remove-padding" id="photos">
                    <div class="title">
                        图片 photos
                    </div>
                    <div id="photo-items">
                        <% foreach (var photo in Model.Photos.Items)
                           { %>
                        <a href="<%= photo.Picture.SourceUrl %>" class="fancybox-thumbs" data-fancybox-group="thumb">
                            <img src="<%= photo.Picture.ThumbUrl %>" alt="<%= Model.FilmMan.ToString() %> 的图片" />
                        </a>
                        <%} %>
                    </div>
                </div>
                <%} %>
            </div>
                </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">
    <script src="/js/controllers/movies/name.js"></script>
</asp:Content>
