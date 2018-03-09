using System.Collections.Generic;
using System.Linq;
using U.Application.Services.Dto;
using U.UI;
using UFilm.Domain.Tags;

namespace UFilm.Services.Tags
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        public TagService(ITagRepository tagRepository) {
            _tagRepository = tagRepository;
        }

        public PagedResultDto<Tag> Search(TagType tagType = TagType.None, string keywords = "", int pageIndex = 1, int pageSize = 20) {
            int typeId = (int)tagType;
            keywords = keywords.Trim();

            var query = _tagRepository.GetAll();
            if (tagType != TagType.None) {
                query = query.Where(x => x.TypeId == typeId);
            }
            if (keywords.IsNotNullOrEmpty()) {
                query = query.Where(x => x.Name.Contains(keywords));
            }

            query = query.OrderByDescending(x => x.Count)
                         .ThenByDescending(x => x.CreationTime);

            var count = query.Count();

            var list = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PagedResultDto<Tag>(count, list);
        }

        public IList<Tag> GetAllTags(TagType type, int count = 0) {
            int typeId = (int)type;
            var query = _tagRepository.GetAll()
                        .WhereIf(!type.Equals(TagType.None), x => x.TypeId == typeId);

            query = query.OrderByDescending(x => x.Count);
            if (count > 0)
                query = query.Take(count);

            var list = query.ToList();

            return list;
        }

        public bool ExistsName(TagType tagType, string tagName) {
            int typeid = (int)tagType;
            var count = _tagRepository.Count(x => x.TypeId == typeid && x.Name == tagName.Trim());
            return count > 0;
        }

        public Tag GetByName(TagType tagType, string tagName) {
            if (tagName.IsNullOrEmpty())
                throw new UserFriendlyException("标签名称不能为空");
            int typeId = (int)tagType;
            var tag = _tagRepository.GetAll().Where(x => x.TypeId == typeId && x.Name == tagName).FirstOrDefault();
            return tag;
        }

        public Tag Get(int tagId) {
            return _tagRepository.Get(tagId);
        }

        public void Update(TagType tagType, List<string> newTagNames, List<string> oldTagNames = null)
        {
            if (oldTagNames == null)
            {
                //insert
                if (newTagNames != null)
                {
                    foreach (var tag in newTagNames)
                        UpdateTag(tagType, tag);
                }
            }
            else
            {
                if (newTagNames != null)
                {
                    foreach (var tag in newTagNames)
                    {
                        if (oldTagNames.Contains(tag))
                        {
                            //已存在
                            oldTagNames.Remove(tag);
                        }
                        else
                        {
                            //新的
                            UpdateTag(tagType, tag);
                        }
                    }

                    //剩下的就是删除的
                    if (oldTagNames.Count > 0)
                    {
                        foreach (var otag in oldTagNames)
                            DeleteTag(tagType, otag);
                    }
                }
            }
        }

        public void Delete(int tagId) {
            var tag = Get(tagId);
            Delete(tag);
        }

        public void Delete(Tag tag) {
            _tagRepository.Delete(tag);
        }

        #region Utilities
        private void UpdateTag(TagType tagType, string tagName)
        {
            if (ExistsName(tagType, tagName))
            {
                //更新计数
                var tagInfo = GetByName(tagType, tagName);
                if (tagInfo != null)
                {
                    tagInfo.Count++;
                    _tagRepository.Update(tagInfo);
                }
            }
            else
            {
                Tag tagInfo = new Tag();
                tagInfo.Count = 1;
                tagInfo.Name = tagName;
                tagInfo.Alias = System.Web.HttpUtility.UrlEncode(tagInfo.Name);
                tagInfo.Type = tagType;
                _tagRepository.Insert(tagInfo);
            }
        }

        private void DeleteTag(TagType tagType, string tagName)
        {
            var tagInfo = GetByName(tagType, tagName);
            if (tagInfo != null && tagInfo.Count > 0)
            {
                tagInfo.Count--;
                _tagRepository.Update(tagInfo);
            }
        }
        #endregion
    }
}
