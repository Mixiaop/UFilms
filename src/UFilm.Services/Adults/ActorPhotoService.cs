using System;
using System.Collections.Generic;
using System.Linq;
using U.Application.Services;
using U.Application.Services.Dto;
using UFilm.Domain.Adults;
using UFilm.Services.Media;

namespace UFilm.Services.Adults
{
    public class ActorPhotoService : IActorPhotoService
    {
        private readonly IActorPhotoRepository _photoRepository;
        private readonly IThumbService _thumbService;
        private readonly IActorService _actorService;

        public ActorPhotoService(IActorPhotoRepository photoRepository, IThumbService thumbService, IActorService actorService)
        {
            _photoRepository = photoRepository;
            _thumbService = thumbService;
            _actorService = actorService;
        }
        public PagedResultDto<ActorPhoto> QueryPhotos(int actorId, bool? isX = null, int pageIndex = 1, int pageSize = 16) {
            var query = _photoRepository.GetAll()
                        .WhereIf(actorId > 0, x => x.ActorId == actorId);

            if (isX.HasValue) {
                if (isX.Value)
                    query = query.Where(x => x.IsX == true);
                else
                    query = query.Where(x => x.IsX == false);
            }
            query = query.OrderByDescending(x => x.CreationTime);
            var count = query.Count();
            var list = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            foreach (var info in list)
                _thumbService.Format(info);

            return new PagedResultDto<ActorPhoto>(count, list);
        }

        public ActorPhoto Get(int photoId) {
            return _photoRepository.Get(photoId);
        }

        public void InsertPhotos(int actorId, List<int> photoIds, bool isX = false)
        {
            var actor = _actorService.Get(actorId);
            if (actor != null) {
                if (photoIds != null && photoIds.Count > 0)
                {
                    foreach (var pid in photoIds)
                    {
                        ActorPhoto photo = new ActorPhoto();
                        photo.IsX = isX;
                        photo.ActorId = actor.Id;
                        photo.PictureId = pid;
                        _photoRepository.Insert(photo);
                    }

                    actor.PhotoCount += photoIds.Count;
                    _actorService.Update(actor);
                }
            }
        }

        public void Update(ActorPhoto photo) {
            _photoRepository.Update(photo);
        }

        public void Delete(int photoId) {
            var photo = Get(photoId);
            if (photo != null)
                Delete(photo);
        }

        public void Delete(ActorPhoto photo) {
            _photoRepository.Delete(photo);
            var actor = _actorService.Get(photo.ActorId);
            if (actor != null)
            {
                actor.PhotoCount--;
                _actorService.Update(actor);
            }

            
        }
    }
}
