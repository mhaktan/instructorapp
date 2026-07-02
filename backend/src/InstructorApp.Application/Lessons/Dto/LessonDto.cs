using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace InstructorApp.Lessons.Dto
{
    [AutoMapFrom(typeof(Entities.Lesson))]
    public class LessonDto : EntityDto<long>
    {
        public DateTime LessonDate { get; set; }

        public int DayOfWeek { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string LessonType { get; set; }

        public string Notes { get; set; }

        public long InstructorId { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}