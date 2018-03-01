using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using UFilm.Services.Spiders.Events;

namespace UFilm.Services.Spiders
{
    public class Spider : ISpider
    {
        /// <summary>
        /// 蜘蛛启动事件
        /// </summary>
        public event EventHandler<OnStartEventArgs> OnStart;

        /// <summary>
        /// 蜘蛛完成事件
        /// </summary>
        public event EventHandler<OnCompletedEventArgs> OnCompleted;

        /// <summary>
        /// 蜘蛛出错事件
        /// </summary>
        public event EventHandler<OnErrorEventArgs> OnError;

        /// <summary>
        /// 定义PhantomJS内核参数
        /// </summary>
        private PhantomJSOptions _options;

        /// <summary>
        /// 定义Selenium驱动配置
        /// </summary>
        private PhantomJSDriverService _service;

        public Spider(string proxy = null)
        {
            this._options = new PhantomJSOptions();//定义PhantomJS的参数配置对象
            this._service = PhantomJSDriverService.CreateDefaultService(Environment.CurrentDirectory);//初始化Selenium配置，传入存放phantomjs.exe文件的目录
            _service.IgnoreSslErrors = true;//忽略证书错误
            _service.WebSecurity = false;//禁用网页安全
            _service.HideCommandPromptWindow = true;//隐藏弹出窗口
            _service.LoadImages = false;//禁止加载图片
            _service.LocalToRemoteUrlAccess = true;//允许使用本地资源响应远程 URL
            _options.AddAdditionalCapability(@"phantomjs.page.settings.userAgent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36");
            if (proxy != null)
            {
                _service.ProxyType = "HTTP";//使用HTTP代理
                _service.Proxy = proxy;//代理IP及端口
            }
            else
            {
                _service.ProxyType = "none";//不使用代理
            }

        }

        /// <summary>
        /// 高级蜘蛛
        /// </summary>
        /// <param name="uri">抓取地址URL</param>
        /// <param name="script">要执行的Javascript脚本代码</param>
        /// <param name="operation">要执行的页面操作</param>
        /// <returns></returns>
        public void Start(Uri uri, Script script = null, Operation operation = null)
        {
            if (operation == null)
                operation = new Operation();

            if (OnStart != null) this.OnStart(this, new OnStartEventArgs(uri));
            var driver = new PhantomJSDriver(_service, _options);//实例化PhantomJS的WebDriver

            try
            {
                var watch = DateTime.Now;
                driver.Navigate().GoToUrl(uri.ToString());//请求URL地址
                if (script != null) driver.ExecuteScript(script.Code, script.Args);//执行Javascript代码
                if (operation.Action != null) operation.Action.Invoke(driver);
                var driverWait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(operation.Timeout));//设置超时时间为x毫秒
                if (operation.Condition != null) driverWait.Until(operation.Condition);
                var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;//获取当前任务线程ID
                var milliseconds = DateTime.Now.Subtract(watch).Milliseconds;//获取请求执行时间;
                var pageSource = driver.PageSource;//获取网页Dom结构
                if (this.OnCompleted != null)
                {
                    this.OnCompleted.Invoke(this, new OnCompletedEventArgs(uri, threadId, milliseconds, pageSource, driver));
                }
            }
            catch (Exception ex)
            {
                if (this.OnError != null)
                    this.OnError.Invoke(this, new OnErrorEventArgs(uri, ex));
            }
            finally
            {
                driver.Close();
                driver.Quit();
            }
        }
    }
}
