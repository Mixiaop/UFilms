using System;
using AjaxPro;
using U;
using U.BackgroundJobs;
using UFilm.Domain.Spiders;
using UFilm.Services.Spiders;
using UFilm.Console.Jobs;

namespace UFilm.Console.Spiders.Douban
{
    [AjaxNamespace("CurrentApp")]
    public partial class CollectMovie : UZeroConsole.Web.AuthPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Utility.RegisterTypeForAjax(typeof(CollectMovie));
        }

        [AjaxMethod]
        public void Spider(string name, string links)
        {
            links = links.Replace("，", ",");
            var items = links.Split(',');
            ISpiderTaskService taskService = UPrimeEngine.Instance.Resolve<ISpiderTaskService>();

            SpiderTask task = new SpiderTask();
            task.Name = name;
            task.Links = links;
            taskService.InsertTask(task);

            //IDoubanSpiderAdminService spiderService = UPrimeEngine.Instance.Resolve<IDoubanSpiderAdminService>();
            
            //foreach (var item in items)
            //{
            //    if (!string.IsNullOrEmpty(item.Trim()))
            //    {
            //        spiderService.CollectMovieTask(item);
            //    }
            //    break;
            //}
        }

        [AjaxMethod]
        public void Execute()
        {
            var backgroundJobManager = UPrimeEngine.Instance.Resolve<IBackgroundJobManager>();

            backgroundJobManager.EnqueueAsync<SpiderDoubanMovieJob, int>(0);

            //ltlMessage.Text = AlertSuccess("开启成功，已进入队列");
        }
    }
}