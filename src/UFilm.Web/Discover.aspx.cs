using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UFilm.Web
{
    public partial class Discover : UI.PageBase<Controllers.CommonController, Models.DiscoverModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageTitle = "找电影";
        }


        protected string GetMovieAreaUrl(string name)
        {
            string tagName = string.IsNullOrEmpty(Model.GetMovieArea) ? name : Model.GetMovieArea;
            string url = Route.GetRoute("Find").Value;

            if (Model.GetMovieAreas.Contains(name))
            {
                string tags = string.Empty;
                foreach (var info in Model.GetMovieAreas)
                {

                    if (name != info)
                    {
                        tags += info + "_";
                    }
                    tags = tags.TrimEnd('_');
                }

                url += string.Format("?t={1}&a={0}", tags, Model.GetMovieType);
            }
            else
            {
                url = string.Format("?t={1}&a={0}",
                                        (Model.GetMovieArea.IsNotNullOrEmpty() && !Model.GetMovieArea.Contains(name) ? (Model.GetMovieArea + "_" + name) : tagName),
                                        Model.GetMovieType);
            }

            
            return url;
        }

        protected string GetMovieTypeUrl(string name)
        {
            string tagName = string.IsNullOrEmpty(Model.GetMovieType) ? name : Model.GetMovieType;
            string url = Route.GetRoute("Find").Value;

            if (Model.GetMovieTypes.Contains(name))
            {
                string tags = string.Empty;
                foreach (var info in Model.GetMovieTypes) {

                    if (name != info)
                    {
                        tags += info + "_";
                    }
                    tags = tags.TrimEnd('_');
                }

                url += string.Format("?t={0}&a={1}", tags, Model.GetMovieArea);
            }
            else
            {
                url += string.Format("?t={0}&a={1}",
                                           (Model.GetMovieType.IsNotNullOrEmpty() && !Model.GetMovieType.Contains(name) ? Model.GetMovieType + "_" + name : tagName),
                                           Model.GetMovieArea);
            }
            return url;
        }
    }
}