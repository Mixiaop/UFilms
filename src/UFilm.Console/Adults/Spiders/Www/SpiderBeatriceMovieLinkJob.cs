using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using U;
using U.BackgroundJobs;
using UZeroMedia.Client.Services;
using UFilm.Domain.Adults;
using UFilm.Services.Adults;

namespace UFilm.Console.Adults.Spiders.Www
{
    public class SpiderBeatriceMovieLinkJob : BackgroundJob<int>, U.Dependency.ITransientDependency
    {
        public override void Execute(int args)
        {

        }
    }
}