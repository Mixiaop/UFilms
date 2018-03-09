using UFilm.Domain.Tags;

namespace UFilm.EntityFramework.Mapping.Movies
{
    public partial class TagMap : UFilmEntityTypeConfiguration<Tag>
    {
        public TagMap()
        {
            this.ToTable(DbConsts.DbTableName.Tags_Tags);
            this.HasKey(x => x.Id);

            this.Ignore(x => x.Type);
        }
    }
}
