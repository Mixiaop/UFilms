using System;
using UFilm.Services.Movies.Dto;

namespace UFilm.Services.Spiders.Dto
{
    public class SpiderTaskDto : U.Application.Services.Dto.CreationAuditedEntityDto
    {
        /// <summary>
        /// 采集的对象Id，默认为电影
        /// </summary>
        public int ObjectId { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 需要采集的任务链接,逗号分割
        /// </summary>
        public string Links { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public bool Finished { get; set; }

        /// <summary>
        /// 正在采集中
        /// </summary>
        public bool Spidering { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? FinishedTime { get; set; }

        /// <summary>
        /// 成功采集的内容描述
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 成功解析的链接数
        /// </summary>
        public int LinkCount { get; set; }

        /// <summary>
        /// 成功解析的图片数
        /// </summary>
        public int ImageCount { get; set; }

        public MovieBriefDto Movie { get; set; }
    }
}
