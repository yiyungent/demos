using System.Collections.Generic;

namespace ThankNet.Routing
{
    public class RouteTable
    {
        /// <summary>
        /// 全部路由表（路由规则）
        /// </summary>
        public static IList<Route> Routes { get; set; }

        static RouteTable()
        {
            Routes = new List<Route>();
        }

        public static Route MatchRoutes(string requestUrl, out IDictionary<string, object> dict)
        {
            dict = null;
            // 遍历全局路由表中的路由规则
            foreach (Route route in Routes)
            {
                // 让当前遍历到的路由对象规则匹配当前请求地址
                if (route.MatchRoute(requestUrl, out dict))
                {
                    return route;
                }
            }

            return null;
        }
    }
}