using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using InstructorApp.Entities;
using InstructorApp.Lessons.Dto;
using InstructorApp.Authorization;

namespace InstructorApp.Lessons
{
    public class LessonAppService : AsyncCrudAppService<
        Lesson,
        LessonDto,
        long,
        PagedLessonResultRequestDto,
        CreateLessonDto,
        LessonDto>,
        ILessonAppService
    {
        public LessonAppService(IRepository<Lesson, long> repository)
            : base(repository)
        {
            // Claim-based authorization (JwtPermissionChecker reads JWT "permission" claims)
            GetPermissionName = PermissionNames.Lesson_Read;
            GetAllPermissionName = PermissionNames.Lesson_Read;
            CreatePermissionName = PermissionNames.Lesson_Create;
            UpdatePermissionName = PermissionNames.Lesson_Update;
            DeletePermissionName = PermissionNames.Lesson_Delete;
        }

        protected override IQueryable<Lesson> CreateFilteredQuery(PagedLessonResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Id.ToString().Contains(input.Keyword) ||
                    (x.StartTime != null && x.StartTime.Contains(input.Keyword)) ||
                    (x.EndTime != null && x.EndTime.Contains(input.Keyword)) ||
                    (x.LessonType != null && x.LessonType.Contains(input.Keyword)) ||
                    (x.Notes != null && x.Notes.Contains(input.Keyword)))
                .WhereIf(!input.StartTime.IsNullOrWhiteSpace(), x => x.StartTime != null && x.StartTime.Contains(input.StartTime))
                .WhereIf(!input.EndTime.IsNullOrWhiteSpace(), x => x.EndTime != null && x.EndTime.Contains(input.EndTime))
                .WhereIf(!input.LessonType.IsNullOrWhiteSpace(), x => x.LessonType != null && x.LessonType.Contains(input.LessonType))
                .WhereIf(!input.Notes.IsNullOrWhiteSpace(), x => x.Notes != null && x.Notes.Contains(input.Notes))
                .WhereIf(input.LessonDate.HasValue, x => x.LessonDate == input.LessonDate.Value)
                .WhereIf(input.DayOfWeek.HasValue, x => x.DayOfWeek == (DayOfWeek)input.DayOfWeek.Value)
                .WhereIf(input.InstructorId.HasValue, x => x.InstructorId == input.InstructorId.Value);
        }
    }
}
