using System;
using U.Utilities.Web;
using UFilm.Domain.Movies;
using UFilm.Console.Controllers;
using UFilm.Console.Models.Movies;

namespace UFilm.Console.Movies.MovieTorrents
{
    public partial class Add : UI.AuthPageBase<MoviesController, TorrentsAddModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Click += btnSave_Click;
            if (!IsPostBack) {
                tbName.Text = Model.Movie.CnName;
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            MovieTorrent torrent = new MovieTorrent();
            torrent.Name = tbName.Text.Trim();
            torrent.Type = ddlType.SelectedValue;
            torrent.Size = tbSize.Text.Trim();
            torrent.Link = tbLink.Text.Trim();
            torrent.MovieId = Model.Movie.Id;

            if (torrent.Name.IsNullOrEmpty()) {
                ltlMessage.Text = AlertError("名称不能为空");
                return;
            }

            if (torrent.Link.IsNullOrEmpty()) {
                ltlMessage.Text = AlertError("链接不能为空");
                return;
            }

            Controller.SaveTorrent(torrent);
            ltlMessage.Text = AlertSuccess("添加成功");
            this.RedirectByTime(WebHelper.GetThisPageUrl(true), 1000);

        }
    }
}