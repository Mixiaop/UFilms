<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Navigation.master" AutoEventWireup="true" CodeBehind="Discover.aspx.cs" Inherits="UFilm.Web.Discover" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCss" runat="server">
    <link rel="stylesheet" href="/css/views/search.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="wrapper found-list">

        <!--PC条件-->
        <div class="container">
            <div class="cdi-wrap">
                <div class="col-xs-12">
                    <div class="row cdi-box">

                        <ul>
                            <li><a href="<%= Route.GetRoute("Find","")  %>" title="全部">全部</a></li>
                            <% foreach (var t in Model.MovieTypes)
                               { %>
                            <% if (Model.GetMovieTypes.Contains(t.Name))
                               { %>
                            <li class="current"><a href="<%= GetMovieTypeUrl(t.Name) %>" title="<%= t.Name %>"><%= t.Name %> <i>×</i></a></li>
                            <%}
                               else
                               { %>
                            <li><a href="<%= GetMovieTypeUrl(t.Name) %>" title="<%= t.Name %>"><%= t.Name %></a></li>
                            <%} %>
                            <%} %>
                            <% foreach (var t in Model.MovieAreas)
                               { %>
                            <% if (Model.GetMovieAreas.Contains(t.Name))
                               { %>
                            <li class="current"><a href="<%= GetMovieAreaUrl(t.Name) %>" title="<%= t.Name %>"><%= t.Name %> <i>×</i></a></li>
                            <%}
                               else
                               { %>
                            <li><a href="<%= GetMovieAreaUrl(t.Name) %>" title="<%= t.Name %>"><%= t.Name %></a></li>
                            <%} %>
                            <%} %>
                        </ul>
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>

        <div class="container">
            <div class="row">
                <% foreach (var m in Model.Movies.Items)
                   { %>
                <div class="col-xs-6 col-sm-4 col-md-3 col-lg-2 found-list-box" data-id="<%= m.Id %>">
                    <div class="zoom"><i class="si si-magnifier-add"></i></div>
                    <a href="<%= Route.GetRoute("Movies.Subject", m.Id) %>" title="<%= m.FormatName %>" target="_blank">
                        <img src="<%= m.Cover.ThumbUrl %>" class="img-responsive" alt="<%= m.FormatName %>" title="<%= m.FormatName %>" data-id="<%= m.Id %>" /></a>
                    <a href="<%= Route.GetRoute("Movies.Subject", m.Id) %>" title="<%= m.FormatName %>" class="title" target="_blank"><%= m.CnName %> <%= m.EnName %></a>
                    <span><%= m.MovieType.Replace("/","<em>/</em>") %></span>
                </div>
                <%} %>
            </div>
        </div>

        <div class="col-xs-12 text-center">
            <ul class="pagination">
                <%= Model.PagingHTML %>
            </ul>
        </div>

        <!--Model 电影信息-->
        <div class="info-window-wrap">
           
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">
    <script id="tempMovieInfo" type="text/x-handlebars-template">
        <div class="info-window">
            <div class="box">
                <div class="triangle"></div>
                <h3><a href="/subject/{{movie.Id}}" title="{{movie.CnName}} {{movie.EnName}}" target="_blank">{{movie.CnName}} {{movie.EnName}} <span>（{{movie.Year}}）</span></a></h3>
                <ul>
                    <li><span>类型：</span>{{{movie.MovieType}}}</li>
                    <li><span>制片国家/地区：</span> {{{movie.Area}}}</li>
                    <li><span>演员：</span> {{{movie.Actor}}} </li>
                </ul>
                <div class="intro">
                    {{{movie.Introduction}}}
                </div>
            </div>
        </div>
    </script>
    <script>
        require(['jquery', 'handlebars', 'bootstrap'], function ($, handlebars) {
            var $modal = $('.info-window-wrap');

            $('.found-list-box img').on('mouseenter', function () {
                var selfWidth = $(this).width();
                var leftWidth = $(this).offset().left;
                var topHeight = $(this).offset().top - $("body").scrollTop();

                var movieId = parseInt($(this).data('id'));
                setTimeout(function () {
                    MoviesService.Get(movieId, function (res) {
                        var result = res.value;
                        if (result.Success) {
                            var movie = result.Result;
                            console.log(movie);
                            var $html = $('#tempMovieInfo').html();
                            var template = handlebars.compile($html);
                            var $data = template({ movie: movie });
                            $modal.html($data);

                            if (leftWidth < 360) {
                                $modal.removeClass('left');
                                $modal.addClass('right');
                                $modal.css('left', leftWidth + selfWidth - 20);
                            } else {
                                $modal.removeClass('right');
                                $modal.addClass('left');
                                $modal.css('left', leftWidth - 335);
                            }
                            $modal.css('top', topHeight);
                            $modal.addClass('active');
                        }
                    });
                }, 500);
            })

            $('.found-list-box img').on('mouseleave', function () {
                $modal.removeClass('active');
            })

            $modal.on('mouseenter', function () {
                $(this).addClass('active');
            })
            $modal.on('mouseleave', function () {
                $(this).removeClass('active');
            })

        });
    </script>
</asp:Content>
