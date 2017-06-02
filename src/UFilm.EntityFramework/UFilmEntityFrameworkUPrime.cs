using System.Reflection;
using U.UPrimes;


namespace UFilm.EntityFramework
{
    public class UFilmEntityFrameworkUPrime : UPrime
    {
        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
