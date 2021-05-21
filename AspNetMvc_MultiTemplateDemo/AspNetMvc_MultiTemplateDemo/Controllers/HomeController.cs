using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMvc_MultiTemplateDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult TestDemo()
        {
            return View("Test");
        }

        public ViewResult TestUseLayout()
        {
            return View("Test", "_Layout");
        }
    }
}