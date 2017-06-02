using UFilm.Domain.Movies;

namespace UFilm.EntityFramework.Repositories.Movies
{
    public class MovieCoverRepository : UFilmRepositoryBase<MovieCover>, IMovieCoverRepository
    {
        public MovieCoverRepository(UFilmDbContext dbContext) : base(dbContext) { }
    }
}
