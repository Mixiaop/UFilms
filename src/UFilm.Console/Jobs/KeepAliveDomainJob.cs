using System;
using System.Net;
using System.Collections.Generic;
using U.BackgroundJobs;

namespace UFilm.Console.Jobs
{
    public class KeepAliveDomainJob : BackgroundJob<int>, U.Dependency.ITransientDependency
    {
        public override void Execute(int args)
        {

            var list = new List<string>();
            list.Add("http://www.mbjuan.com");
            list.Add("http://console.mbjuan.com");
            list.Add("http://jobs.mbjuan.com");

            if (list != null)
            {
                foreach (var domain in list)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(domain))
                        {
                            var url = domain.Trim();


                            using (var wc = new WebClient())
                            {
                                wc.DownloadStringAsync(new Uri(url));
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}