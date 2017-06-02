using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UFilm.Domain.Tags;

namespace UFilm.Console.Models.Collection
{
    public class CollectionAddModel : ModelBase
    {
        public CollectionAddModel() {
            Tags = new List<Tag>();
        }

        public IList<Tag> Tags { get; set; }
    }
}