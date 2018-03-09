using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AjaxPro;
using U;
using U.AutoMapper;
using U.Web.Models;
using U.FakeMvc.Controllers;
using U.Application.Services.Dto;
using UFilm.Domain.Movies;
using UFilm.Services.Movies;
using UFilm.Services.Movies.Dto;

namespace UFilm.Web.Controllers
{
    [AjaxNamespace("MoviesService")]
    public class MovieAjaxController : AjaxControllerBase
    {
        IMovieQueryService _movieQueryService = UPrimeEngine.Instance.Resolve<IMovieQueryService>();
        IMovieService _movieService = UPrimeEngine.Instance.Resolve<IMovieService>();

        [AjaxMethod]
        public AjaxResponse<PagedResultDto<MovieBriefDto>> QueryLastActivityMovies(int pageIndex = 1, int pageSize = 16, string keywords = "", string movieTypes = "", string movieAreas = "")
        {
            AjaxResponse<PagedResultDto<MovieBriefDto>> res = new AjaxResponse<PagedResultDto<MovieBriefDto>>();

            List<string> types = new List<string>();
            List<string> areas = new List<string>();

            if (movieTypes.IsNotNullOrEmpty())
            {
                foreach (var t in movieTypes.Trim().Split('_'))
                {
                    if (t.IsNotNullOrEmpty())
                        types.Add(t.Trim());
                }
            }

            if (movieAreas.IsNotNullOrEmpty())
            {
                foreach (var t in movieAreas.Trim().Split('_'))
                {
                    if (t.IsNotNullOrEmpty())
                        areas.Add(t.Trim());
                }
            }

            try
            {

                var result = _movieQueryService.QueryLastActivityMovies(pageIndex, pageSize, keywords, types, areas);

                res.Result = result;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Error = new ErrorInfo(ex.Message);
            }
            return res;
        }

        [AjaxMethod]
        public AjaxResponse<MovieDto> Get(int movieId)
        {
            AjaxResponse<MovieDto> res = new AjaxResponse<MovieDto>();
            var movie = _movieService.Get(movieId);
            movie.Introduction = movie.Introduction.StripHtml().GetSubString(0, 120, "..");
            movie.Area = movie.Area.Replace("/", "<em>/</em>");
            movie.MovieType = movie.MovieType.Replace("/", "<em>/</em>");
            movie.Actor = movie.Actor.Replace("/", "<em>/</em>");
            res.Result = movie.MapTo<MovieDto>();

            return res;
        }
    }
}