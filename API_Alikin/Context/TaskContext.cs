using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using API_Alikin.Model;

using System.Data.Common;

namespace API_Alikin.Context
{
    public class TaskContext : DbContext
    {
        public DbSet<Tasks> Tasks { get; set; }
        public TaskContext()
        {
            Database.EnsureCreated();
            Tasks.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseMySql("server=127.0.0.1;" +
                "port=3307;" +
                "uid=root;" +
                "pwd=;" +
                "database=TaskManager;",
                new MySqlServerVersion(new Version(8, 0, 30)));
    }
}
