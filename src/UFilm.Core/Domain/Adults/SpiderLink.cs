using U.Domain.Entities.Auditing;

namespace UFilm.Domain.Adults
{
    public class SpiderLink : CreationAuditedEntity
    {
        public SpiderLink() {
            Name = "";
            Link = "";
            IsJoinTask = false;
            Source = "";
            Remark = "";
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 是否加入过采集任务
        /// </summary>
        public bool IsJoinTask { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
