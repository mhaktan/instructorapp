using System.Collections.Generic;
using Abp.Dependency;

namespace InstructorApp.Authorization
{
    /// <summary>Single permission descriptor — name, group (entity), description.</summary>
    public class PermissionInfo
    {
        public string Name { get; }
        public string Group { get; }
        public string Description { get; }
        public bool IsRbac { get; }

        public PermissionInfo(string name, string group, string description, bool isRbac)
        {
            Name = name; Group = group; Description = description; IsRbac = isRbac;
        }
    }

    public interface IPermissionRegistry
    {
        IReadOnlyList<PermissionInfo> All { get; }
    }

    public class PermissionRegistry : IPermissionRegistry, ISingletonDependency
    {
        public IReadOnlyList<PermissionInfo> All { get; } = new List<PermissionInfo>
        {
            new PermissionInfo("Instructor.Read", "Instructor", "Read Instructor", false),
            new PermissionInfo("Instructor.Create", "Instructor", "Create Instructor", false),
            new PermissionInfo("Instructor.Update", "Instructor", "Update Instructor", false),
            new PermissionInfo("Instructor.Delete", "Instructor", "Delete Instructor", false),
            new PermissionInfo("Member.Read", "Member", "Read Member", false),
            new PermissionInfo("Member.Create", "Member", "Create Member", false),
            new PermissionInfo("Member.Update", "Member", "Update Member", false),
            new PermissionInfo("Member.Delete", "Member", "Delete Member", false),
            new PermissionInfo("MemberInstructor.Read", "MemberInstructor", "Read MemberInstructor", false),
            new PermissionInfo("MemberInstructor.Create", "MemberInstructor", "Create MemberInstructor", false),
            new PermissionInfo("MemberInstructor.Update", "MemberInstructor", "Update MemberInstructor", false),
            new PermissionInfo("MemberInstructor.Delete", "MemberInstructor", "Delete MemberInstructor", false),
            new PermissionInfo("Lesson.Read", "Lesson", "Read Lesson", false),
            new PermissionInfo("Lesson.Create", "Lesson", "Create Lesson", false),
            new PermissionInfo("Lesson.Update", "Lesson", "Update Lesson", false),
            new PermissionInfo("Lesson.Delete", "Lesson", "Delete Lesson", false),
            new PermissionInfo("LessonAttendance.Read", "LessonAttendance", "Read LessonAttendance", false),
            new PermissionInfo("LessonAttendance.Create", "LessonAttendance", "Create LessonAttendance", false),
            new PermissionInfo("LessonAttendance.Update", "LessonAttendance", "Update LessonAttendance", false),
            new PermissionInfo("LessonAttendance.Delete", "LessonAttendance", "Delete LessonAttendance", false),
            new PermissionInfo("Payment.Read", "Payment", "Read Payment", false),
            new PermissionInfo("Payment.Create", "Payment", "Create Payment", false),
            new PermissionInfo("Payment.Update", "Payment", "Update Payment", false),
            new PermissionInfo("Payment.Delete", "Payment", "Delete Payment", false),
            new PermissionInfo("AppUser.Read", "AppUser", "Read users", true),
            new PermissionInfo("AppRole.Read", "AppRole", "Read roles", true),
            new PermissionInfo("AppUser.Create", "AppUser", "Create users", true),
            new PermissionInfo("AppRole.Create", "AppRole", "Create roles", true),
            new PermissionInfo("AppUser.Update", "AppUser", "Update users", true),
            new PermissionInfo("AppRole.Update", "AppRole", "Update roles", true),
            new PermissionInfo("AppUser.Delete", "AppUser", "Delete users", true),
            new PermissionInfo("AppRole.Delete", "AppRole", "Delete roles", true),
            new PermissionInfo("AppRole.AssignPermissions", "AppRole", "Assign permissions to roles", true),
        };
    }
}
