using System.Linq;
using U.Application.Services.Dto;
using UFilm.Domain.Movies;
using UFilm.Services.Media;

namespace UFilm.Services.Movies
{
    public class FilmManPhotoService : IFilmManPhotoService
    {
         private readonly IFilmManPhotoRepository _photoRepository;

        private readonly IThumbService _thumbService;

        public FilmManPhotoService(IFilmManPhotoRepository photoRepository, IThumbService thumbService)
        {
            _photoRepository = photoRepository;
            _thumbService = thumbService;
        }

        public PagedResultDto<FilmManPhoto> Search(int filmManId, int pageIndex = 1, int pageSize = 16) {
            var query = _photoRepository.GetAll()
                        .WhereIf(filmManId > 0,
                        x => x.FilmManId == filmManId);

            var count = query.Count();
            var list = query.OrderByDescending(x => x.CreationTime)
                       .PageBy((pageIndex - 1) * pageSize, pageSize).ToList();
            foreach (var p in list)
                _thumbService.Format(p);
            return new PagedResultOutput<FilmManPhoto>(count, list);
        }

        public void Insert(FilmManPhoto photo)
        {
            _photoRepository.Insert(photo);
        }
    }
}
