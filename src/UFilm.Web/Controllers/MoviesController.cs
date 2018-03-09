using U;
using U.FakeMvc;
using U.FakeMvc.Controllers;
using U.Utilities.Web;
using UFilm.Services.Tags;
using UFilm.Services.Movies;
using UFilm.Web.Models.Movies;

namespace UFilm.Web.Controllers
{
    public class MoviesController : ControllerBase
    {
        ITagService _tagService = UPrimeEngine.Instance.Resolve<ITagService>();

        IMovieQueryService _movieQueryService = UPrimeEngine.Instance.Resolve<IMovieQueryService>();
        IMovieService _movieService = UPrimeEngine.Instance.Resolve<IMovieService>();
        IMoviePhotoService _photoService = UPrimeEngine.Instance.Resolve<IMoviePhotoService>();
        IMovieTorrentService _torrentService = UPrimeEngine.Instance.Resolve<IMovieTorrentService>();

        IFilmManService _filmManService = UPrimeEngine.Instance.Resolve<IFilmManService>();
        IFilmManPhotoService _filmManPhotoService = UPrimeEngine.Instance.Resolve<IFilmManPhotoService>();

        [HttpGet]
        public SubjectModel SubjectView(int movieId)
        {
            SubjectModel Model = new SubjectModel();
            Model.Movie = _movieService.Get(movieId);

            if (Model.Movie == null)
                Invoke404();

            Model.Participarts = _movieService.GetParticipants(movieId);
            Model.Torrents = _torrentService.GetTorrents(movieId);
            Model.Photos = _photoService.Search(1, 99, movieId, "");

            if (Model.Movie.IsSeries == 1)
                Model.SeriesMovies = _movieService.GetMoviesBySeries(Model.Movie.SeriesId);
            return Model;
        }

        [HttpGet]
        public NameModel NameView(int filmmanId)
        {
            NameModel Model = new NameModel();
            Model.FilmMan = _filmManService.Get(filmmanId);
            if (Model.FilmMan == null)
                Invoke404();

            Model.Photos = _filmManPhotoService.Search(filmmanId, 1, 99);

            Model.JoinMovies = _filmManService.QueryParticipantMovies(filmmanId, 1, 12);

            return Model;
        }
    }
}