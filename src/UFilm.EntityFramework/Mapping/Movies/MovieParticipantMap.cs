using UFilm.Domain.Movies;

namespace UFilm.EntityFramework.Mapping.Movies
{
    public partial class MovieParticipantMap : UFilmEntityTypeConfiguration<MovieParticipant>
    {
        public MovieParticipantMap()
        {
            this.ToTable(DbConsts.DbTableName.Movies_MovieParticipants);
            this.HasKey(x => x.Id);

            this.Property(x => x.JobTypeId).HasColumnName("JobType");

            this.HasRequired(x => x.Movie).WithMany().HasForeignKey(x => x.MovieId);
            this.HasRequired(x => x.FilmMan).WithMany().HasForeignKey(x => x.FilmManId);

            this.Ignore(x => x.JobType);
        }
    }
}
