using System;
using Abp.Application.Services.Dto;

namespace InstructorApp.Members.Dto
{
    public class PagedMemberResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? MembershipStartDate { get; set; }
        public DateTime? MembershipEndDate { get; set; }
        public int? MembershipStatus { get; set; }
        public int? TotalSessions { get; set; }
        public int? UsedSessions { get; set; }
        public string Notes { get; set; }
    }
}
