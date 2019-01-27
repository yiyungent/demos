using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pjax_aspnet_mvc.Controllers
{
    public class SpaceController : Controller
    {
        // GET: Space
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(Request.Headers["X-PJAX"]) && Convert.ToBoolean(Request.Headers["X-PJAX"]))
            {
                // 为 pjax 请求
                string partHtmlStr = "<h2>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "</h2>";
                if (!string.IsNullOrEmpty(Request.QueryString["action"]))
                {
                    partHtmlStr += " 第二种方式获取的 ";
                }
                return Content(partHtmlStr);
            }
            return View();
        }
    }
}