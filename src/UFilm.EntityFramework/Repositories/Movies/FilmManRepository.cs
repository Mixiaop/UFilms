using UFilm.Domain.Movies;

namespace UFilm.EntityFramework.Repositories.Movies
{
    public class FilmManRepository : UFilmRepositoryBase<FilmMan>, IFilmManRepository
    {
        public FilmManRepository(UFilmDbContext dbContext) : base(dbContext) { }
    }
}
