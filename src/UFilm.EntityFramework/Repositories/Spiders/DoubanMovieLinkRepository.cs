using UFilm.Domain.Spiders;

namespace UFilm.EntityFramework.Repositories.Spiders
{

    public class DoubanMovieLinkRepository : UFilmRepositoryBase<DoubanMovieLink>, IDoubanMovieLinkRepository
    {
        public DoubanMovieLinkRepository(UFilmDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
