using U.Domain.Entities.Auditing;

namespace UFilm.Domain.Awards
{
    public class AwardItem : CreationAuditedEntity
    {
        public AwardItem() {
            AwardId = 0;
            Name = "";
            IsWinning = 0;
            WinningType = 0;
            MovieId = 0;
            MovieName = "";
            MovieEnName = "";
            FilmManId = 0;
            FilmManName = "";
            FilmManEnName = "";
            Description = "";
        }

        /// <summary>
        /// 奖项ID
        /// </summary>
        public int AwardId { get; set; }

        /// <summary>
        /// 当前奖项名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否获奖
        /// </summary>
        public int IsWinning { get; set; }

        /// <summary>
        /// 获奖类型（0-影片 1-影人）
        /// </summary>
        public int WinningType { get; set; }

        /// <summary>
        /// 影片ID
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// 影片名称
        /// </summary>
        public string MovieName { get; set; }

        /// <summary>
        /// 影片英文名
        /// </summary>
        public string MovieEnName { get; set; }

        /// <summary>
        /// 影人ID
        /// </summary>
        public int FilmManId { get; set; }

        /// <summary>
        /// 影人名称
        /// </summary>
        public string FilmManName { get; set; }

        /// <summary>
        /// 影人英文名
        /// </summary>
        public string FilmManEnName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

    }
}
