using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U;
using U.Utilities.Web;
using UFilm.Console.Models.Movies;
using UFilm.Services.Movies;

namespace UFilm.Console.Movies.Movies
{
    public partial class EditMovie : UI.AuthPageBase<MoviesEditModel>
    {
        IMovieService _movieService = UPrimeEngine.Instance.Resolve<IMovieService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Click += btnSave_Click;
            Model.Movie = _movieService.Get(Model.GetMovieId);
            if (Model.Movie == null)
                Response.Redirect(GetBackUrlDecoded());

            if (!IsPostBack) {
                tbMoreEnName.Text = Model.Movie.OtherEnName;
                tbArea.Text = Model.Movie.Area;
                tbYear.Text = Model.Movie.Year.ToString();
                tbOtherPostYear.Text = Model.Movie.OtherPostYear;
                tbLength.Text = Model.Movie.MovieLength;
                tbIntroduction.Text = Model.Movie.Introduction;
                hfPictureThumb.Value = Model.Movie.Cover.ThumbUrl;
                hfPictureId.Value = Model.Movie.CoverId.ToString();
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            #region 保存
            bool isChangeCover = false;
            if (hfPictureId.Value.ToInt() != Model.Movie.CoverId)
                isChangeCover = true;

            Model.Movie.OtherEnName = tbMoreEnName.Text.Trim();
            Model.Movie.Area = tbArea.Text.Trim();
            Model.Movie.Year = tbYear.Text.ToInt();
            Model.Movie.OtherPostYear = tbOtherPostYear.Text.Trim();
            Model.Movie.MovieLength = tbLength.Text.Trim();
            Model.Movie.Introduction = tbIntroduction.Text.Trim();
            Model.Movie.CoverId = hfPictureId.Value.ToInt();
            _movieService.Update(Model.Movie, isChangeCover);

            ltlMessage.Text = AlertSuccess("保存成功");
            RedirectByTime(GetBackUrlDecoded(), 1000);
            #endregion
        }
    }
}