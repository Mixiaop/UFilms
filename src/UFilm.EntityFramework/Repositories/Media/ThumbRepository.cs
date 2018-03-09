using UFilm.Domain.Media;

namespace UFilm.EntityFramework.Repositories.Media
{
    public class ThumbRepository : UFilmRepositoryBase<Thumb>, IThumbRepository
    {
        public ThumbRepository(UFilmDbContext dbContext) : base(dbContext) { }
    }
}
