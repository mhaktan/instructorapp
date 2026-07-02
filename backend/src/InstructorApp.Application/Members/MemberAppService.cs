using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using InstructorApp.Entities;
using InstructorApp.Members.Dto;
using InstructorApp.Authorization;

namespace InstructorApp.Members
{
    public class MemberAppService : AsyncCrudAppService<
        Member,
        MemberDto,
        long,
        PagedMemberResultRequestDto,
        CreateMemberDto,
        MemberDto>,
        IMemberAppService
    {
        public MemberAppService(IRepository<Member, long> repository)
            : base(repository)
        {
            // Claim-based authorization (JwtPermissionChecker reads JWT "permission" claims)
            GetPermissionName = PermissionNames.Member_Read;
            GetAllPermissionName = PermissionNames.Member_Read;
            CreatePermissionName = PermissionNames.Member_Create;
            UpdatePermissionName = PermissionNames.Member_Update;
            DeletePermissionName = PermissionNames.Member_Delete;
        }

        protected override IQueryable<Member> CreateFilteredQuery(PagedMemberResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Id.ToString().Contains(input.Keyword) ||
                    (x.FirstName != null && x.FirstName.Contains(input.Keyword)) ||
                    (x.LastName != null && x.LastName.Contains(input.Keyword)) ||
                    (x.Email != null && x.Email.Contains(input.Keyword)) ||
                    (x.Phone != null && x.Phone.Contains(input.Keyword)) ||
                    (x.Notes != null && x.Notes.Contains(input.Keyword)))
                .WhereIf(!input.FirstName.IsNullOrWhiteSpace(), x => x.FirstName != null && x.FirstName.Contains(input.FirstName))
                .WhereIf(!input.LastName.IsNullOrWhiteSpace(), x => x.LastName != null && x.LastName.Contains(input.LastName))
                .WhereIf(!input.Email.IsNullOrWhiteSpace(), x => x.Email != null && x.Email.Contains(input.Email))
                .WhereIf(!input.Phone.IsNullOrWhiteSpace(), x => x.Phone != null && x.Phone.Contains(input.Phone))
                .WhereIf(!input.Notes.IsNullOrWhiteSpace(), x => x.Notes != null && x.Notes.Contains(input.Notes))
                .WhereIf(input.BirthDate.HasValue, x => x.BirthDate == input.BirthDate.Value)
                .WhereIf(input.MembershipStartDate.HasValue, x => x.MembershipStartDate == input.MembershipStartDate.Value)
                .WhereIf(input.MembershipEndDate.HasValue, x => x.MembershipEndDate == input.MembershipEndDate.Value)
                .WhereIf(input.MembershipStatus.HasValue, x => x.MembershipStatus == (MembershipStatus)input.MembershipStatus.Value)
                .WhereIf(input.TotalSessions.HasValue, x => x.TotalSessions == input.TotalSessions.Value)
                .WhereIf(input.UsedSessions.HasValue, x => x.UsedSessions == input.UsedSessions.Value);
        }
    }
}
