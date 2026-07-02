using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace InstructorApp.Lessons.Dto
{
    [AutoMapTo(typeof(Entities.Lesson))]
    public class CreateLessonDto
    {
        public DateTime LessonDate { get; set; }

        public int DayOfWeek { get; set; }

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

    }
}