using UFilm.Domain.Movies;

namespace UFilm.EntityFramework.Repositories.Movies
{
    public class FilmManPhotoRepository : UFilmRepositoryBase<FilmManPhoto>, IFilmManPhotoRepository 
    {
        public FilmManPhotoRepository(UFilmDbContext dbContext) : base(dbContext) { }
    }
}
