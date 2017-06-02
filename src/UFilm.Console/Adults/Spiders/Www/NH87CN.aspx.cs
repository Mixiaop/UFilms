using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using U;
using U.Utilities.Web;
using U.BackgroundJobs;
using UZeroMedia.Client.Services;
using UFilm.Domain.Adults;
using UFilm.Services.Adults;

namespace UFilm.Console.Adults.Spiders.Www
{
    public partial class NH87CN : UZeroConsole.Web.AuthPageBase
    {
        ISpiderService _spiderService = UPrimeEngine.Instance.Resolve<ISpiderService>();
        IActorService _actorService = UPrimeEngine.Instance.Resolve<IActorService>();
        PictureClientService _pictureClientService = UPrimeEngine.Instance.Resolve<PictureClientService>();
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSpiderListLinks.Click += btnSpiderListLinks_Click;
            btnSpiderMovies.Click += btnSpiderMovies_Click;
            //btnRun.Click += btnRun_Click;
        }

        void btnRun_Click(object sender, EventArgs e)
        {
            #region 运行任务 采集影人的作品页
            var backgroundJobManager = UPrimeEngine.Instance.Resolve<IBackgroundJobManager>();

            backgroundJobManager.EnqueueAsync<SpiderWwwNH87CNJob, int>(0);
            #endregion
        }

        void btnSpiderMovies_Click(object sender, EventArgs e)
        {
            #region 测试任务 采集影人的作品页
            var link = _spiderService.Get(717);
            if (link != null)
            {
                var info = link;
                UPrimeEngine.Instance.Resolve<NH87CNService>().SpiderHomePage(info.Id, info.Link, info.Name);
            }
            #endregion
        }

        void btnSpiderListLinks_Click(object sender, EventArgs e)
        {
            #region 采集所有影人的主页链接
            int linkCount = 0;
            Regex reg;
            RegexOptions regOptions = RegexOptions.None;
            WebClient client = new WebClient();
            var pageUrl = WebHelper.GetHost().TrimEnd('/') + "/Adults/Spiders/Www/nh87cn.html";
            byte[] pageBytes = client.DownloadData(pageUrl);
            string pageHtml = Encoding.GetEncoding("utf-8").GetString(pageBytes);
            //解析链接
            for (int i = 1; i <= 500; i++)
            {
                string content = "";
                if (i == 500)
                {
                    reg = new Regex(@"TOP.500<([\s\S]+?)<end>", regOptions);
                    content = reg.Match(pageHtml).Groups[1].ToString();
                }
                else
                {
                    reg = new Regex(string.Format(@"TOP.{0}<([\s\S]+?)TOP.{1}<", i, (i + 1)), regOptions);
                    content = reg.Match(pageHtml).Groups[1].ToString();
                }

                //链接拼音
                reg = new Regex(@"<a href=""([\s\S]+?)""><img src=", regOptions);
                string link = reg.Match(content).Groups[1].ToString();
                string pinyin = link.Replace("/", "").Trim();
                //姓名
                reg = new Regex(@"alt=""([\s\S]+?)""[\s\S]+data-", regOptions);
                string name = reg.Match(content).Groups[1].ToString();
                //头像
                reg = new Regex(@"data-original=""([\s\S]+?)""></a><div", regOptions);
                string avatarLink = reg.Match(content).Groups[1].ToString();

                //点击
                reg = new Regex(@"<p>票数:([\s\S]+?)</p></div></div>", regOptions);
                string hits = reg.Match(content).Groups[1].ToString();

                var actor = new Actor();
                #region 获取影人
                if (!_actorService.Exists(name))
                {
                    #region 添存在则添加
                    actor.CnName = name;
                    actor.Pinyin = pinyin;
                    actor.Hits = hits.ToInt();

                    #region 上传头像到图片应用
                    try
                    {
                        string imgUrl = avatarLink;

                        //上传到图片应用
                        var imgBytes = client.DownloadData(imgUrl);
                        var contentType = MimeMapping.GetMimeMapping(imgUrl);
                        var fileName = System.IO.Path.GetFileName(imgUrl);
                        var response = _pictureClientService.Upload(fileName, imgBytes, contentType, imgBytes.Length);

                        if (response.IsSuccess() && response.Results != null)
                        {
                            actor.AvatarId = response.Results.PictureId;
                        }
                        string url = response.Results.PictureUrl;
                    }
                    catch (Exception ex)
                    {

                    }
                    #endregion
                    _actorService.Insert(actor);
                    #endregion
                }
                else
                {
                    actor = _actorService.Get(name);

                    if (actor.AvatarId == 0 && avatarLink.IsNotNullOrEmpty())
                    {
                        #region 上传头像到图片应用
                        try
                        {
                            string imgUrl = avatarLink;

                            //上传到图片应用
                            var imgBytes = client.DownloadData(imgUrl);
                            var contentType = MimeMapping.GetMimeMapping(imgUrl);
                            var fileName = System.IO.Path.GetFileName(imgUrl);
                            var response = _pictureClientService.Upload(fileName, imgBytes, contentType, imgBytes.Length);

                            if (response.IsSuccess() && response.Results != null)
                            {
                                actor.AvatarId = response.Results.PictureId;
                            }
                            _actorService.Update(actor);
                        }
                        catch (Exception ex)
                        {

                        }
                        #endregion
                    }
                }
                #endregion
                actor.Gender = 0;

                //采集个人主页
                string homePageLink = "http://www.nh87.cn" + link;
                byte[] homePageBytes = client.DownloadData(homePageLink);
                string homePageHtml = Encoding.GetEncoding("utf-8").GetString(homePageBytes);
                if (homePageHtml.IsNotNullOrEmpty())
                {
                    //解析简介
                    if (actor.Introduction.IsNullOrEmpty())
                    {
                        reg = new Regex(@"<p class='avms'>([\s\S]+?)</p>[\s\S]+?<div class='nian btn-group btn-group-justified'>", regOptions);
                        actor.Introduction = reg.Match(homePageHtml).Groups[1].ToString();
                        _actorService.Update(actor);
                    }
                }

                //添加到任务列表
                if (!_spiderService.Exists(homePageLink))
                {
                    SpiderLink spiderLink = new SpiderLink();
                    spiderLink.Name = actor.CnName;
                    spiderLink.Link = homePageLink;
                    spiderLink.Remark = "个人主页";
                    _spiderService.InsertLink(spiderLink, SpiderLinkSource.WwwNH87cn);
                }
                linkCount++;
                //Thread.Sleep(1000);



            }
            #endregion
            Response.Write("采集主页完成");
        }
    }

}