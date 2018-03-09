using System.Reflection;
using U.UPrimes;
using U.FakeMvc.Startup;

namespace UFilm.AlibWeb
{
    public class UFilmAlibWebUPrime : UPrime
    {
        public override void PreInitialize()
        {
            Engine.IocManager.Register<IUFakeMvcConfiguration, UFilmAlibWebUFakeMvcConfiguration>();
        }
        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}