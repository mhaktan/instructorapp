using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using InstructorApp.Members.Dto;

namespace InstructorApp.Members
{
    public interface IMemberAppService : IAsyncCrudAppService<
        MemberDto,
        long,
        PagedMemberResultRequestDto,
        CreateMemberDto,
        MemberDto>
    {
    }
}
