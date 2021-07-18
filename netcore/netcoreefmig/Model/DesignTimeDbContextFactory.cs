using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    #region 用于dotnet ef migrations
    // 解决此问题，当不需要迁移时则不再需要此 来获取连接字符串
    // Unable to create an object of type 'MyDbContext'. Add an implementation of 'IDesignTimeDbContextFactory<LightContext>' to the project, or see https://go.microsoft.com/fwlink/?linkid=851728 for additional patterns supported at design time.

    //public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    //{
    //    public MyDbContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
    //        optionsBuilder.UseMySQL("server=localhost;user=root;pwd=admin;database=testNetCoreEfMig;SslMode=none;");

    //        return new MyDbContext(optionsBuilder.Options);
    //    }
    //} 
    #endregion
}
