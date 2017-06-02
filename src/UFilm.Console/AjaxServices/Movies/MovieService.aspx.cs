using System;
using System.Collections.Generic;
using AjaxPro;
using U;
using U.Web.Models;
using U.Application.Services.Dto;
using UFilm.Domain.Movies;
using UFilm.Services.Movies;
using UFilm.Services.Movies.Dto;

namespace UFilm.Console.AjaxServices.Movies
{
    [AjaxNamespace("Movies.MovieService")]
    public partial class MovieService : System.Web.UI.Page
    {
        IMovieService _movieService = UPrimeEngine.Instance.Resolve<IMovieService>();
        IMoviePhotoService _moviePhotoService = UPrimeEngine.Instance.Resolve<IMoviePhotoService>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="photoIds"></param>
        /// <param name="photoType">1=剧照 2=海报</param>
        /// <returns></returns>
        [AjaxMethod]
        public AjaxResponse SavePhotos(int movieId, List<int> photoIds, int photoType = 1)
        {
            AjaxResponse res = new AjaxResponse();
            var movie = _movieService.Get(movieId);
            if (movie != null && photoIds != null && photoIds.Count > 0)
            {
                foreach (var pid in photoIds)
                {
                    MoviePhoto photo = new MoviePhoto();
                    photo.PhotoType = (MoviePhotoType)photoType;
                    photo.MovieId = movie.Id;
                    photo.PictureId = pid;
                    _moviePhotoService.Insert(photo);
                }
            }
            return res;
        }

        [AjaxMethod]
        public AjaxResponse DeletePhoto(int photoId)
        {
            AjaxResponse res = new AjaxResponse();
            var photo = _moviePhotoService.Get(photoId);
            if (photo != null)
            {
                _moviePhotoService.Delete(photo);
            }

            return res;
        }
    }
}