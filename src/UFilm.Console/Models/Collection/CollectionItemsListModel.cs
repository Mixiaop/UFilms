using U.Application.Services.Dto;
using UFilm.Domain.Collection;

namespace UFilm.Console.Models.Collection
{
    public class CollectionItemsListModel : PagingModel
    {
        public MovieCollection Collection { get; set; }
        public PagedResultDto<MovieCollectionItem> Results { get; set; }
    }
}