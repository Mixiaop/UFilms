using System.Collections.Generic;
using U.Application.Services;
using U.Application.Services.Dto;
using UFilm.Domain.Movies;

namespace UFilm.Services.Movies
{
    public interface IFilmManService : IApplicationService
    {
        /// <summary>
        /// 分页搜索影人信息
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页数</param>
        /// <param name="keywords">搜索关键字</param>
        /// <returns></returns>
        PagedResultDto<FilmMan> Search(int pageIndex = 1, int pageSize = 16, string keywords = "");

        bool Exists(string cnName);

        /// <summary>
        /// 获取影人信息
        /// </summary>
        /// <param name="filmManId"></param>
        /// <returns></returns>
        FilmMan Get(int filmManId);

        FilmMan Get(string cnName);

        /// <summary>
        /// 获取影人参与的电影列表（时间倒序）
        /// </summary>
        /// <param name="filmManId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagedResultDto<Movie> QueryParticipantMovies(int filmManId, int pageIndex = 1, int pageSize = 12);

        int Insert(FilmMan man);

        void Update(FilmMan man);
    }
}
