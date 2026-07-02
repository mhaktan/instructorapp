using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace InstructorApp.Entities
{
    [Table("MemberInstructors")]
    public class MemberInstructor : FullAuditedEntity<long>
    {
        public DateTime AssignedDate { get; set; }

        public bool IsActive { get; set; }

        public long InstructorId { get; set; }

        [ForeignKey(nameof(InstructorId))]
        public virtual Instructor Instructor { get; set; }

        public long MemberId { get; set; }

        [ForeignKey(nameof(MemberId))]
        public virtual Member Member { get; set; }

    }
}