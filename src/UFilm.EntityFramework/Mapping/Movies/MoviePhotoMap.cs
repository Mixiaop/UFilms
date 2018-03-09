using UFilm.Domain.Movies;

namespace UFilm.EntityFramework.Mapping.Movies
{
    public partial class MoviePhotoMap : UFilmEntityTypeConfiguration<MoviePhoto>
    {
        public MoviePhotoMap()
        {
            this.ToTable(DbConsts.DbTableName.Movies_MoviePhotos);
            this.HasKey(x => x.Id);

            this.Property(x => x.PhotoTypeId).HasColumnName("PhotoType");

            this.HasRequired(x => x.Movie).WithMany().HasForeignKey(x => x.MovieId);
            this.HasMany(x => x.Thumbs).WithRequired(x => x.MoviePhoto);

            this.Ignore(x => x.PhotoType);
            this.Ignore(x => x.Picture);
        }
    }
}
