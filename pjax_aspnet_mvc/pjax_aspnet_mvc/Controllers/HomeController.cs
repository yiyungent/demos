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
            return View();
        }

        public ActionResult Net()
        {
            return View();
        }

        public ActionResult Php()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}