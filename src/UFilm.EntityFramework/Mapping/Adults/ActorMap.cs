using UFilm.Domain.Adults;

namespace UFilm.EntityFramework.Mapping.Adults
{

    public partial class ActorMap : UFilmEntityTypeConfiguration<Actor>
    {
        public ActorMap()
        {
            this.ToTable(DbConsts.DbTableName.Adults_Actors);
            this.HasKey(x => x.Id);

            this.HasMany(x => x.Avatars).WithRequired(x => x.AdultActor);

            this.Ignore(x => x.Avatar);
            this.Ignore(x => x.FormatGender);
        }
    }
}



