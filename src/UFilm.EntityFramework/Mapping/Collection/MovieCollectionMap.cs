using UFilm.Domain.Collection;

namespace UFilm.EntityFramework.Mapping.Collection
{
    public partial class MovieCollectionMap : UFilmEntityTypeConfiguration<MovieCollection>
    {
        public MovieCollectionMap()
        {
            this.ToTable(DbConsts.DbTableName.Collection_MovieCollections);
            this.HasKey(x => x.Id);

            this.HasMany(x => x.Covers).WithRequired(x => x.Collection);

            this.Ignore(x => x.Cover);
        }
    }
}
