using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U;
using U.BackgroundJobs;
using UFilm.Console.Adults.Spiders.Www;

namespace UFilm.Console.Jobs
{
    public partial class List : UZeroConsole.Web.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnDoubanSpiderNow.Click += btnDoubanSpiderNow_Click;
            btnKeepAliveDomainJob.Click += btnKeepAliveDomainJob_Click;
            btnKeepAliveDomainJobRunNow.Click += btnKeepAliveDomainJobRunNow_Click;
            btnSpiderNH87CN.Click += btnSpiderNH87CN_Click;
        }

        void btnSpiderNH87CN_Click(object sender, EventArgs e)
        {
            #region 男人团NH87.CN
            var backgroundJobManager = UPrimeEngine.Instance.Resolve<IBackgroundJobManager>();

            backgroundJobManager.EnqueueAsync<SpiderWwwNH87CNJob, int>(0);
            ltlMessage.Text = AlertSuccess("开启成功", "", 3000);
            #endregion
        }

        void btnDoubanSpiderNow_Click(object sender, EventArgs e)
        {
            #region 豆瓣电影
            var backgroundJobManager = UPrimeEngine.Instance.Resolve<IBackgroundJobManager>();

            backgroundJobManager.EnqueueAsync<UFilm.Console.Jobs.SpiderDoubanMovieJob, int>(0);
            ltlMessage.Text = AlertSuccess("开启成功", "", 3000);
            #endregion
        }

        #region 保持活动
        void btnKeepAliveDomainJobRunNow_Click(object sender, EventArgs e)
        {
            var backgroundJobManager = UPrimeEngine.Instance.Resolve<IBackgroundJobManager>();

            backgroundJobManager.EnqueueAsync<KeepAliveDomainJob, int>(0);

            ltlMessage.Text = AlertSuccess("开启成功，已进入队列");
        }

        void btnKeepAliveDomainJob_Click(object sender, EventArgs e)
        {
            #region 保持活动
            var backgroundJobManager = UPrimeEngine.Instance.Resolve<IBackgroundJobManager>();

            backgroundJobManager.AddRecurringJob<KeepAliveDomainJob, int>(0, "0/3 * * * *", TimeZoneInfo.Local);

            ltlMessage.Text = AlertSuccess("开启成功，已进入队列");
            #endregion
        }
        #endregion
    }

    public class ListModel : Models.ModelBase
    {

    }
}