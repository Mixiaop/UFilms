<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Navigation.master" AutoEventWireup="true" CodeBehind="Subject.aspx.cs" Inherits="UFilm.Web.Movies.Subject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCss" runat="server">
    <link rel="stylesheet" href="/css/views/movies/subject.css" />
    <link rel="stylesheet" href="/lib/plugins/jquery.fancybox/jquery.fancybox.css"  />
    <link rel="stylesheet" href="/lib/plugins/jquery.fancybox/helpers/jquery.fancybox-thumbs.css"  />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div id="page-subject">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <div id="movie-title">
                        <%= Model.Movie.CnName%> <%= Model.Movie.EnName%> <span>(<%= Model.Movie.Year%>)</span>
                    </div>
                </div>
            </div>
        </div>
        <div class="container">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                    <div id="movie-info">
                        <div class="row">
                            <div class="col-xs-4 col-lg-4">
                                <a href="<%= Model.Movie.Cover.SourceUrl %>" class="img" target="_blank">
                                    <img src="<%= Model.Movie.Cover.ThumbUrl %>" alt="<%= Model.Movie.FormatName %>" class="img-responsive" /></a>
                            </div>
                            <div class="col-xs-8 col-lg-8">
                                <ul>
                                    <li><span>类型：</span> <%=  Model.Movie.MovieType.Replace("/","<em>/</em>")%></li>
                                    <li><span>制片国家/地区：</span> <%= Model.Movie.Area.Replace("/","<em>/</em>")%></li>
                                    <li><span>语言：</span> <%= Model.Movie.Language.Replace("/","<em>/</em>")%></li>
                                    <% if (Model.Movie.IsSeries == 1)
                                   { %>
                                <li><span>首播时间:</span> <%= Model.Movie.OtherPostYear%></li>
                                <li><span>总集数:</span> <%= Model.Movie.SeriesCount%>集</li>
                                <li><span>单集片长:</span> <%= Model.Movie.SeriesLength%></li>
                                <%}
                                   else
                                   { %>
                                    <li><span>上映日期：</span> <%= Model.Movie.OtherPostYear.Replace("/","<em>/</em>")%></li>
                                    <% if (!string.IsNullOrEmpty(Model.Movie.MovieLength))
                                       { %>
                                    <li><span>片长：</span> <%= Model.Movie.MovieLength.Replace("/","<em>/</em>")%></li>
                                    <%} %>
                                    <%} %>
                                    <% if (!string.IsNullOrEmpty(Model.Movie.OtherEnName))
                                       { %>
                                    <li><span>又名：</span> <%= Model.Movie.OtherEnName.Replace("/","<em>/</em>")%></li>
                                    <%} %>
                                    <li class="action">
                                <% if (Model.Movie.IsSeries == 1 && Model.Movie.SeriesNumber != 0)
                                   { %><div class="btn-group series" >
                                      <a class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                                        第 <%= Model.Movie.SeriesNumber %> 季 <span class="caret"></span>
                                      </a>
                                      <ul class="dropdown-menu" role="menu">
                                          <% foreach(var m in Model.SeriesMovies){ %>
                                          <li><a href="<%= Route.GetRoute("Subject", m.Id) %>" title="<%= m.FormatName %>">第 <%= m.SeriesNumber %> 季</a></li>
                                          <%} %>
                                        <%--<%=SeriesNumberList %>--%>
                                      </ul>
                                      </div>
                                      <%} %>
                                </li>
                                </ul>
                                <div id="movie-mans">
                                    <div class="title">导演 director</div>
                                    <div class="list">
                                        <% 
                                            var index = 0;
                                            var directors = Model.Participarts.Where(x=>x.JobType == UFilm.MovieJobType.Director);
                                            foreach(var m in directors){ %>
                                        <a href="<%= Route.GetRoute("Name", m.FilmManId) %>" title="<%= m.FilmMan.ToString() %>"><%= m.FilmMan.CnName %></a> <%= (index == directors.Count()-1)?"":" <em>/</em> " %>
                                        <% index++;} %>
                                    </div>
                                    <div class="title">编剧 writer</div>
                                    <div class="list">
                                   <% 
                                            index = 0;
                                            var writers = Model.Participarts.Where(x=>x.JobType == UFilm.MovieJobType.ScreenWriter);
                                            foreach (var m in writers)
                                            { %>
                                       <a href="<%= Route.GetRoute("Name", m.FilmManId) %>" title="<%= m.FilmMan.ToString() %>"><%= m.FilmMan.CnName %></a> <%= (index == writers.Count()-1)?"":" <em>/</em> " %>
                                       <% index++;} %>
                                    </div>
                                    <div class="title">演员 actor</div>
                                    <div class="list">
                                        <% 
                                            index = 0;
                                            var actors = Model.Participarts.Where(x=>x.JobType == UFilm.MovieJobType.Actor);
                                            foreach (var m in actors)
                                            { %>
                                       <a href="<%= Route.GetRoute("Name", m.FilmManId) %>" title="<%= m.FilmMan.ToString() %>"><%= m.FilmMan.CnName %></a> <%= (index == actors.Count()-1)?"":" <em>/</em> " %>
                                      <% index++;} %>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 ">
                    <div id="movie-summary" class="container">
                        <div class="title">剧情 storyline</div>
                        <div class="text"><%= Model.Movie.Introduction%></div>
                    </div>
                    <% if(Model.Torrents != null && Model.Torrents.Count > 0){ %>
                    <div id="torrents">
                        <div class="title">资源 torrents</div>
                        <ul>
                            <%foreach(var t in Model.Torrents){ %>
                            <li><a href="<%= t.Link %>" title="<%= t.Name %> <%= t.Type %> 下载"><%= t.Name %> 【<%= t.Size %>】 <%= t.Type %></a> &nbsp;&nbsp;&nbsp;&nbsp;  <i>分享于 <%= t.CreationTime.ToString("yyyy-MM-dd") %></i> </li> 
                            <%} %>
                        </ul>

                    </div>
                    <%} %>
                </div>
                <% if(Model.Photos.TotalCount>0){ %>
                <div class="col-xs-12" id="photo-title">剧照 photos</div>
                <%} %>
            </div>
            <% if(Model.Photos.TotalCount>0){ %>
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
        </div>
        <%} %>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">
    <script src="/js/controllers/movies/subject.js"></script>
</asp:Content>
