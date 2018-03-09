using System;
using System.Web.UI.WebControls;
using U;
using U.Utilities.Web;
using U;
using U.BackgroundJobs;
using UFilm.Domain.Spiders;
using UFilm.Services.Spiders;
using UFilm.Services.Spiders.Dto;
using UFilm.Console.UI;

namespace UFilm.Console.Spiders.Tasks
{
    public partial class List : UI.AuthPageBase<UFilm.Console.Models.Spiders.TasksListModel>
    {
        ISpiderTaskService taskService = UPrimeEngine.Instance.Resolve<ISpiderTaskService>();
        protected string PagerHtml;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSearch.Click += btnSearch_Click;
            //btnRunJob.Click += btnRunJob_Click;
            if (!IsPostBack)
            {
                tbSeachKeywords.Text = WebHelper.GetString("wd");
                ddlStatus.SelectedValue = WebHelper.GetInt("status", 0).ToString();
                BindPageDatas(GetUrlParam(), true);
            }
        }

        //void btnRunJob_Click(object sender, EventArgs e)
        //{
        //    #region
        //    var backgroundJobManager = UPrimeEngine.Instance.Resolve<IBackgroundJobManager>();

        //    backgroundJobManager.EnqueueAsync<UFilm.Console.Jobs.SpiderDoubanMovieJob, int>(0);
        //    ltlMessage.Text = AlertSuccess("开启成功", "", 3000);
        //    #endregion
        //}

        protected void HandleExceptionClick(object sender, EventArgs e) {
            LinkButton lb = (LinkButton)sender;
            int id = lb.CommandArgument.ToInt();
            if (id > 0)
            {
                try
                {
                    var task = taskService.GetTask(id);
                    task.Content = "";
                    taskService.UpdateTask(task);
                    ltlMessage.Text = AlertSuccess("处理成功", "", 2000);
                    BindPageDatas(GetUrlParam(), false);
                }
                catch (Exception ex)
                {
                    LogError(ex.Message, ex);
                    ltlMessage.Text = AlertError(ex.Message);
                }
            }

            BindPageDatas(GetUrlParam(), true);
        }

        protected void RestartClick(object sender, EventArgs e) {
            LinkButton lb = (LinkButton)sender;
            int id = lb.CommandArgument.ToInt();
            if (id > 0)
            {
                try
                {
                    var task = taskService.GetTask(id);
                    task.Finished = false;
                    task.Content = "";
                    var linkList = task.Links.Split(',');
                    if (linkList.Length > 0) {
                        task.Links = linkList[0];
                    }
                    taskService.UpdateTask(task);
                    ltlMessage.Text = AlertSuccess("重置成功", "", 2000);
                    BindPageDatas(GetUrlParam(), false);
                }
                catch (Exception ex)
                {
                    LogError(ex.Message, ex);
                    ltlMessage.Text = AlertError(ex.Message);
                }
            }

            BindPageDatas(GetUrlParam(), true);
        }

        protected void DeleteClick(object sender, EventArgs e) {
            LinkButton lb = (LinkButton)sender;
            int id = lb.CommandArgument.ToInt();
            if (id > 0)
            {
                try
                {
                    taskService.DeleteTask(id);
                    ltlMessage.Text = AlertSuccess("删除成功", "", 2000);
                    BindPageDatas(GetUrlParam(), false);
                }
                catch (Exception ex)
                {
                    LogError(ex.Message, ex);
                    ltlMessage.Text = AlertError(ex.Message);
                }
            }

            BindPageDatas(GetUrlParam(), true);
        }

        protected void FinishClick(object sender, EventArgs e) {
            LinkButton lb = (LinkButton)sender;
            int id = lb.CommandArgument.ToInt();
            if (id > 0)
            {
                try
                {
                    var task = taskService.GetTask(id);
                    task.Finished = true;
                    task.FinishedTime = DateTime.Now;
                    task.Spidering = false;
                    taskService.UpdateTask(task);
                    ltlMessage.Text = AlertSuccess("操作成功", "", 2000);
                    BindPageDatas(GetUrlParam(), false);
                }
                catch (Exception ex)
                {
                    LogError(ex.Message, ex);
                    ltlMessage.Text = AlertError(ex.Message);
                }
            }

            BindPageDatas(GetUrlParam(), true);
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

            if (ddlStatus.SelectedValue.ToInt() > 0)
            {
                if (cdi != "")
                    cdi += "&";

                cdi += "status=" + ddlStatus.SelectedValue.ToInt();
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
            SpiderTaskStatus status = SpiderTaskStatus.All;
            if (ddlStatus.SelectedValue.ToInt() == 1) {
                status = SpiderTaskStatus.Spidering;
            }
            else if (ddlStatus.SelectedValue.ToInt() == 2)
            {
                status = SpiderTaskStatus.UnFinished;
            }
            else if (ddlStatus.SelectedValue.ToInt() == 3)
            {
                status = SpiderTaskStatus.Finished;
            }

            var results = taskService.Search(tbSeachKeywords.Text.Trim(), status, pageInfo.PageIndex, pageInfo.PageSize);

            rptDatas.DataSource = results.Items;
            rptDatas.DataBind();

            pageInfo.TotalCount = results.TotalCount;

            PagerHtml = new Paginations(pageInfo).GetPaging();
        }
        #endregion
    }
}