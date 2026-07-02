using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using InstructorApp.Entities;
using InstructorApp.LessonAttendances.Dto;
using InstructorApp.Authorization;

namespace InstructorApp.LessonAttendances
{
    public class LessonAttendanceAppService : AsyncCrudAppService<
        LessonAttendance,
        LessonAttendanceDto,
        long,
        PagedLessonAttendanceResultRequestDto,
        CreateLessonAttendanceDto,
        LessonAttendanceDto>,
        ILessonAttendanceAppService
    {
        public LessonAttendanceAppService(IRepository<LessonAttendance, long> repository)
            : base(repository)
        {
            // Claim-based authorization (JwtPermissionChecker reads JWT "permission" claims)
            GetPermissionName = PermissionNames.LessonAttendance_Read;
            GetAllPermissionName = PermissionNames.LessonAttendance_Read;
            CreatePermissionName = PermissionNames.LessonAttendance_Create;
            UpdatePermissionName = PermissionNames.LessonAttendance_Update;
            DeletePermissionName = PermissionNames.LessonAttendance_Delete;
        }

        protected override IQueryable<LessonAttendance> CreateFilteredQuery(PagedLessonAttendanceResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Id.ToString().Contains(input.Keyword) ||
                    (x.AttendanceNote != null && x.AttendanceNote.Contains(input.Keyword)))
                .WhereIf(!input.AttendanceNote.IsNullOrWhiteSpace(), x => x.AttendanceNote != null && x.AttendanceNote.Contains(input.AttendanceNote))
                .WhereIf(input.Attended.HasValue, x => x.Attended == input.Attended.Value)
                .WhereIf(input.SessionConsumed.HasValue, x => x.SessionConsumed == input.SessionConsumed.Value)
                .WhereIf(input.LessonId.HasValue, x => x.LessonId == input.LessonId.Value)
                .WhereIf(input.MemberId.HasValue, x => x.MemberId == input.MemberId.Value);
        }
    }
}
