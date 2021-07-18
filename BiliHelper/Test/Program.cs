using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            int thId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("线程:" + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(1);
            var r = GetNet();
            int thId2 = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("线程:" + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(3);
            Console.ReadLine();
        }

        static async Task<string> GetNet()
        {
            Console.WriteLine(2);
            string rtn = "";

            int thId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("线程:" + Thread.CurrentThread.ManagedThreadId);

            rtn = await Task.Run<string>(() =>
           {
               string testStr = Test();

               return rtn;
           });

            int thId2 = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("线程:" + Thread.CurrentThread.ManagedThreadId);

            return rtn;
        }

        static string Test()
        {
            string rtn = "";
            Task.Delay(5000);
            int thId = Thread.CurrentThread.ManagedThreadId;

            rtn = "测试数据";

            return rtn;
        }
    }
}
