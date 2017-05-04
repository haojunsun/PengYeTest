using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PengYe.Project.Infrastructure;
using PengYe.Project.Model;

namespace PengYe.Project.Repository
{
    public class MyDbContext:DbContext, IDependencyPerRequest
    {
        public MyDbContext(): base("DefaultConnection")//数据连接
        {

        }
        public DbSet<Article> Articles { get; set; }
    }
}
