using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UFilm.Domain.Collection;

namespace UFilm.Web.Models.Collection
{
    public class SubjectModel : ModelBase
    {
        public MovieCollection Collection { get; set; }

        public IList<MovieCollectionItem> Items { get; set; }
    }
}