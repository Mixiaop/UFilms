using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U;
using U.Utilities.Web;
using UFilm.Domain.Collection;
using UFilm.Services.Collection;
using UFilm.Services.Movies;
using UFilm.Console.Controllers;
using UFilm.Console.Models.Collection;

namespace UFilm.Console.Collection
{
    public partial class Add : UI.AuthPageBase<CollectionController, CollectionAddModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Click += btnSave_Click;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            var collection = new MovieCollection();
            collection.Name = tbName.Text.Trim();
            collection.Introduction = tbIntroduction.Text.Trim();
            collection.CoverId = hidCoverId.Value.ToInt();
            collection.Tags = tbTags.Text.Trim();
            Controller.AddCollection(collection);
            ltlMessage.Text = AlertSuccess("保存成功");
            RedirectByTime(GetBackUrlDecoded(), 1000);
        }
    }
}