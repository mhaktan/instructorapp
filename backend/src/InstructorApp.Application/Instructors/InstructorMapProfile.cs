using AutoMapper;
using InstructorApp.Entities;
using InstructorApp.Instructors.Dto;

namespace InstructorApp.Instructors
{
    public class InstructorMapProfile : Profile
    {
        public InstructorMapProfile()
        {
            CreateMap<Instructor, InstructorDto>();
            CreateMap<CreateInstructorDto, Instructor>();
            CreateMap<InstructorDto, Instructor>();
        }
    }
}
