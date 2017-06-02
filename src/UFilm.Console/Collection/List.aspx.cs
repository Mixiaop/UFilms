using System;

namespace UFilm.Console.Collection
{
    public partial class List : UI.AuthPageBase<Controllers.CollectionController, Models.Collection.CollectionListModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                rptDatas.DataSource = Model.Results.Items;
                rptDatas.DataBind();
            }
        }
    }
}