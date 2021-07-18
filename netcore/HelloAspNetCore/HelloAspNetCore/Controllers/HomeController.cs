using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloAspNetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        //public HomeController()
        //{
        //}

        private readonly HelloWorldDBContext _context;

        public HomeController(HelloWorldDBContext context)
        {
            _context = context;
        }

        //public ContentResult Index()
        //{
        //    //return "你好，世界! 此消息来自 HomeController...";
        //    return Content("你好，世界! 这条消息来自使用了 Action Result 的 Home 控制器");
        //}

        //public ObjectResult Index()
        //{
        //    var employee = new Employee { ID = 1, Name = "语飞" };
        //    return new ObjectResult(employee);
        //}

        public ViewResult Index()
        {
            //var employee = new Employee { ID = 1, Name = "语飞" };
            //return View(employee);

            //var model = new HomePageViewModel();
            //using (var context = new HelloWorldDBContext())
            //{
            //    SQLEmployeeData sqlData = new SQLEmployeeData(context);
            //    model.Employees = sqlData.GetAll();
            //}

            var model = new HomePageViewModel();
            SQLEmployeeData sqlData = new SQLEmployeeData(_context);
            model.Employees = sqlData.GetAll();

            return View(model);
        }
    }

    public class SQLEmployeeData
    {
        private HelloWorldDBContext _context { get; set; }

        public SQLEmployeeData(HelloWorldDBContext context)
        {
            _context = context;
        }

        public void Add(Employee emp)
        {
            _context.Add(emp);
            _context.SaveChanges();
        }

        public Employee Get(int ID)
        {
            return _context.Employees.FirstOrDefault(e => e.ID == ID);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList<Employee>();
        }
    }

    public class HomePageViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }
    }
}
