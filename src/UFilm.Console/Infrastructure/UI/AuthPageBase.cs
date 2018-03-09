using System;
using U.FakeMvc.UI;
using U.FakeMvc.Controllers;
using U.FakeMvc.Routes;
using U.Utilities.Web;
using UFilm.Console.Models;
using UFilm.Web;

namespace UFilm.Console.UI
{

    public abstract class AuthPageBase<T> : UZeroConsole.Web.AuthPageBase where T : UFilm.Console.Models.ModelBase
    {
        /// <summary>
        /// 前端视图页路由
        /// </summary>
        public RouteContext WebRoutes = RouteContext.Instance;

        public AuthPageBase()
        {
            _model = Activator.CreateInstance<T>();
        }

        T _model;
        protected virtual T Model { get { return _model; } }

        public string GetBackUrlEncoded()
        {
            return "redirectUrl=" + WebHelper.GetThisPageUrl(true).EncodeUTF8Base64();
        }

        public string GetBackUrlDecoded()
        {
            return WebHelper.GetString("redirectUrl").DecodeUTF8Base64();
        }
    }

    public abstract class AuthPageBase<TCtrl, TModel> : UZeroConsole.Web.AuthPageBase<TCtrl, TModel> where TCtrl : U.FakeMvc.Controllers.ControllerBase
    {
        /// <summary>
        /// 前端视图页路由
        /// </summary>
        public RouteContext WebRoutes = RouteContext.Instance;

        public string GetBackUrlEncoded(string defaultName = "redirectUrl")
        {
            return defaultName + "=" + WebHelper.GetThisPageUrl(true).EncodeUTF8Base64();
        }

        public string GetBackUrlDecoded(string defaultUrl = "", string defaultName = "redirectUrl")
        {
            var url = WebHelper.GetString(defaultName).DecodeUTF8Base64();
            if (url.IsNotNullOrEmpty())
            {
                return url;
            }
            else
                return defaultUrl;
        }
    }
}