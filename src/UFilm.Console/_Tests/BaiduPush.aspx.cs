using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Http;
using U.Logging;
namespace UFilm.Console._Tests
{
    public partial class BaiduPush : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
           // using (HttpClient client = new HttpClient())
            //{
            //    //client.BaseAddress = new Uri("http://data.zz.baidu.com/urls?site=www.mbjuan.com&token=fwbdEoJY79JlckET");
            //    using (var requestContent = new StringContent("http://www.mbjuan.com/subject/5253", System.Text.Encoding.UTF8))
            //    {
            //        using (var res = await client.PostAsync("http://data.zz.baidu.com/urls?site=www.mbjuan.com&token=fwbdEoJY79JlckET", requestContent)) {
            //            //U.Logging.LogHelper.Logger.Information(res.Content.SerializeJson());
            //            var content = await res.Content.ReadAsStringAsync();
            //            var b = 1;
            //        }
            //    }
            //}
        }
    }
}