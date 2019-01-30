using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pjax_aspnet_mvc.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(Request.Headers["X-PJAX"]) && Convert.ToBoolean(Request.Headers["X-PJAX"]))
            {
                // pjax请求部分视图
                // 下面两种都行
                //return View("_IndexContent");
                return PartialView("_IndexContent");
            }
            // 返回 完整视图
            return View();
        }

        public ActionResult Net()
        {
            if (!string.IsNullOrEmpty(Request.Headers["X-PJAX"]) && Convert.ToBoolean(Request.Headers["X-PJAX"]))
            {
                // pjax请求部分视图
                return View("_NetContent");
            }
            // 返回 完整视图
            return View();
        }

        public ActionResult Php()
        {
            if (!string.IsNullOrEmpty(Request.Headers["X-PJAX"]) && Convert.ToBoolean(Request.Headers["X-PJAX"]))
            {
                // pjax请求部分视图
                //return View("_PhpContent");
                return PartialView("_PhpContent");
            }
            // 返回 完整视图
            return View();
        }

        public ActionResult About()
        {
            if (!string.IsNullOrEmpty(Request.Headers["X-PJAX"]) && Convert.ToBoolean(Request.Headers["X-PJAX"]))
            {
                // pjax请求部分视图
                return View("_AboutContent");
            }
            // 返回 完整视图
            return View();
        }
    }
}