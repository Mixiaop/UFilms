using System;
using System.Collections.Generic;
using U.Domain.Entities.Auditing;
using UFilm.Domain.Media;

namespace UFilm.Domain.Movies
{
    /// <summary>
    /// 代表一张影人的“照片”
    /// </summary>
    public class FilmManPhoto : FullAuditedEntity
    {
        public FilmManPhoto() {
            Picture = new Picture();
        }

        /// <summary>
        /// 影人Id
        /// </summary>
        public int FilmManId { get; set; }

        /// <summary>
        /// 图片Id
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
        public string Description { get; set; }

        #region Navigation Properties
        /// <summary>
        /// 影人信息
        /// </summary>
        public virtual FilmMan FilmMan { get; set; }

        /// <summary>
        /// 缩略片列表
        /// 默认根据配置剪切
        /// </summary>
        public virtual List<Thumb> Thumbs { get; set; }
        #endregion

        #region Custom
        public Picture Picture { get; set; }
        #endregion
    }
}
