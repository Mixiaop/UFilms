using System;
using System.Collections.Generic;
using System.Linq;
using U.UI;
using U.Application.Services.Dto;
using UFilm.Domain.Movies;

namespace UFilm.Services.Movies
{
    public class MovieTorrentService : IMovieTorrentService
    {
        protected IMovieTorrentRepository _torrentRepository;
        protected IMovieService _movieService;
        public MovieTorrentService(IMovieTorrentRepository torrentRepository, IMovieService movieService)
        {
            _torrentRepository = torrentRepository;
            _movieService = movieService;
        }

        /// <summary>
        /// 获取资源列表
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public IList<MovieTorrent> GetTorrents(int movieId)
        {
            var query = _torrentRepository.GetAll()
                       .Where(x => x.MovieId == movieId)
                       .OrderByDescending(x => x.CreationTime);

            var list = query.ToList();
            return list;
        }

        public PagedResultDto<MovieTorrent> Search(string keywords = "", int pageIndex = 1, int pageSize = 16)
        {
            var query = _torrentRepository.GetAll();

            if (keywords.IsNotNullOrEmpty())
            {
                query = query.Where(x => x.Name.Contains(keywords) ||
                                      x.Movie.CnName.Contains(keywords) ||
                                      x.Movie.EnName.Contains(keywords));
            }

            var count = query.Count();

            query = query.OrderByDescending(x => x.CreationTime);
            var list = query
                       .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                       .ToList();

            return new PagedResultDto<MovieTorrent>(count, list);
        }

        public PagedResultDto<MovieTorrent> Search(int movieId, int pageIndex = 1, int pageSize = 16)
        {
            var query = _torrentRepository.GetAll()
                        .Where(x => x.MovieId == movieId);

            query = query.OrderByDescending(x => x.CreationTime);

            var count = query.Count();

            var list = query
                       .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                       .ToList();

            return new PagedResultDto<MovieTorrent>(count, list);
        }

        public int Insert(MovieTorrent torrent)
        {
            if (torrent.MovieId == 0)
                throw new UserFriendlyException("MovieId 不能为空");

            var torrentId = _torrentRepository.InsertAndGetId(torrent);
            var movie = _movieService.Get(torrent.MovieId);
            if (movie != null)
            {
                movie.TorrentCount++;
                movie.LastShareTime = DateTime.Now;
                _movieService.Update(movie);
            }

            return torrentId;

        }

        public void Delete(int torrentId)
        {
            var torrent = _torrentRepository.Get(torrentId);
            if (torrent != null)
            {
                var movie = _movieService.Get(torrent.MovieId);
                if (movie != null && movie.TorrentCount > 0) {
                    movie.TorrentCount -= 1;
                    _movieService.Update(movie);
                }
                _torrentRepository.Delete(torrent);
            }
        }
    }
}
