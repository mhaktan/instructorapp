using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace InstructorApp
{
    [DependsOn(typeof(InstructorAppCoreModule), typeof(AbpAutoMapperModule))]
    public class InstructorAppApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                cfg.AddMaps(typeof(InstructorAppApplicationModule).GetAssembly());
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(InstructorAppApplicationModule).GetAssembly());
        }
    }
}
