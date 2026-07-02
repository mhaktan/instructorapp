using Abp.Modules;
using Abp.Reflection.Extensions;

namespace InstructorApp
{
    public class InstructorAppCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = InstructorAppConsts.ConnectionStringName;
            // SMTP: ABP reads email settings from AbpSettings table by default.
            // Override via ISmtpEmailSenderConfiguration registration if needed.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(InstructorAppCoreModule).GetAssembly());
        }
    }
}
