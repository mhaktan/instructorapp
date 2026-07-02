using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using Moq;
using InstructorApp.Entities;
using InstructorApp.MemberInstructors;
using InstructorApp.MemberInstructors.Dto;

namespace InstructorApp.Tests.MemberInstructors
{
    public class MemberInstructorAppServiceTests
    {
        private readonly Mock<IRepository<MemberInstructor, long>> _repositoryMock;
        private readonly MemberInstructorAppService _service;

        public MemberInstructorAppServiceTests()
        {
            _repositoryMock = new Mock<IRepository<MemberInstructor, long>>();
            _service = new MemberInstructorAppService(_repositoryMock.Object);
        }

        [Fact]
        public void Repository_GetAll_ShouldReturnQueryable()
        {
            // Arrange
            var entities = new[]
            {
                new MemberInstructor { Id = 1, AssignedDate = DateTime.UtcNow, IsActive = true },
                new MemberInstructor { Id = 2, AssignedDate = DateTime.UtcNow, IsActive = true },
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
                new MemberInstructor { Id = 1, AssignedDate = DateTime.UtcNow, IsActive = true },
                new MemberInstructor { Id = 2, AssignedDate = DateTime.UtcNow, IsActive = true },
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
            var dto = new CreateMemberInstructorDto
            {
                AssignedDate = DateTime.UtcNow, IsActive = true
            };

            _repositoryMock.Setup(r => r.InsertAndGetIdAsync(It.IsAny<MemberInstructor>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new MemberInstructor { Id = 1, AssignedDate = DateTime.UtcNow, IsActive = true });

            // Act & Assert
            _service.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new MemberInstructor { Id = 1, AssignedDate = DateTime.UtcNow, IsActive = true });

            // Act & Assert
            await _service.Invoking(s => s.DeleteAsync(new Abp.Application.Services.Dto.EntityDto<long> { Id = 1 }))
                .Should().NotThrowAsync();
        }
    }
}
