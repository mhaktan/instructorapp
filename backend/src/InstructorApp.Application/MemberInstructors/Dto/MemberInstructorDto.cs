using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace InstructorApp.MemberInstructors.Dto
{
    [AutoMapFrom(typeof(Entities.MemberInstructor))]
    public class MemberInstructorDto : EntityDto<long>
    {
        public DateTime AssignedDate { get; set; }

        public bool IsActive { get; set; }

        public long InstructorId { get; set; }

        public long MemberId { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}