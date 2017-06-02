using System;
using U;
using UFilm.Web.Models;
using UFilm.Services.Tags;

namespace UFilm.Web
{
    public partial class Index : UI.PageBase<Controllers.CommonController, Models.IndexModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SeoMetaKeywords = "面包卷电影、经典电影、电视剧、美剧、排行、推荐、下载";
            SeoMetaDescription = "面包卷提供经典的电影、电视剧资源，卷出你的电影状态。";
        }

        protected string GetMovieAreaUrl(string name)
        {
            string tagName = string.IsNullOrEmpty(Model.GetMovieArea) ? name : Model.GetMovieArea;
            string url = string.Format("/?t={1}&a={0}",
                                      (Model.GetMovieArea.IsNotNullOrEmpty() && !Model.GetMovieArea.Contains(name) ? (Model.GetMovieArea + "_" + name) : tagName),
                                      Model.GetMovieType);
            return url;
        }

        protected string GetMovieTypeUrl(string name)
        {
            string tagName = string.IsNullOrEmpty(Model.GetMovieType) ? name : Model.GetMovieType;
            string url = string.Format("/?t={0}&a={1}",
                                      (Model.GetMovieType.IsNotNullOrEmpty() && !Model.GetMovieType.Contains(name) ? Model.GetMovieType + "_" + name : tagName),
                                      Model.GetMovieArea);
            return url;
        }
    }
}