using Microsoft.EntityFrameworkCore;
using Abp.EntityFrameworkCore;
using InstructorApp.Entities;

namespace InstructorApp.EntityFrameworkCore
{
    public class InstructorAppDbContext : AbpDbContext
    {
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberInstructor> MemberInstructors { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonAttendance> LessonAttendances { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }


        public InstructorAppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Instructor 1:N MemberInstructor
            modelBuilder.Entity<MemberInstructor>()
                .HasOne(x => x.Instructor)
                .WithMany(x => x.MemberInstructors)
                .HasForeignKey(x => x.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Member 1:N MemberInstructor
            modelBuilder.Entity<MemberInstructor>()
                .HasOne(x => x.Member)
                .WithMany(x => x.MemberInstructors)
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            // Instructor 1:N Lesson
            modelBuilder.Entity<Lesson>()
                .HasOne(x => x.Instructor)
                .WithMany(x => x.Lessons)
                .HasForeignKey(x => x.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Lesson 1:N LessonAttendance
            modelBuilder.Entity<LessonAttendance>()
                .HasOne(x => x.Lesson)
                .WithMany(x => x.LessonAttendances)
                .HasForeignKey(x => x.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            // Member 1:N LessonAttendance
            modelBuilder.Entity<LessonAttendance>()
                .HasOne(x => x.Member)
                .WithMany(x => x.LessonAttendances)
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

            // Member 1:N Payment
            modelBuilder.Entity<Payment>()
                .HasOne(x => x.Member)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Restrict);


            // RBAC: AppUser N:N AppRole via UserRole junction
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserRole>()
                .HasIndex(ur => new { ur.UserId, ur.RoleId })
                .IsUnique();

            // RolePermission: AppRole 1:N RolePermission
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<RolePermission>()
                .HasIndex(rp => new { rp.RoleId, rp.PermissionName })
                .IsUnique();

            // AppRole.Name unique
            modelBuilder.Entity<AppRole>()
                .HasIndex(r => r.Name)
                .IsUnique();

        }
    }
}
