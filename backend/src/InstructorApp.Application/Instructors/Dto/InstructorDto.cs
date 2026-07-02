using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace InstructorApp.Instructors.Dto
{
    [AutoMapFrom(typeof(Entities.Instructor))]
    public class InstructorDto : EntityDto<long>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Specialization { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}