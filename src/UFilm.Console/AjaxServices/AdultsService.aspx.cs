using System;
using System.Collections.Generic;
using AjaxPro;
using U;
using U.Web.Models;
using UFilm.Services.Adults;
using UFilm.Services.Adults.Dto;

namespace UFilm.Console.AjaxServices
{
    [AjaxNamespace("AdultsService")]
    public partial class AdultsService : System.Web.UI.Page
    {
        IActorService _actorService = UPrimeEngine.Instance.Resolve<IActorService>();
        IActorPhotoService _actorPhotoService = UPrimeEngine.Instance.Resolve<IActorPhotoService>();
        IMovieService _movieService = UPrimeEngine.Instance.Resolve<IMovieService>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Movies
        [AjaxMethod]
        public AjaxResponse DeleteMovie(int movieId)
        {
            AjaxResponse res = new AjaxResponse();
            try
            {
                _movieService.Delete(movieId);
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Error.Message = ex.Message;
            }
            return res;
        }

        [AjaxMethod]
        public AjaxResponse<int> ExistsMovie(string code) {
            AjaxResponse<int> res = new AjaxResponse<int>();

            res.Result = _movieService.Exists(code) ? 1 : 0;

            return res;
        }
        #endregion

        #region Actors
        [AjaxMethod]
        public AjaxResponse<IList<ActorBriefDto>> SearchActors(string keywords, int count = 10) {
            AjaxResponse<IList<ActorBriefDto>> res = new AjaxResponse<IList<ActorBriefDto>>();
            res.Result = _actorService.GetAll(keywords, count);
            return res;
        }

        [AjaxMethod]
        public AjaxResponse DeleteActor(int actorId)
        {
            AjaxResponse res = new AjaxResponse();
            try
            {
                _actorService.Delete(actorId);
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Error.Message = ex.Message;
            }
            return res;
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="actorId"></param>
        /// <param name="photoIds"></param>
        /// <param name="isX">1=限制级 0=不是</param>
        /// <returns></returns>
        [AjaxMethod]
        public AjaxResponse SaveActorPhotos(int actorId, List<int> photoIds, int isX = 0)
        {
            AjaxResponse res = new AjaxResponse();
            if (actorId > 0 && photoIds != null && photoIds.Count > 0)
            {
                _actorPhotoService.InsertPhotos(actorId, photoIds, (isX == 1));
            }
            return res;
        }

        [AjaxMethod]
        public AjaxResponse DeleteActorPhoto(int photoId)
        {
            AjaxResponse res = new AjaxResponse();
            _actorPhotoService.Delete(photoId);

            return res;
        }
        #endregion
    }
}