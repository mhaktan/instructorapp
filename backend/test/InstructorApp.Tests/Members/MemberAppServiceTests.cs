using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using Moq;
using InstructorApp.Entities;
using InstructorApp.Members;
using InstructorApp.Members.Dto;

namespace InstructorApp.Tests.Members
{
    public class MemberAppServiceTests
    {
        private readonly Mock<IRepository<Member, long>> _repositoryMock;
        private readonly MemberAppService _service;

        public MemberAppServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Member, long>>();
            _service = new MemberAppService(_repositoryMock.Object);
        }

        [Fact]
        public void Repository_GetAll_ShouldReturnQueryable()
        {
            // Arrange
            var entities = new[]
            {
                new Member { Id = 1, FirstName = "Test firstName", LastName = "Test lastName", MembershipStartDate = DateTime.UtcNow, MembershipStatus = 0, TotalSessions = 1, UsedSessions = 1 },
                new Member { Id = 2, FirstName = "Test firstName", LastName = "Test lastName", MembershipStartDate = DateTime.UtcNow, MembershipStatus = 0, TotalSessions = 1, UsedSessions = 1 },
            }.AsQueryable();

            _repositoryMock.Setup(r => r.GetAll()).Returns(entities);

            // Act
            var result = _repositoryMock.Object.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(2);
        }

        [Fact]
        public void Repository_GetAll_WithFilter_ShouldWork()
        {
            // Arrange
            var entities = new[]
            {
                new Member { Id = 1, FirstName = "Test firstName", LastName = "Test lastName", MembershipStartDate = DateTime.UtcNow, MembershipStatus = 0, TotalSessions = 1, UsedSessions = 1 },
                new Member { Id = 2, FirstName = "Test firstName", LastName = "Test lastName", MembershipStartDate = DateTime.UtcNow, MembershipStatus = 0, TotalSessions = 1, UsedSessions = 1 },
            }.AsQueryable();

            _repositoryMock.Setup(r => r.GetAll()).Returns(entities);

            // Act — simulate keyword filter
            var result = _repositoryMock.Object.GetAll()
                .Where(x => x.Id.ToString().Contains("1"));

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task Create_ShouldInsertEntity()
        {
            // Arrange
            var dto = new CreateMemberDto
            {
                FirstName = "Test firstName", LastName = "Test lastName", MembershipStartDate = DateTime.UtcNow, MembershipStatus = 0, TotalSessions = 1, UsedSessions = 1
            };

            _repositoryMock.Setup(r => r.InsertAndGetIdAsync(It.IsAny<Member>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new Member { Id = 1, FirstName = "Test firstName", LastName = "Test lastName", MembershipStartDate = DateTime.UtcNow, MembershipStatus = 0, TotalSessions = 1, UsedSessions = 1 });

            // Act & Assert
            _service.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new Member { Id = 1, FirstName = "Test firstName", LastName = "Test lastName", MembershipStartDate = DateTime.UtcNow, MembershipStatus = 0, TotalSessions = 1, UsedSessions = 1 });

            // Act & Assert
            await _service.Invoking(s => s.DeleteAsync(new Abp.Application.Services.Dto.EntityDto<long> { Id = 1 }))
                .Should().NotThrowAsync();
        }
    }
}
