using UFilm.Domain.Adults;

namespace UFilm.EntityFramework.Mapping.Adults
{
    public partial class LMovieActorMap : UFilmEntityTypeConfiguration<LMovieActor>
    {
        public LMovieActorMap()
        {
            this.ToTable(DbConsts.DbTableName.Adults_MovieActors);
            this.HasKey(x => x.Id);

            this.HasRequired(x => x.Movie).WithMany().HasForeignKey(x => x.MovieId);
            this.HasRequired(x => x.Actor).WithMany().HasForeignKey(x => x.ActorId);
        }
    }
}



