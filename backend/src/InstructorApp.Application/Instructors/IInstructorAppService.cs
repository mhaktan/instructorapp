using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using InstructorApp.Instructors.Dto;

namespace InstructorApp.Instructors
{
    public interface IInstructorAppService : IAsyncCrudAppService<
        InstructorDto,
        long,
        PagedInstructorResultRequestDto,
        CreateInstructorDto,
        InstructorDto>
    {
    }
}
