using UFilm.Domain.Movies;

namespace UFilm.EntityFramework.Repositories.Movies
{
    public class MovieRepository : UFilmRepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(UFilmDbContext dbContext) : base(dbContext) { }
    }
}
