using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using Newtonsoft.Json;
using U;
using UZeroMedia.Client.Services;
using UFilm.Services.Spiders;
namespace UNote.Console._Tests
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(SignatureHelper.GetEncodeSign());
            //var mediaSettings = UPrimeEngine.Instance.Resolve<DatabaseSettings>();
            //var pictureService = UPrimeEngine.Instance.Resolve<UZeroMedia.Domain.IPictureRepository>();
            //pictureService.Insert(new Domain.Picture());
            Response.Write(System.Web.HttpUtility.UrlEncode("你好"));
            btnUpload.Click += BtnUpload_Click;
            //Response.Write(CommonHelper.FormatFileSize(481512));
            //var doubanService = UPrimeEngine.Instance.Resolve<IDoubanSpiderService>();
            //var spiderService = UPrimeEngine.Instance.Resolve<ISpiderTaskService>();
            //var task = spiderService.GetTask(22);
            //var count = doubanService.CollectMovie(22,"https://movie.douban.com/subject/1308145");
            //Response.Write(count);
            //doubanService.ExecuteTaskByUnfinished();
            //task.Spidering = true;
            //spiderService.UpdateTask(task);

            using (WebClient client = new WebClient()) {
                byte[] bytes = client.DownloadData("http://img.mbjuan.com/content/media/thumbs/p00300814.jpeg");
                System.Drawing.Image img = bytes.ToImage();
                Response.Write("Width" + img.Size.Width + " HEIGHT:" + img.Size.Height);
            }
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            //PictureService pictureService = UPrimeEngine.Instance.Resolve<PictureService>();
            //var result = pictureService.Upload(fuUpload.PostedFile);
            FileClientService fileService = UPrimeEngine.Instance.Resolve<FileClientService>();
            var result = fileService.Upload(fuUpload.PostedFile);
            Response.Write(JsonConvert.SerializeObject(result));
        }
    }
}