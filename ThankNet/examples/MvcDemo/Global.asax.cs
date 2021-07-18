using System;
using System.Collections.Generic;
using ThankNet.Routing;

namespace MvcDemo
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.Add(
                new Route(
                    "Home",
                    "{controller}/{action}",
                    new { controller = "Home", action = "Index" }
                )
            );
        }
    }
}