using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Hangfire;
using Hangfire.Dashboard;
[assembly: OwinStartup(typeof(UFilm.Console.Startup))]

namespace UFilm.Console
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            var authList = new List<IAuthorizationFilter>();
            authList.Add(new HangfireAuthorizationFilter());
            var option = new DashboardOptions();
            option.AuthorizationFilters = authList;
            app.UseHangfireDashboard("/jobs", option);
        }
    }
}


