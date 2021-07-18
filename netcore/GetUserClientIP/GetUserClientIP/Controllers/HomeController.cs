using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GetUserClientIP.Models;

namespace GetUserClientIP.Controllers
{
    public class HomeController : Controller
    {
        // 成功
        // {"userClientIPMapToIPv4":"183.226.142.188","userClientIP":"183.226.142.188","userClientPort":"1566"}
        public IActionResult Index()
        {
            string userClientIPMapToIPv4 = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            string userClientIP = HttpContext.Connection.RemoteIpAddress.ToString();
            string userClientPort = HttpContext.Connection.RemotePort.ToString();

            return Json(new { userClientIPMapToIPv4, userClientIP, userClientPort });
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
