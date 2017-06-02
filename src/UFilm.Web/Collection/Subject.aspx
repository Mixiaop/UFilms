<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Navigation.master" AutoEventWireup="true" CodeBehind="Subject.aspx.cs" Inherits="UFilm.Web.Collection.Subject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCss" runat="server">
    <style>
        .workitems {
            margin-top: 15px;
        }

        .workitem {
            float: left;
            margin-left: 25px;
            height: 310px;
        }

            .workitem img {
                max-width: 166px;
            }

            .workitem > a.img {
                display: block;
            }

            .workitem > a.title {
                display: block;
                text-align: center;
                margin-top: 10px;
                color: #000;
            }

            .workitem span {
                color: #888;
                font-size: 12px;
                display: block;
                text-align: center;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <div class="wrapper">

        <div class="container">
            <div class="row">
                <div class="col-xs-12 mb15">
                    <h2 class="h2-title"><%= Model.Collection.Name %> <span>（<%= Model.Collection.Count %> 部）</span></h2>
                </div>
            </div>
        </div>

        <div class="container">
            <div class="col-xs-12 detail-wrap">
                <div class="row">
                    <div class="col-lg-3 col-xs-12 detail-pic">
                        <div class="row">
                            <div class="pd18-20 text-center bdm">
                                <img src="<%= Model.Collection.Cover.ThumbUrl %>" width="180" />
                            </div>
                            <div class="col-xs-12 ">
                                <div class="row">
                                    <div class="pd18-20 detail-type">
                                        <ul>
                                            <li><%= Model.Collection.Introduction %></li>
                                        </ul>
                                    </div>

                                </div>
                            </div>

                        </div>
                    </div>

                    <div class="detail-con-wrap col-lg-9 col-xs-12" >
                        <div class="shadow"></div>

                        <div class="detail-con">
                            <div class="row">

                                <div class="col-xs-12">
                                    <div class="row">
                                        <div class="pd18-20">
                                            <div class="row">
                                                <% foreach (var m in Model.Items)
                                                   { %>
                                                <div class="workitems remove-padding col-xs-6 col-lg-3 ">
                                                    <div class="workitem">
                                                        <a href="<%= Route.GetRoute("Movies.Subject", m.MovieId) %>" class="img" title="<%= m.Movie.FormatName %>" target="_blank">
                                                            <img src="<%= m.Movie.Cover.ThumbUrl %>" alt="<%= m.Movie.FormatName %>"></a>
                                                        <a href="<%= Route.GetRoute("Movies.Subject", m.MovieId) %>" class="title" title="<%= m.Movie.FormatName %>" target="_blank"><%= m.Movie.CnName %></a>
                                                        <span><%= m.Title %></span>
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

    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="foot" runat="server">
</asp:Content>
