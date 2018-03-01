using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Net;

namespace UFilm.Console.Adults.Spiders.Www
{
    public partial class Beatricee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSpiderMovieLinks.Click += BtnSpiderMovieLinks_Click;
        }

        private void BtnSpiderMovieLinks_Click(object sender, EventArgs e)
        {
            Regex reg;
            RegexOptions regOptions = RegexOptions.None;
            WebClient client = new WebClient();
        }
    }
}