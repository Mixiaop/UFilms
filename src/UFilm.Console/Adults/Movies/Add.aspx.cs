using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UFilm.Domain.Adults;

namespace UFilm.Console.Adults.Movies
{
    public partial class Add : UI.AuthPageBase<Controllers.AdultsController, Models.Adults.MoviesAddModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Click += btnSave_Click;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            #region 提交添加
            var controller = Controller.Current<Controllers.AdultsController>();
            Model.Movie = new LMovie();

            Model.Movie.Code = tbCode.Text.Trim();
            Model.Movie.CnName = tbCnName.Text.Trim();
            Model.Movie.Year = tbYear.Text.Trim().ToInt();
            Model.Movie.OtherPostYear = tbOtherPostYear.Text.Trim();
            Model.Movie.MovieType = tbMovieType.Text.Trim();
            Model.Movie.MovieLength = tbMovieLength.Text.Trim();
            Model.Movie.Area = tbArea.Text.Trim();
            Model.Movie.Language = tbLanguage.Text.Trim();
            Model.Movie.Introduction = tbIntroduction.Text.Trim();
            Model.Movie.OtherEnName = tbOtherEnName.Text.Trim();
            Model.Movie.Hits = tbHits.Text.Trim().ToInt();
            Model.Movie.PhotoCount = tbPhotoCount.Text.Trim().ToInt();
            Model.Movie.CoverId = hfPictureId.Value.ToInt();

            List<Actor> actors = new List<Actor>();
            if (hfActors.Value.IsNotNullOrEmpty()) {
                var actorList = hfActors.Value.Split(',');
                if (actorList.Length > 0) {
                    foreach (var actorStr in actorList) {
                        var item = actorStr.Split(':');
                        var info = controller.ActorService.Get(item[0].ToInt());
                        actors.Add(info);
                    }
                }
            }

            if (Model.Movie.Code.IsNullOrEmpty()) {
                ltlMessage.Text = AlertError("番号不能为空", "", 3000);
                return;
            }
            
            if (controller.MovieService.Exists(Model.Movie.Code)) {
                ltlMessage.Text = AlertError("番号已存在", "", 3000);
                return;
            }

            Controller.MovieService.Insert(Model.Movie, actors);
            ltlMessage.Text = AlertSuccess("添加成功");
            RedirectByTime(GetBackUrlDecoded(), 1000);
            #endregion
        }
    }
}