using U;
using U.Utilities.Web;
using U.FakeMvc;
using U.FakeMvc.Controllers;
using UFilm.Domain.Movies;
using UFilm.Services.Movies;
using UFilm.Console.Models.Movies;
using UFilm.Console.UI;

namespace UFilm.Console.Controllers
{
    public class MoviesController : ControllerBase
    {
        IMovieService _movieService = UPrimeEngine.Instance.Resolve<IMovieService>();
        IMoviePhotoService _photoService = UPrimeEngine.Instance.Resolve<IMoviePhotoService>();
        IMovieTorrentService _torrentService = UPrimeEngine.Instance.Resolve<IMovieTorrentService>();

        #region Photos
        [HttpGet]
        public MoviePhotosListModel PhotoListView(string keywords, int movieId)
        {
            MoviePhotosListModel model = new MoviePhotosListModel();
            model.GetKeywords = keywords;
            model.GetMovieId = movieId;
            if (model.GetMovieId > 0)
                model.Movie = _movieService.Get(movieId);

            var pagiInfo = new PagingInfo();
            pagiInfo.PageIndex = WebHelper.GetInt("page", 1);
            pagiInfo.PageSize = 16;
            pagiInfo.Url = WebHelper.GetThisPageUrl(true);
            model.Photos = _photoService.Search(pagiInfo.PageIndex, pagiInfo.PageSize, movieId, keywords);
            pagiInfo.TotalCount = model.Photos.TotalCount;

            model.PagingHTML = new Paginations(pagiInfo).GetPaging();
            return model;
        }
        #endregion

        #region Torrents
        [HttpGet]
        public TorrentsListModel TorrentListView(string keywords, int movieId) {
            TorrentsListModel model = new TorrentsListModel();
            model.GetKeywords = keywords;
            model.GetMovieId = movieId;
            if (model.GetMovieId > 0)
                model.Movie = _movieService.Get(movieId);


            model.Torrents = _torrentService.Search(movieId);
            model.Paging(model.Torrents.TotalCount);

            return model;
        }

        [HttpGet]
        public TorrentsAddModel TorrentAddView(int movieId)
        {
            TorrentsAddModel model = new TorrentsAddModel();
            if (movieId > 0)
                model.Movie = _movieService.Get(movieId);

            if (model.Movie == null)
                Invoke404();

            return model;
        }

        public void SaveTorrent(MovieTorrent torrent) {
            if (torrent.Id == 0)
                _torrentService.Insert(torrent);
            
        }
        #endregion
    }
}