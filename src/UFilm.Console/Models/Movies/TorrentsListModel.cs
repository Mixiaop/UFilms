using U.Application.Services.Dto;
using UFilm.Domain.Movies;

namespace UFilm.Console.Models.Movies
{
    public class TorrentsListModel : PagingModel
    {
        public string GetKeywords { get; set; }

        public int GetMovieId { get; set; }

        public Movie Movie { get; set; }

        public PagedResultDto<MovieTorrent> Torrents { get; set; }
    }
}