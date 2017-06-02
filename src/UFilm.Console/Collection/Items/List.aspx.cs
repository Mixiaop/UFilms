using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UFilm.Console.Collection.Items
{
    public partial class List : UI.AuthPageBase<Controllers.CollectionController, Models.Collection.CollectionItemsListModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }
    }
}