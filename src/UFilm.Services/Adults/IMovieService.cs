using System.Collections.Generic;
using U.Application.Services.Dto;
using UFilm.Domain.Adults;
using UFilm.Services.Adults.Dto;

namespace UFilm.Services.Adults
{
    public interface IMovieService : U.Application.Services.IApplicationService
    {
        PagedResultDto<LMovie> Search(string keywords = "", List<string> movieTypes = null,int pageIndex = 1, int pageSize = 16);

        PagedResultDto<LMovieDto> Search(string keywords, LMovieSearchOrderBy orderBy = LMovieSearchOrderBy.Hits, int pageIndex = 1, int pageSize = 16);

        LMovie Get(int movieId);

        LMovie Get(string code);

        LMovie Get(string cnName, int year);
        bool Exists(string cnName, int year);

        bool Exists(string code);

        int Insert(LMovie movie, IList<Actor> actors = null);

        void Update(LMovie movie, bool isChangeCover = false);

        void Delete(int movieId);

        void Delete(LMovie movie);

        #region MovieActors
        /// <summary>
        /// 获取电影参与人员
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        IList<LMovieActor> GetMovieActors(int movieId);

        void InsertMovieActor(LMovieActor movieActor);
        #endregion
    }

    
}
