using U.Domain.Entities;
using U.EntityFramework.Repositories;

namespace UFilm.EntityFramework
{
    public abstract class UFilmRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<UFilmDbContext, TEntity, TPrimaryKey>
       where TEntity : class, IEntity<TPrimaryKey>
    {
        protected UFilmRepositoryBase(UFilmDbContext dbContext)
            : base(dbContext, false)
        {

        }
    }

    public abstract class UFilmRepositoryBase<TEntity> : EfRepositoryBase<UFilmDbContext, TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected UFilmRepositoryBase(UFilmDbContext dbContext)
            : base(dbContext, false)
        {

        }
    }
}
