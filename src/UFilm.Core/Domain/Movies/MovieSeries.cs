using System;
using U.Domain.Entities;
using U.Domain.Entities.Auditing;

namespace UFilm.Domain.Movies
{
    /// <summary>
    /// 代表一个电影剧系列之间的关系
    /// </summary>
    public class MovieSeries : CreationAuditedEntity
    {
        public MovieSeries() {
            SeriesId = 0;
            MovieId = 0;
            SeriesNumber = 0;
        }

        /// <summary>
        /// 电影系列ID（自动生成）
        /// </summary>
        public int SeriesId { get; set; }

        /// <summary>
        /// 电影ID
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// 季数（第几季）
        /// </summary>
        public int SeriesNumber { get; set; }

        /// <summary>
        /// 系列别名，如：第一部
        /// </summary>
        public string SeriesAlias { get; set; }

        /// <summary>
        /// 电影信息
        /// </summary>
        public virtual Movie Movie { get; set; }
    }
}
