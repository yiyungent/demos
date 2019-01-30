using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pjax_aspnet_mvc.Controllers
{
    public class GoodsController : Controller
    {
        // GET: Goods
        public ActionResult Index()
        {
            ViewBag.Title = "首页";

            #region 1.失败:在控制器 控制 不使用Layout
            //if (!string.IsNullOrEmpty(Request.Headers["X-PJAX"]) && Convert.ToBoolean(Request.Headers["X-PJAX"]))
            //{
            //    // pjax请求不使用Layout的视图
            //    // 无法在控制器 ，控制对应视图不使用Layout，所以启用此方法
            //    return View();
            //}
            //// 返回 使用Layout的完整视图
            //return View(); 
            #endregion

            // 2.在视图中做逻辑判断，如果是pjax请求，则Layout=null
            return View();
        }

        public ActionResult Cat()
        {
            ViewBag.Title = "Cat";
            return View();
        }

        public ActionResult Dog()
        {
            ViewBag.Title = "Dog";
            return View();
        }
    }
}