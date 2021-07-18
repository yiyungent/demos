using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThankNet.Mvc
{
    /// <summary>
    /// 约束所有具备处理请求能力
    /// </summary>
    public interface IController
    {
        void Execute(HttpContext httpContext);
    }
}