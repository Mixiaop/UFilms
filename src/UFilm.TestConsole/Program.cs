using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using UFilm.Services.Spiders;

namespace UFilm.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            BeatriceeSpliderLinks();
        }

        #region Beatricee
        static void BeatriceeSpliderLinks() {
            var spiderUrl = "https://www.beatricee.com/default.aspx?page=1";
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
                Regex reg = new Regex(@"<div class=""moviecontent span10"">([\s\S]+?)</div>", regOption);
                string linkList = reg.Match(pageHtml).Groups[1].ToString();
                Console.WriteLine(linkList);
                
                
            };

            var operation = new Operation();

            spider.Start(new Uri(spiderUrl));
            Console.ReadLine();
        }
        #endregion

    }
}
