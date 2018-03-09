using System.Collections.Generic;
using System.Linq;
using UFilm.Domain.Adults;

namespace UFilm.Services.Adults
{
    public class SpiderService : ISpiderService
    {
        private readonly ISpiderLinkRepository _linkRepository;
        public SpiderService(ISpiderLinkRepository linkRepository) {
            _linkRepository = linkRepository;
        }

        #region SpiderLink

        public bool Exists(string link) { 
            link = link.Trim();
            return _linkRepository.Count(x => x.Link == link) > 0;
        }

        public SpiderLink Get(int linkId) {
            return _linkRepository.Get(linkId);
        }

        public List<SpiderLink> GetListByUnUsed(SpiderLinkSource source = SpiderLinkSource.WwwNH87cn, int count = 0, OrderBy orderBy = OrderBy.CreationTimeDesc)
        {
            var strSource = source.ToString();

            var query = _linkRepository.GetAll().Where(x => x.Source == strSource && x.IsJoinTask == false);

            if (orderBy == OrderBy.CreationTimeAsc)
            {
                query = query.OrderBy(x => x.CreationTime);
            }
            else {
                query = query.OrderBy(x => x.CreationTime);
            }

            return query.ToList();
        }

        public int GetCountByUnUsed(SpiderLinkSource source = SpiderLinkSource.WwwNH87cn) {
            var strSource = source.ToString();

            return _linkRepository.Count(x => x.Source == strSource && x.IsJoinTask == false);
        }

        public void InsertLink(SpiderLink link, SpiderLinkSource source = SpiderLinkSource.WwwNH87cn) {
            link.Source = source.ToString();
            _linkRepository.Insert(link);
        }

        public void Update(SpiderLink link) {
            _linkRepository.Update(link);
        }
        #endregion
    }
}
