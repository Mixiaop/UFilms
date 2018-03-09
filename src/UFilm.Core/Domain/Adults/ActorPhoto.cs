using System.Collections.Generic;
using U.Domain.Entities.Auditing;
using UFilm.Domain.Media;

namespace UFilm.Domain.Adults
{

    /// <summary>
    /// 代表一张影人的“照片”
    /// </summary>
    public class ActorPhoto : FullAuditedEntity
    {
        public ActorPhoto()
        {
            Picture = new Picture();
            ActorId = 0;
            PictureId = 0;
            Description = "";
            IsX = false;
        }

        /// <summary>
        /// 影人Id
        /// </summary>
        public int ActorId { get; set; }

        /// <summary>
        /// 图片Id
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否限制级
        /// </summary>
        public bool IsX { get; set; }

        #region Navigation Properties
        /// <summary>
        /// 影人信息
        /// </summary>
        public virtual Actor Actor { get; set; }

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
