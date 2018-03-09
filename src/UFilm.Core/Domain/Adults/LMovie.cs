using System;
using System.Collections.Generic;
using U.Domain.Entities.Auditing;
using UFilm.Domain.Media;

namespace UFilm.Domain.Adults
{ 
    /// <summary>
    /// 代表一部“影片”
    /// </summary>
    public class LMovie : FullAuditedEntity
    {
        public LMovie()
        {
            Code = "";
            CnName = "";
            EnName = "";
            Year = 0;
            OtherEnName = "";
            OtherPostYear = "";
            MovieLength = "";
            Area = "";
            Language = "";
            MovieType = "";
            Actors = "";
            Introduction = "";
            CoverId = 0;
            PhotoCount = 0;
            Cover = new Picture();
        }

        /// <summary>
        /// 番号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 电影中文名
        /// </summary>
        public string CnName { get; set; }

        /// <summary>
        /// 电影英文名
        /// </summary>
        public string EnName { get; set; }

        /// <summary>
        /// 电影年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 其他外文名
        /// </summary>
        public string OtherEnName { get; set; }

        /// <summary>
        /// 上映日期其他
        /// </summary>
        public string OtherPostYear { get; set; }

        /// <summary>
        /// 片长如：120分钟
        /// </summary>
        public string MovieLength { get; set; }

        /// <summary>
        /// 国家地区如：美国 日韩
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 语言 如：英语
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 电影类型如：动作
        /// </summary>
        public string MovieType { get; set; }

        /// <summary>
        /// 演员信息
        /// </summary>
        public string Actors { get; set; }

        /// <summary>
        /// 影片简介
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 电影封面Id
        /// Covers的封面比例都是通过此 Id 动态切的
        /// </summary>
        public int CoverId { get; set; }

        /// <summary>
        /// 图片数量
        /// </summary>
        public int PhotoCount { get; set; }

        /// <summary>
        /// 热度 
        /// </summary>
        public int Hits { get; set; }

        #region Navigation Properties
        /// <summary>
        /// 电影封面列表
        /// </summary>
        public virtual List<Thumb> Covers { get; set; }
        #endregion

        #region Custom Properties
        public virtual Picture Cover { get; set; }

        /// <summary>
        /// 格式化电影名称（年份 中文名 英文名）
        /// </summary>
        public string FormatName
        {
            get { return (CnName + " " + EnName + " " + Year).Trim(); }
        }
        #endregion

        public override string ToString()
        {
            return FormatName;
        }
    }
}
