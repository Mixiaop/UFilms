<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Navigation.master" AutoEventWireup="true" CodeBehind="Subject.aspx.cs" Inherits="UFilm.AlibWeb.Movies.Subject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCss" runat="server">
    <link rel="stylesheet" href="/css/views/movies/subject.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="wrapper">

        <div class="container">
            <div class="col-xs-12 mb15">
                <div class="row">
                    <h2 class="h2-title"><%= Model.Movie.CnName%> <%= Model.Movie.EnName%> <% if(Model.Movie.Year>0){ %><span>(<%= Model.Movie.Year%>)</span><%} %></h2>
                </div>
            </div>
        </div>

        <div class="container">
            <div class="col-xs-12 detail-wrap">
                <div class="row">

                    <div class="col-lg-3 col-xs-12 detail-pic">
                        <div class="row">
                            <div class="pd18-20 bdm">
                                <a href="<%= Model.Movie.Cover.SourceUrl %>" target="_blank">
                                    <img src="<%= Model.Movie.Cover.SourceUrl %>" alt="<%= Model.Movie.FormatName %>" class="img-responsive" /></a>
                            </div>
                            <div class="col-xs-12 ">
                                <div class="row">
                                    <div class="pd18-20 detail-type">
                                        <%
                                            string actorName = "";
                                            int actorId = 0;
                                            if (Model.Actors != null && Model.Actors.Count > 0){
                                                actorName = Model.Actors[0].Actor.CnName;
                                                actorId = Model.Actors[0].ActorId;
                                            }
                                             %>
                                        <ul>
                                            <% if(Model.Movie.Code.IsNotNullOrEmpty()){ %>
                                            <li><span>番号：</span> <%=  Model.Movie.Code %></li>
                                            <%} %>
                                            <% if (actorName.IsNotNullOrEmpty())
                                               { %>
                                            <li><span>女优：</span> <%=  actorName %></li>
                                            <%} %>
                                            <% if (Model.Movie.MovieType.IsNotNullOrEmpty())
                                               { %>
                                            <li><span>类型：</span> <%=  Model.Movie.MovieType.Replace(",","<em>/</em>")%></li>
                                            <%} %>
                                            <% if(Model.Movie.MovieLength.IsNotNullOrEmpty()){ %>
                                            <li><span>片长：</span> <%= Model.Movie.MovieLength %></li>
                                            <%} %>
                                            <% if(Model.Movie.OtherPostYear.IsNotNullOrEmpty()){ %>
                                            <li><span>上映日期：</span> <%= Model.Movie.OtherPostYear%></li>
                                            <%} %>
                                        </ul>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="detail-con-wrap col-lg-9 col-xs-12 ">
                        <div class="shadow"></div>

                        <div class="detail-con">
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="pd18-20 bdm">
                                            <h6 class="h6-title"><%= actorName %>的作品</h6>
                                            <div>
                                                <%--<%= Model.Movie.Introduction%>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-12 detail-photo">
                                    <div class="row">
                                        <div class="pd18-20">
                                            <h6 class="h6-title">猜你喜欢 </h6>
                                            <%--<% if (Model.Photos.TotalCount > 0)
                                               { %>
                                            <div id="photos">
                                                <div class="pitems">
                                                    <% foreach (var photo in Model.Photos.Items)
                                                       { %>
                                                    <a href="<%= photo.Picture.SourceUrl %>" class="fancybox-thumbs" data-fancybox-group="thumb">
                                                        <img src="<%= photo.Picture.ThumbUrl %>" alt="<%= Model.Movie.CnName %> 的剧照" />
                                                    </a>
                                                    <%} %>
                                                </div>
                                            </div>
                                            <%} %>--%>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">
    <link rel="stylesheet" href="/lib/plugins/jquery.fancybox/jquery.fancybox.css" />
    <link rel="stylesheet" href="/lib/plugins/jquery.fancybox/helpers/jquery.fancybox-thumbs.css" />
    <script src="/js/controllers/movies/subject.js"></script>
</asp:Content>
