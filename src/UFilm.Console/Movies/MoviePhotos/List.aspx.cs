using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UFilm.Console.Controllers;
using UFilm.Console.Models.Movies;

namespace UFilm.Console.Movies.MoviePhotos
{
    public partial class List : UI.AuthPageBase<MoviesController, MoviePhotosListModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}