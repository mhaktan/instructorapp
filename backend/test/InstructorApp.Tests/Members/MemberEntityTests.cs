using System;
using Xunit;
using FluentAssertions;
using InstructorApp.Entities;

namespace InstructorApp.Tests.Members
{
    public class MemberEntityTests
    {
        [Fact]
        public void Member_ShouldBeCreatable()
        {
            // Act
            var entity = new Member();

            // Assert
            entity.Should().NotBeNull();
        }

        [Fact]
        public void Member_ShouldHaveDefaultValues()
        {
            // Act
            var entity = new Member();

            // Assert
            entity.Id.Should().Be(default(long));

        }

        [Fact]
        public void Member_FirstName_ShouldAcceptValue()
        {
            var entity = new Member { FirstName = "Test Value" };
            entity.FirstName.Should().Be("Test Value");
        }

        [Fact]
        public void Member_LastName_ShouldAcceptValue()
        {
            var entity = new Member { LastName = "Test Value" };
            entity.LastName.Should().Be("Test Value");
        }

    }
}
