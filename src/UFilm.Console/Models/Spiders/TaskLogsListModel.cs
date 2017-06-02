using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UFilm.Domain.Spiders;

namespace UFilm.Console.Models.Spiders
{
    public class TaskLogsListModel : ModelBase
    {
        public SpiderTask Task { get; set; }
    }
}