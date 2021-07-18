using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutPicWithPhantomjs
{
    class Program
    {
        static void Main(string[] args)
        {
            WebHelper webHelper = new WebHelper(@"https://www.moeci.com", @"F:\Practice\C#\Demos\phantomjsDemo\CutPicWithPhantomjs\bin\Debug\pic.png", @"F:\Practice\C#\Demos\phantomjsDemo\CutPicWithPhantomjs\Pic");
            webHelper.GetImg();
            Console.WriteLine("完成");
            Console.ReadKey();
        }
    }
}
