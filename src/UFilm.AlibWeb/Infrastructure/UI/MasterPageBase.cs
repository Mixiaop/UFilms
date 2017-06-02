using U.FakeMvc.Routes;

namespace UFilm.AlibWeb.UI
{
    public class MasterPageBase : System.Web.UI.MasterPage
    {
        public RouteContext Route = RouteContext.Instance;
    }


}
