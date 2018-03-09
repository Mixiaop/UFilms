using U.Application.Services.Dto;
using UFilm.Domain.Movies;

namespace UFilm.Console.Models.Movies
{
    /// <summary>
    /// 所有剧照列表模型
    /// </summary>
    public class MoviePhotosListModel : PagingModel
    {
        public string GetKeywords { get; set; }

        public int GetMovieId { get; set; }

        public Movie Movie { get; set; }

        public PagedResultDto<MoviePhoto> Photos { get; set; }
    }
}