using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InstructorApp.EntityFrameworkCore
{
    public class InstructorAppDbContextFactory : IDesignTimeDbContextFactory<InstructorAppDbContext>
    {
        public InstructorAppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../InstructorApp.Web.Host"))
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connStr = configuration.GetConnectionString("Default");
            var builder = new DbContextOptionsBuilder();
            builder.UseMySql(connStr, ServerVersion.AutoDetect(connStr));
            return new InstructorAppDbContext(builder.Options);
        }
    }
}
