using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using U.Application.Services.Dto;
using UFilm.Domain.Tags;
using UFilm.Services.Movies.Dto;

namespace UFilm.Web.Models
{
    public class DiscoverModel : PagingModel
    {
        public DiscoverModel()
        {
            MovieTypes = new List<Tag>();
            MovieAreas = new List<Tag>();
        }

        public string GetMovieArea { get; set; }

        public string GetMovieType { get; set; }

        public string[] GetMovieAreas {
            get {
                if (GetMovieArea.IsNotNullOrEmpty())
                {
                    return GetMovieArea.Split('_');
                }
                else
                    return new string[0];
            }
        }

        public string[] GetMovieTypes {
            get
            {
                if (GetMovieType.IsNotNullOrEmpty())
                {
                    return GetMovieType.Split('_');
                }
                else
                    return new string[0];
            }
        }

        public IList<Tag> MovieTypes { get; set; }

        public IList<Tag> MovieAreas { get; set; }

        public PagedResultDto<MovieBriefDto> Movies { get; set; }
    }
}