using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace InstructorApp.Entities
{
    [Table("Members")]
    public class Member : FullAuditedEntity<long>
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

        public MembershipStatus MembershipStatus { get; set; }

        public int TotalSessions { get; set; }

        public int UsedSessions { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        public virtual ICollection<MemberInstructor> MemberInstructors { get; set; }

        public virtual ICollection<LessonAttendance> LessonAttendances { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }

    }
}