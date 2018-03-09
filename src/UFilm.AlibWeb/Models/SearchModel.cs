using U.Application.Services.Dto;
using UFilm.Domain.Movies;
using UFilm.Domain.Adults;
using UFilm.Services.Adults.Dto;

namespace UFilm.AlibWeb.Models
{
    public class SearchModel : ModelBase
    {
        public SearchModel()
        {
            Movies = new PagedResultDto<LMovieBriefDto>();
        }

        public string GetKeywords { get; set; }

        public int GetPage { get; set; }

        public FilmMan SearchActor { get; set; }

        public PagedResultDto<LMovie> SearchActorMovies { get; set; }

        public PagedResultDto<LMovieBriefDto> Movies { get; set; }

        public string PagiHTML { get; set; }
    }
}