using AutoMapper;
using InstructorApp.Entities;
using InstructorApp.MemberInstructors.Dto;

namespace InstructorApp.MemberInstructors
{
    public class MemberInstructorMapProfile : Profile
    {
        public MemberInstructorMapProfile()
        {
            CreateMap<MemberInstructor, MemberInstructorDto>();
            CreateMap<CreateMemberInstructorDto, MemberInstructor>();
            CreateMap<MemberInstructorDto, MemberInstructor>();
        }
    }
}
