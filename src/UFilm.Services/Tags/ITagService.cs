using System.Collections.Generic;
using U.Application.Services.Dto;
using UFilm.Domain.Tags;

namespace UFilm.Services.Tags
{
    public interface ITagService : U.Application.Services.IApplicationService
    {
        /// <summary>
        /// 搜索标签
        /// </summary>
        /// <param name="tagType"></param>
        /// <param name="keywords"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>

        PagedResultDto<Tag> Search(TagType tagType = TagType.None, string keywords = "", int pageIndex = 1, int pageSize = 20);

        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <param name="type">标签类型</param>
        /// <param name="count">多少条（默认为全部）</param>
        /// <returns></returns>
        IList<Tag> GetAllTags(TagType type, int count = 0);

        /// <summary>
        /// 是否存在标签名称
        /// </summary>
        /// <param name="tagType"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        bool ExistsName(TagType tagType, string tagName);

        /// <summary>
        /// 通过名称获取标签信息
        /// </summary>
        /// <param name="tagType"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        Tag GetByName(TagType tagType, string tagName);

        Tag Get(int tagId);

        /// <summary>
        /// 更新多个标签（如果不存在则添加，如果已存在则数量+1）
        /// 更新内容时，会比对旧数组与新数组，来（增加或减少）标签的Count数
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="newTagNames">新标签数组</param>
        /// <param name="oldTagNames">旧标签数组，更新内容时使用</param>
        void Update(TagType tagType, List<string> newTagNames, List<string> oldTagNames = null);

        void Delete(int tagId);

        void Delete(Tag tag);

    }
}
