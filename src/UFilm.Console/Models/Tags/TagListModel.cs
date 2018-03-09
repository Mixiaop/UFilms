using U.Application.Services.Dto;
using UFilm.Domain.Tags;

namespace UFilm.Console.Models.Tags
{
    public class TagListModel : PagingModel
    {
        public string GetKeywords { get; set; }

        public int GetTypeId { get; set; }

        public PagedResultDto<Tag> Tags { get; set; }
    }
}