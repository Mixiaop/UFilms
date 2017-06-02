using System.Collections.Generic;
using U.Application.Services.Dto;
using UFilm.Domain.Adults;
using UFilm.Domain.Tags;

namespace UFilm.Console.Models.Adults
{
    public class MoviesListModel : PagingModel
    {
        public MoviesListModel() {
            MovieTypes = new List<Tag>();
        }

        public string GetMovieType { get; set; }

        public string GetKeywords { get; set; }

        public IList<Tag> MovieTypes { get; set; }

        public PagedResultDto<LMovie> Results { get; set; }
    }
}