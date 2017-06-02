using System;

namespace UFilm.Web.Movies
{
    public partial class Name : UI.PageBase<Controllers.MoviesController, Models.Movies.NameModel>
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            PageTitle = Model.FilmMan.CnName + " " + Model.FilmMan.EnName;
            SeoMetaKeywords = Model.FilmMan.CnName + "，" + Model.FilmMan.EnName + "、简介、个人资料、图片、电影作品";
            SeoMetaDescription = Model.FilmMan.CnName + "简介、图片写真及电影作品";
        }
    }
}