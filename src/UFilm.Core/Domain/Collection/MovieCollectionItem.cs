using System;
using System.Collections.Generic;
using U.Domain.Entities.Auditing;
using UFilm.Domain.Movies;

namespace UFilm.Domain.Collection
{
    /// <summary>
    /// 代表一条“精选集项（1部电影）"
    /// </summary>
    public class MovieCollectionItem : FullAuditedEntity
    {
        /// <summary>
        /// 精选集Id
        /// </summary>
        public int CollectionId { get; set; }

        /// <summary>
        /// 电影Id
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 排序，默认为时间倒序
        /// </summary>
        public int Order { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
