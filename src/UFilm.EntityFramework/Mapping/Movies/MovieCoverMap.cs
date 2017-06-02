using UFilm.Domain.Movies;

namespace UFilm.EntityFramework.Mapping.Movies
{
    public partial class MovieCoverMap : UFilmEntityTypeConfiguration<MovieCover>
    {
        public MovieCoverMap()
        {
            this.ToTable(DbConsts.DbTableName.Movies_MovieCovers);
            this.HasKey(x => x.Id);

            this.HasRequired(x => x.Movie).WithMany().HasForeignKey(x => x.MovieId);
        }
    }
}
