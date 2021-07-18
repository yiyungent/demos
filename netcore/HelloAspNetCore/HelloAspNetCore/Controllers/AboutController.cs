using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloAspNetCore.Controllers
{
    //[Route("about")]
    [Route("[controller]")]
    public class AboutController
    {
        public AboutController()
        {
        }

        [Route("")]
        public string Phone()
        {
            return "+10086";
        }

        [Route("country")]
        public string Country()
        {
            return "中国";
        }
    }
}
