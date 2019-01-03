using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace WebUploaderDemo
{
    /// <summary>
    /// FileUpload 的摘要说明
    /// </summary>
    public class FileUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.UTF8;
            if (context.Request["REQUEST_METHOD"] == "OPTIONS")
            {
                context.Response.End();
            }
            SaveFile();
        }

        #region 补充关于/...和~/...路径问题
        ///是根路径是不错，不过这个跟路径是域名的跟路径，
        //比如你的网站是在iis里面是添加网站，这样子/跟 ~/都是表示跟路径，
        //但是，如果你的程序是在某一个网站下新建虚拟目录，那么就不一样了，~/路径asp.net会帮你解析成你程序的根目录，但是/就不是了，是网站的跟目录，
        //打个比方吧：你有一个网站，你在iis里面发布为www.abc.com，
        //这是一个网站，然后你又做了一个网站，在iis里面你是以虚拟目录放在www.abc.com下的（就是你是在www.abc.com下右键，新建虚拟目录这样形式），
        //比如虚拟目录名称叫test，那么这时候两个路径就不一样了，比如你这个 test虚拟目录下有一个images文件夹，文件夹里面有一个bg.jpg，
        //那么你访问的时候其实是www.abc.com/test/images/bg.jpg，
        //这个时候，你在test程序里面的路径如果引用这个图片的时候用的是 ~/images/bg.jpg是没有任何问题的，但是如果你引用的是/images/bg.jpg那就表示引用的是www.abc.com/images/bg.jpg。。。所以，应该怎么做，你心里应该清楚了吧？？？
        #endregion


        /// <summary>
        /// 文件保存操作
        /// </summary>
        /// <param name="basePath"></param>
        private void SaveFile(string basePath = "~/Upload/Images/")
        {
            string name = string.Empty;
            // 如果路径含有~，即需要服务器映射为绝对路径，则进行映射
            basePath = (basePath.IndexOf("~") > -1) ? System.Web.HttpContext.Current.Server.MapPath(basePath) : basePath;
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            // 如果目录不存在，则创建目录
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            // ????????????
            string[] suffix = files[0].ContentType.Split('/');
            // 获取文件格式
            string _suffix = suffix[1].Equals("jpeg", StringComparison.CurrentCultureIgnoreCase) ? "" : suffix[1];
            string _temp = System.Web.HttpContext.Current.Request["name"];
            // 如果不修改文件名，则创建随机文件名
            if (!string.IsNullOrEmpty(_temp))
            {
                name = _temp;
            }
            else
            {
                Random rand = new Random(24 * (int)DateTime.Now.Ticks);
                name = rand.Next() + "." + _suffix;
            }
            // 文件保存
            string full = basePath + name;
            files[0].SaveAs(full);
            string _result = "{\"jsonrpc\" : \"2.0\", \"result\" : null, \"id\" : \"" + name + "\"}";
            System.Web.HttpContext.Current.Response.Write(_result);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}