using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UFilm.Console.Adults.Actors
{
    public partial class Edit : UI.AuthPageBase<Controllers.AdultsController, Models.Adults.ActorsEditModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Click += btnSave_Click;
            if (!IsPostBack) {
                tbCnName.Text = Model.Actor.CnName;
                tbEnName.Text = Model.Actor.EnName;
                tbPinyin.Text = Model.Actor.Pinyin;
                ddlSex.SelectedValue = Model.Actor.Gender.ToString();
                tbConstellation.Text = Model.Actor.Constellation;
                tbBirthday.Text = Model.Actor.Birthday;
                tbPlaceOfBirth.Text = Model.Actor.PlaceOfBirth;
                tbIntroduction.Text = Model.Actor.Introduction;
                hfPictureId.Value = Model.Actor.AvatarId.ToString();
                hfPictureThumb.Value = Model.Actor.Avatar.ThumbUrl;
                tbJob.Text = Model.Actor.Job;
                tbMoreCnName.Text = Model.Actor.MoreCnName;
                tbMoreEnName.Text = Model.Actor.MoreEnName;
                tbWebSite.Text = Model.Actor.WebSite;
                tbMovieCount.Text = Model.Actor.MovieCount.ToString();
                tbPhotoCount.Text = Model.Actor.PhotoCount.ToString();
            }
            Model.AvatarId = Model.Actor.AvatarId;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            #region 编辑保存
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
            Model.Actor.MovieCount = tbMovieCount.Text.ToInt();
            Model.Actor.PhotoCount = tbPhotoCount.Text.ToInt();

            if (Model.Actor.Id > 0)
            {
                Controller.Current<Controllers.AdultsController>().ActorService.Update(Model.Actor, (Model.AvatarId != Model.Actor.AvatarId));
                ltlMessage.Text = AlertSuccess("保存成功");
                RedirectByTime(GetBackUrlDecoded(), 1000);
            }
            #endregion
        }
    }
}