using U.FakeMvc.Startup;
namespace UFilm.AlibWeb
{
    public class UFilmAlibWebUFakeMvcConfiguration : IUFakeMvcConfiguration
    {
        public string AjaxProGenerateScriptsPath { get { return "/js/ajaxservices.js"; } }
    }
}