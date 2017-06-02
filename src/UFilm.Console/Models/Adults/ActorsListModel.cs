using U.Application.Services.Dto;
using UFilm.Domain.Adults;

namespace UFilm.Console.Models.Adults
{
    public class ActorsListModel : PagingModel
    {
        public string GetKeywords { get; set; }


        public PagedResultDto<Actor> Results { get; set; }
    }
}