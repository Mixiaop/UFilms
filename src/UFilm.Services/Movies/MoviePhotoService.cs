using System.Linq;
using U.Application.Services.Dto;
using UFilm.Domain.Movies;
using UFilm.Services.Media;

namespace UFilm.Services.Movies
{
    public class MoviePhotoService : IMoviePhotoService
    {
        protected readonly IMoviePhotoRepository _photoRepository;

        protected readonly IThumbService _thumbService;
        protected readonly IMovieService _movieService;

        public MoviePhotoService(IMoviePhotoRepository photoRepository, IThumbService thumbService, IMovieService movieService)
        {
            _photoRepository = photoRepository;
            _thumbService = thumbService;
            _movieService = movieService;
        }

        /// <summary>
        /// 分页搜索影片的图片信息
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页数</param>
        /// <param name="movieId">电影Id</param>
        /// <param name="keywords">搜索关键字</param>
        /// <param name="photoType"></param>
        /// <returns></returns>
        public PagedResultDto<MoviePhoto> Search(int pageIndex = 1, int pageSize = 16, int movieId = 0, string keywords = "", MoviePhotoType photoType = MoviePhotoType.None)
        {
            var query = _photoRepository.GetAll()
                          .WhereIf(movieId > 0, x => x.MovieId == movieId)
                          .WhereIf(!string.IsNullOrEmpty(keywords),
                          x => x.Movie.CnName.Contains(keywords) ||
                              x.Movie.EnName.Contains(keywords) ||
                              x.Description.Contains(keywords));

            if (photoType != MoviePhotoType.None) { 
                int typeId = (int)photoType;
                query = query.Where(x => x.PhotoTypeId == typeId);
            }

            var count = query.Count();
            var list = query.OrderByDescending(x => x.Id)
                       .PageBy((pageIndex - 1) * pageSize, pageSize).ToList();

            foreach (var p in list)
                _thumbService.Format(p);

            return new PagedResultOutput<MoviePhoto>(count, list);
        }

        public MoviePhoto Get(int photoId) {
            return _photoRepository.Get(photoId);
        }

        public void Insert(MoviePhoto photo)
        {
            if (_photoRepository.Count(x => x.MovieId == photo.MovieId && x.PictureId == photo.PictureId) == 0)
            {
                _photoRepository.Insert(photo);
                var movie = _movieService.Get(photo.MovieId);
                if (movie != null)
                {
                    movie.PhotoCount++;
                    _movieService.Update(movie);
                }
            }
        }

        public void Update(MoviePhoto photo)
        {
            _photoRepository.Update(photo);
        }

        public void Delete(MoviePhoto photo)
        {
            _photoRepository.Delete(photo);
            var movie = _movieService.Get(photo.MovieId);
            if (movie != null)
            {
                movie.PhotoCount--;
                _movieService.Update(movie);
            }
        }
    }
}
