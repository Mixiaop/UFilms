using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using U;
using U.Logging;
using U.Utilities.SqlServer;
using UFilm.Domain.Movies;
using UFilm.Services.Movies;
using UZeroMedia.Client.Services;

namespace UFilm.Web._Test
{
    public partial class Movie : System.Web.UI.Page
    {
        PictureClientService _pictureClientService = UPrimeEngine.Instance.Resolve<PictureClientService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            var dt = SqlHelper.GetDataTable("select * from AL_CollectItems");
            foreach (DataRow row in dt.Rows) {
                SqlHelper.ExecuteSql("insert into [Mbj_MovieCollectionItems](CollectionId,MovieId,Title,[Order]) values(1," + row["MovieID"] + ",'" + row["Title"] + "'," + row["Taxis"] + ")");
            }

            Response.Write("成功了");

            //LogHelper.Logger.Error("错误了啊");
            //int count = 1;
            //foreach (var m in list)
            //{
            //    //string sql = "select [lastShareTime] from al_movies where id=" + m.Id;
            //    //var data = SqlHelper.ExecuteScalar(sql);
            //    //if (data != null)
            //    //{
            //    //    try
            //    //    {
            //    //        DateTime time = data.ToDateTime();
            //    //        m.LastShareTime = time;
            //    //        movieRepository.Update(m);
            //    //    }
            //    //    catch (Exception ex)
            //    //    {

            //    //    }
            //    //}
            //    try{
            //        if (m.CoverId > 0)
            //        {
            //            var picUrl = _pictureClientService.GetPictureUrl(m.CoverId);
            //            MovieCover cover = new MovieCover();
            //            cover.MovieId = m.Id;
            //            cover.Url = picUrl;
            //            _coverRepository.Insert(cover);
            //            count++;
            //            //Response.Write(picUrl);
            //        }
            //    }catch{}
            //}
            //Response.Write(count + " 条成功了");
            //Response.Write(U.Utilities.Security.EncriptionHelper.MD5("123123"));

            //IMovieQueryService movieQueryService = UPrimeEngine.Instance.Resolve<IMovieQueryService>();
            //var result = movieQueryService.QueryLastActivityMovies();

            //Response.Write(result.Items.Count);
        }
    }
}