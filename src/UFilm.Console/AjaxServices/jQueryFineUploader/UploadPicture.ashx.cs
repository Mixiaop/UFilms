using System.Web;
using System.IO;
using Newtonsoft.Json;
using U.Utilities.Web;
using UZeroMedia.Client.Services;

namespace UFilm.Console.AjaxServices.jQueryFineUploader
{
    /// <summary>
    /// UploadPicture 的摘要说明
    /// </summary>
    public class UploadPicture : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            PictureClientService pictureService = new PictureClientService();
            HttpPostedFile image = context.Request.Files["qqfile"];
            int size = WebHelper.GetInt("size", 0);

            StringWriter sw = new StringWriter();
            using (JsonWriter w = new JsonTextWriter(sw))
            {
                w.WriteStartObject();
                if (image == null)
                    context.Response.End();

                var pic = pictureService.Upload(image, size);
                if (pic != null && pic.Results != null)
                {
                    w.WritePropertyName("pictureId");
                    w.WriteValue(pic.Results.PictureId);
                    w.WritePropertyName("thumb");
                    w.WriteValue(pic.Results.PictureUrl);
                    w.WritePropertyName("showthumb");
                    w.WriteValue(pic.Results.ShowPictureUrl);
                    w.WritePropertyName("success");
                    w.WriteValue(true);
                    w.WriteEndObject();
                }
                else
                {
                    w.WritePropertyName("pictureId");
                    w.WriteValue("");
                    w.WritePropertyName("thumb");
                    w.WriteValue("");
                    w.WriteEndObject();
                }
            }
            sw.Close();
            context.Response.Write(sw.GetStringBuilder().ToString());
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