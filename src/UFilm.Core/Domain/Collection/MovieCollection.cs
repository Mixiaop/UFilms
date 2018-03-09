using System;
using System.Collections.Generic;
using U.Domain.Entities.Auditing;
using UFilm.Domain.Media;

namespace UFilm.Domain.Collection
{
    /// <summary>
    /// 代表一张“电影精选集”
    /// </summary>
    public class MovieCollection : FullAuditedEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 简介 
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 封面
        /// </summary>
        public int CoverId { get; set; }

        /// <summary>
        /// 集数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 点击量
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tags { get; set; }

        public virtual IList<Thumb> Covers { get; set; }

        public Picture Cover { get; set; }
    }
}
