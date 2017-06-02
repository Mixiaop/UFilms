using System;
using U.Domain.Entities.Auditing;

namespace UFilm.Domain.Movies
{
    /// <summary>
    /// 代表一个“电影资源（种子)”
    /// </summary>
    public class MovieTorrent : FullAuditedEntity
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 电影Id
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// 点击量
        /// </summary>
        public int Hits { get; set; }

        /// <summary>
        /// 资源类型，如：电驴、迅雷等
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 资源大小
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 资源链接
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 电影信息
        /// </summary>
        public virtual Movie Movie { get; set; }
    }
}
