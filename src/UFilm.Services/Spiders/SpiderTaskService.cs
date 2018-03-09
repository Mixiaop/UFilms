using System;
using System.Collections.Generic;
using System.Linq;
using U.Application.Services.Dto;
using UFilm.Domain.Spiders;
using UFilm.Services.Spiders.Dto;
namespace UFilm.Services.Spiders
{
    public class SpiderTaskService : ISpiderTaskService
    {
        #region Fields & Ctor
        private readonly ISpiderTaskRepository _taskRepository;
        private readonly ISpiderTaskLogRepository _taskLogRepository;
        public SpiderTaskService(ISpiderTaskRepository taskRepository, ISpiderTaskLogRepository taskLogRepository)
        {
            _taskRepository = taskRepository;
            _taskLogRepository = taskLogRepository;
        }
        #endregion

        #region Tasks
        /// <summary>
        /// 插入一条采集任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public int InsertTask(SpiderTask task)
        {
            return _taskRepository.InsertAndGetId(task);
        }

        /// <summary>
        /// 更新一条采集任务
        /// </summary>
        /// <param name="task">任务对象</param>
        public void UpdateTask(SpiderTask task)
        {
            _taskRepository.Update(task);
        }

        /// <summary>
        /// 获取一条采集任务
        /// </summary>
        /// <param name="taskId">任务Id</param>
        /// <returns></returns>
        public SpiderTask GetTask(int taskId)
        {
            return _taskRepository.Get(taskId);
        }

        public void DeleteTask(int taskId)
        {
            var task = _taskRepository.Get(taskId);
            _taskRepository.Delete(task);
        }

        /// <summary>
        /// 搜索采集任务列表
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">多少条</param>
        /// <returns></returns>
        public PagedResultDto<SpiderTask> Search(string keywords, SpiderTaskStatus status = SpiderTaskStatus.All, int pageIndex = 1, int pageSize = 16)
        {
            var query = _taskRepository.GetAll()
                        .WhereIf(!string.IsNullOrEmpty(keywords), x => x.Name.Contains(keywords));

            switch (status) { 
                case SpiderTaskStatus.Finished:
                    query = query.Where(x => x.Finished == true);
                    break;
                case SpiderTaskStatus.UnFinished:
                    query = query.Where(x => x.Finished == false);
                    break;
                case SpiderTaskStatus.Spidering:
                    query = query.Where(x => x.Spidering == true);
                    break;
            }
            var count = query.Count();

            query = query.OrderBy(x => x.Finished)
                         .ThenByDescending(x => x.FinishedTime)
                         .Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var list = query.ToList();

            return new PagedResultDto<SpiderTask>(count, list);
        }
        public IList<SpiderTask> GetAllByUnfinished()
        {
            var query = _taskRepository.GetAll()
                        .Where(x => x.Finished == false && x.Spidering == false)
                        .OrderBy(x => x.CreationTime);

            var count = query.Count();
            //var list = query.ToList().MapTo<List<SpiderTaskDto>>();
            var list = query.ToList();

            return list;
        }
        #endregion

        #region Logs
        /// <summary>
        /// 添加一条日志
        /// </summary>
        /// <param name="taskId">任务Id</param>
        /// <param name="message">消息</param>
        public void AddLog(int taskId, string message)
        {
            var log = new SpiderTaskLog();
            log.TaskId = taskId;
            log.Message = message;
            _taskLogRepository.Insert(log);
        }

        public IList<SpiderTaskLog> GetLastLogs(int taskId, DateTime? time = null)
        {
            var query = _taskLogRepository.GetAll();
            query = query.Where(x => x.TaskId == taskId);
            if (time.HasValue)
            {
                var list = query.Where(x => x.CreationTime > time.Value).OrderByDescending(x => x.CreationTime).ToList();
                return list;
            }
            else
            {
                query = query.OrderByDescending(x => x.CreationTime).Take(1);
                var list = query.ToList();

                return list;
            }
        }

        /// <summary>
        /// 查询某个任务的日志列表
        /// </summary>
        /// <param name="taskId">任务Id</param>
        /// <param name="keywords">关键字</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">多少条</param>
        /// <returns></returns>
        public PagedResultDto<SpiderTaskLog> SearchLogs(int taskId, string keywords, int pageIndex, int pageSize)
        {
            var query = _taskLogRepository.GetAll()
                        .WhereIf(taskId > 0, x => x.TaskId == taskId)
                        .WhereIf(!string.IsNullOrEmpty(keywords), x => x.Message.Contains(keywords))
                        .OrderByDescending(x => x.CreationTime);

            var count = query.Count();
            //var list = query.ToList().MapTo<List<SpiderTaskLogDto>>();
            var list = query.ToList();


            return new PagedResultDto<SpiderTaskLog>(count, list);
        }
        #endregion
    }
}
