using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore2._1UseMySqlEFCore.Controllers
{
    public class TestController : Controller
    {
        private UserContext _userContext;

        public TestController(UserContext userContext)
        {
            // 这里用了asp.net自带的依赖注入功能，不需要显式new对象
            _userContext = userContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            User user = new User { Aaccount = "test", Password = "2333及哈哈" };
            UserService userService = new UserService();
            userService.Add(this._userContext, user);
            return Json(new { isSuccess = true });
        }

        // 最后根据模型自动生成数据库，表
        // 参考这里：https://blog.csdn.net/tilovefa/article/details/80972041

        #region 第一种使用 MySql.Data.EntityFrameworkCore
        // 最后发现使用.net core 2.1 + MySql.Data.EntityFrameworkCore 8.0.12
        // 失败,在 Add-Migration时 抛出异常：Method not found: 'Void Microsoft.EntityFrameworkCore.Storage.Internal.RelationalCommandBuilderFactory..ctor(Microsoft.EntityFrameworkCore.Diagnostics.IDiagnosticsLogger`1<Command>, Microsoft.EntityFrameworkCore.Storage.IRelationalTypeMapper)'.

        // 而使用.net core 2.0 则可以 
        #endregion

        #region 第二种使用 Pomelo.EntityFrameworkCore.MySql 2.1.1
        // .net core 2.1 完全成功

        // 值得注意的是在 Pomelo.EntityFrameworkCore.MySql 的项目doc中写道
        // 如果使用的是旧版本mysql，例如mysql5.5，则需要这个特殊的选项
        // 自2.1.0起 通知提供程序MySQL服务器的功能。如果您使用的是旧版本，例如MySQL 5.5，则需要指定此选项。
        // 默认为mysql最新版本
        //services.AddDbContext<AppDb>(
        //    options => options.UseMySql(connection,
        //        mysqlOptions =>
        //        {
        //            mysqlOptions
        //                .ServerVersion(new Version(5, 7, 17), ServerType.MySql);
        //        }
        //));
        // 原文：https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/wiki/MySql-Provider-Configuration-Options
        #endregion

        #region 扩展：MySQL utf8 和 utf8mb4 的区别
        // https://www.cnblogs.com/zienzir/p/9094092.html
        #endregion
    }
}