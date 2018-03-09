using UFilm.Domain.Adults;

namespace UFilm.EntityFramework.Mapping.Adults
{

 public partial class SpiderLinkMap : UFilmEntityTypeConfiguration<SpiderLink>
    {
        public SpiderLinkMap()
        {
            this.ToTable(DbConsts.DbTableName.Adults_SpiderLinks);
            this.HasKey(x => x.Id);
        }
    }
}



