using UFilm.Domain.Media;

namespace UFilm.EntityFramework.Mapping.Media
{
    public partial class ThumbMap : UFilmEntityTypeConfiguration<Thumb>
    {
        public ThumbMap()
        {
            this.ToTable(DbConsts.DbTableName.Media_Thumbs);
            this.HasKey(x => x.Id);

            this.HasRequired(x => x.Movie).WithMany().HasForeignKey(x => x.ObjectId);
            this.HasRequired(x => x.MoviePhoto).WithMany().HasForeignKey(x => x.ObjectId);
            this.HasRequired(x => x.FilmMan).WithMany().HasForeignKey(x => x.ObjectId);
            this.HasRequired(x => x.FilmManPhoto).WithMany().HasForeignKey(x => x.ObjectId);
            this.HasRequired(x => x.Collection).WithMany().HasForeignKey(x => x.ObjectId);

            this.HasRequired(x => x.AdultActor).WithMany().HasForeignKey(x => x.ObjectId);
            this.HasRequired(x => x.AdultsActorPhoto).WithMany().HasForeignKey(x => x.ObjectId);
            this.HasRequired(x => x.AdultMovie).WithMany().HasForeignKey(x => x.ObjectId);

            this.Ignore(x => x.Type);
        }
    }
}
