using AutoMapper;
using InstructorApp.Entities;
using InstructorApp.Members.Dto;

namespace InstructorApp.Members
{
    public class MemberMapProfile : Profile
    {
        public MemberMapProfile()
        {
            CreateMap<Member, MemberDto>();
            CreateMap<CreateMemberDto, Member>();
            CreateMap<MemberDto, Member>();
        }
    }
}
