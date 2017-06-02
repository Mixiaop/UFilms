using U.FakeMvc.Routes;

namespace UFilm.Web.UI
{
    public class MasterPageBase : System.Web.UI.MasterPage
    {
        public RouteContext Route = RouteContext.Instance;
    }


}
