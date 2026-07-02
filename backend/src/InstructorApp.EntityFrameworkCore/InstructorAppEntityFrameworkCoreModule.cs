using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InstructorApp.EntityFrameworkCore
{
    [DependsOn(typeof(InstructorAppCoreModule), typeof(AbpEntityFrameworkCoreModule))]
    public class InstructorAppEntityFrameworkCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpEfCore().AddDbContext<InstructorAppDbContext>(options =>
            {
                // Read connection string from appsettings.json
                var config = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();
                var connStr = config.GetConnectionString("Default");
                options.DbContextOptions.UseMySql(connStr, ServerVersion.AutoDetect(connStr));
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(InstructorAppEntityFrameworkCoreModule).GetAssembly());
        }
    }
}
