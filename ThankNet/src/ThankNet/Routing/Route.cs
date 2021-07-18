using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThankNet.Routing
{
    /// <summary>
    /// 路由对象（路由规则）
    /// </summary>
    public class Route
    {
        #region Ctor
        public Route(string name, string url, object defaults)
        {
            this.Name = name;
            this.UrlTemplate = url;

            this.Defaults = new Dictionary<string, object>();
            var defaultsProps = defaults.GetType().GetProperties();
            foreach (var item in defaultsProps)
            {
                this.Defaults.Add(item.Name, item.GetValue(defaults));
            }
        }
        #endregion

        #region Properities

        public string Name { get; set; }

        /// <summary>
        /// 路由对象Url模板 "{controller}/{action}"
        /// </summary>
        public string UrlTemplate { get; set; }

        /// <summary>
        /// new { controller = "Home", action = "Index" }
        /// </summary>
        public IDictionary<string, object> Defaults { get; set; }

        #endregion

        #region Methods

        public bool MatchRoute(string requestUrl, out IDictionary<string, object> routeData)
        {
            routeData = new Dictionary<string, object>();

            return true;
        }

        #endregion
    }
}