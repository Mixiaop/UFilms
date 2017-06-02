using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;
using UFilm.Domain.Movies;

namespace UFilm.Console.Spiders.Douban
{
    public partial class Test : UZeroConsole.Web.AuthPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnMovieTests.Click += btnMovieTests_Click;
            btnFilmManTests.Click += btnFilmManTests_Click;
        }

        void btnFilmManTests_Click(object sender, EventArgs e)
        {
            #region 影人
            string spiderLink = tbLinks.Text.Trim();
            if (string.IsNullOrEmpty(spiderLink))
            {
                ltlMessage.Text = AlertError("链接不能为空");
                return;
            }
            if (!spiderLink.EndsWith("/"))
                spiderLink = spiderLink + "/";

            if (spiderLink.IndexOf("https://movie.douban.com/celebrity/") == -1 && spiderLink.IndexOf("http://movie.douban.com/celebrity/") == -1)
            {
                ltlMessage.Text = AlertError("链接格式有误(http://movie.douban.com/celebrity/)");
                return;
            }


            RegexOptions regOptions = RegexOptions.None;
            byte[] pageBytes;
            string pageHtml = ""; //页面数据
            Regex reg; //正则
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.56 Safari/537.17");

            #region 影人页采集测试
            #region 解析 - 影人页
            FilmMan filmMan = new FilmMan();
            pageBytes = client.DownloadData(spiderLink);
            pageHtml = Encoding.GetEncoding("utf-8").GetString(pageBytes);

            //解析影人内容区
            reg = new Regex(@"<div id=""content"">([\s\S]+?)上传照片", regOptions);
            var filmManContent = reg.Match(pageHtml).Groups[0].ToString();
            #endregion

            #region 影人中英文
            reg = new Regex(@"<title>([\s\S]+?)</title>", regOptions);
            filmMan.CnName = reg.Match(pageHtml).Groups[1].ToString().Trim();
            filmMan.CnName = filmMan.CnName.Replace("（", "(").Replace("）", ")").Replace("(豆瓣)", "").Trim();
            reg = new Regex(@"", regOptions);

            reg = new Regex(@"<h1>([\s\S]+?)</h1>", regOptions);
            filmMan.EnName = reg.Match(filmManContent).Groups[1].ToString().Trim();
            filmMan.EnName = filmMan.EnName.Replace(filmMan.CnName, "").Trim();

            Log("影人中文名", filmMan.CnName);
            Log("影人英文名", filmMan.EnName);
            #endregion

            #region 影人信息
            //头像
            reg = new Regex(@"<div id=""headline"" class=""item"">([\s\S]+?)<span class=""gact"">");
            string avatarHtml = reg.Match(filmManContent).Groups[0].ToString();
            reg = new Regex(@"<a class=""nbg""[\s\S]*href=""([\s\S]+?)"">");
            string avatarLargeUrl = reg.Match(avatarHtml).Groups[1].ToString();

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

            Log("头像链接", avatarLargeUrl);
            Log("姓别", (filmMan.Gender == 1 ? "男" : (filmMan.Gender == 0 ? "女" : "")));
            Log("星座", filmMan.Constellation);
            Log("出生日期", filmMan.Birthday);
            Log("生卒日期", filmMan.Deadday);
            Log("出生地", filmMan.PlaceOfBirth);
            Log("职业", filmMan.Job);
            Log("更多外文名", filmMan.MoreEnName);
            Log("更多中文名", filmMan.MoreCnName);
            Log("家庭成员", filmMan.FamilyInfo);
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
            Log("简介", filmMan.Introduction, 4);
            #endregion

            #region 影人图片
            string photoLink = spiderLink + "photos/"; //reg.Match(movieContent).Groups[1].ToString();

            Log("影人图片链接", photoLink);

            int photoCount = 0; //影人图片数
            reg = new Regex(@"影人图片([\s\S]+?)上传照片", regOptions);
            string photoHtml = reg.Match(filmManContent).Groups[0].ToString();
            reg = new Regex(@"target=""_self"">([\S\s]+?)</a>", regOptions);
            photoCount = Convert.ToInt32(reg.Match(photoHtml).Groups[1].ToString().Replace("全部", "").Replace("张", "").Trim());

            Log("影人图片", "共" + photoCount + "张");

            Thread.Sleep(1000);

            pageBytes = client.DownloadData(photoLink);
            pageHtml = Encoding.GetEncoding("utf-8").GetString(pageBytes);
            reg = new Regex(@"<ul class=""poster-col4 clearfix"">([\s\S]+?)</ul>", regOptions);
            var results = reg.Match(pageHtml).Groups[0].ToString();

            //获取影人图片Urls，格式例子：http;//douban.com/thumb1.jpg,http;//douban.com/thumb2.jpg
            reg = new Regex(@"<img src=""([\s\S]+?)"" class="""" />", regOptions);
            var imgSrcMatches = reg.Matches(results);
            string photoImageUrls = "";
            if (imgSrcMatches.Count > 0)
            {
                foreach (Match m in imgSrcMatches)
                {
                    //上传到图片应用【因为采集到的为缩略图，把缩略图替换成源图，当前是600比例内的】
                    string imgThumbUrl = m.Groups[1].ToString();
                    string imgUrl = imgThumbUrl.Replace("thumb", "photo");
                    photoImageUrls += imgThumbUrl + ",";
                }

                Log("影人图片URL列表", photoImageUrls, 3);
            }
            #endregion

            #endregion
            #endregion
        }

        void btnMovieTests_Click(object sender, EventArgs e)
        {
            #region 电影
            string spiderLink = tbLinks.Text.Trim();
            if (string.IsNullOrEmpty(spiderLink))
            {
                ltlMessage.Text = AlertError("链接不能为空");
                return;
            }
            if (!spiderLink.EndsWith("/"))
                spiderLink = spiderLink + "/";

            RegexOptions regOptions = RegexOptions.None;
            byte[] pageBytes;
            string pageHtml = ""; //页面数据
            Regex reg; //正则
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1312.56 Safari/537.17");

            #region 电影页采集测试

            #region 解析 - 电影页
            Movie movie = new Movie();
            pageBytes = client.DownloadData(spiderLink);
            pageHtml = Encoding.GetEncoding("utf-8").GetString(pageBytes);

            //解析电影内容区
            reg = new Regex(@"<div id=""wrapper""[\s\t\r\n""'<>]*[^<>]*?>([\s\S]+?)<div id=""related-pic"" class=""related-pic"">", regOptions);
            var movieContent = reg.Match(pageHtml).Groups[0].ToString();
            #endregion

            #region 解析 - 电影名称 & 年份
            //中文名
            reg = new Regex(@"<title>([\s\S]+?)</title>", regOptions);
            movie.CnName = reg.Match(pageHtml).Groups[1].ToString().Trim();
            movie.CnName = movie.CnName.Replace("（", "(").Replace("）", ")").Replace("(豆瓣)", "").Trim();

            //reg = new Regex(@"<h2>([\s\S]+?)的剧情简介", regOptions);
            //movie.CnName = reg.Match(movieContent).Groups[1].ToString();
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

            Log("电影年份", (movie.Year.ToString()));
            Log("电影中文名", (movie.CnName));
            Log("电影英文名", (movie.EnName));
            #endregion

            //影人与电影信息解析的都是同一块内容
            reg = new Regex(@"<div id=""info"">([\s\S]+?)</div>", regOptions);
            var infoContent = reg.Match(movieContent).Groups[0].ToString();
            reg = new Regex(@"<span([\s\S]+?)<br/>", regOptions);
            MatchCollection mInfoItems = reg.Matches(infoContent); //电影每项信息

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
                        foreach (Match fmInfoMatch in fmListMatches)
                        {
                            string manUrl = fmLinkReg.Match(fmInfoMatch.Groups[0].ToString()).Groups[1].ToString();
                            string fmName = fmInfoMatch.Groups[1].ToString();
                            switch (manType)
                            {
                                case "导演":
                                    director += fmName + "(http://movie.douban.com" + manUrl + "),";
                                    break;
                                case "编剧":
                                    bianJ += fmName + "(http://movie.douban.com" + manUrl + "),";
                                    break;
                                case "主演":
                                    actor += fmName + "(http://movie.douban.com" + manUrl + "),";
                                    break;
                            }
                        }
                    }
                }
            }

            Log("导演", director);
            Log("编剧", bianJ);
            Log("演员", actor);
            #endregion

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

                                Log("电影类型", movie.MovieType);
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

                                Log("电影地区", movie.Area);
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

                                Log("电影语言", movie.Language);
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

                                Log("电影上映日期", movie.OtherPostYear);
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

                                Log("电影片长", movie.MovieLength);
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

                                Log("电影又名", movie.OtherEnName);
                                break;
                            case "季数":
                                movie.IsSeries = 1;

                                reg = new Regex(@"selected=selected >([\s\S]+?)</option>", regOptions);
                                movie.SeriesNumber = reg.Match(mInfo.Groups[0].ToString()).Groups[1].ToString().Trim().ToInt();
                                Log("电影季数", movie.SeriesNumber.ToString());
                                break;
                            case "集数":
                                movie.IsSeries = 1;

                                reg = new Regex(@"</span>([\s\S]+?)<br/>", regOptions);
                                movie.SeriesCount = reg.Match(mInfo.Groups[0].ToString()).Groups[1].ToString().Trim().ToInt();
                                Log("电影集数", movie.SeriesCount.ToString());
                                break;
                            case "单集片长":
                                movie.IsSeries = 1;

                                reg = new Regex(@"</span>([\s\S]+?)<br/>", regOptions);
                                movie.SeriesLength = reg.Match(mInfo.Groups[0].ToString()).Groups[1].ToString().Trim();
                                Log("电影单集片长", movie.SeriesLength);
                                break;
                            case  "官方网站":
                                reg = new Regex(@"_blank"">([\s\S]+?)</a>", regOptions);
                                movie.WebSite = reg.Match(mInfo.Groups[0].ToString()).Groups[1].ToString().Trim();
                                Log("官方网站", movie.WebSite);
                                break;
                        }
                    }
                }
            }

            //IMDB编号
            reg = new Regex(@"rel=""nofollow"">([\s\S]+?)</a>", regOptions);
            movie.ImdbCode = reg.Match(infoContent).Groups[1].ToString().Trim();
            Log("IMDB编号", movie.ImdbCode);

            //豆瓣评分
            reg = new Regex(@"<strong class=""ll rating_num"" property=""v:average"">([\s\S]+?)</strong>", regOptions);
            movie.DoubanRating = reg.Match(pageHtml).Groups[1].ToString().Trim();
            if (movie.DoubanRating.Length > 5)
            {
                Log("豆瓣评分", "");
            }
            else {
                Log("豆瓣评分", movie.DoubanRating);
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

            Log("电影简介", movie.Introduction, 4);
            #endregion
            #endregion

            #region 解析 - 电影封面 & 剧照
            //封面链接
            reg = new Regex(@"<a class=""nbgnbg"" href=""([\s\S]+?)"" title=""点击看更多海报"">", regOptions);
            string moviePosterLink = spiderLink + "photos?type=R"; //reg.Match(movieContent).Groups[1].ToString();

            Log("电影海报列表链接", moviePosterLink);

            Thread.Sleep(1000);//【休息几秒】
            pageBytes = client.DownloadData(moviePosterLink);
            pageHtml = Encoding.GetEncoding("utf-8").GetString(pageBytes);
            reg = new Regex(@"<ul class=""poster-col4 clearfix""[\s\t\r\n""'<>]*[^<>]*?>([\s\S]+?)</ul>", regOptions);
            string posterContent = reg.Match(pageHtml).ToString();


            //获取封面图片Urls，格式例子：http;//douban.com/thumb1.jpg,http;//douban.com/thumb2.jpg
            reg = new Regex(@"<img src=""([\s\S]+?)"" />[\s\t\r\n""'<>]*</a>", regOptions);
            var posterImagesMatches = reg.Matches(posterContent);
            if (posterImagesMatches.Count > 0)
            {
                string posterImageUrls = "";
                foreach (Match posterImage in posterImagesMatches)
                {
                    posterImageUrls += posterImage.Groups[1].ToString() + "，";
                }
                Log("电影海报图片URL列表", posterImageUrls, 3);
            }
            else {
                //海报列表 = 0，拿默认的
                reg = new Regex(@"<a class=""nbgnbg"" href=""([\s\S]+?)"" title=""点击看大图"">", regOptions);
                string defaultPoster = reg.Match(movieContent).Groups[1].ToString();
                Log("海报列表 = 0，拿默认海报", defaultPoster);
            }
            //电影图片
            string moviePhotosLink = spiderLink + "photos?type=S";
            Log("电影图片列表链接", moviePhotosLink);

            Thread.Sleep(1000);//【休息几秒】
            pageBytes = client.DownloadData(moviePhotosLink);
            pageHtml = Encoding.GetEncoding("utf-8").GetString(pageBytes);
            reg = new Regex(@"<ul class=""poster-col4 clearfix""[a-z\s\S\t\r\n""']*>([\s\S]+?)</ul>", regOptions);
            var photoContent = reg.Match(pageHtml).Groups[0].ToString();
            reg = new Regex(@"<img src=""([\s\S]+?)"" />", regOptions);
            var photoImagesMatch = reg.Matches(photoContent);
            if (photoImagesMatch.Count > 0)
            {
                string photoImagesUrls = "";
                foreach (Match photoImage in photoImagesMatch)
                {
                    photoImagesUrls += photoImage.Groups[1].ToString() + "，";
                }

                Log("电影图片URL列表", photoImagesUrls, 3);
            }
            #endregion
            #endregion
            #endregion
        }

        /// <summary>
        /// 将结果输出到页面
        /// </summary>
        /// <param name="name">业务名称</param>
        /// <param name="value">业务值</param>
        /// <param name="br">多少个br</param>
        void Log(string name, string value, int br = 1)
        {

            string result = "解析---" + name + "：";
            result += "<div>" + value + "</div>";
            for (int i = 0; i < br; i++)
            {
                result += "<br />";
            }

            Response.Write(result);
        }
    }
}