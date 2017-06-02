using System;
using System.Collections.Generic;
using System.Linq;
using U.Application.Services.Dto;
using U.UI;
using UFilm.Configuration;
using UFilm.Domain.Movies;
using UFilm.Services.Media;

namespace UFilm.Services.Movies
{
    public class MovieService : DisplayFormatService, IMovieService
    {
        protected readonly IMovieRepository _movieRepository;
        protected readonly IMovieParticipantRepository _movieParticipantRepository;
        protected readonly IMovieSeriesRepository _seriesRepository;

        public MovieService(UFilmSettings settings, IThumbService thumbService, IMovieRepository movieRepository, IMovieParticipantRepository movieParticipantRepository, IMovieSeriesRepository seriesRepository)
            : base(settings, thumbService)
        {
            _movieRepository = movieRepository;
            _movieParticipantRepository = movieParticipantRepository;
            _seriesRepository = seriesRepository;
        }

        public bool Exists(string cnName, string enName = "", int year = 0)
        {
            if (cnName.IsNullOrEmpty())
                throw new UserFriendlyException("cnName is null");

            var query = _movieRepository.GetAll();
            query = query.Where(x => x.CnName == cnName.Trim());
            if (year > 0)
                query = query.Where(x => x.Year == year);
            if (enName.IsNotNullOrEmpty())
                query = query.Where(x => x.EnName == enName);

            return query.Count() > 0;
        }

        public Movie Get(int movieId)
        {
            return Format(_movieRepository.Get(movieId));
        }

        public Movie Get(string cnName, string enName = "", int year = 0)
        {
            if (cnName.IsNullOrEmpty())
                return null;
            if (!Exists(cnName, enName, year))
                return null;
            else
                return Format(_movieRepository.GetAll().Where(x => x.CnName == cnName).FirstOrDefault());
        }

        public int Insert(Movie movie)
        {
            return _movieRepository.InsertAndGetId(movie);
        }

        public void Update(Movie movie, bool isChangeCover = false)
        {
            _movieRepository.Update(movie);
            if (isChangeCover)
                _thumbService.Clear(movie.Id);
        }

        public void Delete(Movie movie)
        {
            _movieRepository.Delete(movie);
        }

        public void Delete(int movieId)
        {
            _movieRepository.Delete(movieId);
        }

        #region Series
        /// <summary>
        /// 创建电影系列信息（返回系列ID）
        /// </summary>
        /// <param name="info">系列信息</param>
        /// <returns>返回系列ID</returns>
        public int InsertSeries(MovieSeries series)
        {
            if (series.SeriesNumber == 0 || series.MovieId == 0)
                throw new UserFriendlyException("SeriesNumber || MovieId 不能为空");

            var rand = new Random();
            series.SeriesId = (DateTime.Now.ToString("dd") +
                                DateTime.Now.Second +
                                rand.Next(9).ToString() +
                                rand.Next(9).ToString() +
                                rand.Next(9).ToString() +
                                rand.Next(9).ToString()).ToInt();
            _seriesRepository.Insert(series);
            return series.SeriesId;
        }

        /// <summary>
        ///  通过电影系列Id获取电影列表
        /// </summary>
        /// <param name="seriesId"></param>
        /// <returns></returns>
        public IList<Movie> GetMoviesBySeries(int seriesId)
        {
            var query = _seriesRepository.GetAll().Where(x => x.SeriesId == seriesId);
            var list = query.ToList();

            List<Movie> result = new List<Movie>();
            foreach (var s in list)
                result.Add(s.Movie);

            return result;
        }

        /// <summary>
        /// 搜索电影系列
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedResultDto<MovieSeries> Search(string keywords, int pageIndex = 1, int pageSize = 16) {
            var query = _seriesRepository.GetAll();

            if (keywords.IsNotNullOrEmpty())
            {
                query = query.Where(x => x.Movie.CnName.Contains(keywords) ||
                                         x.Movie.EnName.Contains(keywords));
            }
            query = query.OrderByDescending(x => x.Id);

            var count = query.Count();

            var list = query
                       .Skip((pageIndex - 1) * pageSize).Take(pageSize)
                       .ToList();

            return new PagedResultDto<MovieSeries>(count, list);
        }
        #endregion

        #region Participant
        public void InsertParticipant(MovieParticipant participant)
        {
            _movieParticipantRepository.Insert(participant);
        }

        /// <summary>
        /// 获取电影参与人员
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public IList<MovieParticipant> GetParticipants(int movieId)
        {
            var query = _movieParticipantRepository.GetAll().Where(x => x.MovieId == movieId);
            query = query.OrderBy(x => x.Sort).ThenBy(x => x.JobTypeId);
            var list = query.ToList();

            return list;
        }

        #endregion
    }
}
