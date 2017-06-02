using System.Reflection;
using U.UPrimes;
using UFilm.Services.Mapping;

namespace UFilm.Services
{
    public class UFilmServicesUPrime : UPrime
    {
        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            CustomDtoMapper.CreateMappings();
        }
    }
}
