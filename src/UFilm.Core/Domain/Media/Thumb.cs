using U.Domain.Entities.Auditing;
using UFilm.Domain.Movies;
using UFilm.Domain.Collection;

namespace UFilm.Domain.Media
{
    /// <summary>
    /// 代表一张“缩略图”
    /// 影片或影人的缩略图，解决一张图片被剪切成多张
    /// </summary>
    public class Thumb : CreationAuditedEntity
    {
        public Thumb()
        {
            ObjectId = 0;
            IsSquare = false;
            Size = 0;
            Url = "";
        }

        public int ObjectId { get; set; }

        public bool IsSquare { get; set; }

        public int TypeId { get; set; }

        public int Size { get; set; }

        public string Url { get; set; }

        public virtual Movie Movie { get; set; }

        public virtual MoviePhoto MoviePhoto { get; set; }

        public virtual FilmMan FilmMan { get; set; }

        public virtual FilmManPhoto FilmManPhoto { get; set; }

        public virtual MovieCollection Collection { get; set; }

        public virtual Domain.Adults.Actor AdultActor { get; set; }

        public virtual Domain.Adults.ActorPhoto AdultsActorPhoto { get; set; }

        public virtual Domain.Adults.LMovie AdultMovie { get; set; }

        public ThumbType Type {
            get { return (ThumbType)TypeId; }
            set { TypeId = (int)value; }
        }
    }
}
