using System.Reflection;
using U.UPrimes;


namespace UFilm
{
    public class UFilmCoreUPrime : UPrime
    {
        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
