using System;
using U.Application.Services.Dto;
using UFilm.Domain.Adults;

namespace UFilm.Console.Models.Adults
{
    public class ActorPhotosListModel : PagingModel
    {
        public int GetActorId { get; set; }

        public int GetIsX { get; set; }


        public Actor Actor { get; set; }
        public PagedResultDto<ActorPhoto> Photos { get; set; }
    }
}