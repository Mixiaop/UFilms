using System.Collections.Generic;
using System.Reflection;
using U.UPrimes;
using U.Hangfire;

namespace UFilm.Console
{
    [DependsOn(typeof(UHangfireUPrime))]
    public class UFilmConsoleUPrime : UPrime
    {
        public override void PreInitialize()
        {
            base.PreInitialize();

            Engine.Configuration.BackgroundJob.IsJobExecutionEnabled = true;
        }

        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Engine.Configuration.BackgroundJob.UseHangfire();
        }
    }
}