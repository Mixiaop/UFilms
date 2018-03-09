using System;
using System.Collections.Generic;
using U.Application.Services.Dto;
using UFilm.Domain.Movies;
using UFilm.Services.Movies.Dto;

namespace UFilm.Services.Movies
{
    public interface IMovieService : U.Application.Services.IApplicationService
    {
        Movie Get(int movieId);

        Movie Get(string cnName, string enName = "", int year = 0);

        bool Exists(string cnName, string enName = "", int year = 0);

        int Insert(Movie movie);

        /// <summary>
        /// 更新电影
        /// </summary>
        /// <param name="movie"></param>
        /// <param name="isChangeCover">是否替换封面</param>
        void Update(Movie movie, bool isChangeCover = false);

        void Delete(Movie movie);

        //void Delete(int movieId);

        #region Series
        /// <summary>
        /// 创建一个电视剧系列（电影之间的关联）
        /// </summary>
        /// <param name="series"></param>
        /// <returns></returns>
        int InsertSeries(MovieSeries series);

        /// <summary>
        ///  通过电影系列Id获取电影列表
        /// </summary>
        /// <param name="seriesId"></param>
        /// <returns></returns>
        IList<Movie> GetMoviesBySeries(int seriesId);

        /// <summary>
        /// 搜索电影系列
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagedResultDto<MovieSeries> Search(string keywords, int pageIndex = 1, int pageSize = 16);
        #endregion
        

        #region Participant
        /// <summary>
        /// 获取电影参与人员
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        IList<MovieParticipant> GetParticipants(int movieId);

        void InsertParticipant(MovieParticipant participant);
        #endregion
    }
}
