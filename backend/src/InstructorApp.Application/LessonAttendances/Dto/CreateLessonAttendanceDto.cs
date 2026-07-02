using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace InstructorApp.LessonAttendances.Dto
{
    [AutoMapTo(typeof(Entities.LessonAttendance))]
    public class CreateLessonAttendanceDto
    {
        public bool Attended { get; set; }

        public bool SessionConsumed { get; set; }

        [MaxLength(300)]
        public string AttendanceNote { get; set; }

        public long LessonId { get; set; }

        public long MemberId { get; set; }

    }
}