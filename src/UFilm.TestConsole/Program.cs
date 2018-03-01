using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Text.RegularExpressions;
using UFilm.Services.Spiders;

namespace UFilm.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //link
            //SpliderLinks(261); 

            //movie
            SpiderMovie(1, true);
            Console.ReadLine();
        }

        #region Beatricee
        #region 链接
        static void SpliderLinks(int page)
        {
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


                var filePath = @"D:\WebApps\UFilm.Spiders\Beatricee\Links\" + page + ".txt"; //保存的文件
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
        #endregion

        static void SpiderMovie(int page, bool isFirst = false) {
            if (isFirst)
                Console.WriteLine(string.Format("开始解析第 {0} 页-----------", page));

            string filePath = @"D:\!Github\UFilms\src\UFilm.TestConsole\bin\Debug\beatricee links"; //链接路径
            string saveFilePath = @"D:\!Github\UFilms\src\UFilm.TestConsole\bin\Debug\beatricee movies"; //采集到并保存的路径

            saveFilePath += "\\" + page;

            var pageTxtPath = filePath + "\\" + page + ".txt";
            if (File.Exists(pageTxtPath))
            {
                if (!Directory.Exists(saveFilePath))
                {
                    Directory.CreateDirectory(saveFilePath);
                }
                bool allCompleted = true;
                string currentLink = "";
                bool currentComplted = false;

                using (StreamReader sr = File.OpenText(pageTxtPath)) {
                    var index = 1;
                    while ((currentLink = sr.ReadLine()) != null) {
                        if (!currentLink.Contains("$completed"))
                        {
                            Thread.Sleep(3000);
                            //未完成
                            Console.WriteLine(string.Format("第 {0} 页 第 {1} 条 - {2} 开始采集", page, index, currentLink));

                            #region 开始采集
                            Thread.Sleep(3000);
                            currentComplted = true;
                            Console.WriteLine(string.Format("-------------------------------------------- 采集完成", page, index));
                            Console.WriteLine();
                            break;
                            #endregion
                        }
                        
                        index++;
                    }
                }

                //标记已完成
                if (currentComplted) {
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
                    page++;
                    isFirst = true;
                    Console.WriteLine(string.Format("第 {0} 页都已完成，执行下一页", page));
                    Console.WriteLine();
                }
                else {
                    isFirst = false;
                }

                SpiderMovie(page, isFirst);
            }
            
        }

        /// <summary>
        /// 确保干掉采集进程，不然系统内存跑满
        /// </summary>
        static void KillSpiderProcess() {
            var process = Process.GetProcessesByName("phantomjs");
            foreach (Process p in process)
            {
                p.Kill();
            }
        }
        #endregion

    }
}
