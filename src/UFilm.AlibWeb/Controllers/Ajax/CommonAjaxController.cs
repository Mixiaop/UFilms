using System;
using AjaxPro;
using U;
using U.Web.Models;
using U.FakeMvc.Controllers;
using U.Application.Services.Dto;
using UFilm.Services.Adults;
using UFilm.Services.Adults.Dto;

namespace UFilm.AlibWeb.Controllers.Ajax
{
    [AjaxNamespace("CommonService")]
    public class CommonAjaxController : AjaxControllerBase
    {
        IMovieService _movieService = UPrimeEngine.Instance.Resolve<IMovieService>();

        [AjaxMethod]
        public AjaxResponse<PagedResultDto<LMovieDto>> QueryHotMovies(string keywords = "", int pageIndex = 1, int pageSize = 16)
        {
            AjaxResponse<PagedResultDto<LMovieDto>> res = new AjaxResponse<PagedResultDto<LMovieDto>>();

            try
            {

                var result = _movieService.Search(keywords, LMovieSearchOrderBy.CreationTimeDesc, pageIndex, pageSize);

                res.Result = result;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Error = new ErrorInfo(ex.Message);
            }
            return res;
        }
    }
}