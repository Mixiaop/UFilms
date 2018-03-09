using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U;
using U.FakeMvc.Routes;
using UFilm.Configuration;

namespace UFilm.Web
{
    public class WebRouteProvider : IRouteProvider
    {
        UFilmSettings _settings = UPrimeEngine.Instance.Resolve<UFilmSettings>();

        #region Url重写
        public void RegisterRewriterRole(RouteContext context)
        {

            context.AddRewriterRule(new RewriterRule("/", "/Index.aspx"));         //首页
            context.AddRewriterRule(new RewriterRule("/search", "/Search.aspx"));  //搜索
            context.AddRewriterRule(new RewriterRule("/find", "/Discover.aspx"));
            

            //电影单页
            context.AddRewriterRule(new RewriterRule("/subject/([0-9-]*)", "/Movies/Subject.aspx?movieId=$1"));
            context.AddRewriterRule(new RewriterRule( "/subject/([0-9-]*)/","/Movies/Subject.aspx?movieId=$1"));


            //影人单页
            context.AddRewriterRule(new RewriterRule("/name/([0-9-]*)", "/Movies/Name.aspx?filmmanId=$1"));
            context.AddRewriterRule(new RewriterRule("/name/([0-9-]*)/", "/Movies/Name.aspx?filmmanId=$1"));

            context.AddRewriterRule(new RewriterRule("/collection", "/Collection/Search.aspx"));
            context.AddRewriterRule(new RewriterRule("/collection/", "/Collection/Search.aspx"));
            context.AddRewriterRule(new RewriterRule("/collection/([0-9-]*)", "/Collection/Subject.aspx?collectionId=$1"));
            context.AddRewriterRule(new RewriterRule("/collection/([0-9-]*)/", "/Collection/Subject.aspx?collectionId=$1"));

            #region Adults
            #endregion

        }
        #endregion

        #region 路由
        public void RegisterRoutes(RouteContext context)
        {
            context.AddRoute(new Route("Index", Format("/")));
            context.AddRoute(new Route("Search", Format("/search?wd={0}&page={1}")));
            context.AddRoute(new Route("Find", Format("/find")));

            context.AddRoute(new Route("Movies.Subject", Format("/subject/{0}")));
            context.AddRoute(new Route("Movies.Name", Format("/name/{0}")));

            context.AddRoute(new Route("Collection.Index", Format("/collection/")));
            context.AddRoute(new Route("Collection.Subject", Format("/collection/{0}")));

            #region Adults
            #endregion
        }

        private string Format(string url)
        {
            var host = _settings.Host.EndsWith("/") ? _settings.Host : _settings.Host + "/";
            url = url.TrimStart('/');
            return host + url;
        }
        #endregion

    }
}
