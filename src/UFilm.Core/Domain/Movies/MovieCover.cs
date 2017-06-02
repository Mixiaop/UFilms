using U.Domain.Entities.Auditing;

namespace UFilm.Domain.Movies
{
    public class MovieCover : FullAuditedEntity
    {
        public MovieCover() {
            MovieId = 0;
            IsSquare = false;
            Size = 0;
            Url = "";
        }

        public int MovieId { get; set; }

        public bool IsSquare { get; set; }

        public int Size { get; set; }

        public string Url { get; set; }

        #region Navigation Properties
        /// <summary>
        /// 电影信息
        /// </summary>
        public virtual Movie Movie { get; set; }
        #endregion
    }
}
