using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloAspNetCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HelloAspNetCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddEntityFrameworkSqlite().AddDbContext<HelloWorldDBContext>(options => options.UseSqlite(Configuration["database:connection"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region 学习1
            ////if (env.IsDevelopment())
            ////{
            ////    app.UseDeveloperExceptionPage();
            ////}

            //// 添加中间件
            ////app.UseWelcomePage();

            ////app.Run(async (context) =>
            ////{
            ////    await context.Response.WriteAsync("Hello World!");
            ////});

            ////app.Run(async (context) =>
            ////{
            ////    var msg = Configuration["message"];
            ////    await context.Response.WriteAsync(msg);
            ////});
            //app.Run(async (context) =>
            //{
            //    throw new System.Exception("Throw Exception");
            //    var msg = Configuration["message"];
            //    await context.Response.WriteAsync(msg);
            //}); 
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // 要特别注意顺序，app.UseDefaultFiles(); 必须在 app.UseStaticFiles(); 之前，否则会提示出错
            //app.UseDefaultFiles();
            // UseDefaultFiles 中间件会检查传入的请求并检查它是否用于目录的根目录，以及是否有任何匹配的默认文件
            // 我们可以覆盖这个中间件的选项来告诉它哪些是要查找的默认文件，但是 Index.html(不区分大小写) 默认是默认文件之一
            //app.UseStaticFiles();

            // ASP.NET Core 提供了 UseFileServer 中间件，这个中间件是对它们(UseDefaultFiles 和 UseStaticFiles 两个中间件)的封装
            app.UseFileServer();
            //app.UseMvcWithDefaultRoute();
            app.UseMvc(ConfigureRoute);

            app.Run(async (context) =>
            {
                var msg = Configuration["message"];
                await context.Response.WriteAsync(msg);
            });
        }

        public IConfiguration Configuration { get; set; }

        public Startup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("AppSettings.json");
            Configuration = builder.Build();
        }

        private void ConfigureRoute(IRouteBuilder routeBuilder)
        {
            //Home/Index 
            //routeBuilder.MapRoute("Default", "{controller}/{action}/{id?}");
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
