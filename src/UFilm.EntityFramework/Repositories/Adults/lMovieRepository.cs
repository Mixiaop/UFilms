using UFilm.Domain.Adults;

namespace UFilm.EntityFramework.Repositories.Adults
{

    public class MovieRepository : UFilmRepositoryBase<LMovie>, ILMovieRepository
    {
        public MovieRepository(UFilmDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
