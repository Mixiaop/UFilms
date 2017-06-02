using System.Collections.Generic;
using U.Application.Services;
using U.Application.Services.Dto;
using UFilm.Domain.Movies;

namespace UFilm.Services.Movies
{
    /// <summary>
    /// 电影“资源（种子）”服务
    /// </summary>
    public interface IMovieTorrentService : IApplicationService
    {
        /// <summary>
        /// 获取资源列表
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        IList<MovieTorrent> GetTorrents(int movieId);


        PagedResultDto<MovieTorrent> Search(string keywords = "", int pageIndex = 1, int pageSize = 16);
        PagedResultDto<MovieTorrent> Search(int movieId, int pageIndex = 1, int pageSize = 16);

        int Insert(MovieTorrent torrent);

        void Delete(int torrentId);
    }
}
