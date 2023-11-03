using RS_DataAccess;
using RS_DataAccess.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RS_DataAccess
{
    public class AppDBContext : DbContext, IAppDBContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }

    }

    public class DesignTimeDbContextFactory  : IDesignTimeDbContextFactory<AppDBContext>
    {
        public AppDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../RS_Proyect/appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<AppDBContext>();
            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            builder.UseSqlServer(connectionString); 

            return new AppDBContext(builder.Options);
        }
    }
}
