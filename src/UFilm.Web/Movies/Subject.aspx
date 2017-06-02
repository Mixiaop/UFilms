<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Navigation.master" AutoEventWireup="true" CodeBehind="Subject.aspx.cs" Inherits="UFilm.Web.Movies.Subject" %>

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
                    <h2 class="h2-title"><%= Model.Movie.CnName%> <%= Model.Movie.EnName%> <span>(<%= Model.Movie.Year%>)</span></h2>
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
                                        <ul>
                                            <li><span>类型：</span> <%=  Model.Movie.MovieType.Replace("/","<em>/</em>")%></li>
                                            <li><span>制片国家/地区：</span> <%= Model.Movie.Area.Replace("/","<em>/</em>")%></li>
                                            <li><span>语言：</span> <%= Model.Movie.Language.Replace("/","<em>/</em>")%></li>
                                            <% if (Model.Movie.IsSeries == 1)
                                               { %>
                                            <% if(Model.Movie.OtherPostYear.IsNotNullOrEmpty()){ %>
                                            <li><span>首播时间:</span> <%= Model.Movie.OtherPostYear%></li>
                                            <%} %>
                                            <% if(Model.Movie.SeriesCount==0){ %>
                                            <li><span>总集数:</span> <%= Model.Movie.SeriesCount%>集</li>
                                            <%} %>
                                            <% if(Model.Movie.SeriesLength.IsNotNullOrEmpty()){ %>
                                            <li><span>单集片长:</span> <%= Model.Movie.SeriesLength%></li>
                                            <%} %>
                                            <%}
                                               else
                                               { 
                                                   if(Model.Movie.OtherPostYear.IsNotNullOrEmpty()){
                                                   %>
                                            <li><span>上映日期：</span> <%= Model.Movie.OtherPostYear.Replace("/","<em>/</em>")%></li>
                                            <%} %>
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
                                                   { %><div class="btn-group series">
                                                   <a class="btn btn-default dropdown-toggle" data-toggle="dropdown">第 <%= Model.Movie.SeriesNumber %> 季 <span class="caret"></span>
                                                   </a>
                                                   <ul class="dropdown-menu" role="menu">
                                                       <% foreach (var m in Model.SeriesMovies)
                                                          { %>
                                                       <li><a href="<%= Route.GetRoute("Movies.Subject", m.Id) %>" title="<%= m.FormatName %>">第 <%= m.SeriesNumber %> 季</a></li>
                                                       <%} %>
                                                       <%--<%=SeriesNumberList %>--%>
                                                   </ul>
                                               </div>
                                                <%} %>
                                            </li>
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

                                <div class="col-xs-12 col-lg-12 bdm">
                                    <div class="row">
                                        <div class="col-lg-5 col-xs-12">
                                            <div class="row">
                                                <div class="pd18-20 detail-people">
                                                    <% var index = 0;
                                                       var directors = Model.Participarts.Where(x => x.JobType == UFilm.MovieJobType.Director); %>
                                                    <% if(directors.Count()>0){ %>
                                                    <div class="title">导演 director</div>
                                                    <div class="list">
                                                        <% 
                                                            
                                                            foreach (var m in directors)
                                                            { %>
                                                        <a href="<%= Route.GetRoute("Movies.Name", m.FilmManId) %>" title="<%= m.FilmMan.ToString() %>"><%= m.FilmMan.CnName %></a> <%= (index == directors.Count()-1)?"":" <em>/</em> " %>
                                                        <% index++;
                                                            } %>
                                                    </div>
                                                    <%}
                                                       index = 0;
                                                       var writers = Model.Participarts.Where(x => x.JobType == UFilm.MovieJobType.ScreenWriter);
                                                       if(writers.Count() > 0){
                                                       %>
                                                    <div class="title">编剧 writer</div>
                                                    <div class="list">
                                                        <% 
                                                            
                                                            foreach (var m in writers)
                                                            { %>
                                                        <a href="<%= Route.GetRoute("Movies.Name", m.FilmManId) %>" title="<%= m.FilmMan.ToString() %>"><%= m.FilmMan.CnName %></a> <%= (index == writers.Count()-1)?"":" <em>/</em> " %>
                                                        <% index++;
                                                            } %>
                                                    </div>
                                                    <%}
                                                       index = 0;
                                                       var actors = Model.Participarts.Where(x => x.JobType == UFilm.MovieJobType.Actor);
                                                       if(actors.Count()>0){
                                                      %>
                                                    <div class="title">演员 actor</div>
                                                    <div class="list">
                                                        <% 
                                                            
                                                            foreach (var m in actors)
                                                            { %>
                                                        <a href="<%= Route.GetRoute("Movies.Name", m.FilmManId) %>" title="<%= m.FilmMan.ToString() %>"><%= m.FilmMan.CnName %></a> <%= (index == actors.Count()-1)?"":" <em>/</em> " %>
                                                        <% index++;
                                                            } %>
                                                    </div>
                                                    <%} %>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-lg-7 col-xs-12">
                                            <div class="row">
                                                <div class="pd18-20 torrent">
                                                    <h6 class="h6-title">资源 torrents</h6>
                                                    <% if (Model.Torrents != null && Model.Torrents.Count > 0)
                                                       { %>
                                                    <ul>
                                                        <%foreach (var t in Model.Torrents)
                                                          { %>
                                                        <li><a href="<%= t.Link %>" title="<%= t.Name %> <%= t.Type %> 下载"><%= t.Name %> <% if(t.Size.IsNotNullOrEmpty()){ %>【<%= t.Size %>】<%} %> <% if(t.Type.IsNotNullOrEmpty()){ %>【<%= t.Type %>】<%} %></a> &nbsp;&nbsp;&nbsp;&nbsp;  <i>分享于 <%= t.CreationTime.ToString("yyyy-MM-dd") %></i> </li>
                                                        <%} %>
                                                    </ul>
                                                    <%} %>
                                                </div>

                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="pd18-20 bdm">
                                            <h6 class="h6-title">剧情 storyline</h6>
                                            <div>
                                                <%= Model.Movie.Introduction%>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xs-12 detail-photo">
                                    <div class="row">
                                        <div class="pd18-20">
                                            <h6 class="h6-title">剧照 photos</h6>
                                            <% if (Model.Photos.TotalCount > 0)
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
                                            <%} %>
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
