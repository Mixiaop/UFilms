using System.Collections.Generic;
using UFilm.Domain.Spiders;

namespace UFilm.Services.Spiders
{
    public interface IDoubanSpiderService : U.Application.Services.IApplicationService
    {


        /// <summary>
        /// 采集一条电影信息任务
        /// </summary>
        /// <param name="taskId">电影采集的任务Id</param>
        /// <param name="collectLink">采集链接</param>
        int CollectMovie(int taskId, string collectLink);

        /// <summary>
        /// 采集一条影人信息
        /// </summary>
        /// <param name="collectLink">采集链接</param>
        /// <param name="name">采集的影人名</param>
        /// <param name="task">task=null，则会创建新的主要用来存放采集日志</param>
        // int CollectFilmManTask(string collectLink, string name = "", SpiderTask task = null);
    }
}
