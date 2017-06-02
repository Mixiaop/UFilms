using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;
using System.Web;
using U;
using U.Logging;
using UFilm.Domain.Spiders;
using UFilm.Domain.Movies;
using UFilm.Services.Movies;
using UZeroMedia.Client.Services;
//using Abp.Domain.Uow;
//using Mbj.Movies;
//using Mbj.SoaClients.Image;

namespace UFilm.Services.Spiders
{
    public class DoubanSpiderService : IDoubanSpiderService
    {
        //private readonly IDoubanLinkRepository _linkRepository;
        private readonly ISpiderTaskService _spiderTaskService;
        private readonly IMovieService _movieService;
        private readonly IMoviePhotoService _moviePhotoService;
        private readonly IFilmManService _filmManService;
        private readonly IFilmManPhotoService _filmManPhotoService;

        ISpiderTaskRepository _taskRepository;
        public DoubanSpiderService(ISpiderTaskService spiderTaskService, IMovieService movieService, IMoviePhotoService moviePhotoService, IFilmManService filmManService, IFilmManPhotoService filmManPhotoService,
        ISpiderTaskRepository taskRepository)
        {
            //_linkRepository = linkRepository;
            _spiderTaskService = spiderTaskService;
            _movieService = movieService;
            _moviePhotoService = moviePhotoService;
            _filmManService = filmManService;
            _filmManPhotoService = filmManPhotoService;

            _taskRepository = taskRepository;
        }

        #region Links
        ///// <summary>
        ///// 检查是否存在对象Id
        ///// </summary>
        ///// <param name="objectId">电影或影人Id</param>
        ///// <param name="typeId">类型【1=电影，2=影人】</param>
        ///// <returns></returns>
        //public bool IsExists(int objectId, int typeId)
        //{
        //    var count = _linkRepository.Count(x => x.ObjectId == objectId && x.TypeId == typeId);

        //    return count > 0;
        //}

        ///// <summary>
        ///// 插入一条链接
        ///// </summary>
        ///// <param name="link"></param>
        //public int InsertLink(DoubanLink link)
        //{
        //    return _linkRepository.InsertAndGetId(link);
        //}

        ///// <summary>
        ///// 添加一条豆瓣的采集链接
        ///// </summary>
        ///// <param name="typeId">类型【1=电影，2=影人】</param>
        ///// <param name="objectName">名称</param>
        ///// <param name="objectId">我们的标识</param>
        ///// <param name="link">链接</param>
        //public void AddLink(int typeId, string objectName, int objectId, string link)
        //{
        //    DoubanLink sLink = new DoubanLink();
        //    sLink.TypeId = typeId;
        //    sLink.ObjectName = objectName;
        //    sLink.ObjectId = objectId;
        //    sLink.Link = link;

        //    InsertLink(sLink);
        //}

        ///// <summary>
        ///// 通过类型获取链接列表
        ///// </summary>
        ///// <param name="typeId">类型【1=电影，2=影人】</param>
        ///// <returns></returns>
        //public IList<DoubanLink> GetLinks(int typeId)
        //{
        //    return _linkRepository.GetAllList(x => x.TypeId == typeId);
        //}

        ///// <summary>
        ///// 更新上次图片数量
        ///// </summary>
        ///// <param name="linkId"></param>
        ///// <param name="count"></param>
        //public void UpdatePrevPhotoCount(int linkId, int count)
        //{
        //    _linkRepository.Update(linkId, new System.Action<DoubanLink>(x =>
        //    {
        //        x.PrevPhotoCount = count;
        //        x.PrevHasPhotoCount = count > 0;
        //    }));
        //}
        #endregion

        #region Spider Movies


