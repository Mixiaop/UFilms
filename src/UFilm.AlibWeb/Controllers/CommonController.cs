using System;
using System.Collections.Generic;
using U;
using U.FakeMvc.Controllers;
using U.Utilities.Web;
using UFilm.Services.Tags;
using UFilm.Services.Adults;
using UFilm.Web;
using UFilm.AlibWeb.Models;
using UFilm.AlibWeb.Models.Movies;


namespace UFilm.AlibWeb.Controllers
{
    public class CommonController : ControllerBase
    {
        IMovieService _movieService = UPrimeEngine.Instance.Resolve<IMovieService>();
        
        #region Index
        [HttpGet]
        public IndexModel IndexView()
        {
            IndexModel Model = new IndexModel();
            return Model;
        }
        #endregion

        #region Search
        public SearchModel SearchView(string wd, int page = 1)
        {
            SearchModel Model = new SearchModel();
            Model.GetKeywords = wd.Trim();
            Model.GetPage = page;

            if (!string.IsNullOrEmpty(Model.GetKeywords))
            {
                //var filmManResult = _filmManService.Search(1, 1, Model.GetKeywords.Trim());
                //if (filmManResult != null && filmManResult.Items.Count > 0)
                //{
                //    Model.SearchFilmMan = filmManResult.Items[0];
                //    Model.SearchFilmManMovies = _filmManService.QueryParticipantMovies(Model.SearchFilmMan.Id, 1, 3);
                //}

                //Model.Movies = _movieQueryService.Search(page, 16, Model.GetKeywords);

                //PagingInfo pageInfo = new PagingInfo();
                //pageInfo.PageIndex = Model.GetPage;
                //pageInfo.PageSize = 16;

                //pageInfo.Url = WebHelper.GetUrl();

                //pageInfo.TotalCount = Model.Movies.TotalCount;
                //Model.PagiHTML = new Paginations(pageInfo).GetPaging();


            }
            return Model;
        }
        #endregion
        #region Movies
        [HttpGet]
        public SubjectModel MoviesSubjectView(int movieId) {
            SubjectModel Model = new SubjectModel();
            Model.Movie = _movieService.Get(movieId);
            Model.Actors = _movieService.GetMovieActors(movieId);
            return Model;
        }
        #endregion
    }
}