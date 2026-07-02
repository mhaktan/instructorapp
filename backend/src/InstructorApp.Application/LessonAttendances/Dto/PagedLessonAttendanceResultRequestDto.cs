using System;
using Abp.Application.Services.Dto;

namespace InstructorApp.LessonAttendances.Dto
{
    public class PagedLessonAttendanceResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public long? LessonId { get; set; }
        public long? MemberId { get; set; }
        public bool? Attended { get; set; }
        public bool? SessionConsumed { get; set; }
        public string AttendanceNote { get; set; }
    }
}
