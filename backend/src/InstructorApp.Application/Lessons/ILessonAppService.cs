using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using InstructorApp.Lessons.Dto;

namespace InstructorApp.Lessons
{
    public interface ILessonAppService : IAsyncCrudAppService<
        LessonDto,
        long,
        PagedLessonResultRequestDto,
        CreateLessonDto,
        LessonDto>
    {
    }
}
