using UFilm.Domain.Collection;

namespace UFilm.EntityFramework.Repositories.Collection
{
    public class MovieCollectionRepository : UFilmRepositoryBase<MovieCollection>, IMovieCollectionRepository
    {
        public MovieCollectionRepository(UFilmDbContext dbContext) : base(dbContext) { }
    }
}
