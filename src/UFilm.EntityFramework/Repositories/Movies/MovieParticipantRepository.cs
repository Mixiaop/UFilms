using UFilm.Domain.Movies;

namespace UFilm.EntityFramework.Repositories.Movies
{
    public class MovieParticipantRepository : UFilmRepositoryBase<MovieParticipant>, IMovieParticipantRepository
    {
        public MovieParticipantRepository(UFilmDbContext dbContext) : base(dbContext) { }
    }
}
