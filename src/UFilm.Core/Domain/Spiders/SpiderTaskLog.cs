using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U.Domain.Entities.Auditing;

namespace UFilm.Domain.Spiders
{
    /// <summary>
    /// 代表一条“蜘蛛任务日志”
    /// </summary>
    public class SpiderTaskLog : CreationAuditedEntity
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
        public virtual SpiderTask Task { get; set; }
    }
}
