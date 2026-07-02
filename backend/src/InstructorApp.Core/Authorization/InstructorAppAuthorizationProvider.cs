using Abp.Authorization;
using Abp.Localization;

namespace InstructorApp.Authorization
{
    public class InstructorAppAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var pages = context.GetPermissionOrNull("Pages") ?? context.CreatePermission("Pages", L("Pages"));

            // Instructor
            pages.CreateChildPermission(PermissionNames.Instructor_Read, L("Instructor.Read"));
            pages.CreateChildPermission(PermissionNames.Instructor_Create, L("Instructor.Create"));
            pages.CreateChildPermission(PermissionNames.Instructor_Update, L("Instructor.Update"));
            pages.CreateChildPermission(PermissionNames.Instructor_Delete, L("Instructor.Delete"));

            // Member
            pages.CreateChildPermission(PermissionNames.Member_Read, L("Member.Read"));
            pages.CreateChildPermission(PermissionNames.Member_Create, L("Member.Create"));
            pages.CreateChildPermission(PermissionNames.Member_Update, L("Member.Update"));
            pages.CreateChildPermission(PermissionNames.Member_Delete, L("Member.Delete"));

            // MemberInstructor
            pages.CreateChildPermission(PermissionNames.MemberInstructor_Read, L("MemberInstructor.Read"));
            pages.CreateChildPermission(PermissionNames.MemberInstructor_Create, L("MemberInstructor.Create"));
            pages.CreateChildPermission(PermissionNames.MemberInstructor_Update, L("MemberInstructor.Update"));
            pages.CreateChildPermission(PermissionNames.MemberInstructor_Delete, L("MemberInstructor.Delete"));

            // Lesson
            pages.CreateChildPermission(PermissionNames.Lesson_Read, L("Lesson.Read"));
            pages.CreateChildPermission(PermissionNames.Lesson_Create, L("Lesson.Create"));
            pages.CreateChildPermission(PermissionNames.Lesson_Update, L("Lesson.Update"));
            pages.CreateChildPermission(PermissionNames.Lesson_Delete, L("Lesson.Delete"));

            // LessonAttendance
            pages.CreateChildPermission(PermissionNames.LessonAttendance_Read, L("LessonAttendance.Read"));
            pages.CreateChildPermission(PermissionNames.LessonAttendance_Create, L("LessonAttendance.Create"));
            pages.CreateChildPermission(PermissionNames.LessonAttendance_Update, L("LessonAttendance.Update"));
            pages.CreateChildPermission(PermissionNames.LessonAttendance_Delete, L("LessonAttendance.Delete"));

            // Payment
            pages.CreateChildPermission(PermissionNames.Payment_Read, L("Payment.Read"));
            pages.CreateChildPermission(PermissionNames.Payment_Create, L("Payment.Create"));
            pages.CreateChildPermission(PermissionNames.Payment_Update, L("Payment.Update"));
            pages.CreateChildPermission(PermissionNames.Payment_Delete, L("Payment.Delete"));

            // RBAC
            pages.CreateChildPermission(PermissionNames.AppUser_Read, L("AppUser.Read"));
            pages.CreateChildPermission(PermissionNames.AppRole_Read, L("AppRole.Read"));
            pages.CreateChildPermission(PermissionNames.AppUser_Create, L("AppUser.Create"));
            pages.CreateChildPermission(PermissionNames.AppRole_Create, L("AppRole.Create"));
            pages.CreateChildPermission(PermissionNames.AppUser_Update, L("AppUser.Update"));
            pages.CreateChildPermission(PermissionNames.AppRole_Update, L("AppRole.Update"));
            pages.CreateChildPermission(PermissionNames.AppUser_Delete, L("AppUser.Delete"));
            pages.CreateChildPermission(PermissionNames.AppRole_Delete, L("AppRole.Delete"));
            pages.CreateChildPermission(PermissionNames.AppRole_AssignPermissions, L("AppRole.AssignPermissions"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, InstructorAppConsts.LocalizationSourceName);
        }
    }
}
