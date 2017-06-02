using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using U.Application.Services.Dto;
using U.Utilities.Web;
using UFilm.Domain.Movies;
using UFilm.Services.Movies.Dto;

namespace UFilm.Web.Models
{
    public class SearchModel : ModelBase
    {
        public SearchModel() {
            Movies = new PagedResultDto<MovieBriefDto>();
        }

        public string GetKeywords { get; set; }

        public int GetPage { get; set; }

        public FilmMan SearchFilmMan { get; set; }

        public PagedResultDto<Movie> SearchFilmManMovies { get; set; }

        public PagedResultDto<MovieBriefDto> Movies { get; set; }

        public string PagiHTML { get; set; }
    }
}