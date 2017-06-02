using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U;
using UFilm.Domain.Spiders;
using UFilm.Services.Spiders;

namespace UFilm.Console.Spiders.Douban
{
    public partial class MovieLinkToTask : UZeroConsole.Web.AuthPageBase
    {
        IDoubanLinkService _linkService = UPrimeEngine.Instance.Resolve<IDoubanLinkService>();
        ISpiderTaskService _taskService = UPrimeEngine.Instance.Resolve<ISpiderTaskService>();

        public MovieLinkToTaskModel Model = new MovieLinkToTaskModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            btnCommit.Click += btnCommit_Click;
            Model.Count = _linkService.GetCountByUnUsed();

        }

        void btnCommit_Click(object sender, EventArgs e)
        {
            var list = _linkService.GetListByUnUsed(tbCount.Text.Trim().ToInt());
            if (list != null)
            {
                var index = 0;
                foreach (var link in list)
                {
                    SpiderTask task = new SpiderTask();
                    task.Name = link.Name;
                    task.Links = link.Link;
                    _taskService.InsertTask(task);

                    link.IsJoinTask = true;
                    _linkService.Update(link);
                    index++;
                }

                ltlMessage.Text = AlertSuccess(index + " 条导入成功");
            }
        }
    }

    public class MovieLinkToTaskModel
    {
        public int Count { get; set; }
    }
}