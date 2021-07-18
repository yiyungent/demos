using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcoreUseMySqlEFCore
{
    public class DataContext : DbContext
    {
        #region 第一种方法
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        #endregion

        #region 第二种方法
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("server=localhost;user=root;pwd=admin;database=testMySqlEFCore;SslMode=none;");
        //    base.OnConfiguring(optionsBuilder);
        //} 
        #endregion

        public DbSet<Data> DataSet { get; set; }
    }
}
