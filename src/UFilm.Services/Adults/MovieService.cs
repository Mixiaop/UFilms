using System.Collections.Generic;
using System.Linq;
using U.AutoMapper;
using U.Application.Services.Dto;
using UFilm.Domain.Adults;
using UFilm.Services.Media;
using UFilm.Services.Tags;
using UFilm.Services.Adults.Dto;

namespace UFilm.Services.Adults
{
    public class MovieService : IMovieService
    {
        private readonly ILMovieRepository _movieRepository;
        private readonly ILMovieActorRepository _movieActorRepository;
        private readonly IThumbService _thumbService;
        private readonly IActorService _actorService;
        private readonly ITagService _tagService;
        public MovieService(ILMovieRepository movieRepository, ILMovieActorRepository movieActorRepository, IThumbService thumbService, IActorService actorService, ITagService tagService)
        {
            _movieRepository = movieRepository;
            _movieActorRepository = movieActorRepository;
            _thumbService = thumbService;
            _actorService = actorService;
            _tagService = tagService;
        }

        #region Search / Getts
        public PagedResultDto<LMovie> Search(string keywords = "", List<string> movieTypes = null, int pageIndex = 1, int pageSize = 16)
        {
            var query = _movieRepository.GetAll()
                            .WhereIf(!string.IsNullOrEmpty(keywords),
                            x => x.CnName.Contains(keywords) ||
                                x.EnName.Contains(keywords) ||
                                x.Code.Contains(keywords) ||
                                x.OtherPostYear.Contains(keywords) ||
                                x.Actors.Contains(keywords));

            if (movieTypes != null && movieTypes.Count > 0)
            {
                foreach (var t in movieTypes)
                    query = query.Where(x => x.MovieType.Contains(t));
            }

            var count = query.Count();
            List<LMovie> list;
            list = query
                       .OrderByDescending(x => x.CreationTime)
                       .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            if (list != null)
            {
                foreach (var m in list)
                    Format(m);
            }

            return new PagedResultOutput<LMovie>(count, list);
        }

        public PagedResultDto<LMovieDto> Search(string keywords, LMovieSearchOrderBy orderBy = LMovieSearchOrderBy.Hits, int pageIndex = 1, int pageSize = 16)
        {
            var query = _movieRepository.GetAll()
                            .WhereIf(!string.IsNullOrEmpty(keywords),
                            x => x.CnName.Contains(keywords) ||
                                x.EnName.Contains(keywords) ||
                                x.Code.Contains(keywords) ||
                                x.OtherPostYear.Contains(keywords) ||
                                x.Actors.Contains(keywords));


            var count = query.Count();
            List<LMovie> list;
            switch (orderBy)
            {
                case LMovieSearchOrderBy.Hits:
                    list = query
                      .OrderByDescending(x => x.Hits)
                      .ThenByDescending(x => x.CreationTime)
                      .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case LMovieSearchOrderBy.CreationTimeDesc:
                    list = query
                      .OrderByDescending(x => x.CreationTime)
                      .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    break;
                case LMovieSearchOrderBy.CreationTimeAsc:
                    list = query
                      .OrderBy(x => x.CreationTime)
                      .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    break;
                default:
                    list = query
                      .OrderByDescending(x => x.Hits)
                      .ThenByDescending(x => x.CreationTime)
                      .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    break;
            }


            if (list != null)
            {
                foreach (var m in list)
                    Format(m);
            }

            var result = list.MapTo<List<LMovieDto>>();
            return new PagedResultDto<LMovieDto>(count, result);
        }

        public LMovie Get(int movieId)
        {
            var movie = _movieRepository.Get(movieId);
            Format(movie);
            return movie;
        }

        public LMovie Get(string code)
        {
            code = code.Trim();
            return _movieRepository.GetAll().Where(x => x.Code == code).FirstOrDefault();
        }

        public LMovie Get(string cnName, int year) {
            cnName = cnName.Trim();
            var query = _movieRepository.GetAll().Where(x => x.CnName == cnName);
            if (year > 0)
            {
                query = query.Where(x => x.Year == year).OrderByDescending(x => x.CreationTime);
            }

            return query.Take(1).FirstOrDefault();
        }
        public bool Exists(string cnName, int year) {
            cnName = cnName.Trim();
            var query = _movieRepository.GetAll().Where(x => x.CnName == cnName);
            if (year > 0) {
                query = query.Where(x => x.Year == year);
            }

            return query.Count() > 0;
        }

        public bool Exists(string code)
        {
            return _movieRepository.Count(x => x.Code == code) > 0;
        }

        #endregion

        #region CURD
        public int Insert(LMovie movie, IList<Actor> actors = null)
        {
            //标签
            if (movie.MovieType.IsNotNullOrEmpty())
            {
                string[] list = movie.MovieType.Split(',');

                List<string> tags = new List<string>();
                if (list != null)
                {
                    foreach (var s in list)
                        tags.Add(s);
                }

                _tagService.Update(TagType.LMovieType, tags);
            }
            if (movie.CnName.IsNullOrEmpty() && movie.Code.IsNotNullOrEmpty())
                movie.CnName = movie.Code;
            movie.MovieType = movie.MovieType.Replace("，", ",");
            movie.Area = movie.Area.Replace("，", ",");
            movie.Language = movie.Language.Replace("，", ",");

            movie.Id = _movieRepository.InsertAndGetId(movie);

            if (actors != null) {
                foreach (var actor in actors) {
                    LMovieActor mActor = new LMovieActor();
                    mActor.MovieId = movie.Id;
                    mActor.ActorId = actor.Id;
                    InsertMovieActor(mActor);

                    movie.Actors += actor.CnName + ",";
                }

                if (movie.Actors.IsNotNullOrEmpty())
                    movie.Actors = movie.Actors.TrimEnd(",");

                _movieRepository.Update(movie);
            }

            return movie.Id;
        }

        public void Update(LMovie movie, bool isChangeCover = false)
        {
            movie.MovieType = movie.MovieType.Replace("，", ",");
            movie.Area = movie.Area.Replace("，", ",");
            movie.Language = movie.Language.Replace("，", ",");
            _movieRepository.Update(movie);

            if (isChangeCover)
                _thumbService.Clear(movie.Id);

        }

        public void Delete(int movieId)
        {
            var m = Get(movieId);
            Delete(m);
        }

        public void Delete(LMovie movie)
        {
            _movieRepository.Delete(movie);
            //应该影响影人的电影数
        }
        #endregion

        #region MovieActors
        public IList<LMovieActor> GetMovieActors(int movieId)
        {
            var query = _movieActorRepository.GetAll().Where(x => x.MovieId == movieId);
            query = query.OrderBy(x => x.Sort);
            var list = query.ToList();

            return list;
        }

        public void InsertMovieActor(LMovieActor movieActor)
        {
            _movieActorRepository.Insert(movieActor);
            if (movieActor.ActorId > 0)
            {
                var actor = _actorService.Get(movieActor.ActorId);
                actor.MovieCount++;
                _actorService.Update(actor);
            }
        }
        #endregion

        #region Utilities
        public void Format(LMovie movie)
        {
            _thumbService.Format(movie);
            if (movie.MovieType.IsNotNullOrEmpty()) {
                movie.MovieType = movie.MovieType.Replace(",", " / ");
            }
        }
        #endregion
    }
}
