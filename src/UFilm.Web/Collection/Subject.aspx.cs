using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UFilm.Web.Collection
{
    public partial class Subject : UI.PageBase<Controllers.CollectionController, Models.Collection.SubjectModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageTitle = Model.Collection.Name;

            SeoMetaKeywords = Model.Collection.Name + "、合集、影集、电影精选集、资源";
            SeoMetaDescription = Model.Collection.Name + "的合集、影集、精选集资源";
        }
    }
}