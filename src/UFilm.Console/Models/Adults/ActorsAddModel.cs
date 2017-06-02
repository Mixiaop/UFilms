using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UFilm.Domain.Adults;

namespace UFilm.Console.Models.Adults
{
    public class ActorsAddModel : ModelBase
    {
        public Actor Actor { get; set; }
    }
}