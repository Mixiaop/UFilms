using UFilm.Domain.Adults;

namespace UFilm.EntityFramework.Repositories.Adults
{

    public class SpiderLinkRepository : UFilmRepositoryBase<SpiderLink>, ISpiderLinkRepository
    {
        public SpiderLinkRepository(UFilmDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
