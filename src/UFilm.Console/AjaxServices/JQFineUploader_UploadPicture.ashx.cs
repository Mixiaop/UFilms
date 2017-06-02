using System.Web;
using System.IO;
using Newtonsoft.Json;
using UZeroMedia.Client.Services;
namespace UNote.Console.AjaxServices
{
    /// <summary>
    /// JQFineUploader_UploadPicture 的摘要说明
    /// </summary>
    public class JQFineUploader_UploadPicture : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //PictureClientService pictureService = new PictureClientService();
            //HttpPostedFile image = context.Request.Files["qqfile"];

            //StringWriter sw = new StringWriter();
            //using (JsonWriter w = new JsonTextWriter(sw))
            //{
            //    w.WriteStartObject();
            //    if (image == null)
            //        context.Response.End();

            //    var pic = pictureService.Upload(image);
            //    if (pic != null && pic.Results != null && pic.Results.Count > 0)
            //    {
            //        w.WritePropertyName("pictureId");
            //        w.WriteValue(pic.Results[0].PictureId);
            //        w.WritePropertyName("thumb");
            //        w.WriteValue(pic.Results[0].PictureUrl);
            //        w.WritePropertyName("showthumb");
            //        w.WriteValue(pic.Results[0].ShowPictureUrl);
            //        w.WritePropertyName("success");
            //        w.WriteValue(true);
            //        w.WriteEndObject();
            //    }
            //    else
            //    {
            //        w.WritePropertyName("pictureId");
            //        w.WriteValue("");
            //        w.WritePropertyName("thumb");
            //        w.WriteValue("");
            //        w.WriteEndObject();
            //    }
            //}
            //sw.Close();
            //context.Response.Write(sw.GetStringBuilder().ToString());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}