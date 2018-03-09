using System;
using U.Application.Services.Dto;
using UFilm.Services.Media.Dto;

namespace UFilm.Services.Movies.Dto
{
    public class MovieBriefDto : FullAuditedEntityDto
    {
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
        /// 导演信息
        /// </summary>
        public string Director { get; set; }

        /// <summary>
        /// 演员信息
        /// </summary>
        public string Actor { get; set; }

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

        #region Custom Properties
        public PictureDto Cover { get; set; }

        /// <summary>
        /// 格式化电影名称（年份 中文名 英文名）
        /// </summary>
        public string FormatName { get; set; }
        #endregion
    }
}
