using System;
using OpenQA.Selenium;

namespace UFilm.Services.Spiders.Events
{
    
    public class OnStartEventArgs
    {
        public Uri Uri { get; private set; }
        public IWebDriver WebDriver { get; private set; }

        public OnStartEventArgs(Uri uri, IWebDriver driver)
        {
            this.Uri = uri;
            this.WebDriver = driver;
        }
    }
}
