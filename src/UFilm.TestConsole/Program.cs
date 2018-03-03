using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using UFilm.Services.Spiders;

namespace UFilm.TestConsole
{
    class Program
    {
        static Settings settings = GetSettings();
        static void Main(string[] args)
        {
            //link
            //SpliderLinks(settings.SpiderLinkCurrentPage);

            //movie
            SpiderMovie(settings.SpiderMovieCurrentPage, true);

            #region 登录测试
            //var spider = new Spider();
            //spider.OnStart += (s, e) => {
            //    //先登录
            //    string loginUrl = "https://www.beatricee.com/usercenter/login.aspx";
            //    e.WebDriver.Navigate().GoToUrl(loginUrl.ToString());
            //    e.WebDriver.FindElement(By.Id("userName")).SendKeys("lisimple@126.com");
            //    e.WebDriver.FindElement(By.Id("userPassword")).SendKeys("123123");
            //    e.WebDriver.FindElement(By.Id("primary")).FindElement(By.ClassName("btn-identification-4")).Click();
            //};
            //spider.OnCompleted += (s, e) =>
            //{
            //    File.WriteAllText(@"D:\!Github\UFilms\src\UFilm.TestConsole\bin\Debug\f1.txt", e.PageSource);

            //};

            //spider.Start(new Uri("https://www.beatricee.com/"));
            #endregion

            Console.ReadLine();
        }

        #region Beatricee
        /// <summary>
        /// 采链接
        /// </summary>
        /// <param name="page"></param>
        static void SpliderLinks(int page)
        {
            var settings = GetSettings();
            var spiderUrl = "https://www.beatricee.com/default.aspx?page=" + page;
            var spider = new Spider();
            spider.OnStart += (s, e) =>
            {
                Console.WriteLine("Spider OnStart：" + e.Uri.ToString());
            };
            spider.OnError += (s, e) =>
            {
                Console.WriteLine("Spider OnError：" + e.Uri.ToString() + "，异常消息：" + e.Exception.ToString());
            };
            spider.OnCompleted += (s, e) =>
            {
                Console.WriteLine("Spider OnCompleted：" + e.Uri.ToString());

                var pageHtml = e.PageSource;
                RegexOptions regOption = RegexOptions.None;
                Regex reg = new Regex(@"<div class=""moviecontent span10"">([\s\S]+?)</h2>", regOption);
                List<string> movieLinks = new List<string>();
                var matchLinks = reg.Matches(pageHtml); //列表项

                foreach (Match m in matchLinks)
                {
                    var item = m.Groups[1].ToString();
                    if (!string.IsNullOrEmpty(item))
                    {
                        reg = new Regex(@"<a href=""([\s\S]+?)"" title=");
                        var link = reg.Match(item);
                        if (link.Groups.Count >= 2)
                        {
                            movieLinks.Add(link.Groups[1].ToString());
                        }
                        //Console.WriteLine(link.Groups[1].ToString());
                    }
                }


                var filePath = settings.SpiderLinkSavePath + page + ".txt"; //保存的文件
                var fileContent = "";
                foreach (var link in movieLinks)
                {
                    fileContent += "https://www.beatricee.com" + link + "\r\n";
                }
                File.WriteAllText(filePath, fileContent);

                KillSpiderProcess();
                SpliderLinks(++page);
            };

            var operation = new Operation();

            if (page <= 9576)
            {
                spider.Start(new Uri(spiderUrl));
                Console.ReadLine();
            }
        }

