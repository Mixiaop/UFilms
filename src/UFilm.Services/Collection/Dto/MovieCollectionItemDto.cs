
namespace UFilm.Services.Collection.Dto
{
    public class MovieCollectionItemDto: U.Application.Services.Dto.FullAuditedEntityDto
    {
        /// <summary>
        /// 精选集Id
        /// </summary>
        public int CollectionId { get; set; }

        /// <summary>
        /// 电影Id
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 排序，默认为时间倒序
        /// </summary>
        public int Order { get; set; }
    }
}
