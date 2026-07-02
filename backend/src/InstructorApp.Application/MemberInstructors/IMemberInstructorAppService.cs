using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using InstructorApp.MemberInstructors.Dto;

namespace InstructorApp.MemberInstructors
{
    public interface IMemberInstructorAppService : IAsyncCrudAppService<
        MemberInstructorDto,
        long,
        PagedMemberInstructorResultRequestDto,
        CreateMemberInstructorDto,
        MemberInstructorDto>
    {
    }
}
