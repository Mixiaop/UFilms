using System;
using System.Collections.Generic;
using AjaxPro;
using U;
using U.Web.Models;
using U.Application.Services.Dto;
using U.AutoMapper;
using UFilm.Domain.Spiders;
using UFilm.Services.Spiders;
using UFilm.Services.Spiders.Dto;

namespace UFilm.Console.AjaxServices
{
    [AjaxNamespace("SpidersService")]
    public partial class SpidersService : System.Web.UI.Page
    {
        ISpiderTaskService _taskService = UPrimeEngine.Instance.Resolve<ISpiderTaskService>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [AjaxMethod]
        public AjaxResponse<SpiderTaskDto> GetTask(int id)
        {
            AjaxResponse<SpiderTaskDto> res = new AjaxResponse<SpiderTaskDto>();
            var task = _taskService.GetTask(id);
            res.Result = task.MapTo<SpiderTaskDto>();
            return res;
        }

        [AjaxMethod]
        public AjaxResponse<IList<SpiderTaskLogDto>> GetLastLogs(int taskId, string time) {
            AjaxResponse<IList<SpiderTaskLogDto>> res = new AjaxResponse<IList<SpiderTaskLogDto>>();
            DateTime? now = null;
            if (time.IsNotNullOrEmpty())
            {
                now = time.ToDateTime();
            }

            var list = _taskService.GetLastLogs(taskId, now);

            res.Result = list.MapTo<IList<SpiderTaskLogDto>>();
            if (res.Result != null) {
                foreach (var m in res.Result) {
                    m.Time = m.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            return res;
        }
    }
}