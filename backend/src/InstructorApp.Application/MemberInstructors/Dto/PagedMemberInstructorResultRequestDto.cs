using System;
using Abp.Application.Services.Dto;

namespace InstructorApp.MemberInstructors.Dto
{
    public class PagedMemberInstructorResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public long? InstructorId { get; set; }
        public long? MemberId { get; set; }
        public DateTime? AssignedDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
