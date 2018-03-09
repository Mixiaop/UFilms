using UFilm.Domain.Movies;

namespace UFilm.EntityFramework.Repositories.Movies
{
    public class MoviePhotoRepository : UFilmRepositoryBase<MoviePhoto>, IMoviePhotoRepository
    {
        public MoviePhotoRepository(UFilmDbContext dbContext) : base(dbContext) { }
    }
}
