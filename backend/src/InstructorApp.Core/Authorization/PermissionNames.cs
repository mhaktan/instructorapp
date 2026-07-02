namespace InstructorApp.Authorization
{
    public static class PermissionNames
    {
        public const string Pages = "Pages";

        // Instructor
        public const string Instructor_Read = "Instructor.Read";
        public const string Instructor_Create = "Instructor.Create";
        public const string Instructor_Update = "Instructor.Update";
        public const string Instructor_Delete = "Instructor.Delete";

        // Member
        public const string Member_Read = "Member.Read";
        public const string Member_Create = "Member.Create";
        public const string Member_Update = "Member.Update";
        public const string Member_Delete = "Member.Delete";

        // MemberInstructor
        public const string MemberInstructor_Read = "MemberInstructor.Read";
        public const string MemberInstructor_Create = "MemberInstructor.Create";
        public const string MemberInstructor_Update = "MemberInstructor.Update";
        public const string MemberInstructor_Delete = "MemberInstructor.Delete";

        // Lesson
        public const string Lesson_Read = "Lesson.Read";
        public const string Lesson_Create = "Lesson.Create";
        public const string Lesson_Update = "Lesson.Update";
        public const string Lesson_Delete = "Lesson.Delete";

        // LessonAttendance
        public const string LessonAttendance_Read = "LessonAttendance.Read";
        public const string LessonAttendance_Create = "LessonAttendance.Create";
        public const string LessonAttendance_Update = "LessonAttendance.Update";
        public const string LessonAttendance_Delete = "LessonAttendance.Delete";

        // Payment
        public const string Payment_Read = "Payment.Read";
        public const string Payment_Create = "Payment.Create";
        public const string Payment_Update = "Payment.Update";
        public const string Payment_Delete = "Payment.Delete";

        // RBAC management
        public const string AppUser_Read = "AppUser.Read";
        public const string AppRole_Read = "AppRole.Read";
        public const string AppUser_Create = "AppUser.Create";
        public const string AppRole_Create = "AppRole.Create";
        public const string AppUser_Update = "AppUser.Update";
        public const string AppRole_Update = "AppRole.Update";
        public const string AppUser_Delete = "AppUser.Delete";
        public const string AppRole_Delete = "AppRole.Delete";
        public const string AppRole_AssignPermissions = "AppRole.AssignPermissions";

    }
}
