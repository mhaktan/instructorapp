using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace InstructorApp.LessonAttendances.Dto
{
    [AutoMapFrom(typeof(Entities.LessonAttendance))]
    public class LessonAttendanceDto : EntityDto<long>
    {
        public bool Attended { get; set; }

        public bool SessionConsumed { get; set; }

        public string AttendanceNote { get; set; }

        public long LessonId { get; set; }

        public long MemberId { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}