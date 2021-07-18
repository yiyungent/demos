using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phantomjsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetImage(@"http://www.moeci.com/");
            GetImg(@"http://www.moeci.com/");

            Console.ReadKey();
        }

       // 失败,必须通过cmd调用phantomjs
        private static void GetImg(string url)
        {
            Process p = new Process();
            p.StartInfo.FileName=Environment.CurrentDirectory+ "//phantomjs.exe";
            p.StartInfo.Arguments = "--ignore-ssl-errors=yes autoWebSijsze. 4.png";
            p.StartInfo.CreateNoWindow = true;
            if (!p.Start())
            {
                throw new Exception("无法Headless浏览器.");
            }
        }


        // 实测：无效
        /// <summary>
        /// 网页截图: 参考https://www.cnblogs.com/yesicoo/archive/2012/05/25/2518729.html
        /// </summary>
        /// <param name="url"></param>
        private static void GetImage(string url)
        {

            string links = url.IndexOf("http://") > -1 ? url : "http://" + url;

            #region 启动进程
            Process p = new Process();
            p.StartInfo.FileName = Environment.CurrentDirectory + "//phantomjs.exe";
            p.StartInfo.WorkingDirectory = Environment.CurrentDirectory + "//pic//";
            // 最新版phantomjs已经不支持flash，所以不要传--load-plugins=yes
            p.StartInfo.Arguments = string.Format("--ignore-ssl-errors=yes " + Environment.CurrentDirectory + "//autoWebSize.js  " + links + " " + url + ".png");

            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            if (!p.Start())
                throw new Exception("无法Headless浏览器.");

            #endregion

        }
    }
}
