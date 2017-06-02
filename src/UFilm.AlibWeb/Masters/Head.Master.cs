using System;

namespace UFilm.AlibWeb.Masters
{
    public partial class Head : UI.MasterPageBase
    {
        public UI.IPageCustomAttribute PageAttr;
        protected void Page_Load(object sender, EventArgs e)
        {
            PageAttr = (UI.IPageCustomAttribute)this.Page;
        }
    }
}