using System;
using U.Domain.Entities.Auditing;

namespace UFilm.Domain.Movies
{
    /// <summary>
    /// 代表一个“电影参与人员”
    /// </summary>
    public class MovieParticipant : FullAuditedEntity
    {
        /// <summary>
        /// 电影Id
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// 影人Id
        /// </summary>
        public int FilmManId { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public int JobTypeId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 电影信息
        /// </summary>
        public virtual Movie Movie { get; set; }

        /// <summary>
        /// 影人信息
        /// </summary>
        public virtual FilmMan FilmMan { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public MovieJobType JobType {
            get
            {
                return (MovieJobType)JobTypeId;
            }
            set {
                JobTypeId = (int)value;
            }
        }
    }
}
