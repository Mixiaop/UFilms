using UFilm.Domain.Spiders;

namespace UFilm.EntityFramework.Repositories.Spiders
{

    public class SpiderTaskLogRepository : UFilmRepositoryBase<SpiderTaskLog>, ISpiderTaskLogRepository
    {
        public SpiderTaskLogRepository(UFilmDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
