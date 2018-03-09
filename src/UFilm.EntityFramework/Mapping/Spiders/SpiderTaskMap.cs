using UFilm.Domain.Spiders;

namespace UFilm.EntityFramework.Mapping.Spiders
{
    public partial class SpiderTaskMap : UFilmEntityTypeConfiguration<SpiderTask>
    {
        public SpiderTaskMap()
        {
            this.ToTable(DbConsts.DbTableName.Spiders_Tasks);
            this.HasKey(x => x.Id);

            this.HasRequired(x => x.Movie).WithMany().HasForeignKey(x => x.ObjectId);
        }
    }
}
