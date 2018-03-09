using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFilm.Domain.Spiders;

namespace UFilm.Services.Spiders
{
    public class DoubanLinkService : IDoubanLinkService
    {
        private readonly IDoubanMovieLinkRepository _linkRepository;
        public DoubanLinkService(IDoubanMovieLinkRepository linkRepository)
        {
            _linkRepository = linkRepository;
        }

        public bool Exists(string link) { 
            link = link.Trim();
            var count = _linkRepository.Count(x => x.Link == link);

            return count > 0;
        }

        public List<DoubanMovieLink> GetListByUnUsed(int count = 0)
        {
            var query = _linkRepository.GetAll();
            query = query.Where(x => x.IsJoinTask == false).OrderByDescending(x => x.CreationTime);
            if (count > 0)
            {
                return query.Take(count).ToList();
            }
            else
                return query.ToList();
        }

        public int GetCountByUnUsed() { 
            return _linkRepository.Count(x => x.IsJoinTask == false);
        }

        public void Insert(DoubanMovieLink link)
        {
            if (!Exists(link.Link))
            {
                _linkRepository.Insert(link);
            }
        }

        public void Update(DoubanMovieLink link)
        {
            _linkRepository.Update(link);
        }
    }
}
