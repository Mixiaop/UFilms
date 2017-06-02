using System.Threading;
using U;
using U.BackgroundJobs;
using UFilm.Services.Spiders;

namespace UFilm.Console.Jobs
{
    public class SpiderDoubanMovieJob : BackgroundJob<int>, U.Dependency.ITransientDependency
    {
        public override void Execute(int args)
        {
            IDoubanSpiderService doubanSpiderService = UPrimeEngine.Instance.Resolve<IDoubanSpiderService>();
            ISpiderTaskService taskService = UPrimeEngine.Instance.Resolve<ISpiderTaskService>();

            //取出所有未完成的任务并执行取1条
            var tasks = taskService.GetAllByUnfinished();
            if (tasks != null && tasks.Count > 0)
            {
                var task = tasks[0];
                var links = task.Links.Split(',');
                foreach (var link in links)
                {
                    if (!link.Trim().IsNullOrEmpty())
                    {
                        doubanSpiderService.CollectMovie(task.Id, link);
                        break;
                    }
                }
                //递归下一个
                var backgroundJobManager = UPrimeEngine.Instance.Resolve<IBackgroundJobManager>();

                backgroundJobManager.EnqueueAsync<UFilm.Console.Jobs.SpiderDoubanMovieJob, int>(0);

            }
            
        }
    }
}