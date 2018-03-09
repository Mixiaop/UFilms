using System;

namespace UFilm.Console.Adults.Actors
{
    public partial class Add : UI.AuthPageBase<Controllers.AdultsController, Models.Adults.ActorsAddModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Click += btnSave_Click;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            #region 添加提交
            Model.Actor = new Domain.Adults.Actor();
            Model.Actor.CnName = tbCnName.Text.Trim();
            Model.Actor.EnName = tbEnName.Text.Trim();
            Model.Actor.Pinyin = tbPinyin.Text.Trim();
            Model.Actor.Gender = ddlSex.SelectedValue.ToInt();
            Model.Actor.Constellation = tbConstellation.Text.Trim();
            Model.Actor.Birthday = tbBirthday.Text.Trim();
            Model.Actor.PlaceOfBirth = tbPlaceOfBirth.Text.Trim();
            Model.Actor.Introduction = tbIntroduction.Text.Trim();
            Model.Actor.AvatarId = hfPictureId.Value.ToInt();
            Model.Actor.Job = tbJob.Text.Trim();
            Model.Actor.MoreCnName = tbMoreCnName.Text.Trim();
            Model.Actor.MoreEnName = tbMoreEnName.Text.Trim();
            Model.Actor.WebSite = tbWebSite.Text.Trim();
            
            Controller.Current<Controllers.AdultsController>().ActorService.Insert(Model.Actor);

            ltlMessage.Text = AlertSuccess("添加成功");
            RedirectByTime(GetBackUrlDecoded(), 1000);
            #endregion
        }
    }
}