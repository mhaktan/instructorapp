using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace InstructorApp.MemberInstructors.Dto
{
    [AutoMapTo(typeof(Entities.MemberInstructor))]
    public class CreateMemberInstructorDto
    {
        public DateTime AssignedDate { get; set; }

        public bool IsActive { get; set; }

        public long InstructorId { get; set; }

        public long MemberId { get; set; }

    }
}