using System;
using Xunit;
using FluentAssertions;
using InstructorApp.Entities;

namespace InstructorApp.Tests.Lessons
{
    public class LessonEntityTests
    {
        [Fact]
        public void Lesson_ShouldBeCreatable()
        {
            // Act
            var entity = new Lesson();

            // Assert
            entity.Should().NotBeNull();
        }

        [Fact]
        public void Lesson_ShouldHaveDefaultValues()
        {
            // Act
            var entity = new Lesson();

            // Assert
            entity.Id.Should().Be(default(long));

        }

        [Fact]
        public void Lesson_StartTime_ShouldAcceptValue()
        {
            var entity = new Lesson { StartTime = "Test Value" };
            entity.StartTime.Should().Be("Test Value");
        }

    }
}
