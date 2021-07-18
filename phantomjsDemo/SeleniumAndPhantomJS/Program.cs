using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAndPhantomJS
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://www.moeci.com";
            IWebDriver driver = new PhantomJSDriver(/*GetPhantomJSDriverService()*/);
            driver.Navigate().GoToUrl(url);
            //Console.WriteLine(driver.Title);
            //Console.WriteLine(driver.PageSource);
            Console.Read();
        }

        private static PhantomJSDriverService GetPhantomJSDriverService()
        {
            PhantomJSDriverService pds = PhantomJSDriverService.CreateDefaultService();
            //设置代理服务器地址
            //pds.Proxy = $"{ip}:{port}";  
            //设置代理服务器认证信息
            //pds.ProxyAuthentication = GetProxyAuthorization();
            return pds;
        }
    }
}
