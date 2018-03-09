using System.Collections.Generic;
using Hangfire.Dashboard;
using U;
using UZeroConsole.Services;

namespace UFilm.Console
{
    /// <summary>
    /// Hangfire后台作业权限过滤
    /// </summary>
    public class HangfireAuthorizationFilter : IAuthorizationFilter
    {
        public bool Authorize(IDictionary<string, object> owinEnvironment)
        {

            IAuthenticationService authService = UPrimeEngine.Instance.Resolve<IAuthenticationService>();

            var admin = authService.GetAuthenticatedAdmin();
            if (admin == null)
            {
                return false;
            }

            return true;
        }
    }
}