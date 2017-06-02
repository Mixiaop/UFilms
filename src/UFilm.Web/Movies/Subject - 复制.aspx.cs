using System;
using U;
using U.Utilities.Web;
using UFilm.Domain.Movies;
using UFilm.Services.Movies;
using UFilm.Web.Models.Movies;

namespace UFilm.Web.Movies
{
    public partial class Subject : UI.PageBase<SubjectModel>
    {
        IMovieService _movieService = UPrimeEngine.Instance.Resolve<IMovieService>();
        IMoviePhotoService _photoService = UPrimeEngine.Instance.Resolve<IMoviePhotoService>();
        IMovieTorrentService _torrentService = UPrimeEngine.Instance.Resolve<IMovieTorrentService>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Model.Movie = _movieService.Get(WebHelper.GetInt("movieId", 0));
            if (Model.Movie == null)
                Invoke404();

            Model.Participarts = _movieService.GetParticipants(Model.GetMovieId);
            Model.Torrents = _torrentService.GetTorrents(Model.GetMovieId);
            Model.Photos = _photoService.Search(1, 99, Model.GetMovieId, "");

            if (Model.Movie.IsSeries == 1)
                Model.SeriesMovies = _movieService.GetMoviesBySeries(Model.Movie.SeriesId);
        }
    }
}