using System;
using U;
using U.Utilities.Web;
using UFilm.Domain.Spiders;
using UFilm.Services.Spiders;
using UFilm.Console.UI;

namespace UFilm.Console.Spiders.TaskLogs
{
    public partial class List : UI.AuthPageBase<UFilm.Console.Models.Spiders.TaskLogsListModel>
    {
        ISpiderTaskService taskService = UPrimeEngine.Instance.Resolve<ISpiderTaskService>();
        protected string PagerHtml;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSearch.Click += btnSearch_Click;
            Model.Task = taskService.GetTask(WebHelper.GetInt("taskid", 0));

            if (!IsPostBack)
            {
                tbSeachKeywords.Text = WebHelper.GetString("wd");
                BindPageDatas(GetUrlParam(), true);
            }
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            #region 搜索
            BindPageDatas(GetUrlParam(), false);
            #endregion
        }

        #region BindPageDatas
        string GetUrlParam()
        {
            string cdi = "";

            if (!string.IsNullOrEmpty(tbSeachKeywords.Text.Trim()))
            {
                cdi += "wd=" + tbSeachKeywords.Text.Trim();
            }

            if (WebHelper.GetString("page") != "")
            {
                if (cdi != "")
                    cdi += "&";

                cdi += "page=" + WebHelper.GetString("page");
            }
            return cdi;
        }

        void BindPageDatas(string url, bool pageInit)
        {
            PagingInfo pageInfo = new PagingInfo();
            pageInfo.PageIndex = pageInit ? WebHelper.GetInt("page", 1) : 1;
            pageInfo.PageSize = 40;
            pageInfo.Url = url == "" ? WebHelper.GetUrl() : "List.aspx?" + url;
            var results = taskService.SearchLogs(WebHelper.GetInt("taskid", 0), tbSeachKeywords.Text.Trim(), pageInfo.PageIndex, pageInfo.PageSize);

            rptDatas.DataSource = results.Items;
            rptDatas.DataBind();

            pageInfo.TotalCount = results.TotalCount;

            PagerHtml = new Paginations(pageInfo).GetPaging();
        }
        #endregion
    }
}