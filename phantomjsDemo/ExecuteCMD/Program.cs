using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecuteCMD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入要执行的命令:");
            string strInput = Console.ReadLine();
            strInput = @"phantomjs.exe autoWebSize.js https://www.moeci.com 5.png";
            Process p = new Process();
            //设置要启动的应用程序
            p.StartInfo.FileName = "cmd.exe";
            //是否使用操作系统shell启动
            p.StartInfo.UseShellExecute = false;
            // 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardInput = true;
            //输出信息
            p.StartInfo.RedirectStandardOutput = true;
            // 输出错误
            p.StartInfo.RedirectStandardError = true;
            //不显示程序窗口
            p.StartInfo.CreateNoWindow = true;
            //启动程序
            p.Start();

            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(strInput + "&exit");

            p.StandardInput.AutoFlush = true;

            // 注意：不知道为什么卡在此步，无法获取输出信息
            //获取输出信息
            string strOuput = p.StandardOutput.ReadToEnd();
            //等待程序执行完退出进程
            p.WaitForExit();
            p.Close();

            Console.WriteLine(strOuput);

            Console.ReadKey();
        }
    }
}
