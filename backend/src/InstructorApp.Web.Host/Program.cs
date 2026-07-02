using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using InstructorApp.EntityFrameworkCore;
using InstructorApp.EntityFrameworkCore.Seed;
using InstructorApp.Entities;

namespace InstructorApp.Web.Host
{
    /// <summary>
    /// Background service that runs migration + seed once at startup
    /// without blocking the HTTP pipeline.
    /// </summary>
    public class MigrationHostedService : IHostedService
    {
        private readonly IConfiguration _config;

        public MigrationHostedService(IConfiguration config)
        {
            _config = config;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var connStr = _config.GetConnectionString("Default") ?? "";
            if (string.IsNullOrEmpty(connStr)) return;

            // Run in background to avoid blocking host startup (prevents EF tooling timeout)
            _ = Task.Run(async () =>
            {
                // Small delay to ensure host is fully started before DB operations
                await Task.Delay(1000, cancellationToken);
                try
                {
                    var optionsBuilder = new DbContextOptionsBuilder();
                    optionsBuilder.UseMySql(connStr, ServerVersion.AutoDetect(connStr));

                    using (var db = new InstructorAppDbContext(optionsBuilder.Options))
                    {
                        db.Database.EnsureCreated();
                        Console.WriteLine("[Migration] Database is up to date.");

                        // Seed sample data — wrapped in its own try so a failure here doesn't block RBAC seed below.
                        try
                        {
                    if (!db.Instructors.Any())
                    {
                        db.Instructors.AddRange(
                    new Instructor { Id = 1, FirstName = "Alice Johnson", LastName = "Alice Johnson", Email = "alice@example.com", Phone = "+1-555-0101", Specialization = "Sample Item 1", IsActive = true },
                    new Instructor { Id = 2, FirstName = "Bob Smith", LastName = "Bob Smith", Email = "bob@example.com", Phone = "+1-555-0102", Specialization = "Sample Item 2", IsActive = false }
                        );
                    }
                    if (!db.Members.Any())
                    {
                        db.Members.AddRange(
                    new Member { Id = 3, FirstName = "Alice Johnson", LastName = "Alice Johnson", Email = "alice@example.com", Phone = "+1-555-0101", BirthDate = new DateTime(2024, 3, 15), MembershipStartDate = new DateTime(2024, 3, 15), MembershipEndDate = new DateTime(2024, 3, 15), MembershipStatus = (MembershipStatus)0, TotalSessions = 42, UsedSessions = 42, Notes = "Lorem ipsum dolor sit amet" },
                    new Member { Id = 4, FirstName = "Bob Smith", LastName = "Bob Smith", Email = "bob@example.com", Phone = "+1-555-0102", BirthDate = new DateTime(2024, 6, 20), MembershipStartDate = new DateTime(2024, 6, 20), MembershipEndDate = new DateTime(2024, 6, 20), MembershipStatus = (MembershipStatus)1, TotalSessions = 17, UsedSessions = 17, Notes = "Consectetur adipiscing elit" }
                        );
                    }
                    if (!db.MemberInstructors.Any())
                    {
                        db.MemberInstructors.AddRange(
                    new MemberInstructor { Id = 5, AssignedDate = new DateTime(2024, 3, 15), IsActive = true, InstructorId = 1, MemberId = 3 },
                    new MemberInstructor { Id = 6, AssignedDate = new DateTime(2024, 6, 20), IsActive = false, InstructorId = 2, MemberId = 4 }
                        );
                    }
                    if (!db.Lessons.Any())
                    {
                        db.Lessons.AddRange(
                    new Lesson { Id = 7, LessonDate = new DateTime(2024, 3, 15), DayOfWeek = (DayOfWeek)0, StartTime = "Sampl", EndTime = "Sampl", LessonType = "Sample Item 1", Notes = "Lorem ipsum dolor sit amet", InstructorId = 1 },
                    new Lesson { Id = 8, LessonDate = new DateTime(2024, 6, 20), DayOfWeek = (DayOfWeek)1, StartTime = "Sampl", EndTime = "Sampl", LessonType = "Sample Item 2", Notes = "Consectetur adipiscing elit", InstructorId = 2 }
                        );
                    }
                    if (!db.LessonAttendances.Any())
                    {
                        db.LessonAttendances.AddRange(
                    new LessonAttendance { Id = 9, Attended = true, SessionConsumed = true, AttendanceNote = "Lorem ipsum dolor sit amet", LessonId = 7, MemberId = 3 },
                    new LessonAttendance { Id = 10, Attended = false, SessionConsumed = false, AttendanceNote = "Consectetur adipiscing elit", LessonId = 8, MemberId = 4 }
                        );
                    }
                    if (!db.Payments.Any())
                    {
                        db.Payments.AddRange(
                    new Payment { Id = 11, PaymentDate = new DateTime(2024, 3, 15), Amount = 99.99m, SessionsPurchased = 42, PaymentMethod = (PaymentMethod)0, Notes = "Lorem ipsum dolor sit amet", MemberId = 3 },
                    new Payment { Id = 12, PaymentDate = new DateTime(2024, 6, 20), Amount = 149.50m, SessionsPurchased = 17, PaymentMethod = (PaymentMethod)1, Notes = "Consectetur adipiscing elit", MemberId = 4 }
                        );
                    }
                            db.SaveChanges();
                            Console.WriteLine("[Seed] Sample data created.");
                        }
                        catch (Exception sampleEx)
                        {
                            Console.WriteLine($"[Seed] Sample data skipped: {sampleEx.GetType().Name}: {sampleEx.Message}");
                            // Carry on — RBAC seed must still run so admin/123qwe is usable.
                        }
                    }
                    // RBAC seed (Admin/User roles + permissions + admin user) runs through ABP DI
                    // so PermissionRegistry can be injected. SeedHelper is idempotent.
                    SeedHelper.SeedHostDb(Abp.Dependency.IocManager.Instance);
                    Console.WriteLine("[Seed] RBAC seed complete (Admin role + admin user).");
                }
                catch (Exception ex)
                {
                    // Full diagnostic — surface the real cause so silent seed failures are debuggable.
                    Console.WriteLine($"[Migration] FAILED: {ex.GetType().Name}: {ex.Message}");
                    if (ex.InnerException != null)
                        Console.WriteLine($"[Migration] InnerException: {ex.InnerException.GetType().Name}: {ex.InnerException.Message}");
                    Console.WriteLine("[Migration] StackTrace:");
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine("[Migration] App continues without migration — admin user will not exist.");
                }
            }, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }

    public class Program
    {
        // Runtime entry: WebHost is required because ABP Startup returns IServiceProvider.
        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build()
                .Run();
        }

        // Design-time entry for EF Core tools (dotnet ef migrations).
        // Without this, EF tools wait 5 minutes for IHost build (resolver default timeout)
        // and then SIGTERM any running dotnet process — killing live dev servers.
        // We expose a minimal IHost that EF tools resolve in milliseconds; the actual
        // DbContext is built by IDesignTimeDbContextFactory in the EntityFrameworkCore project.
        public static IHostBuilder CreateHostBuilder(string[] args)
            => Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args);
    }
}
