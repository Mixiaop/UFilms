using UFilm.Domain.Adults;

namespace UFilm.EntityFramework.Mapping.Adults
{

    public partial class ActorPhotoMap : UFilmEntityTypeConfiguration<ActorPhoto>
    {
        public ActorPhotoMap()
        {
            this.ToTable(DbConsts.DbTableName.Adults_ActorPhotos);
            this.HasKey(x => x.Id);


            this.HasRequired(x => x.Actor).WithMany().HasForeignKey(x => x.ActorId);
            this.HasMany(x => x.Thumbs).WithRequired(x => x.AdultsActorPhoto);

            this.Ignore(x => x.Picture);
        }
    }
}



