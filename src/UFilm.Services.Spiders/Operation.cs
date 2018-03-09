using System;
using OpenQA.Selenium;

namespace UFilm.Services.Spiders
{
    public class Operation
    {
        public int Timeout { get; set; }

        public Action<IWebDriver> Action { get; set; }

        public Func<IWebDriver, bool> Condition { get; set; }

    }
}
