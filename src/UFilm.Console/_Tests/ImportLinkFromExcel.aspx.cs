using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using U;
using U.Utilities.Web;
using U.Utilities.Excel;
using UFilm.Domain.Spiders;
using UFilm.Services.Spiders;

namespace UFilm.Console._Tests
{
    public partial class ImportLinkFromExcel : System.Web.UI.Page
    {
        IDoubanLinkService _doubanLinkService = UPrimeEngine.Instance.Resolve<IDoubanLinkService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            btnImport.Click += btnImport_Click;
        }

        void btnImport_Click(object sender, EventArgs e)
        {
            if (tbName.Text.Trim().IsNotNullOrEmpty())
            {
                var path = WebHelper.MapPath(tbName.Text.Trim());

                var dt = ExcelHelper.ExcelToDataTable(path, "Sheet1");
                int index = 0;
                foreach (DataRow row in dt.Rows) {
                    var name = row[0];
                    var link = row[1];
                    var movieLink = new DoubanMovieLink();
                    movieLink.Name = name.ToString();
                    movieLink.Link = link.ToString();
                    movieLink.Remark = "Excel导入，文件：" + tbName.Text.Trim();
                    _doubanLinkService.Insert(movieLink);
                    index++;
                }

                Response.Write(index + " 条导入成功");
            }
            else {
                Response.Write("请输入链接");
            }

        }
    }
}