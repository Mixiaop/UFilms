using UFilm.Domain.Adults;

namespace UFilm.EntityFramework.Repositories.Adults
{

    public class ActorRepository : UFilmRepositoryBase<Actor>, IActorRepository
    {
        public ActorRepository(UFilmDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
