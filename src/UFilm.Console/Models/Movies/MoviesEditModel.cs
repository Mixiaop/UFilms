using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using U.Utilities.Web;
using UFilm.Domain.Movies;

namespace UFilm.Console.Models.Movies
{
    public class MoviesEditModel : ModelBase
    {
        public int GetMovieId { get { return WebHelper.GetInt("movieId", 0); } }

        public Movie Movie { get; set; }
    }
}