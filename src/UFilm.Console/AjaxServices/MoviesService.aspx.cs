using System;
using System.Collections.Generic;
using AjaxPro;
using U;
using U.Web.Models;
using UFilm.Services.Movies;
using UFilm.Services.Movies.Dto;

namespace UFilm.Console.AjaxServices
{
    [AjaxNamespace("MoviesService")]
    public partial class MoviesService : System.Web.UI.Page
    {
        IMovieService _movieService = UPrimeEngine.Instance.Resolve<IMovieService>();
        IMovieTorrentService _movieTorrentService = UPrimeEngine.Instance.Resolve<IMovieTorrentService>();
        IMovieQueryService _movieQueryService = UPrimeEngine.Instance.Resolve<IMovieQueryService>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        [AjaxMethod]
        public AjaxResponse DeleteTorrent(int torrentId)
        {
            AjaxResponse res = new AjaxResponse();
            _movieTorrentService.Delete(torrentId);

            return res;
        }

        [AjaxMethod]
        public AjaxResponse<IList<MovieBriefDto>> Search(string keywords, int count = 5)
        {
            AjaxResponse<IList<MovieBriefDto>> res = new AjaxResponse<IList<MovieBriefDto>>();
            res.Result = _movieQueryService.Search(keywords, count);

            return res;
        }

    }
}