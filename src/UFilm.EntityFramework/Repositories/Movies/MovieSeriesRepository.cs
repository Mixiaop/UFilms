using UFilm.Domain.Movies;

namespace UFilm.EntityFramework.Repositories.Movies
{
    public class MovieSeriesRepository : UFilmRepositoryBase<MovieSeries>, IMovieSeriesRepository
    {
        public MovieSeriesRepository(UFilmDbContext dbContext) : base(dbContext) { }
    }
}
