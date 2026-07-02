using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using InstructorApp.EntityFrameworkCore;

namespace InstructorApp.Web.Host
{
    [DependsOn(typeof(InstructorAppApplicationModule), typeof(InstructorAppEntityFrameworkCoreModule), typeof(AbpAspNetCoreModule))]
    public class InstructorAppWebHostModule : AbpModule
    {
        public override void PreInitialize()
        {
            // Expose all AppServices as dynamic API controllers
            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(InstructorAppApplicationModule).GetAssembly(),
                    moduleName: "app",
                    useConventionalHttpVerbs: true
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(InstructorAppWebHostModule).GetAssembly());
        }
    }
}
