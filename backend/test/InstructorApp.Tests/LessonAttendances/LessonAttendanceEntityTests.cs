using System;
using Xunit;
using FluentAssertions;
using InstructorApp.Entities;

namespace InstructorApp.Tests.LessonAttendances
{
    public class LessonAttendanceEntityTests
    {
        [Fact]
        public void LessonAttendance_ShouldBeCreatable()
        {
            // Act
            var entity = new LessonAttendance();

            // Assert
            entity.Should().NotBeNull();
        }

        [Fact]
        public void LessonAttendance_ShouldHaveDefaultValues()
        {
            // Act
            var entity = new LessonAttendance();

            // Assert
            entity.Id.Should().Be(default(long));
            entity.Attended.Should().Be(false);
            entity.SessionConsumed.Should().Be(false);
        }


    }
}
