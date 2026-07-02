using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using InstructorApp.LessonAttendances.Dto;

namespace InstructorApp.LessonAttendances
{
    public interface ILessonAttendanceAppService : IAsyncCrudAppService<
        LessonAttendanceDto,
        long,
        PagedLessonAttendanceResultRequestDto,
        CreateLessonAttendanceDto,
        LessonAttendanceDto>
    {
    }
}
