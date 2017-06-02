using U;
using U.Utilities.Web;
using U.Application.Services.Dto;
using UFilm.Domain.Movies;
using UFilm.Services.Movies;
using UFilm.Console.UI;

namespace UFilm.Console.Models.Movies
{
    public class MoviesListModel : ModelBase
    {
        public string PagiHtml { get; set; }
    }
}