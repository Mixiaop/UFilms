using UFilm.Domain.Movies;

namespace UFilm.EntityFramework.Mapping.Movies
{
    public partial class FilmManMap : UFilmEntityTypeConfiguration<FilmMan>
    {
        public FilmManMap()
        {
            this.ToTable(DbConsts.DbTableName.Movies_FilmMans);
            this.HasKey(x => x.Id);

            this.HasMany(x => x.Avatars).WithRequired(x => x.FilmMan);

            this.Ignore(x => x.Avatar);
            this.Ignore(x => x.FormatGender);
        }
    }
}
