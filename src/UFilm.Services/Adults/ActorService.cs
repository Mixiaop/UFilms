using System.Collections.Generic;
using System.Linq;
using U.AutoMapper;
using U.Application.Services.Dto;
using UFilm.Services.Media;
using UFilm.Domain.Adults;
using UFilm.Services.Adults.Dto;

namespace UFilm.Services.Adults
{
    public class ActorService : IActorService
    {
        private IActorRepository _actorRepository;
        private IThumbService _thumbService;

        public ActorService(IActorRepository actorRepository, IThumbService thumbService)
        {
            _actorRepository = actorRepository;
            _thumbService = thumbService;
        }

        public PagedResultDto<Actor> Search(string keywords, int pageIndex = 1, int pageSize = 16, ActorSearchOrderBy orderBy = ActorSearchOrderBy.CreationTimeDesc)
        {
            keywords = keywords.Trim();
            var query = _actorRepository.GetAll();
            if (keywords.IsNotNullOrEmpty()) {
                query = query.Where(x => x.CnName.Contains(keywords) || x.EnName.Contains(keywords) || x.MoreCnName.Contains(keywords) || x.MoreEnName.Contains(keywords));
            }

            switch (orderBy) { 
                case ActorSearchOrderBy.CreationTimeDesc:
                    query = query.OrderByDescending(x => x.CreationTime);
                    break;
                case ActorSearchOrderBy.CreatoinTimeAsc:
                    query = query.OrderBy(x => x.CreationTime);
                    break;
                case ActorSearchOrderBy.HitsDesc:
                    query = query.OrderByDescending(x => x.Hits);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreationTime);
                    break;
            }

            var count = query.Count();
            var list = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            foreach (var info in list)
                Format(info);

            return new PagedResultDto<Actor>(count, list);
        }

        public IList<ActorBriefDto> GetAll(string keywords, int count = 10, ActorSearchOrderBy orderBy = ActorSearchOrderBy.HitsDesc)
        {
            keywords = keywords.Trim();
            var query = _actorRepository.GetAll();
            if (keywords.IsNotNullOrEmpty())
            {
                query = query.Where(x => x.CnName.Contains(keywords) || x.EnName.Contains(keywords) || x.MoreCnName.Contains(keywords) || x.MoreEnName.Contains(keywords));
            }

            switch (orderBy)
            {
                case ActorSearchOrderBy.CreationTimeDesc:
                    query = query.OrderByDescending(x => x.CreationTime);
                    break;
                case ActorSearchOrderBy.CreatoinTimeAsc:
                    query = query.OrderBy(x => x.CreationTime);
                    break;
                case ActorSearchOrderBy.HitsDesc:
                    query = query.OrderByDescending(x => x.Hits);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreationTime);
                    break;
            }

            IList<Actor> list;
            if (count > 0)
            {
                list = query.Take(count).ToList();
            }
            else {
                list = query.ToList();
            }
            if (list != null)
            {
                foreach (var info in list)
                    Format(info);
            }

            return list.MapTo<IList<ActorBriefDto>>();
        }

        public bool Exists(string cnName)
        {
            cnName = cnName.Trim();
            return _actorRepository.Count(x => x.CnName == cnName) > 0;
        }

        public Actor Get(int actorId) {
            var actor =  _actorRepository.Get(actorId);
            Format(actor);
            return actor;
        }

        public Actor Get(string cnName) {
            cnName = cnName.Trim();
            var actor = _actorRepository.GetAll().Where(x => x.CnName == cnName).FirstOrDefault();
            Format(actor);
            return actor;
        }

        public int Insert(Actor actor) {
            int id = _actorRepository.InsertAndGetId(actor);
            return id;
        }

        public void Update(Actor actor, bool isChangeCover = false)
        {
            _actorRepository.Update(actor);
            if (isChangeCover)
                _thumbService.Clear(actor.Id);
        }

        public void Delete(int actorId) {
            var actor = Get(actorId);
            Delete(actor);
        }

        public void Delete(Actor actor) {
            _actorRepository.Delete(actor);
        }

        #region Utilities
        public void Format(Actor actor)
        {
            if (actor != null)
            {
                _thumbService.Format(actor);
            }
        }
        #endregion
    }
}
