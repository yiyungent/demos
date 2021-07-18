using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcore2._1UseMySqlEFCore
{
    public class UserContext : DbContext
    {
        public DbSet<User> User { get; set; }

        // 必须使用带参数的构造函数
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            // 检测数据库中是否存在该库及表，若用现有的库则不会创建表，导致后面save时报错
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 这里用来重命名表名，ef 6 时代使用的[Table("table_name")]注解好像没用了
            modelBuilder.Entity<User>().ToTable("sys_user");
        }
    }
}
