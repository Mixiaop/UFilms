using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using U.Application.Services.Dto;
using UFilm.Domain.Collection;

namespace UFilm.Web.Models.Collection
{
    public class SearchModel : PagingModel
    {
        public PagedResultDto<MovieCollection> Results { get; set; }
    }
}