using System.Collections.Generic;
using System;
using U.Application.Services.Dto;
using UFilm.Domain.Spiders;
using UFilm.Services.Spiders.Dto;

namespace UFilm.Services.Spiders
{
    /// <summary>
    /// 蜘蛛采集任务日志
    /// 一条任务包含多个日志，比如豆瓣采集电影记录采集的过程
    /// </summary>
    public interface ISpiderTaskService : U.Application.Services.IApplicationService
    {
        #region Tasks
        /// <summary>
        /// 插入一条采集任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        int InsertTask(SpiderTask task);

        /// <summary>
        /// 更新一条采集任务
        /// </summary>
        /// <param name="task">任务对象</param>
        void UpdateTask(SpiderTask task);

        /// <summary>
        /// 获取一条采集任务
        /// </summary>
        /// <param name="taskId">任务Id</param>
        /// <returns></returns>
        SpiderTask GetTask(int taskId);

        void DeleteTask(int taskId);

        /// <summary>
        /// 搜索采集任务列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="status"></param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">多少条</param>
        /// <returns></returns>
        PagedResultDto<SpiderTask> Search(string keywords, SpiderTaskStatus status = SpiderTaskStatus.All, int pageIndex = 1, int pageSize = 16);

        /// <summary>
        /// 获取所有未完成的任务
        /// </summary>
        IList<SpiderTask> GetAllByUnfinished();
        #endregion

        #region Logs
        /// <summary>
        /// 添加一条日志
        /// </summary>
        /// <param name="taskId">任务Id</param>
        /// <param name="message">消息</param>
        void AddLog(int taskId, string message);

        IList<SpiderTaskLog> GetLastLogs(int taskId, DateTime? time = null);

        /// <summary>
        /// 查询某个任务的日志列表
        /// </summary>
        /// <param name="taskId">任务Id</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">多少条</param>
        /// <returns></returns>
        PagedResultDto<SpiderTaskLog> SearchLogs(int taskId, string keywords, int pageIndex, int pageSize);
        #endregion
    }
}
