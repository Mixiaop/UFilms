using UFilm.Domain.Adults;

namespace UFilm.EntityFramework.Repositories.Adults
{

    public class ActorPhotoRepository : UFilmRepositoryBase<ActorPhoto>, IActorPhotoRepository
    {
        public ActorPhotoRepository(UFilmDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
