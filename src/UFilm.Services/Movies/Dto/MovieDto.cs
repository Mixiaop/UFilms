using System;
using U.Application.Services.Dto;

namespace UFilm.Services.Movies.Dto
{
    public class MovieDto : MovieBriefDto
    {
      
        /// <summary>
        /// 影片简介
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 电影封面Id
        /// </summary>
        public int CoverId { get; set; }

        
    }
}
