using System;
using System.Collections.Generic;
using U.Application.Services;
using U.Application.Services.Dto;
using UFilm.Domain.Adults;

namespace UFilm.Services.Adults
{
    public interface IActorPhotoService : IApplicationService
    {
        PagedResultDto<ActorPhoto> QueryPhotos(int actorId, bool? isX = null, int pageIndex = 1, int pageSize = 16);

        ActorPhoto Get(int photoId);

        void InsertPhotos(int actorId, List<int> photoIds, bool isX = false);

        void Update(ActorPhoto photo);

        void Delete(int photoId);

        void Delete(ActorPhoto photo);
    }
}
