using System.Collections.Generic;
using System.Linq;
using U.UI;
using U.Application.Services.Dto;
using UFilm.Configuration;
using UFilm.Domain.Movies;
using UFilm.Services.Media;

namespace UFilm.Services.Movies
{
    public class FilmManService : DisplayFormatService, IFilmManService
    {
        protected readonly IFilmManRepository _filmManRepository;
        protected readonly IMovieParticipantRepository _movieParticipantRepository;
        public FilmManService(UFilmSettings settings, IThumbService thumbService, IFilmManRepository filmManRepository, IMovieParticipantRepository movieParticipantRepository)
            : base(settings, thumbService)
        {
            _filmManRepository = filmManRepository;
            _movieParticipantRepository = movieParticipantRepository;
        }

        public PagedResultDto<FilmMan> Search(int pageIndex = 1, int pageSize = 16, string keywords = "")
        {
            var query = _filmManRepository.GetAll()
                        .WhereIf(!string.IsNullOrEmpty(keywords),
                        x => x.CnName.Contains(keywords) ||
                            x.EnName.Contains(keywords) ||
                            x.Birthday.Contains(keywords) ||
                            x.MoreCnName.Contains(keywords));

            var count = query.Count();
            var list = query.OrderByDescending(x => x.PhotoCount)
                       .PageBy((pageIndex - 1) * pageSize, pageSize).ToList();
            foreach (var f in list)
                Format(f);

            return new PagedResultOutput<FilmMan>(count, list);
        }

        public bool Exists(string cnName)
        {
            if (cnName.IsNullOrEmpty())
                throw new UserFriendlyException("cnName is null");

            var query = _filmManRepository.GetAll();
            query = query.Where(x => x.CnName == cnName.Trim());


            return query.Count() > 0;
        }

        public FilmMan Get(string cnName)
        {
            if (cnName.IsNullOrEmpty())
                return null;

            if (!Exists(cnName))
                return null;
            else
                return Format(_filmManRepository.GetAll().Where(x => x.CnName == cnName).FirstOrDefault());
        }

        public FilmMan Get(int filmManId)
        {
            return Format(_filmManRepository.Get(filmManId));
        }

        public PagedResultDto<Movie> QueryParticipantMovies(int filmManId, int pageIndex, int pageSize)
        {
            //先取出所有，再GROUPBY 将来有优化的空间
            var query = _movieParticipantRepository.GetAll().Where(x => x.FilmManId == filmManId);
            query = query.OrderByDescending(x => x.Movie.Year);
            var count = query.Count();
            
            var list = query.ToList();
            var results = new List<Movie>();
            
            var index = 1;
            foreach (var info in list)
            {
                if (index > pageSize) break;
                if (results.Find(x => x.Id == info.MovieId) == null)
                {
                    results.Add(Format(info.Movie));
                    index++;
                }
            }

            return new PagedResultDto<Movie>(count, results);
        }

        public int Insert(FilmMan man)
        {
            return _filmManRepository.InsertAndGetId(man);
        }

        public void Update(FilmMan man)
        {
            _filmManRepository.Update(man);
        }
    }
}
