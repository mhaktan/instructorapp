using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace InstructorApp.Entities
{
    [Table("Lessons")]
    public class Lesson : FullAuditedEntity<long>
    {
        public DateTime LessonDate { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        [MaxLength(5)]
        public string StartTime { get; set; }

        [MaxLength(5)]
        public string EndTime { get; set; }

        [MaxLength(100)]
        public string LessonType { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        public long InstructorId { get; set; }

        [ForeignKey(nameof(InstructorId))]
        public virtual Instructor Instructor { get; set; }

        public virtual ICollection<LessonAttendance> LessonAttendances { get; set; }

    }
}