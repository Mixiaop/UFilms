using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UFilm.Web.Collection
{
    public partial class Search : UI.PageBase<Controllers.CollectionController, Models.Collection.SearchModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageTitle = "影集";

            SeoMetaKeywords = "电影合集、影集、电影精选集、资源";
            SeoMetaDescription = "电影合集、影集、精选集的资源";
        }
    }
}