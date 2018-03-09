using UFilm.Domain.Collection;

namespace UFilm.EntityFramework.Repositories.Collection
{
    public class MovieCollectionItemRepository : UFilmRepositoryBase<MovieCollectionItem>, IMovieCollectionItemRepository
    {
        public MovieCollectionItemRepository(UFilmDbContext dbContext) : base(dbContext) { }
    }
}
