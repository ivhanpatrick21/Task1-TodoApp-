using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Task1.Models;

namespace Task1.Data
{
    public class TodoDBContext : DbContext
    {
        public IConfiguration _appConfig { get; }

        public TodoDBContext(IConfiguration appConfig)
        {
            _appConfig = appConfig;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //var server = _appConfig.GetConnectionString("Server");
            //var db = _appConfig.GetConnectionString("DB");
            //var userName = _appConfig.GetConnectionString("UserName");
            //var password = _appConfig.GetConnectionString("Password");
            //string connectionString = $"Server={server};Database={db};User Id={userName};Password={password};MultipleActiveResultSets=true";
            string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=TodoTaskDB;Integrated Security=True ";
            optionsBuilder.UseSqlServer(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            /*optionsBuilder.UseSqlServer(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);*/

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Todo> Todos { get; set; }
    }
}
