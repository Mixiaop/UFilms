using U.FakeMvc.Startup;
namespace UFilm.Web
{
    public class UFilmWebUFakeMvcConfiguration : IUFakeMvcConfiguration
    {
        public string AjaxProGenerateScriptsPath { get { return "/js/ajaxservices.js"; } }
    }
}