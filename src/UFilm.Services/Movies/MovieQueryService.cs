using System.Collections.Generic;
using System.Linq;
using U.AutoMapper;
using U.Application.Services.Dto;
using UFilm.Configuration;
using UFilm.Domain.Movies;
using UFilm.Services.Media;
using UFilm.Services.Movies.Dto;

namespace UFilm.Services.Movies
{
    public class MovieQueryService : DisplayFormatService, IMovieQueryService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieQueryService(UFilmSettings settings, IThumbService thumbService, IMovieRepository movieRepository)
            : base(settings, thumbService)
        {
            _movieRepository = movieRepository;
        }

        public PagedResultDto<MovieBriefDto> QueryLastActivityMovies(int pageIndex = 1, int pageSize = 16, string keywords = "", List<string> movieTypes = null, List<string> movieAreas = null)
        {
            var query = _movieRepository.GetAll();
            query = query.Where(x => x.TorrentCount > 0);
            if (keywords.IsNotNullOrEmpty())
            {
                query = query.Where(x => x.CnName.Contains(keywords)
                                    || x.EnName.Contains(keywords)
                                    || x.Actor.Contains(keywords)
                                    || x.Director.Contains(keywords));
            }

            if (movieTypes != null)
            {
                foreach (var t in movieTypes)
                    query = query.Where(x => x.MovieType.Contains(t));
            }

            if (movieAreas != null)
            {
                foreach (var t in movieAreas)
                    query = query.Where(x => x.Area.Contains(t));
            }

            int count = query.Count();
            query = query.OrderByDescending(x => x.LastShareTime).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var list = query.ToList();

            if (list != null)
            {
                foreach (var m in list)
                    Format(m);
            }
            var results = list.MapTo<List<MovieBriefDto>>();
            return new PagedResultDto<MovieBriefDto>(count, results);
        }

        /// <summary>
        /// 分页搜索影片列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页数</param>
        /// <param name="keywords">搜索关键字</param>
        /// <returns></returns>
        public PagedResultDto<MovieBriefDto> Search(int pageIndex = 1, int pageSize = 16, string keywords = "", List<string> movieTypes = null, List<string> movieAreas = null, MovieQueryOrder order = MovieQueryOrder.CreatingTimeDesc)
        {
            var query = _movieRepository.GetAll()
                        .WhereIf(!string.IsNullOrEmpty(keywords),
                        x => x.CnName.Contains(keywords) ||
                            x.EnName.Contains(keywords) ||
                            x.Actor.Contains(keywords) ||
                            x.Director.Contains(keywords) ||
                            x.OtherEnName.Contains(keywords));

            if (movieTypes != null)
            {
                foreach (var t in movieTypes)
                    query = query.Where(x => x.MovieType.Contains(t));
            }

            if (movieAreas != null)
            {
                foreach (var t in movieAreas)
                    query = query.Where(x => x.Area.Contains(t));
            }

            var count = query.Count();
            List<Movie> list;
            if (order == MovieQueryOrder.CreatingTimeDesc)
            {
                list = query
                       .OrderByDescending(x => x.CreationTime)
                       .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            else {
                list = query
                           .OrderByDescending(x => x.CreationTime)
                           .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            

            if (list != null)
            {
                foreach (var m in list)
                    Format(m);
            }

            var results = list.MapTo<List<MovieBriefDto>>();
            return new PagedResultOutput<MovieBriefDto>(count, results);
        }

        public IList<MovieBriefDto> Search(string keywords, int count = 5) {
            var query = _movieRepository.GetAll()
                        .WhereIf(!string.IsNullOrEmpty(keywords),
                        x => x.CnName.Contains(keywords) ||
                            x.EnName.Contains(keywords) ||
                            x.Actor.Contains(keywords) ||
                            x.Director.Contains(keywords) ||
                            x.OtherEnName.Contains(keywords));


            
            if (count <= 0) {
                count = 10;
            }

            List<Movie> list = query.OrderBy(x => x.CreationTime).Take(count).ToList();
           


            if (list != null)
            {
                foreach (var m in list)
                    Format(m);
            }

            var results = list.MapTo<List<MovieBriefDto>>();
            return results;
        }
    }
}
