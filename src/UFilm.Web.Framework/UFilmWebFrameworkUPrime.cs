using System.Reflection;
using U.UPrimes;

namespace UFilm.Web
{
    public class UFilmWebUPrime : UPrime
    {
        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}