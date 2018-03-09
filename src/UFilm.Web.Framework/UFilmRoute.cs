using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using U;
using UFilm.Configuration;

namespace UFilm.Web
{
    public class UFilmRoute
    {
        private IList<Route> _routes = new List<Route>();
        private UFilmSettings _settings = UPrimeEngine.Instance.Resolve<UFilmSettings>();

        public UFilmRoute()
        {
            _routes.Add(new Route("Index", Format("/")));
            _routes.Add(new Route("Search", Format("/search?wd={0}&p={1}")));
            _routes.Add(new Route("Subject", Format("/subject/{0}")));
            _routes.Add(new Route("Name", Format("/name/{0}")));
           
        }

        public string GetRoute(string routeName, params object[] paramValues)
        {
            string url = "";
            var route = _routes.Where(x => x.Key == routeName).FirstOrDefault();
            if (route == null)
                throw new Exception("通过 [" + routeName + "] 未找到路由，请检查是否正确");

            try
            {
                url = string.Format(route.Value, paramValues);

                switch (routeName)
                {
                    case "Search":

                        break;
                    case "Subject":
                        break;
                }
            }
            catch (Exception ex) {
                throw new Exception(ex.Message + " 【" + routeName + "】");
            }


            return url;
        }

        public string GetRouteUrl(string routeName)
        {
            var route = _routes.Where(x => x.Key == routeName).FirstOrDefault();
            if (route == null)
                throw new Exception("通过 [" + routeName + "] 未找到路由，请检查是否正确");

            return route.Value;
        }

        private string Format(string url) {
            var host = _settings.Host.EndsWith("/") ? _settings.Host : _settings.Host + "/";
            url = url.TrimStart('/');
            return host + url;
        }
    }

    public class Route
    {
        public Route() { }
        public Route(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}