using UFilm.Domain.Movies;

namespace UFilm.EntityFramework.Mapping.Movies
{
    public partial class MovieSeriesMap : UFilmEntityTypeConfiguration<MovieSeries>
    {
        public MovieSeriesMap()
        {
            this.ToTable(DbConsts.DbTableName.Movies_MovieSeries);
            this.HasKey(x => x.Id);

            this.HasRequired(x => x.Movie).WithMany().HasForeignKey(x => x.MovieId);
        }
    }
}
