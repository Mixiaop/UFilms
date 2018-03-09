using System;
using UFilm.Services.Movies.Dto;

namespace UFilm.Services.Spiders.Dto
{
    public class SpiderTaskLogDto : U.Application.Services.Dto.CreationAuditedEntityDto
    {
        /// <summary>
        /// 任务标识
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 日志消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 任务信息
        /// </summary>
        public SpiderTaskDto Task { get; set; }

        public string Time { get; set; }
    }
}
