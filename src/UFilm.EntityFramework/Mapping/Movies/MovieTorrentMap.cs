using UFilm.Domain.Movies;

namespace UFilm.EntityFramework.Mapping.Movies
{
    public partial class MovieTorrentMap : UFilmEntityTypeConfiguration<MovieTorrent>
    {
        public MovieTorrentMap()
        {
            this.ToTable(DbConsts.DbTableName.Movies_MovieTorrents);
            this.HasKey(x => x.Id);

            this.HasRequired(x => x.Movie).WithMany().HasForeignKey(x => x.MovieId);
        }
    }
}
