using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using U.Application.Services.Dto;
using UFilm.Domain.Collection;

namespace UFilm.Console.Models.Collection
{
    public class CollectionListModel : PagingModel
    {
        public string GetKeywords { get; set; }

        public PagedResultDto<MovieCollection> Results { get; set; }
    }
}