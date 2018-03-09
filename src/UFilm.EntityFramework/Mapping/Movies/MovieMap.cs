using UFilm.Domain.Movies;

namespace UFilm.EntityFramework.Mapping.Movies
{
    public partial class MovieMap : UFilmEntityTypeConfiguration<Movie>
    {
        public MovieMap()
        {
            this.ToTable(DbConsts.DbTableName.Movies_Movies);
            this.HasKey(x => x.Id);

            this.HasMany(x => x.Covers).WithRequired(x => x.Movie);

            this.Ignore(x => x.Cover);
            this.Ignore(x => x.FormatName);
        }
    }
}
