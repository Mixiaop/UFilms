using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using U;
using U.BackgroundJobs;
using UZeroMedia.Client.Services;
using UFilm.Domain.Adults;
using UFilm.Services.Adults;

namespace UFilm.Console.Adults.Spiders.Www
{
    public class SpiderWwwNH87CNJob : BackgroundJob<int>, U.Dependency.ITransientDependency
    {
        public override void Execute(int args)
        {
            NH87CNService service = UPrimeEngine.Instance.Resolve<NH87CNService>();
            ISpiderService spiderService = UPrimeEngine.Instance.Resolve<ISpiderService>();

            //取出所有未完成的任务并执行取1条
            var tasks = spiderService.GetListByUnUsed(SpiderLinkSource.WwwNH87cn, 1, OrderBy.CreationTimeAsc);
            if (tasks != null && tasks.Count > 0)
            {
                var task = tasks[0];
                service.SpiderHomePage(task.Id, task.Link, task.Name);
                task.IsJoinTask = true;
                spiderService.Update(task);
                //递归下一个
                var backgroundJobManager = UPrimeEngine.Instance.Resolve<IBackgroundJobManager>();
                backgroundJobManager.EnqueueAsync<SpiderWwwNH87CNJob, int>(0);

            }


            //测试 4
            //var task = spiderService.Get(4);
            //service.SpiderHomePage(task.Id, task.Link, task.Name);
        }
    }

    public class NH87CNService : U.Application.Services.IApplicationService
    {
        #region Services
        ISpiderService _spiderService = UPrimeEngine.Instance.Resolve<ISpiderService>();
        IActorService _actorService = UPrimeEngine.Instance.Resolve<IActorService>();
        IMovieService _movieService = UPrimeEngine.Instance.Resolve<IMovieService>();
        PictureClientService _pictureClientService = UPrimeEngine.Instance.Resolve<PictureClientService>();
        public void SpiderHomePage(int linkId, string link, string name)
        {
            try
            {
                name = name.TrimEnd();
                if (_actorService.Exists(name) && link.IsNotNullOrEmpty())
                {
                    var actor = _actorService.Get(name);
                    Regex reg;
                    RegexOptions regOptions = RegexOptions.None;
                    WebClient client = new WebClient();

                    byte[] pageBytes = client.DownloadData(link);
                    string pageHtml = Encoding.GetEncoding("utf-8").GetString(pageBytes);


                    //个人作品 最后一条
                    reg = new Regex(@"<div class=""top_box"">([\s\S]+?)<[ph2]+ class=""next_page"" style=""font-size:10px;"" rel=""nofollow"">", regOptions);
                    string listContent = reg.Match(pageHtml).Groups[1].ToString();

                    reg = new Regex(@"<li class=""post"">([\s\S]+?)</li>", regOptions);
                    MatchCollection items = reg.Matches(listContent);
                    var index = 0;
                    foreach (Match item in items)
                    {
                        var movie = new LMovie();
                        var itemContent = item.Groups[1].ToString();
                        //番号 日期
                        reg = new Regex(@"<a href=""([\s\S]+?)>", regOptions);
                        string contentLink = reg.Match(itemContent).Groups[1].ToString().Replace(@".html"" target=""_blank""", "");
                        string[] contentLinkList = contentLink.Split('/');
                        if (contentLinkList.Length >= 3)
                        {
                            movie.Code = contentLinkList[(contentLinkList.Length - 1)].ToString().Trim();
                            //判断是否存在并获取电影
                            if (_movieService.Exists(movie.Code))
                            {
                                movie = _movieService.Get(movie.Code);
                            }
                        }

                        //标题
                        reg = new Regex(@"<strong>([\s\S]+?)</strong>", regOptions);
                        movie.CnName = reg.Match(itemContent).Groups[1].ToString();

                        if (movie.Id == 0)
                        {
                            if (_movieService.Exists(movie.CnName, 0))
                            {
                                var code = movie.Code;
                                var cnName = movie.CnName;
                                movie = _movieService.Get(movie.CnName, 0);
                                movie.Code = code;
                                movie.CnName = cnName;
                            }
                        }

                        if (movie.CnName.IsNullOrEmpty())
                            movie.CnName = actor.CnName;

                        //封面
                        reg = new Regex(@"data-original='([\s\S]+?)'/></a>", regOptions);
                        string coverUrl = reg.Match(itemContent).Groups[1].ToString();
                        if (coverUrl.Trim().IsNotNullOrEmpty() && movie.CoverId == 0)
                        {
                            #region 上传头像到图片应用
                            try
                            {

                                string imgUrl = "http://www.nh87.cn" + coverUrl;

                                //上传到图片应用
                                var imgBytes = client.DownloadData(imgUrl);
                                var contentType = MimeMapping.GetMimeMapping(imgUrl);
                                var fileName = System.IO.Path.GetFileName(imgUrl);
                                var response = _pictureClientService.Upload(fileName, imgBytes, contentType, imgBytes.Length);

                                if (response.IsSuccess() && response.Results != null)
                                {
                                    movie.CoverId = response.Results.PictureId;
                                }
                                string url = response.Results.PictureUrl;

                            }
                            catch (Exception ex)
                            {

                            }
                            #endregion
                        }

                        if (movie.Hits == 0)
                        {
                            //热度
                            reg = new Regex(@"<div class=""view"" [\s\S]+>([0-9]+?)</div>", regOptions);
                            movie.Hits = reg.Match(itemContent).Groups[1].ToInt();
                        }

                        if (movie.OtherPostYear.IsNullOrEmpty())
                        {
                            //发布日期
                            reg = new Regex(@"title=""发行日期"">([0-9-]+?)</div>", regOptions);
                            movie.OtherPostYear = reg.Match(itemContent).Groups[1].ToString();
                        }

                        if (movie.Year == 0 && movie.OtherPostYear.IsNotNullOrEmpty())
                        {
                            DateTime date = movie.OtherPostYear.StrToDateTime();
                            movie.Year = date.Year;
                        }

                        //类型、分钟
                        contentLink = "http://www.nh87.cn" + contentLink + ".html";
                        if (movie.Id == 0)
                        {
                            byte[] contentPageBytes = client.DownloadData(contentLink);
                            string contentPageHtml = Encoding.GetEncoding("utf-8").GetString(contentPageBytes);

                            reg = new Regex(@"时长([\s\S]+?)正式发片日期是([0-9-]+)", regOptions);

                            var contentIntroduction = reg.Match(contentPageHtml).Groups[1].ToString()
                                .Replace("本番号作品分类定义于：", "")
                                .Replace("正式发片日期是", "")
                                .Replace("</p>", "");


                            var contentIntroList = contentIntroduction.Split("，");
                            if (contentIntroList.Length > 2)
                            {
                                movie.MovieLength = contentIntroList[0].Trim();
                                movie.MovieType = contentIntroList[1].Replace("、", " ").Trim().Replace(" ", ",");

                                //movie.OtherPostYear = reg.Match(contentPageHtml).Groups[2].ToString();
                            }
                        }

                        if (movie.Id == 0)
                        {

                            //增加
                            List<Actor> actors = new List<Actor>();
                            actors.Add(actor);
                            int movieId = _movieService.Insert(movie, actors);

                        }
                        else
                        {
                            if (movie.Actors.IsNullOrEmpty()) {
                                movie.Actors = actor.CnName;
                            }
                            //修改
                            _movieService.Update(movie);
                        }
                        index++;
                        //break; //测试
                    } //foreach
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var a = 1;
            }
        }
        #endregion
    }
}