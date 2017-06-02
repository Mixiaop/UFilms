using System.Collections.Generic;
using U.Application.Services.Dto;
using UFilm.Domain.Movies;
using UFilm.Services.Movies.Dto;

namespace UFilm.Services.Movies
{
    /// <summary>
    /// 电影查询服务 
    /// </summary>
    public interface IMovieQueryService : U.Application.Services.IApplicationService
    {
        /// <summary>
        /// 查询最近活动的电影列表（如分享了最新的电影资源 ）
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="keywords"></param>
        /// <param name="movieTypes"></param>
        /// <param name="movieAreas"></param>
        /// <returns></returns>
        PagedResultDto<MovieBriefDto> QueryLastActivityMovies(int pageIndex = 1, int pageSize = 16, string keywords = "", List<string> movieTypes = null, List<string> movieAreas = null);

        /// <summary>
        /// 分页搜索影片列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页数</param>
        /// <param name="keywords">搜索关键字</param>
        /// <param name="movieTypes"></param>
        /// <param name="movieAreas"></param>
        /// <returns></returns>
        PagedResultDto<MovieBriefDto> Search(int pageIndex = 1, int pageSize = 16, string keywords = "", List<string> movieTypes = null, List<string> movieAreas = null, MovieQueryOrder order = MovieQueryOrder.CreatingTimeDesc);

        IList<MovieBriefDto> Search(string keywords, int count = 5);
    }
}
