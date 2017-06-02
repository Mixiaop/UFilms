using UFilm.Domain.Movies;

namespace UFilm.EntityFramework.Mapping.Movies
{
    public partial class FilmManPhotoMap : UFilmEntityTypeConfiguration<FilmManPhoto>
    {
        public FilmManPhotoMap()
        {
            this.ToTable(DbConsts.DbTableName.Moveis_FilmManPhotos);
            this.HasKey(x => x.Id);


            this.HasRequired(x => x.FilmMan).WithMany().HasForeignKey(x => x.FilmManId);
            this.HasMany(x => x.Thumbs).WithRequired(x => x.FilmManPhoto);

            this.Ignore(x => x.Picture);
        }
    }
}
