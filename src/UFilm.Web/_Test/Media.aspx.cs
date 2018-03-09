using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U;
using U.Logging;
using U.Utilities.SqlServer;
using UFilm.Domain.Movies;
using UFilm.Services.Movies;
using UZeroMedia.Client.Services;

namespace UFilm.Web._Test
{
    public partial class Media : System.Web.UI.Page
    {
        PictureClientService _pictureClientService = UPrimeEngine.Instance.Resolve<PictureClientService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            var pic = _pictureClientService.GetUrl(12798);
            Response.Write(pic.SerializeJson());
        }
    }
}