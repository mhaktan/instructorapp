using System;
using Xunit;
using FluentAssertions;
using InstructorApp.Entities;

namespace InstructorApp.Tests.MemberInstructors
{
    public class MemberInstructorEntityTests
    {
        [Fact]
        public void MemberInstructor_ShouldBeCreatable()
        {
            // Act
            var entity = new MemberInstructor();

            // Assert
            entity.Should().NotBeNull();
        }

        [Fact]
        public void MemberInstructor_ShouldHaveDefaultValues()
        {
            // Act
            var entity = new MemberInstructor();

            // Assert
            entity.Id.Should().Be(default(long));
            entity.IsActive.Should().Be(false);
        }


    }
}
