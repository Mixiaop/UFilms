using System.Web;
using System.IO;
using Newtonsoft.Json;
using UZeroMedia.Client.Services;
namespace UNote.Console.AjaxServices
{
    /// <summary>
    /// JQFineUploader_UploadFile 的摘要说明
    /// </summary>
    public class JQFineUploader_UploadFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            FileClientService fileService = new FileClientService();
            HttpPostedFile qqfile = context.Request.Files["qqfile"];

            StringWriter sw = new StringWriter();
            using (JsonWriter w = new JsonTextWriter(sw))
            {
                w.WriteStartObject();
                if (qqfile == null)
                    context.Response.End();

                var file = fileService.Upload(qqfile);
                
                if (file != null && file.Results != null)
                {
                    w.WritePropertyName("fileId");
                    w.WriteValue(file.Results.FileId);
                    w.WritePropertyName("fileUrl");
                    w.WriteValue(file.Results.FileUrl);
                    w.WritePropertyName("fileSize");
                    w.WriteValue(qqfile.ContentLength.ToString());
                    w.WritePropertyName("success");
                    w.WriteValue(true);
                    w.WriteEndObject();
                }
                else
                {
                    w.WritePropertyName("fileId");
                    w.WriteValue("");
                    w.WritePropertyName("fileUrl");
                    w.WriteValue("");
                    w.WritePropertyName("fileSize");
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