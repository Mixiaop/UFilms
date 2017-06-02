using System.Collections.Generic;
using U.Application.Services.Dto;
using UFilm.Domain.Adults;
using UFilm.Services.Adults.Dto;
namespace UFilm.Services.Adults
{
    public interface IActorService : U.Application.Services.IApplicationService
    {

        PagedResultDto<Actor> Search(string keywords, int pageIndex = 1, int pageSize = 16, ActorSearchOrderBy orderBy = ActorSearchOrderBy.CreationTimeDesc);

        IList<ActorBriefDto> GetAll(string keywords, int count = 10, ActorSearchOrderBy orderBy = ActorSearchOrderBy.HitsDesc);

        bool Exists(string cnName);
        
        Actor Get(int actorId);

        Actor Get(string cnName);

        int Insert(Actor actor);

        void Update(Actor actor, bool isChangeCover = false);

        void Delete(int actorId);

        void Delete(Actor actor);
    }

    public enum ActorSearchOrderBy { 
        CreationTimeDesc,
        CreatoinTimeAsc,
        HitsDesc
    }
}
