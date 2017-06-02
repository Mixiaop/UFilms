using UFilm.Domain.Spiders;

namespace UFilm.EntityFramework.Repositories.Spiders
{

    public class SpiderTaskRepository : UFilmRepositoryBase<SpiderTask>, ISpiderTaskRepository
    {
        public SpiderTaskRepository(UFilmDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
