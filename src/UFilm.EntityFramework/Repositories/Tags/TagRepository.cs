using U.EntityFramework;
using UFilm.Domain.Tags;

namespace UFilm.EntityFramework.Repositories.Tags
{
    public class TagRepository : UFilmRepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(UFilmDbContext dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
