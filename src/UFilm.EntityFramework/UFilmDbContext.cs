using System;
using System.Reflection;
using System.Linq;
using System.Data.Entity;
using U;
using U.EntityFramework;
using UFilm.Configuration;
using UFilm.EntityFramework.Mapping;

namespace UFilm.EntityFramework
{

    public class UFilmDbContext : UDbContext
    {

        public UFilmDbContext(string nameOrConnectionString)
            : base(UPrimeEngine.Instance.Resolve<DatabaseSettings>().SqlConnectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                type.BaseType.GetGenericTypeDefinition() == typeof(UFilmEntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
