using UFilm.Domain.Adults;

namespace UFilm.EntityFramework.Repositories.Adults
{

    public class LMovieActorRepository : UFilmRepositoryBase<LMovieActor>, ILMovieActorRepository
    {
        public LMovieActorRepository(UFilmDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
