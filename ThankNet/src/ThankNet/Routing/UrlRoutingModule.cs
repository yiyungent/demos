using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ThankNet.Routing
{
    /// <summary>
    /// 解析请求带来的路由数据，再将得到的数据交给处理程序
    /// （伪静态）
    /// </summary>
    public class UrlRoutingModule : IHttpModule
    {
        public void Init(HttpApplication application)
        {
            // 注册第7个管道事件，伪静态越晚越好，但再不做就晚了
            application.PostResolveRequestCache += Application_PostResolveRequestCache;
        }

        private void Application_PostResolveRequestCache(object sender, EventArgs e)
        {
            // eg: 请求 http://www.a.com/student/add


        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
