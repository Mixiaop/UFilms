using System;
using System.Web.UI.WebControls;
using AjaxPro;
using U;
using U.Utilities.Web;
using UFilm.Services.Movies;
using UFilm.Services.Media.Dto;
using UFilm.Console.Models.Movies;
using UFilm.Console.UI;

namespace UFilm.Console.Movies.Movies
{
    public partial class SearchList : UI.AuthPageBase<MoviesListModel>
    {
        IMovieQueryService _movieQueryService = UPrimeEngine.Instance.Resolve<IMovieQueryService>();
        IMovieService _movieService = UPrimeEngine.Instance.Resolve<IMovieService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSearch.Click += btnSearch_Click;
            if (!IsPostBack) {
                BindPageDatas(GetUrlParam(), true);
            }
        }

        protected void DeleteClick(object sender, EventArgs e) {
            LinkButton lb = (LinkButton)sender;
            int id = lb.CommandArgument.ToInt();
            if (id > 0)
            {
                try
                {
                    var m = _movieService.Get(id);
                    _movieService.Delete(m);
                    ltlMessage.Text = AlertSuccess("删除成功", "", 2000);
                    BindPageDatas(GetUrlParam(), false);
                }
                catch (Exception ex)
                {
                    LogError(ex.Message, ex);
                    ltlMessage.Text = AlertError(ex.Message);
                }
            }

            BindPageDatas(GetUrlParam(), true);
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            BindPageDatas(GetUrlParam(), true);
        }

        #region BindPageDatas
        string GetUrlParam()
        {
            string cdi = "";

            if (!string.IsNullOrEmpty(tbSeachKeywords.Text.Trim()))
            {
                cdi += "wd=" + tbSeachKeywords.Text.Trim();
            }

            if (WebHelper.GetString("page") != "")
            {
                if (cdi != "")
                    cdi += "&";

                cdi += "page=" + WebHelper.GetString("page");
            }
            return cdi;
        }

        void BindPageDatas(string url, bool pageInit)
        {
            PagingInfo pageInfo = new PagingInfo();
            pageInfo.PageIndex = pageInit ? WebHelper.GetInt("page", 1) : 1;
            pageInfo.PageSize = 12;
            pageInfo.Url = url == "" ? WebHelper.GetUrl() : "SearchList.aspx?" + url;
            var results = _movieQueryService.Search(pageInfo.PageIndex, pageInfo.PageSize, tbSeachKeywords.Text.Trim());
            rptDatas.DataSource = results.Items;
            rptDatas.DataBind();

            pageInfo.TotalCount = results.TotalCount;

            Model.PagiHtml = new Paginations(pageInfo).GetPaging();
        }

        protected string GetCoverUrl(PictureDto picture)
        {
            if (!string.IsNullOrEmpty(picture.ThumbUrl))
            {
                return string.Format("<a href=\"{1}\" target=\"_blank\"><img src=\"{0}\"  /></a>", picture.ThumbUrl, picture.SourceUrl);
            }

            return "";
        }
        #endregion
    }
}