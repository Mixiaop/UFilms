<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Navigation.master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="UFilm.Web.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCss" runat="server">
    <link rel="stylesheet" href="/css/views/index.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="wrapper page-index">
        <input type="hidden" id="hidGetMovieTypes" value="<%= Model.GetMovieType %>" />
        <input type="hidden" id="hidGetMovieAreas" value="<%= Model.GetMovieArea %>" />
        <div class="cdi-container">
            <div class="container">
                <div class="row">
                    <div class="col-lg-3">
                            <ul>
                                <li><a href="/" class="active">资源</a></li>
                                <li><a href="javascript:;" id="chooseCdi" class="end">地区 <span>/</span> 类型</a></li>
                            </ul>
                    </div>
                    <div class="col-lg-9 selected-tags">
                            <%= Model.SelectedTagsHTML %>
                    </div>
                    <div class="clear"></div>
                </div>
                <div class="row items">
                    <div class="col-lg-12">
                            <% foreach (var t in Model.MovieTypes)
                               { %>
                            <a href="<%= GetMovieTypeUrl(t.Name) %>" title="<%= t.Name %>"><%= t.Name %></a>
                            <%} %>
                         <% foreach (var t in Model.MovieAreas)
                               { %>
                            <a href="<%= GetMovieAreaUrl(t.Name) %>" title="<%= t.Name %>"><%= t.Name %></a>
                            <%} %>
                    </div>
                    <%--<div class="col-lg-12">
                           
                    </div>--%>
                </div>
            </div>
            <div class="clearfix"></div>
        </div>
        <div class="container movie-container">
            <div class="row movies">
                <!-- MovieItem-->
                <%--<div class="col-xs-12 col-sm-6 col-md-3 col-lg-2 movie-item">
                                <div class="block">
                                    <img class="img-responsive" src="/img/t1.jpg" alt="">
                                    <div class="block-content-full" >
                                        <div class="title"><a>龙虎少年队2</a></div>
                                        <p class="text-gray-dark summary">香港 / 剧情 / 奇幻 </p>
                                    </div>
                                </div>
                            </div>--%>
                <!-- MovieItem-->

            </div>

        </div>
        <div class="container loading hide">
            <div class="col-lg-12 text-center">
                    <img src="/img/loading.gif" />
            </div>
        </div>
        <div class="container" id="loadOffset"></div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">
    <script id="tempMovieItem" type="text/x-handlebars-template">
            {{#each movies}}
                 <div class="col-xs-6 col-sm-4 col-md-3 col-lg-2 movie-item mbox" data-url="/subject/{{Id}}">
                     <div class="mbox-inner">
                         <div class="block mbox-front">
                             <img class="img-responsive" src="{{Cover.ThumbUrl}}" alt="{{FormatName}}" title="{{FormatName}}">
                             <div class="block-content-full">
                                 <div class="title"><a title="{{FormatName}}">{{CnName}} {{EnName}}</a></div>
                                 <p class="text-gray-dark summary">{{MovieType}}</p>
                             </div>
                         </div>
                         <div class="block movie-item-summary mbox-back">
                             <a href="/subject/{{Id}}" class="stitle" title="{{FormatName}}" target="_blank">{{CnName}} {{EnName}} &nbsp;<i>{{Year}}</i></a>
                             <div class="length">{{OtherEnName}}</div>
                             <div class="length">{{MovieLength}} / {{Area}}</div>
                             <div class="director">导演 &nbsp;{{Director}}</div>
                             <div class="actor">演员 &nbsp;{{Actor}}</div>
                         </div>
                         <div class="mbox-back-t"></div>
                         <div class="mbox-back-b"></div>
                     </div>
                 </div>
            {{/each}}
    </script>
    <script src="/js/controllers/index.js"></script>
</asp:Content>
