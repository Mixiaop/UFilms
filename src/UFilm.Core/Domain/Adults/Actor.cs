using System.Collections.Generic;
using U.Domain.Entities.Auditing;
using UFilm.Domain.Media;

namespace UFilm.Domain.Adults
{
    /// <summary>
    /// 代表一个“（男）女优”
    /// </summary>
    public class Actor : FullAuditedEntity
    {
        public Actor()
        {
            CnName = "";
            EnName = "";
            Pinyin = "";
            Gender = -1;
            Constellation = "";
            Birthday = "";
            Deadday = "";
            PlaceOfBirth = "";
            Job = "";
            MoreEnName = "";
            MoreCnName = "";
            Introduction = "";
            AvatarId = 0;
            WebSite = "";
            FamilyInfo = "";
            PhotoCount = 0;

            Avatar = new Picture();
        }

        /// <summary>
        /// 中文名
        /// </summary>
        public string CnName { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string EnName { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string Pinyin { get; set; }

        /// <summary>
        /// 姓别（1=男，0=女，-1=空）
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 星座
        /// </summary>
        public string Constellation { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// 去世日期
        /// </summary>
        public string Deadday { get; set; }

        /// <summary>
        /// 出生地
        /// </summary>
        public string PlaceOfBirth { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// 更多外文名
        /// </summary>
        public string MoreEnName { get; set; }

        /// <summary>
        /// 更多中文名
        /// </summary>
        public string MoreCnName { get; set; }

        /// <summary>
        /// 影人介绍
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 头像Id
        /// </summary>
        public int AvatarId { get; set; }

        /// <summary>
        /// 个人网站
        /// </summary>
        public string WebSite { get; set; }

        /// <summary>
        /// 家庭信息
        /// </summary>
        public string FamilyInfo { get; set; }

        /// <summary>
        /// 点击量
        /// </summary>
        public int Hits { get; set; }

        /// <summary>
        /// 照片数量
        /// </summary>
        public int PhotoCount { get; set; }

        /// <summary>
        /// 参与的电影数量
        /// </summary>
        public int MovieCount { get; set; }

        #region Navi
        /// <summary>
        /// 缩略片列表
        /// 默认根据配置剪切
        /// </summary>
        public virtual List<Thumb> Avatars { get; set; }
        #endregion

        #region Custom
        /// <summary>
        /// 源图Url
        /// </summary>
        public Picture Avatar { get; set; }

        public string FormatGender { get; set; }

        #endregion

        public override string ToString()
        {
            return CnName + " " + EnName;
        }
    }
}
