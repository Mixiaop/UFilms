﻿using U.Utilities.Web;
using U.FakeMvc.Controllers;
using U.FakeMvc.Routes;

namespace UFilm.Web.UI
{
    public interface IPageCustomAttribute
    {
        string PageTitle { get; set; }

        string SeoMetaKeywords { get; set; }

        string SeoMetaDescription { get; set; }
    }

    public class PageBase<TController, TModel> : U.FakeMvc.UI.PageBase<TController, TModel>, IPageCustomAttribute where TController : ControllerBase
    {

        public RouteContext Route = RouteContext.Instance;

        public PageBase()
        {

        }

        #region IPageCustomAttribute

        private string _title = "";
        public string PageTitle
        {
            get { return _title.IsNotNullOrEmpty() ? (_title + " - 面包卷电影") : "面包卷电影，卷出你的电影态度~"; }
            set
            {
                _title = value;
            }
        }

        private string _seoMetaKeywords;
        private string _seoMetaDescription;
        public string SeoMetaKeywords
        {
            get { return _seoMetaKeywords.IsNotNullOrEmpty() ? _seoMetaKeywords : "电影、经典电影、热映、电视剧、美剧、影评、排行、推荐、下载"; }
            set { _seoMetaKeywords = value; }
        }

        public string SeoMetaDescription
        {
            get { return _seoMetaDescription.IsNotNullOrEmpty() ? _seoMetaDescription : "面包卷电影提供经典的电影、电视剧资源下载，卷出你的电影状态。"; }
            set { _seoMetaDescription = value; }
        }

        #endregion

        #region Method
        /// <summary>
        /// 客服端延时跳转
        /// </summary>
        /// <param name="url">跳转地址</param>
        /// <param name="time">时间1000:1秒</param>
        public void RedirectByTime(string url, int time)
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "RedirectByTime", "<script>setTimeout(function(){window.location='" + url + "';}," + time + ");</script>");
        }

        public string SetUrlParam(string paramName, object paramValue, string defaultUrl = "")
        {
            string url = "";
            if (defaultUrl.IsNullOrEmpty())
            {
                url = WebHelper.GetUrl();
            }
            else
                url = defaultUrl;

            if (paramName.IndexOf("=") == -1)
                paramName += "=";

            if (url.IndexOf(paramName) == -1)
            {
                if (url.IndexOf("?") == -1)
                {
                    url += "?";
                }
                else
                {
                    url += "&";
                }
                url += paramName + paramValue;
            }
            else
            {
                url = System.Text.RegularExpressions.Regex.Replace(url, paramName + @"[\da-z-A-Z]{0,10}", (paramName + paramValue.ToString()));
            }

            return url;
        }


        public void Invoke404()
        {
            Response.Redirect("/404.html");
        }
        #endregion
    }
}