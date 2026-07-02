using System;
using Xunit;
using FluentAssertions;
using InstructorApp.Entities;

namespace InstructorApp.Tests.Instructors
{
    public class InstructorEntityTests
    {
        [Fact]
        public void Instructor_ShouldBeCreatable()
        {
            // Act
            var entity = new Instructor();

            // Assert
            entity.Should().NotBeNull();
        }

        [Fact]
        public void Instructor_ShouldHaveDefaultValues()
        {
            // Act
            var entity = new Instructor();

            // Assert
            entity.Id.Should().Be(default(long));
            entity.IsActive.Should().Be(false);
        }

        [Fact]
        public void Instructor_FirstName_ShouldAcceptValue()
        {
            var entity = new Instructor { FirstName = "Test Value" };
            entity.FirstName.Should().Be("Test Value");
        }

        [Fact]
        public void Instructor_LastName_ShouldAcceptValue()
        {
            var entity = new Instructor { LastName = "Test Value" };
            entity.LastName.Should().Be("Test Value");
        }

        [Fact]
        public void Instructor_Email_ShouldAcceptValue()
        {
            var entity = new Instructor { Email = "Test Value" };
            entity.Email.Should().Be("Test Value");
        }

    }
}
