using System;
using Abp.Application.Services.Dto;

namespace InstructorApp.Lessons.Dto
{
    public class PagedLessonResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public long? InstructorId { get; set; }
        public DateTime? LessonDate { get; set; }
        public int? DayOfWeek { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string LessonType { get; set; }
        public string Notes { get; set; }
    }
}
