using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace InstructorApp.Members.Dto
{
    [AutoMapFrom(typeof(Entities.Member))]
    public class MemberDto : EntityDto<long>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime MembershipStartDate { get; set; }

        public DateTime? MembershipEndDate { get; set; }

        public int MembershipStatus { get; set; }

        public int TotalSessions { get; set; }

        public int UsedSessions { get; set; }

        public string Notes { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}