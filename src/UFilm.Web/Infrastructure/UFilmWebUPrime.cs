using System.Reflection;
using U.UPrimes;
using U.FakeMvc.Startup;
namespace UFilm.Web
{
    public class UFilmWebUPrime : UPrime
    {
        public override void PreInitialize()
        {
            Engine.IocManager.Register<IUFakeMvcConfiguration, UFilmWebUFakeMvcConfiguration>();
        }
        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}