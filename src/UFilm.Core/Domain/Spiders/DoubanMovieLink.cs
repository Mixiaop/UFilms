using U.Domain.Entities.Auditing;

namespace UFilm.Domain.Spiders
{
    /// <summary>
    /// 豆瓣电影单页的链接
    /// </summary>
    public class DoubanMovieLink : CreationAuditedEntity
    {
        public DoubanMovieLink() {
            Name = "";
            Link = "";
            Remark = "";
            IsJoinTask = false;
        }

        /// <summary>
        /// 电影名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 是否加入过采集任务
        /// </summary>
        public bool IsJoinTask { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }
    }
}
