using UFilm.Domain.Spiders;

namespace UFilm.EntityFramework.Mapping.Spiders
{
    public partial class SpiderTaskLogMap : UFilmEntityTypeConfiguration<SpiderTaskLog>
    {
        public SpiderTaskLogMap()
        {
            this.ToTable(DbConsts.DbTableName.Spiders_TaskLogs);
            this.HasKey(x => x.Id);

            this.HasRequired(x => x.Task).WithMany().HasForeignKey(x => x.TaskId);

        }
    }
}
