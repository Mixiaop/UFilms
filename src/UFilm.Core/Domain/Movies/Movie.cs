using System;
using System.Collections.Generic;
using U.Domain.Entities.Auditing;
using UFilm.Domain.Media;

namespace UFilm.Domain.Movies
{
    /// <summary>
    /// 代表一部“影片”
    /// </summary>
    public class Movie : FullAuditedEntity
    {
        public Movie() {
            CnName = "";
            EnName = "";
            Year = 0;
            OtherEnName = "";
            OtherPostYear = "";
            MovieLength = "";
            Area = "";
            Language = "";
            MovieType = "";
            Introduction = "";
            Director = "";
            Actor = "";
            CoverId = 0;
            PhotoCount = 0;
            TorrentCount = 0;

            IsSeries = 0;
            SeriesNumber = 0;
            SeriesCount = 0;
            SeriesLength = "";
            SeriesId = 0;
            WebSite = "";
            DoubanRating = "";
            DoubanLink = "";
            ImdbCode = "";
            ImdbLink = "";
            Cover = new Picture();
        }

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
        /// 影片简介
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 导演信息
        /// </summary>
        public string Director { get; set; }

        /// <summary>
        /// 演员信息
        /// </summary>
        public string Actor { get; set; }

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
        /// 资源数
        /// </summary>
        public int TorrentCount { get; set; }

        /// <summary>
        /// 最后的分享（资源)的时间
        /// </summary>
        public DateTime? LastShareTime { get; set; }

        /// <summary>
        /// 官方网站
        /// </summary>
        public string WebSite { get; set; }

        /// <summary>
        /// IMDB 号
        /// </summary>
        public string ImdbCode { get; set; }

        /// <summary>
        /// IMDb 链接
        /// </summary>
        public string ImdbLink { get; set; }

        /// <summary>
        /// 豆瓣评分
        /// </summary>
        public string DoubanRating { get; set; }

        /// <summary>
        /// 豆瓣链接
        /// </summary>
        public string DoubanLink { get; set; }

        #region Series
        /// <summary>
        /// 是否为剧集 1-是,0-不是
        /// </summary>
        public int IsSeries { get; set; }

        /// <summary>
        /// 季数
        /// </summary>
        public int SeriesNumber { get; set; }

        /// <summary>
        /// 集数
        /// </summary>
        public int SeriesCount { get; set; }

        /// <summary>
        /// 单集片长
        /// </summary>
        public string SeriesLength { get; set; }

        /// <summary>
        /// 电影系列ID（生成或选择）
        /// </summary>
        public int SeriesId { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// 电影封面列表
        /// </summary>
        public virtual List<Thumb> Covers { get;set; }
        #endregion

        #region Custom Properties
        public virtual Picture Cover { get; set; }

        /// <summary>
        /// 格式化电影名称（年份 中文名 英文名）
        /// </summary>
        public string FormatName {
            get { return (CnName + " " + EnName + " " + Year).Trim(); }
        }
        #endregion

        public override string ToString()
        {
            return FormatName;
        }
    }
}
