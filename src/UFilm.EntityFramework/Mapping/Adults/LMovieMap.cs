using UFilm.Domain.Adults;

namespace UFilm.EntityFramework.Mapping.Adults
{
    public partial class LMovieMap : UFilmEntityTypeConfiguration<LMovie>
    {
        public LMovieMap()
        {
            this.ToTable(DbConsts.DbTableName.Adults_Movies);
            this.HasKey(x => x.Id);


            this.HasMany(x => x.Covers).WithRequired(x => x.AdultMovie);

            this.Ignore(x => x.Cover);
            this.Ignore(x => x.FormatName);
        }
    }
}



