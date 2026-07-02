using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using InstructorApp.Entities;
using InstructorApp.Instructors.Dto;
using InstructorApp.Authorization;

namespace InstructorApp.Instructors
{
    public class InstructorAppService : AsyncCrudAppService<
        Instructor,
        InstructorDto,
        long,
        PagedInstructorResultRequestDto,
        CreateInstructorDto,
        InstructorDto>,
        IInstructorAppService
    {
        public InstructorAppService(IRepository<Instructor, long> repository)
            : base(repository)
        {
            // Claim-based authorization (JwtPermissionChecker reads JWT "permission" claims)
            GetPermissionName = PermissionNames.Instructor_Read;
            GetAllPermissionName = PermissionNames.Instructor_Read;
            CreatePermissionName = PermissionNames.Instructor_Create;
            UpdatePermissionName = PermissionNames.Instructor_Update;
            DeletePermissionName = PermissionNames.Instructor_Delete;
        }

        protected override IQueryable<Instructor> CreateFilteredQuery(PagedInstructorResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Id.ToString().Contains(input.Keyword) ||
                    (x.FirstName != null && x.FirstName.Contains(input.Keyword)) ||
                    (x.LastName != null && x.LastName.Contains(input.Keyword)) ||
                    (x.Email != null && x.Email.Contains(input.Keyword)) ||
                    (x.Phone != null && x.Phone.Contains(input.Keyword)) ||
                    (x.Specialization != null && x.Specialization.Contains(input.Keyword)))
                .WhereIf(!input.FirstName.IsNullOrWhiteSpace(), x => x.FirstName != null && x.FirstName.Contains(input.FirstName))
                .WhereIf(!input.LastName.IsNullOrWhiteSpace(), x => x.LastName != null && x.LastName.Contains(input.LastName))
                .WhereIf(!input.Email.IsNullOrWhiteSpace(), x => x.Email != null && x.Email.Contains(input.Email))
                .WhereIf(!input.Phone.IsNullOrWhiteSpace(), x => x.Phone != null && x.Phone.Contains(input.Phone))
                .WhereIf(!input.Specialization.IsNullOrWhiteSpace(), x => x.Specialization != null && x.Specialization.Contains(input.Specialization))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive.Value);
        }
    }
}
