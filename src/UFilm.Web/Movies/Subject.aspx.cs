using System;

namespace UFilm.Web.Movies
{
    public partial class Subject : UI.PageBase<Controllers.MoviesController, Models.Movies.SubjectModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageTitle = Model.Movie.CnName + " " + Model.Movie.EnName + (Model.Movie.Year > 0 ? " " + Model.Movie.Year + "" : "");
            SeoMetaKeywords = Model.Movie.CnName + "、" + Model.Movie.EnName + "、剧情介绍、电影图片、电影下载、电影资源";
            SeoMetaDescription = Model.Movie.CnName + "电影简介和剧情介绍，" + Model.Movie.CnName + "、图片、下载、资源";
        }
    }
}