        /// <summary>
        /// 采内页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="isFirst"></param>
        static void SpiderMovie(int page, bool isFirst = false)
        {
            if (isFirst)
                Console.WriteLine(string.Format("开始解析第 {0} 页-----------", page));

            string filePath = settings.SpiderLinkSavePath; //链接路径
            string saveFilePath = settings.SpiderMovieSavePath; //采集到并保存的路径

            saveFilePath += page;

            var pageTxtPath = filePath + page + ".txt";
            if (File.Exists(pageTxtPath))
            {
                if (!Directory.Exists(saveFilePath))
                {
                    Directory.CreateDirectory(saveFilePath);
                }
                bool allCompleted = true;
                string currentLink = "";
                bool currentComplted = false;
                bool throwException = false;
                using (StreamReader sr = File.OpenText(pageTxtPath))
                {
                    var index = 1;
                    while ((currentLink = sr.ReadLine()) != null)
                    {
                        currentLink = currentLink.Trim();
                        if (!currentLink.Contains("$completed"))
                        {
                            //未完成
                            Console.WriteLine(string.Format("第 {0} 页 第 {1} 条 - {2} 开始采集", page, index, currentLink));

                            #region 开始采集
                            var spider = new Spider();
                            spider.OnStart += (s, e) =>
                            {
                                try
                                {
                                    //先登录
                                    string loginUrl = "https://www.beatricee.com/usercenter/login.aspx";
                                    e.WebDriver.Navigate().GoToUrl(loginUrl.ToString());
                                    e.WebDriver.FindElement(By.Id("userName")).SendKeys("lisimple@126.com");
                                    e.WebDriver.FindElement(By.Id("userPassword")).SendKeys("123123");
                                    e.WebDriver.FindElement(By.Id("primary")).FindElement(By.ClassName("btn-identification-4")).Click();
                                    Console.WriteLine("-------------------------------------------- 登录成功");
                                }
                                catch (Exception ex)
                                {
                                    //登录失败
                                    throwException = true;
                                }
                            };
                            spider.OnError += (s, e) =>
                            {
                                throwException = true;
                            };
                            spider.OnCompleted += (s, e) =>
                            {
                                Regex reg;
                                RegexOptions regOption = RegexOptions.None;
                                WebClient client = new WebClient();
                                #region 获取首页分析并存储内容
                                string domain = "https://www.beatricee.com";
                                var pageHtml = e.PageSource;
                                try
                                {
                                    Console.WriteLine("-------------------------------------------- 开始解析数据");
                                    string code = currentLink.Split('/')[currentLink.Split('/').Length - 1];
                                    saveFilePath += "\\" + code;
                                    if (!Directory.Exists(saveFilePath))
                                    {
                                        Directory.CreateDirectory(saveFilePath);
                                        Console.WriteLine("-------------------------------------------- 创建文件夹（番号）");
                                    }
                                    //保存HTML
                                    File.WriteAllText(saveFilePath + "\\" + "html.txt", e.PageSource);
                                    Console.WriteLine("-------------------------------------------- 保存html");

                                    #region 保存封面
                                    //大图
                                    reg = new Regex(@"<img class=""details_cover attachment-section_portfolio"" src=""([\s\S]+?)"" alt=", regOption);

                                    string posterUrl = reg.Match(pageHtml).Groups[1].ToString();
                                    if (!string.IsNullOrEmpty(posterUrl))
                                    {
                                        Console.WriteLine("-------------------------------------------- 正在下载封面");
                                        //保存大图
                                        var imgBytes = client.DownloadData(posterUrl);
                                        FileStream fs = new FileStream(saveFilePath + "\\" + "poster.jpg", FileMode.Create);
                                        BinaryWriter bw = new BinaryWriter(fs);
                                        bw.Write(imgBytes, 0, imgBytes.Length);
                                        fs.Flush();
                                        fs.Close();
                                        Console.WriteLine("-------------------------------------------- 封面（大）已下载完成");

                                        //保存小图
                                        var imgBytes2 = client.DownloadData(posterUrl.Replace("pl.", "ps."));
                                        FileStream fs2 = new FileStream(saveFilePath + "\\" + "poster_s.jpg", FileMode.Create);
                                        BinaryWriter bw2 = new BinaryWriter(fs2);
                                        bw2.Write(imgBytes2, 0, imgBytes2.Length);
                                        fs2.Flush();
                                        fs2.Close();
                                        Console.WriteLine("-------------------------------------------- 封面（小）已下载完成");
                                    }


                                    #endregion

                                    #region 保存资源链接
                                    if (pageHtml.Contains("下载资源: <strong style=\"color: green;\">0</strong>"))
                                    {
                                        File.WriteAllText(saveFilePath + "\\" + "torrents-0.txt", "");
                                        Console.WriteLine("-------------------------------------------- 下载资源：0");
                                    }
                                    #endregion

                                    #region 保存影人列表
                                    reg = new Regex(@"<ul class=""detailsactorface"">([\s\S]+?)</ul>");
                                    var persons = reg.Matches(pageHtml);
                                    if (persons.Count > 0)
                                    {
                                        Console.WriteLine("-------------------------------------------- 正在下载影人缩略图");
                                        foreach (Match m in persons)
                                        {
                                            var aHtml = m.Groups[1].ToString();
                                            var imgUrl = new Regex(@"data-original=""([\s\S]+?)"" alt=").Match(aHtml).Groups[1].ToString();
                                            if (!string.IsNullOrEmpty(imgUrl))
                                            {
                                                var title = new Regex(@"alt=""([\s\S]+?)"" title=").Match(aHtml).Groups[1].ToString();
                                                var imgBytes = client.DownloadData(imgUrl);
                                                FileStream fs = new FileStream(saveFilePath + "\\" + "person-" + title + ".jpg", FileMode.Create);
                                                BinaryWriter bw = new BinaryWriter(fs);
                                                bw.Write(imgBytes, 0, imgBytes.Length);
                                                fs.Flush();
                                                fs.Close();
                                            }
                                        }
                                        Console.WriteLine("-------------------------------------------- 影人缩略图下载完成");
                                    }
                                    #endregion

                                    #region 保存影人图片
                                    reg = new Regex(@"<div class=""imagesList"">([\s\S]+?)</div>", regOption);
                                    var imgListHtml = reg.Match(pageHtml);
                                    if (!string.IsNullOrEmpty(imgListHtml.Groups[1].ToString()))
                                    {
                                        reg = new Regex(@"<li>([\s\S]+?)</li>", regOption);
                                        var liList = reg.Matches(imgListHtml.Groups[1].ToString());
                                        if (liList.Count > 0)
                                        {
                                            Console.WriteLine("-------------------------------------------- 正在下载图片列表");
                                            var imgIndex = 1;
                                            foreach (Match li in liList)
                                            {
                                                var imgUrl = new Regex(@"<img src=""([\s\S]+?)"" alt=").Match(li.Groups[1].ToString()).Groups[1].ToString();
                                                if (!string.IsNullOrEmpty(imgUrl))
                                                {
                                                    var imgBytes = client.DownloadData(imgUrl);
                                                    FileStream fs = new FileStream(saveFilePath + "\\" + "img-" + imgIndex + ".jpg", FileMode.Create);
                                                    BinaryWriter bw = new BinaryWriter(fs);
                                                    bw.Write(imgBytes, 0, imgBytes.Length);
                                                    fs.Flush();
                                                    fs.Close();
                                                }
                                                imgIndex++;
                                            }
                                            Console.WriteLine("-------------------------------------------- 图片列表 " + imgIndex + " 下载完成");
                                        }
                                    }
                                    #endregion

                                    //File.WriteAllText(@"D:\!Github\UFilms\src\UFilm.TestConsole\bin\Debug\f1.txt", e.PageSource);
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception(ex.Message);
                                }
                                #endregion
                            };

                            spider.Start(new Uri(currentLink));
                            currentComplted = true;
                            KillSpiderProcess();
                            Console.WriteLine(string.Format("-------------------------------------------- 采集完成", page, index));
                            Console.WriteLine();
                            break;
                            #endregion
                        }
                        //else {
                        //    Console.WriteLine(string.Format("第 {0} 页 第 {1} 条 - {2} 已完成则跳过", page, index, currentLink));
                        //    Thread.Sleep(1000);
                        //}

                        index++;
                    }
                }

                if (throwException)
                {
                    Console.WriteLine("-------------------------------------------- 出错了重新来");
                    //出错了重新来
                    SpiderMovie(page, false);
                }
                else
                {
                    //标记已完成
                    if (currentComplted)
                    {
                        string text = File.ReadAllText(pageTxtPath);
                        string result = text.Replace(currentLink, currentLink + "$completed");
                        File.WriteAllText(pageTxtPath, result);
                    }

                    //检查是否全部完成
                    using (StreamReader sr = File.OpenText(pageTxtPath))
                    {
                        while ((currentLink = sr.ReadLine()) != null)
                        {
                            if (!currentLink.Contains("$completed"))
                            {
                                allCompleted = false;
                            }
                        }
                    }

                    if (allCompleted)
                    {
                        Console.WriteLine(string.Format("第 {0} 页都已完成，执行下一页", page));
                        Console.WriteLine();
                        isFirst = true;
                        page++;
                    }
                    else
                    {
                        isFirst = false;
                    }


                    Thread.Sleep(2000);
                    SpiderMovie(page, isFirst);
                }
            }

        }

        /// <summary>
        /// 确保干掉采集进程，不然系统内存跑满
        /// </summary>
        static void KillSpiderProcess()
        {
            var process = Process.GetProcessesByName("phantomjs");
            foreach (Process p in process)
            {
                p.Kill();
            }
        }
        #endregion

        #region Settings
        private static Settings GetSettings()
        {
            Settings settings = null;
            var filePath = AppDomain.CurrentDomain.BaseDirectory + @"\Settings.json";
            if (File.Exists(filePath))
            {
                var fileData = File.ReadAllText(filePath);
                var jsonObj = JsonConvert.DeserializeObject(fileData);
                settings = JsonConvert.DeserializeObject<Settings>(jsonObj.ToString());
            }
            else
            {
                //WriteError("出错了：未找到配置文件" + filePath);
            }

            return settings;
        }

        public class Settings
        {
            public int SpiderLinkCurrentPage { get; set; }
            public string SpiderLinkSavePath { get; set; }
            public int SpiderMovieCurrentPage { get; set; }
            public string SpiderMovieSavePath { get; set; }
        }
        #endregion
    }
}
