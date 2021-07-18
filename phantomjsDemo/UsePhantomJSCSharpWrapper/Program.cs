using NReco.PhantomJS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsePhantomJSCSharpWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var phantomJS = new PhantomJS();

            // write result to stdout
            Console.WriteLine("Getting content from baidu.com directly to C# code...");
            var outFileHtml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bilibili.html");
            if (File.Exists(outFileHtml))
                File.Delete(outFileHtml);
            using (var outFs = new FileStream(outFileHtml, FileMode.OpenOrCreate, FileAccess.Write))
            {
                try
                {
                    phantomJS.RunScript(@"
						var system = require('system');
						var page = require('webpage').create();
						page.open('https://www.bilibili.com/', function() {
							system.stdout.writeLine(page.content);
                            page.render('cutPic.png');
							phantom.exit();
						});
					", null, null, outFs);
                }
                finally
                {
                    phantomJS.Abort(); // ensure that phantomjs.exe is stopped
                }
            }
            Console.WriteLine("Result is saved into " + outFileHtml);
        }
    }
}
