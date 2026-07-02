using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using InstructorApp.Entities;
using InstructorApp.MemberInstructors.Dto;
using InstructorApp.Authorization;

namespace InstructorApp.MemberInstructors
{
    public class MemberInstructorAppService : AsyncCrudAppService<
        MemberInstructor,
        MemberInstructorDto,
        long,
        PagedMemberInstructorResultRequestDto,
        CreateMemberInstructorDto,
        MemberInstructorDto>,
        IMemberInstructorAppService
    {
        public MemberInstructorAppService(IRepository<MemberInstructor, long> repository)
            : base(repository)
        {
            // Claim-based authorization (JwtPermissionChecker reads JWT "permission" claims)
            GetPermissionName = PermissionNames.MemberInstructor_Read;
            GetAllPermissionName = PermissionNames.MemberInstructor_Read;
            CreatePermissionName = PermissionNames.MemberInstructor_Create;
            UpdatePermissionName = PermissionNames.MemberInstructor_Update;
            DeletePermissionName = PermissionNames.MemberInstructor_Delete;
        }

        protected override IQueryable<MemberInstructor> CreateFilteredQuery(PagedMemberInstructorResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Id.ToString().Contains(input.Keyword))
                .WhereIf(input.AssignedDate.HasValue, x => x.AssignedDate == input.AssignedDate.Value)
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive.Value)
                .WhereIf(input.InstructorId.HasValue, x => x.InstructorId == input.InstructorId.Value)
                .WhereIf(input.MemberId.HasValue, x => x.MemberId == input.MemberId.Value);
        }
    }
}
