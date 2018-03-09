using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using U.Utilities.Web;
using U.Application.Services.Dto;
using UFilm.Domain.Movies;

namespace UFilm.Web.Models.Movies
{
    /// <summary>
    /// 影人单页模型
    /// </summary>
    public class NameModel : ModelBase
    {
        public FilmMan FilmMan { get; set; }

        public PagedResultDto<FilmManPhoto> Photos { get; set; }

        public PagedResultDto<Movie> JoinMovies { get; set; }
    }
}