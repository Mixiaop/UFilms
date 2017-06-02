using U.Domain.Entities.Auditing;

namespace UFilm.Domain.Adults
{
    /// <summary>
    /// 代表一个“电影参与人员”
    /// </summary>
    public class LMovieActor : FullAuditedEntity
    {
        /// <summary>
        /// 电影Id
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// 影人Id
        /// </summary>
        public int ActorId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 电影信息
        /// </summary>
        public virtual LMovie Movie { get; set; }

        /// <summary>
        /// 影人信息
        /// </summary>
        public virtual Actor Actor { get; set; }
    }
}
