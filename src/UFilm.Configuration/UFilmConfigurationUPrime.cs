using System.Reflection;
using U.UPrimes;


namespace UFilm.Configuration
{
    public class UFilmConfigurationUPrime : UPrime
    {
        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
