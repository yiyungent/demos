using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aspnetcoreUseMySqlEFCore.Models;

namespace aspnetcoreUseMySqlEFCore.Controllers
{
    public class HomeController : Controller
    {
        #region 第一种方法
        private DataContext _context;

        public HomeController(DataContext context)
        {
            // 这里使用了asp.net自带的依赖注入功能、不需要显式new对象
            _context = context;
        }
        #endregion

        public IActionResult Index()
        {
            #region 第一种方法
            _context.DataSet.Add(new Data { Annotation = DateTime.Now.ToString() });
            _context.SaveChanges();
            #endregion

            #region 第二种方法
            //DataContext dataContext = new DataContext();
            //dataContext.DataSet.Add(new Data { Annotation = DateTime.Now.ToString() });
            //dataContext.SaveChanges(); 
            #endregion

            return View();
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
