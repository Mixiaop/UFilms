using System.Collections.Generic;
using U.Utilities.Web;
using UFilm.Domain.Tags;

namespace UFilm.Web.Models
{
    public class IndexModel : ModelBase
    {
        public IndexModel()
        {
            MovieTypes = new List<Tag>();
            MovieAreas = new List<Tag>();
        }

        public string GetMovieArea { get; set; }

        public string GetMovieType { get; set; }

        public string SelectedTagsHTML { get; set; }

        public IList<Tag> MovieTypes { get; set; }

        public IList<Tag> MovieAreas { get; set; }
    }
}