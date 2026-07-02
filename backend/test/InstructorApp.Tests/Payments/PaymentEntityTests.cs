using System;
using Xunit;
using FluentAssertions;
using InstructorApp.Entities;

namespace InstructorApp.Tests.Payments
{
    public class PaymentEntityTests
    {
        [Fact]
        public void Payment_ShouldBeCreatable()
        {
            // Act
            var entity = new Payment();

            // Assert
            entity.Should().NotBeNull();
        }

        [Fact]
        public void Payment_ShouldHaveDefaultValues()
        {
            // Act
            var entity = new Payment();

            // Assert
            entity.Id.Should().Be(default(long));

        }


    }
}
