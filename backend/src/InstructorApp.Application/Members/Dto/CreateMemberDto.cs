using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace InstructorApp.Members.Dto
{
    [AutoMapTo(typeof(Entities.Member))]
    public class CreateMemberDto
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(256)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime MembershipStartDate { get; set; }

        public DateTime? MembershipEndDate { get; set; }

        public int MembershipStatus { get; set; }

        public int TotalSessions { get; set; }

        public int UsedSessions { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

    }
}