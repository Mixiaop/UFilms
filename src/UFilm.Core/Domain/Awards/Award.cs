using System.Collections.Generic;
using U.Domain.Entities.Auditing;
using UFilm.Domain.Movies;

namespace UFilm.Domain.Awards
{
    public class Award : FullAuditedEntity
    {
        /// <summary>
        /// 奖项名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 奖项类型
        /// </summary>
        public AwardType AwardType { get; set; }

        /// <summary>
        /// 奖项简介
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 第届数
        /// </summary>
        public int Th { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 举办时间
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// 举办地点
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 主持人.例：中文名:ID,中文名2:ID2
        /// </summary>
        public string Comperes { get; set; }

        /// <summary>
        /// 官方网站
        /// </summary>
        public string WebSite { get; set; }

        /// <summary>
        /// 主持人
        /// </summary>
        public IList<FilmMan> FormatComperes { get; set; }
    }
}
