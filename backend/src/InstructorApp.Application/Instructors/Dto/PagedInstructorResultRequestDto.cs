using System;
using Abp.Application.Services.Dto;

namespace InstructorApp.Instructors.Dto
{
    public class PagedInstructorResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Specialization { get; set; }
        public bool? IsActive { get; set; }
    }
}
