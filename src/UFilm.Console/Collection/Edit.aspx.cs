using System;
using UFilm.Domain.Collection;

namespace UFilm.Console.Collection
{
    public partial class Edit : UI.AuthPageBase<Controllers.CollectionController, Models.Collection.CollectionEditModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Click += btnSave_Click;
            if (!IsPostBack) {
                tbName.Text = Model.Collection.Name;
                tbCount.Text = Model.Collection.Count.ToString();
                tbIntroduction.Text = Model.Collection.Introduction;
                hidCoverId.Value = Model.Collection.CoverId.ToString();
                hidCoverUrl.Value = Model.Collection.Cover.ThumbUrl;
            }
            Model.OldTags = Model.Collection.Tags;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            Model.Collection.Name = tbName.Text.Trim();
            Model.Collection.Introduction = tbIntroduction.Text.Trim();
            Model.Collection.CoverId = hidCoverId.Value.ToInt();
            Model.Collection.Tags = tbTags.Text.Trim();
            Model.Collection.Count = tbCount.Text.ToInt();
            Controller.UpdateCollection(Model);
            ltlMessage.Text = AlertSuccess("保存成功");
            RedirectByTime(GetBackUrlDecoded(), 1000);
        }
    }
}