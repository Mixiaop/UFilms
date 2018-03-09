using OpenQA.Selenium;
using System;

namespace UFilm.Services.Spiders.Events
{
    public class OnCompletedEventArgs
    {
        public Uri Uri { get; private set; }

        public int ThreadId { get; private set; }

        public string PageSource { get; private set; }
        public IWebDriver WebDriver { get;private set; }

        public long Milliseconds { get; private set; }

        public OnCompletedEventArgs(Uri uri, int threadId, long milliseconds, string pageSource, IWebDriver driver)
        {
            this.Uri = uri;
            this.ThreadId = threadId;
            this.Milliseconds = milliseconds;
            this.PageSource = pageSource;
            this.WebDriver = driver;
        }
    }
}
