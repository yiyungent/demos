using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using netcoreefmig.Models;

namespace netcoreefmig.Controllers
{
    public class HomeController : Controller
    {
        private MyDbContext _myDbContext;

        public HomeController(MyDbContext myDbContext)
        {
            // 这里用了asp.net自带的依赖注入功能，不需要显式new对象
            _myDbContext = myDbContext;
        }

        public IActionResult Index()
        {
            this._myDbContext.Person.Add(new Person { Name = DateTimeOffset.Now.ToString(), Age = 54 });
            this._myDbContext.SaveChanges();
            return Content("success");
            //return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