        /// <summary>
        /// 采集一条电影信息
        /// </summary>
        /// <param name="collectLink">采集链接</param>
        /// <param name="name"></param>
        /// <param name="task"></param>
        public int CollectMovie(int taskId, string collectLink)
        {
            if (string.IsNullOrEmpty(collectLink))
            {
                throw new Exception("采集链接不能为空");
            }
            if (!collectLink.Contains("https://movie.douban.com/subject") && !collectLink.Contains("http://movie.douban.com/subject"))
            {
                throw new Exception("豆瓣电影链接有误请检查");
            }

            //if (task == null)
            //{
            //    task = new SpiderTask();
            //    task.Name = "采集电影";
            //    if (!string.IsNullOrEmpty(name))
            //    {
            //        task.Name += "【" + name + "】";
            //    }
            //    task.Id = _spiderTaskService.InsertTask(task);
            //}
            var rand = new Random();
            Movie movie = new Movie(); //解析完的实体

            if (!collectLink.EndsWith("/"))
                collectLink = collectLink + "/";

            //采集中
            var task = _spiderTaskService.GetTask(taskId);
            task.Spidering = true;
            _spiderTaskService.UpdateTask(task);

            try
            {
                PictureClientService pictureService = new PictureClientService();
                RegexOptions regOptions = RegexOptions.None;
                byte[] pageBytes;     //页面字节
                string pageHtml = ""; //页面HTML
                Regex reg; //正则
                WebClient client = new WebClient();
                client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.56 Safari/537.17");



                #region 解析 - 电影页
                pageBytes = client.DownloadData(collectLink);
                pageHtml = Encoding.GetEncoding("utf-8").GetString(pageBytes);

                //解析电影内容区
                reg = new Regex(@"<div id=""wrapper""[\s\t\r\n""'<>]*[^<>]*?>([\s\S]+?)<div id=""related-pic"" class=""related-pic"">", regOptions);
                var movieContent = reg.Match(pageHtml).Groups[0].ToString();

                _spiderTaskService.AddLog(task.Id, "解析 - 电影页HTML");
                #endregion

                #region 解析 - 电影名称 & 年份
                //中文名
                //reg = new Regex(@"<h2>([\s\S]+?)的剧情简介", regOptions);
                //movie.CnName = reg.Match(movieContent).Groups[1].ToString();
                reg = new Regex(@"<title>([\s\S]+?)</title>", regOptions);
                movie.CnName = reg.Match(pageHtml).Groups[1].ToString().Trim();
                movie.CnName = movie.CnName.Replace("（", "(").Replace("）", ")").Replace("(豆瓣)", "").Trim();

                //英文名
                reg = new Regex(@"<span property=""v:itemreviewed"">([\s\S]+?)</span>", regOptions);
                movie.EnName = reg.Match(movieContent).Groups[1].ToString().Trim();
                if (!string.IsNullOrEmpty(movie.EnName.Replace(movie.CnName, "")) || movie.EnName == movie.CnName)
                {
                    //英文名带中文一起时，替换掉中文名
                    movie.EnName = movie.EnName.Replace(movie.CnName, "").Trim();
                }
                //年份
                reg = new Regex(@"<span class=""year"">([\s\S]+?)</span>", regOptions);
                movie.Year = Convert.ToInt32(reg.Match(movieContent).Groups[1].ToString().Replace("(", "").Replace(")", "").Trim());
                task.Name = "采集电影【" + movie.Year + " " + movie.CnName + " " + movie.EnName + "】";
                _spiderTaskService.UpdateTask(task);

                _spiderTaskService.AddLog(task.Id, string.Format("解析 - 电影中文名【{0}】，英文名【{1}】，年份【{2}】", movie.CnName, movie.EnName, movie.Year.ToString()));
                #endregion

                var mov = _movieService.Get(movie.CnName, movie.EnName.IsNotNullOrEmpty() ? movie.EnName : "", movie.Year > 0 ? movie.Year : 0);
                if (mov != null)
                {
                    #region 是否已存在电影，则返回
                    _spiderTaskService.AddLog(task.Id, "电影【" + movie.Year + " " + movie.CnName + "】已存在，则跳过！！！");
                    //更新完成任务时间
                    task.Finished = true;
                    task.FinishedTime = DateTime.Now;
                    task.Spidering = false;
                    task.ObjectId = mov.Id;
                    _spiderTaskService.UpdateTask(task);
                    return mov.Id;
                    #endregion

                }
                else
                {
                    #region 不存在电影，则添加
                    //try
                    //{
                    movie.Id = _movieService.Insert(movie); //创建一部电影
                    movie.DoubanLink = collectLink;
                    task.ObjectId = movie.Id;
                    _spiderTaskService.UpdateTask(task);
                    //AddLink(1, movie.CnName + " " + movie.EnName, movie.Id, collectLink); //添加采集链接到库中

                    //影人与电影信息解析的都是同一块内容
                    reg = new Regex(@"<div id=""info"">([\s\S]+?)</div>", regOptions);
                    var infoContent = reg.Match(movieContent).Groups[0].ToString();
                    reg = new Regex(@"<span([\s\S]+?)<br/>", regOptions);
                    MatchCollection mInfoItems = reg.Matches(infoContent); //电影每项信息

                    #region 解析 - 电影信息等简介

                    foreach (Match mInfo in mInfoItems)
                    {
                        var mInfoStr = mInfo.Groups[0].ToString();
                        if (!string.IsNullOrEmpty(mInfoStr))
                        {
                            if (mInfoStr.IndexOf("<span class='pl'>") == -1)
                            {
                                reg = new Regex(@"<span class=""pl"">([\s\S]+?)</span>", regOptions);
                                string type = reg.Match(mInfoStr).Groups[1].ToString().Replace(":", "").Replace("：", "").Trim();
                                switch (type)
                                {
                                    case "类型":
                                        reg = new Regex(@"<span property[\s\t\r\n""'<>]*[^<>]*?>([\s\S]+?)</span>", regOptions);
                                        MatchCollection mTypes = reg.Matches(mInfoStr);
                                        foreach (Match mType in mTypes)
                                        {
                                            movie.MovieType += mType.Groups[1].ToString().Trim() + ",";
                                        }
                                        if (movie.MovieType != "")
                                            movie.MovieType = movie.MovieType.Remove(movie.MovieType.Length - 1);

                                        break;
                                    case "制片国家/地区":
                                        reg = new Regex(@"</span>([\s\S]+?)<br/>", regOptions);
                                        Match mArea = reg.Match(mInfoStr);
                                        var areaList = mArea.Groups[1].ToString().Split('/');
                                        if (areaList.Length != 0)
                                        {
                                            for (int i = 0; i < areaList.Length; i++)
                                            {
                                                movie.Area += areaList[i].Trim() + ",";
                                            }
                                        }
                                        if (movie.Area != "")
                                            movie.Area = movie.Area.Remove(movie.Area.Length - 1);

                                        break;
                                    case "语言":
                                        reg = new Regex(@"</span>([\s\S]+?)<br/>", regOptions);
                                        Match mLanuage = reg.Match(mInfoStr);
                                        var lanuageList = mLanuage.Groups[1].ToString().Split('/');
                                        if (lanuageList.Length != 0)
                                        {
                                            for (int i = 0; i < lanuageList.Length; i++)
                                            {
                                                movie.Language += lanuageList[i].Trim() + ",";
                                            }
                                        }
                                        if (movie.Language != "")
                                            movie.Language = movie.Language.Remove(movie.Language.Length - 1);

                                        break;
                                    case "上映日期":
                                        reg = new Regex(@"<span property=""v:initialReleaseDate"" [\s\t\r\n""'<>]*[^<>]*?>([\s\S]+?)</span>", regOptions);
                                        MatchCollection mDates = reg.Matches(mInfoStr);
                                        foreach (Match mDate in mDates)
                                        {
                                            movie.OtherPostYear += mDate.Groups[1].ToString().Trim() + ",";
                                        }

                                        if (movie.OtherPostYear != "")
                                            movie.OtherPostYear = movie.OtherPostYear.Remove(movie.OtherPostYear.Length - 1);

                                        break;
                                    case "片长":
                                        reg = new Regex(@"<span property=""v:runtime"" [\s\t\r\n""'<>]*[^<>]*?>([\s\S]+?)</span>", regOptions);
                                        MatchCollection mLengths = reg.Matches(mInfo.Groups[0].ToString());
                                        foreach (Match mLength in mLengths)
                                        {
                                            movie.MovieLength += mLength.Groups[1].ToString().Trim() + ",";
                                        }
                                        reg = new Regex(@"</span> /([\s\S]+?)<br/>", regOptions);
                                        Match mEndLength = reg.Match(mInfo.Groups[0].ToString());
                                        if (mEndLength != null && mEndLength.Groups[1].ToString() != "")
                                        {
                                            movie.MovieLength += mEndLength.Groups[1].ToString().Trim() + ",";
                                        }
                                        if (movie.MovieLength != "")
                                            movie.MovieLength = movie.MovieLength.Remove(movie.MovieLength.Length - 1);

                                        break;
                                    case "又名":
                                        reg = new Regex(@"</span>([\s\S]+?)<br/>", regOptions);

                                        Match mNames = reg.Match(mInfo.Groups[0].ToString());
                                        var nameList = mNames.Groups[1].ToString().Trim().Split('/');
                                        if (nameList.Length != 0)
                                        {
                                            for (int i = 0; i < nameList.Length; i++)
                                            {
                                                if (nameList[i] != "")
                                                    movie.OtherEnName += nameList[i].Trim() + ",";
                                            }
                                        }
                                        if (!string.IsNullOrEmpty(movie.OtherEnName) && movie.OtherEnName.IndexOf(",") != -1)
                                            movie.OtherEnName = movie.OtherEnName.Remove(movie.OtherEnName.Length - 1);

                                        break;
                                    case "季数":
                                        movie.IsSeries = 1;

                                        reg = new Regex(@"selected=selected >([\s\S]+?)</option>", regOptions);
                                        movie.SeriesNumber = reg.Match(mInfo.Groups[0].ToString()).Groups[1].ToString().Trim().ToInt();
                                        //Log("电影季数", movie.SeriesNumber.ToString());
                                        break;
                                    case "集数":
                                        movie.IsSeries = 1;

                                        reg = new Regex(@"</span>([\s\S]+?)<br/>", regOptions);
                                        movie.SeriesCount = reg.Match(mInfo.Groups[0].ToString()).Groups[1].ToString().Trim().ToInt();
                                        //Log("电影集数", movie.SeriesCount.ToString());
                                        break;
                                    case "单集片长":
                                        movie.IsSeries = 1;

                                        reg = new Regex(@"</span>([\s\S]+?)<br/>", regOptions);
                                        movie.SeriesLength = reg.Match(mInfo.Groups[0].ToString()).Groups[1].ToString().Trim();
                                        //Log("电影单集片长", movie.SeriesLength);
                                        break;
                                    case "官方网站":
                                        reg = new Regex(@"_blank"">([\s\S]+?)</a>", regOptions);
                                        movie.WebSite = reg.Match(mInfo.Groups[0].ToString()).Groups[1].ToString().Trim();
                                        //Log("官方网站", movie.WebSite);
                                        break;
                                }
                            }
                        }
                    }

                    //IMDB编号
                    reg = new Regex(@"rel=""nofollow"">([\s\S]+?)</a>", regOptions);
                    movie.ImdbCode = reg.Match(infoContent).Groups[1].ToString().Trim();
                    //Log("IMDB编号", movie.ImdbCode);

                    //豆瓣评分
                    reg = new Regex(@"<strong class=""ll rating_num"" property=""v:average"">([\s\S]+?)</strong>", regOptions);
                    movie.DoubanRating = reg.Match(pageHtml).Groups[1].ToString().Trim();
                    if (movie.DoubanRating.Length > 5)
                    {
                        movie.DoubanRating = "";
                    }

                    #region 简介
                    reg = new Regex(@"<div class=""related-info"" style=""margin-bottom:-10px;"">([\s\S]+?)</div>", regOptions);
                    movie.Introduction = reg.Match(movieContent).Groups[1].ToString().Trim();
                    if (movie.Introduction != "")
                    {
                        reg = new Regex(@"<span property=""v:summary"" class="""">([\s\S]+?)</span>", regOptions);
                        Match match = reg.Match(movie.Introduction);

                        if (!string.IsNullOrEmpty(match.Groups[1].ToString().Trim()))
                        {
                            movie.Introduction = match.Groups[1].ToString().Trim();
                        }
                        else
                        {
                            reg = new Regex(@"<span class=""all hidden"">([\s\S]+?)</span>", regOptions);
                            movie.Introduction = reg.Match(movie.Introduction).Groups[1].ToString().Trim();
                        }

                    }
                    #endregion

                    _movieService.Update(movie);
                    _spiderTaskService.AddLog(task.Id, "电影信息等简介解析完毕");
                    #endregion


                    #region 解析 - 电影导演、编剧、影人
                    string director = "";
                    string bianJ = "";
                    string actor = "";
                    foreach (Match mInfo in mInfoItems)
                    {
                        var mInfoStr = mInfo.Groups[0].ToString();
                        if (!string.IsNullOrEmpty(mInfoStr))
                        {
                            if (mInfoStr.IndexOf("<span class='pl'>") != -1)
                            {
                                Regex nameReg = new Regex(@"<span class='pl'>([\s\S]+?)</span>", regOptions);
                                Regex fmReg = new Regex(@"<a[\s\t\r\n""'<>]*[^<>]*?>([\s\S]+?)</a>", regOptions);
                                Regex fmLinkReg = new Regex(@"<a href=""([\s\S]+?)""", regOptions);
                                MatchCollection fmListMatches = fmReg.Matches(mInfo.Groups[0].ToString());

                                string manType = nameReg.Match(mInfo.Groups[0].ToString()).Groups[1].ToString();
                                int manSort = 1;
                                foreach (Match fmInfoMatch in fmListMatches)
                                {
                                    string manUrl = fmLinkReg.Match(fmInfoMatch.Groups[0].ToString()).Groups[1].ToString();
                                    string fmName = fmInfoMatch.Groups[1].ToString();
                                    manUrl = "http://movie.douban.com" + manUrl;
                                    //检查影人是否存在
                                    var man = _filmManService.Get(fmName);
                                    int filmManId = 0;
                                    if (man == null)
                                    {
                                        //去采集
                                        _spiderTaskService.AddLog(task.Id, string.Format("不存在影人【{0}】，准备采集【{1}】", fmName, manUrl));
                                        filmManId = CollectFilmManTask(movie, manUrl, task.Id);
                                    }
                                    else
                                    {
                                        filmManId = man.Id;
                                    }

                                    //采集不到影人，有些名称是这样的 布拉德·皮特 Brad Pitt从库里解析布拉德·皮特 
                                    if (filmManId == 0)
                                    {
                                        var names = fmName.Split(' ');
                                        if (names != null && names.Length > 0)
                                        {
                                            var getman = _filmManService.Get(names[0]);
                                            if (getman != null)
                                                filmManId = getman.Id;
                                        }
                                    }

                                    if (filmManId == 0)
                                    {
                                        FilmMan newman = new FilmMan();
                                        newman.CnName = fmName;
                                        filmManId = _filmManService.Insert(newman);
                                    }

                                    MovieParticipant mp = new MovieParticipant();
                                    mp.FilmManId = filmManId;
                                    mp.MovieId = movie.Id;
                                    mp.Sort = manSort;
                                    switch (manType)
                                    {
                                        case "导演":
                                            movie.Director += fmName + ",";
                                            mp.JobType = MovieJobType.Director;
                                            break;
                                        case "编剧":
                                            mp.JobType = MovieJobType.ScreenWriter;
                                            break;
                                        case "主演":
                                            movie.Actor += fmName + ",";
                                            mp.JobType = MovieJobType.Actor;
                                            break;
                                    }
                                    manSort++;
                                    _movieService.InsertParticipant(mp);
                                    _movieService.Update(movie);
                                }
                            }
                        }
                    }

                    movie.Director = movie.Director.TrimEnd(',');
                    movie.Actor = movie.Actor.TrimEnd(',');
                    _movieService.Update(movie);
                    _spiderTaskService.AddLog(task.Id, "电影导演、编剧、影人解析完毕");
                    #endregion

                    #region 解析 - 电影封面 & 剧照
                    //封面链接
                    reg = new Regex(@"<a class=""nbgnbg"" href=""([\s\S]+?)"" title=""点击看更多海报"">", regOptions);
                    string moviePosterLink = collectLink + "photos?type=R"; //reg.Match(movieContent).Groups[1].ToString();

                    pageBytes = client.DownloadData(moviePosterLink);
                    pageHtml = Encoding.GetEncoding("utf-8").GetString(pageBytes);
                    reg = new Regex(@"<ul class=""poster-col4 clearfix""[\s\t\r\n""'<>]*[^<>]*?>([\s\S]+?)</ul>", regOptions);
                    string posterContent = reg.Match(pageHtml).ToString();

                    _spiderTaskService.AddLog(task.Id, "休息 5 秒，解析 - 电影海报页HTML");
                    Thread.Sleep(8000);//【休息几秒】

                    //获取封面图片Urls，格式例子：http;//douban.com/thumb1.jpg,http;//douban.com/thumb2.jpg
                    reg = new Regex(@"<img src=""([\s\S]+?)"" />[\s\t\r\n""'<>]*</a>", regOptions);
                    var posterImagesMatches = reg.Matches(posterContent);
                    _spiderTaskService.AddLog(task.Id, "解析 - 电影海报共【" + posterImagesMatches.Count + "】张");
                  
                    if (posterImagesMatches.Count > 0)
                    {
                        #region 有海报
                        string posterImageUrls = "";
                        int coverPictureId = 0; //最后的海报Id
                        int posterIndex = 1;
                        foreach (Match posterImage in posterImagesMatches)
                        {
                            var posterUrl = posterImage.Groups[1].ToString();
                            #region 上传到图片应用【因为采集到的为缩略图，把缩略图替换成源图，当前是600比例内的】
                            string imgUrl = posterUrl.Replace("thumb", "photo");

                            //上传到图片应用
                            var imgBytes = client.DownloadData(imgUrl);
                            var contentType = MimeMapping.GetMimeMapping(imgUrl);
                            var fileName = System.IO.Path.GetFileName(imgUrl);
                            var response = pictureService.Upload(fileName, imgBytes, contentType, imgBytes.Length);
                            if (response.IsSuccess() && response.Results != null)
                            {
                                //更新数据库
                                MoviePhoto photo = new MoviePhoto();
                                photo.MovieId = movie.Id;
                                photo.PictureId = response.Results.PictureId;
                                photo.Description = string.Empty;
                                photo.PhotoType = MoviePhotoType.Poster;
                                _moviePhotoService.Insert(photo);
                                coverPictureId = photo.PictureId;

                                //竖的比例才选择当海报
                                System.Drawing.Image img = imgBytes.ToImage();
                                if (movie.CoverId == 0 && img.Size.Height > img.Size.Width)
                                {
                                    //首张当封面
                                    movie.CoverId = photo.PictureId;
                                    _movieService.Update(movie);
                                }
                                task.ImageCount++;
                                
                            }
                            #endregion
                            _spiderTaskService.AddLog(task.Id, "休息 5 秒，已解析电影海报开采第 " + posterIndex + " 张");
                            Thread.Sleep(8000);
                            posterIndex++;
                        }
                        _spiderTaskService.AddLog(task.Id, "【" + movie.CnName + "】电影海报采集完毕");

                        //如果没有合适比例的海报，只能拿最后一张当了
                        if (coverPictureId > 0 && movie.CoverId == 0)
                        {
                            movie.CoverId = coverPictureId;
                            _movieService.Update(movie);
                        }
                        #endregion
                    }
                    else
                    {
                        #region 默认海报
                        _spiderTaskService.AddLog(task.Id, "【" + movie.CnName + "】电影海报列表 0 张，拿默认海报！！！");
                        //海报列表 = 0，拿默认的
                        reg = new Regex(@"<a class=""nbgnbg"" href=""([\s\S]+?)"" title=""点击看大图"">", regOptions);
                        string defaultPosterUrl = reg.Match(movieContent).Groups[1].ToString();
                        if (defaultPosterUrl.IsNotNullOrEmpty() && !defaultPosterUrl.Contains("default"))
                        {
                            //上传到图片应用
                            var imgBytes = client.DownloadData(defaultPosterUrl);
                            var contentType = MimeMapping.GetMimeMapping(defaultPosterUrl);
                            var fileName = System.IO.Path.GetFileName(defaultPosterUrl);
                            var response = pictureService.Upload(fileName, imgBytes, contentType, imgBytes.Length);
                            if (response.IsSuccess() && response.Results != null)
                            {
                                //首张当封面
                                movie.CoverId = response.Results.PictureId;
                                _movieService.Update(movie);
                                task.ImageCount++;
                            }
                        }
                        #endregion
                    }

                    //电影图片
                    string moviePhotosLink = collectLink + "photos?type=S";
                    pageBytes = client.DownloadData(moviePhotosLink);
                    pageHtml = Encoding.GetEncoding("utf-8").GetString(pageBytes);
                    reg = new Regex(@"<ul class=""poster-col4 clearfix""[a-z\s\S\t\r\n""']*>([\s\S]+?)</ul>", regOptions);
                    var photoContent = reg.Match(pageHtml).Groups[0].ToString();

                    _spiderTaskService.AddLog(task.Id, "休息 5 秒，解析 - 电影剧照页HTML");
                    Thread.Sleep(8000);//【休息几秒】

                    reg = new Regex(@"<img src=""([\s\S]+?)"" />", regOptions);
                    var photoImagesMatch = reg.Matches(photoContent);
                    _spiderTaskService.AddLog(task.Id, "解析 - 电影剧照共【" + photoImagesMatch.Count + "】张");
                    if (photoImagesMatch.Count > 0)
                    {
                        string photoImagesUrls = "";
                        int photoIndex = 1;
                        foreach (Match photoImage in photoImagesMatch)
                        {
                            var photoUrl = photoImage.Groups[1].ToString();
                            #region 上传到图片应用【因为采集到的为缩略图，把缩略图替换成源图，当前是600比例内的】
                            string imgUrl = photoUrl.Replace("thumb", "photo");

                            //上传到图片应用
                            var imgBytes = client.DownloadData(imgUrl);
                            var contentType = MimeMapping.GetMimeMapping(imgUrl);
                            var fileName = System.IO.Path.GetFileName(imgUrl);
                            var response = pictureService.Upload(fileName, imgBytes, contentType, imgBytes.Length);

                            if (response.IsSuccess() && response.Results != null)
                            {
                                //更新数据库
                                MoviePhoto photo = new MoviePhoto();
                                photo.MovieId = movie.Id;
                                photo.PictureId = response.Results.PictureId;
                                photo.Description = string.Empty;
                                photo.PhotoType = MoviePhotoType.Stage;
                                _moviePhotoService.Insert(photo);

                                if (movie.CoverId == 0)
                                {
                                    //首张当封面
                                    movie.CoverId = photo.PictureId;
                                    _movieService.Update(movie);
                                }
                                task.ImageCount++;
                            }
                            #endregion
                            _spiderTaskService.AddLog(task.Id, "休息 5 秒，已解析电影剧照开采第 " + photoIndex + " 张");
                            Thread.Sleep(8000);
                            photoIndex++;
                            //photoImagesUrls += photoImage.Groups[1].ToString() + "，";
                        }

                        _spiderTaskService.AddLog(task.Id, "【" + movie.CnName + "】电影剧照采集完毕");
                    }
                    else
                    {
                        _spiderTaskService.AddLog(task.Id, "【" + movie.CnName + "】电影剧照 0 张，则跳过！！！");
                    }
                    #endregion

                    //更新完成任务时间
                    //task = _spiderTaskService.GetTask(taskId);
                    task.Spidering = false;
                    task.Finished = true;
                    task.FinishedTime = DateTime.Now;
                    _spiderTaskService.UpdateTask(task);

                    return movie.Id;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                task.Content = "报错了：" + ex.Message;
                task = _spiderTaskService.GetTask(taskId);
                task.Spidering = false;
                task.Finished = true;
                task.FinishedTime = DateTime.Now;
                _spiderTaskService.UpdateTask(task);
                U.Logging.LogHelper.Logger.Error(ex, "【" + movie.Id + ":" + movie.CnName + "】" + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// 采集一条影人信息
        /// </summary>
        /// <param name="collectLink">采集链接</param>
        /// <param name="name"></param>
        /// <param name="task"></param>
        private int CollectFilmManTask(Movie movie, string collectLink, int taskId)
        {
            var task = _spiderTaskService.GetTask(taskId);
            try
            {
                if (string.IsNullOrEmpty(collectLink))
                {
                    throw new Exception("采集链接不能为空");
                }

                if (!collectLink.Contains("https://movie.douban.com/celebrity") && !collectLink.Contains("http://movie.douban.com/celebrity"))
                {
                    throw new Exception("豆瓣影人链接有误请检查");
                }


                if (!collectLink.EndsWith("/"))
                    collectLink = collectLink + "/";

                PictureClientService pictureService = new PictureClientService();
                RegexOptions regOptions = RegexOptions.None;
                byte[] pageBytes;     //页面字节
                string pageHtml = ""; //页面HTML
                Regex reg; //正则
                WebClient client = new WebClient();
                client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.56 Safari/537.17");

                FilmMan filmMan = new FilmMan(); //解析完的实体

                #region 解析 - 影人页
                pageBytes = client.DownloadData(collectLink);
                pageHtml = Encoding.GetEncoding("utf-8").GetString(pageBytes);



                //解析影人内容区
                reg = new Regex(@"<div id=""content"">([\s\S]+?)上传照片", regOptions);
                var filmManContent = reg.Match(pageHtml).Groups[0].ToString();
                _spiderTaskService.AddLog(taskId, "解析 - 影人页HTML");
                #endregion

                #region 解析 - 影人中英文
                reg = new Regex(@"<title>([\s\S]+?)</title>", regOptions);
                filmMan.CnName = reg.Match(pageHtml).Groups[1].ToString().Trim();
                filmMan.CnName = filmMan.CnName.Replace("（", "(").Replace("）", ")").Replace("(豆瓣)", "").Trim();
                reg = new Regex(@"", regOptions);

                reg = new Regex(@"<h1>([\s\S]+?)</h1>", regOptions);
                filmMan.EnName = reg.Match(filmManContent).Groups[1].ToString().Trim();
                filmMan.EnName = filmMan.EnName.Replace(filmMan.CnName, "").Trim();

                _spiderTaskService.AddLog(taskId, string.Format("解析 - 影人中文名【{0}】，英文名【{1}】", filmMan.CnName, filmMan.EnName));

                #endregion


                var fm = _filmManService.Get(filmMan.CnName);
                if (fm != null)
                {
                    #region 是否已存在影人，则返回
                    _spiderTaskService.AddLog(taskId, "影人【" + filmMan.CnName + "】已存在，则跳过！！！");
                    //更新完成任务时间
                    //task.Finished = true;
                    //task.FinishedTime = DateTime.Now;
                    //_spiderTaskService.UpdateTask(task);
                    return fm.Id;
                    #endregion
                }
                else
                {
                    #region 不存在影人，则添加
                    filmMan.DoubanLink = collectLink;
                    filmMan.Id = _filmManService.Insert(filmMan); //创建一个影人
                    //AddLink(2, filmMan.CnName + " " + filmMan.EnName, filmMan.Id, collectLink); //添加采集链接到库中

                    #region 影人信息
                    //头像
                    reg = new Regex(@"<div id=""headline"" class=""item"">([\s\S]+?)<span class=""gact"">");
                    string avatarHtml = reg.Match(filmManContent).Groups[0].ToString();
                    reg = new Regex(@"<a class=""nbg""[\s\S]*href=""([\s\S]+?)"">");
                    string avatarLargeUrl = reg.Match(avatarHtml).Groups[1].ToString();

                    if (!string.IsNullOrEmpty(avatarLargeUrl) && !avatarLargeUrl.Contains("avatar"))
                    {
                        #region 上传影人头像
                        try
                        {
                            //上传到图片应用【因为采集到的为缩略图，把缩略图替换成源图，当前是600比例内的】
                            var imgBytes = client.DownloadData(avatarLargeUrl);
                            var contentType = MimeMapping.GetMimeMapping(avatarLargeUrl);
                            var fileName = System.IO.Path.GetFileName(avatarLargeUrl);
                            var response = pictureService.Upload(fileName, imgBytes, contentType, imgBytes.Length);
                            if (response.IsSuccess() && response.Results != null)
                            {
                                filmMan.AvatarId = response.Results.PictureId;
                                task.ImageCount++;
                                _spiderTaskService.UpdateTask(task);
                            }


                            _spiderTaskService.AddLog(taskId, "休息 5 秒，影人上传头像");
                            Thread.Sleep(8000);//【上传1张图，休息5秒】
                        #endregion
                        }
                        catch (Exception ex) { }
                    }

                    //个人信息
                    reg = new Regex(@" <ul class="""">([\s\S]+?)</ul>", regOptions);
                    string infoHtml = reg.Match(filmManContent).Groups[0].ToString();
                    reg = new Regex(@"<li>([\s\S]+?)</li>", regOptions);
                    MatchCollection infoMatches = reg.Matches(infoHtml);
                    foreach (Match mInfo in infoMatches)
                    {
                        string s = mInfo.Groups[0].ToString().Replace("<li>", "").Replace("</li>", "").Replace("<span>", "").Replace("</span>", "").Trim();
                        string[] item = s.Replace("：", ":").Split(':');
                        if (item.Length == 2)
                        {
                            string value = item[1].Trim();
                            switch (item[0])
                            {
                                case "性别":
                                    filmMan.Gender = value == "男" ? 1 : (value == "女" ? 0 : -1);
                                    break;
                                case "星座":
                                    filmMan.Constellation = value;
                                    break;
                                case "出生日期":
                                    filmMan.Birthday = value;
                                    break;
                                case "生卒日期":
                                    var list = value.Split('至');
                                    if (list.Length == 2)
                                    {
                                        filmMan.Birthday = list[0].Trim();
                                        filmMan.Deadday = list[1].Trim();
                                    }
                                    break;
                                case "出生地":
                                    filmMan.PlaceOfBirth = value.Replace(" ", "").Replace("，", ",");
                                    break;
                                case "职业":
                                    filmMan.Job = value;
                                    string jobValue = "";
                                    var vlist = filmMan.Job.Split('/');
                                    if (vlist.Length != 0)
                                    {
                                        for (int i = 0; i < vlist.Length; i++)
                                        {
                                            if (vlist[i] != "")
                                            {
                                                jobValue += vlist[i].ToString().Trim() + ",";
                                            }
                                        }
                                    }

                                    if (jobValue != "")
                                        jobValue = jobValue.Remove(jobValue.Length - 1);
                                    filmMan.Job = jobValue;

                                    break;
                                case "更多外文名":
                                    filmMan.MoreEnName = value;
                                    string moreEnNameValue = "";
                                    var vlist2 = filmMan.MoreEnName.Split('/');
                                    if (vlist2.Length != 0)
                                    {
                                        for (int i = 0; i < vlist2.Length; i++)
                                        {
                                            if (vlist2[i] != "")
                                            {
                                                moreEnNameValue += vlist2[i].ToString().Trim() + ",";
                                            }
                                        }
                                    }
                                    if (moreEnNameValue != "")
                                        moreEnNameValue = moreEnNameValue.Remove(moreEnNameValue.Length - 1);
                                    filmMan.MoreEnName = moreEnNameValue;
                                    break;
                                case "更多中文名":
                                    filmMan.MoreCnName = value;
                                    string moreCnNameValue = "";
                                    var vlist3 = filmMan.MoreCnName.Split('/');
                                    if (vlist3.Length != 0)
                                    {
                                        for (int i = 0; i < vlist3.Length; i++)
                                        {
                                            if (vlist3[i] != "")
                                            {
                                                moreCnNameValue += vlist3[i].ToString().Trim() + ",";
                                            }
                                        }
                                    }
                                    if (moreCnNameValue != "")
                                        moreCnNameValue = moreCnNameValue.Remove(moreCnNameValue.Length - 1);
                                    filmMan.MoreCnName = moreCnNameValue;
                                    break;
                                case "家庭成员":
                                    filmMan.FamilyInfo = value;
                                    break;
                            }
                        }
                    }
                    #endregion

                    #region 影人简介
                    reg = new Regex(@"<div class=""bd"">([\s\S]+?)</div>", regOptions);
                    string introHtml = reg.Match(filmManContent).Groups[1].ToString();
                    if (!string.IsNullOrEmpty(introHtml) && introHtml.Contains("<span class=\"all hidden\">"))
                    {
                        //包含更多简介
                        reg = new Regex(@"<span class=""all hidden"">([\s\S]+?)</span>", regOptions);
                        filmMan.Introduction = reg.Match(introHtml).Groups[1].ToString();
                    }
                    else
                    {
                        filmMan.Introduction = introHtml;
                    }
                    #endregion

                    _spiderTaskService.AddLog(taskId, "更新影人信息、简介");
                    _filmManService.Update(filmMan);

                    #region 影人图片
                    string photoLink = collectLink + "photos/"; //reg.Match(movieContent).Groups[1].ToString();

                    int photoCount = 0; //影人图片数
                    reg = new Regex(@"影人图片([\s\S]+?)上传照片", regOptions);
                    string photoHtml = reg.Match(filmManContent).Groups[0].ToString();
                    reg = new Regex(@"target=""_self"">([\S\s]+?)</a>", regOptions);
                    photoCount = Convert.ToInt32(reg.Match(photoHtml).Groups[1].ToString().Replace("全部", "").Replace("张", "").Trim());

                    _spiderTaskService.AddLog(taskId, "解析 - 影人图片链接【" + photoLink + "】，共有图片【" + photoCount + "】张");

                    if (photoCount > 0)
                    {
                        pageBytes = client.DownloadData(photoLink);
                        pageHtml = Encoding.GetEncoding("utf-8").GetString(pageBytes);


                        reg = new Regex(@"<ul class=""poster-col4 clearfix"">([\s\S]+?)</ul>", regOptions);
                        var results = reg.Match(pageHtml).Groups[0].ToString();

                        //获取影人图片Urls，格式例子：http;//douban.com/thumb1.jpg,http;//douban.com/thumb2.jpg
                        reg = new Regex(@"<img src=""([\s\S]+?)"" class="""" />", regOptions);
                        var imgSrcMatches = reg.Matches(results);

                        if (imgSrcMatches.Count > 0)
                        {
                            _spiderTaskService.AddLog(taskId, "已解析影人图片首页图片链接【" + imgSrcMatches.Count + "】张");
                            int imgIndex = 1;
                            foreach (Match m in imgSrcMatches)
                            {
                                #region 上传到图片应用【因为采集到的为缩略图，把缩略图替换成源图，当前是600比例内的】
                                string imgThumbUrl = m.Groups[1].ToString();
                                string imgUrl = imgThumbUrl.Replace("thumb", "photo");

                                //上传到图片应用
                                var imgBytes = client.DownloadData(imgUrl);
                                var contentType = MimeMapping.GetMimeMapping(imgUrl);
                                var fileName = System.IO.Path.GetFileName(imgUrl);
                                var response = pictureService.Upload(fileName, imgBytes, contentType, imgBytes.Length);
                                if (response.IsSuccess() && response.Results != null)
                                {
                                    //更新数据库
                                    FilmManPhoto photo = new FilmManPhoto();
                                    photo.FilmManId = filmMan.Id;
                                    photo.PictureId = response.Results.PictureId;
                                    photo.Description = string.Empty;
                                    _filmManPhotoService.Insert(photo);

                                    task.ImageCount++;
                                    _spiderTaskService.UpdateTask(task);
                                }
                                _spiderTaskService.AddLog(taskId, "休息 5 秒，已解析影人【" + filmMan.CnName + "】图片开采第 " + imgIndex + " 张");
                                Thread.Sleep(8000);
                                imgIndex++;
                                #endregion
                            }
                        }
                    }
                    else
                    {
                        _spiderTaskService.AddLog(taskId, "影人图片 0 张，则跳过！！！");
                    }
                    #endregion

                    //更新完成任务时间
                    //task.Finished = true;
                    //task.Spidering = false;
                    //task.FinishedTime = DateTime.Now;
                    //_spiderTaskService.UpdateTask(task);
                    return filmMan.Id;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                LogHelper.Logger.Error(ex, "【" + movie.Id + ":" + movie.CnName + "】【" + collectLink + "】" + ex.Message);
                return 0;
            }
        }
        #endregion
    }
}
