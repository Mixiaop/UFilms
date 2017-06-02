using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UFilm.Domain.Tags;
using UFilm.Domain.Collection;

namespace UFilm.Console.Models.Collection
{
    public class CollectionEditModel : ModelBase
    {
        public CollectionEditModel()
        {
            Tags = new List<Tag>();
        }

        public MovieCollection Collection { get; set; }

        public IList<Tag> Tags { get; set; }

        public string OldTags { get; set; }
    }
}