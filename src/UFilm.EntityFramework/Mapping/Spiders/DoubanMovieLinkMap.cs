using UFilm.Domain.Spiders;

namespace UFilm.EntityFramework.Mapping.Spiders
{
    public partial class DoubanMovieLinkMap : UFilmEntityTypeConfiguration<DoubanMovieLink>
    {
        public DoubanMovieLinkMap()
        {
            this.ToTable(DbConsts.DbTableName.Spiders_DoubanMovieLinks);
            this.HasKey(x => x.Id);
        }
    }
}
