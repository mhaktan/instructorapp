using AutoMapper;
using InstructorApp.Entities;
using InstructorApp.LessonAttendances.Dto;

namespace InstructorApp.LessonAttendances
{
    public class LessonAttendanceMapProfile : Profile
    {
        public LessonAttendanceMapProfile()
        {
            CreateMap<LessonAttendance, LessonAttendanceDto>();
            CreateMap<CreateLessonAttendanceDto, LessonAttendance>();
            CreateMap<LessonAttendanceDto, LessonAttendance>();
        }
    }
}
