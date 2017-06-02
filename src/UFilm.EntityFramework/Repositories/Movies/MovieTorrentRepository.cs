using UFilm.Domain.Movies;

namespace UFilm.EntityFramework.Repositories.Movies
{
    public class MovieTorrentRepository : UFilmRepositoryBase<MovieTorrent>, IMovieTorrentRepository
    {
        public MovieTorrentRepository(UFilmDbContext dbContext) : base(dbContext) { }
    }
}
