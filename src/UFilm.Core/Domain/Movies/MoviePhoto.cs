using System;
using System.Collections.Generic;
using U.Domain.Entities.Auditing;
using UFilm.Domain.Media;

namespace UFilm.Domain.Movies
{
    /// <summary>
    /// 代表一张“影片的图片”
    /// </summary>
    public class MoviePhoto : FullAuditedEntity
    {
        public MoviePhoto() {
            Picture = new Picture();
        }

        /// <summary>
        /// 关联的影片Id
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// 图片Id
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// 图片类型Id
        /// </summary>
        public int PhotoTypeId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        #region Navi
        /// <summary>
        /// 关联的影片
        /// </summary>
        public virtual Movie Movie { get; set; }

        /// <summary>
        /// 缩略片列表
        /// 默认根据配置剪切
        /// </summary>
        public virtual List<Thumb> Thumbs { get; set; }
        #endregion

        #region Custom
        /// <summary>
        /// 图片类型
        /// </summary>
        public MoviePhotoType PhotoType
        {
            get { return (MoviePhotoType)PhotoTypeId; }
            set { PhotoTypeId = (int)value; }
        }

        /// <summary>
        /// 图片对象
        /// </summary>
        public Picture Picture { get; set; }
        #endregion
    }
}
