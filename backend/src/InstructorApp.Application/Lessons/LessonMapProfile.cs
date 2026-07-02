using AutoMapper;
using InstructorApp.Entities;
using InstructorApp.Lessons.Dto;

namespace InstructorApp.Lessons
{
    public class LessonMapProfile : Profile
    {
        public LessonMapProfile()
        {
            CreateMap<Lesson, LessonDto>();
            CreateMap<CreateLessonDto, Lesson>();
            CreateMap<LessonDto, Lesson>();
        }
    }
}
