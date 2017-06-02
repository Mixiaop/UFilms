using UFilm.Domain.Collection;

namespace UFilm.EntityFramework.Mapping.Collection
{
    public partial class MovieCollectionItemMap : UFilmEntityTypeConfiguration<MovieCollectionItem>
    {
        public MovieCollectionItemMap()
        {
            this.ToTable(DbConsts.DbTableName.Collection_MovieCollectionItems);
            this.HasKey(x => x.Id);

            this.HasRequired(x => x.Movie).WithMany().HasForeignKey(x => x.MovieId);
        }
    }
}
