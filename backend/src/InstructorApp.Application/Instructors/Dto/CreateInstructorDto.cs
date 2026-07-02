using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace InstructorApp.Instructors.Dto
{
    [AutoMapTo(typeof(Entities.Instructor))]
    public class CreateInstructorDto
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(200)]
        public string Specialization { get; set; }

        public bool IsActive { get; set; }

    }
}