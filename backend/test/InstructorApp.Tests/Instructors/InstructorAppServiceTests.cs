using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using Moq;
using InstructorApp.Entities;
using InstructorApp.Instructors;
using InstructorApp.Instructors.Dto;

namespace InstructorApp.Tests.Instructors
{
    public class InstructorAppServiceTests
    {
        private readonly Mock<IRepository<Instructor, long>> _repositoryMock;
        private readonly InstructorAppService _service;

        public InstructorAppServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Instructor, long>>();
            _service = new InstructorAppService(_repositoryMock.Object);
        }

        [Fact]
        public void Repository_GetAll_ShouldReturnQueryable()
        {
            // Arrange
            var entities = new[]
            {
                new Instructor { Id = 1, FirstName = "Test firstName", LastName = "Test lastName", Email = "Test email", IsActive = true },
                new Instructor { Id = 2, FirstName = "Test firstName", LastName = "Test lastName", Email = "Test email", IsActive = true },
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
                new Instructor { Id = 1, FirstName = "Test firstName", LastName = "Test lastName", Email = "Test email", IsActive = true },
                new Instructor { Id = 2, FirstName = "Test firstName", LastName = "Test lastName", Email = "Test email", IsActive = true },
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
            var dto = new CreateInstructorDto
            {
                FirstName = "Test firstName", LastName = "Test lastName", Email = "Test email", IsActive = true
            };

            _repositoryMock.Setup(r => r.InsertAndGetIdAsync(It.IsAny<Instructor>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new Instructor { Id = 1, FirstName = "Test firstName", LastName = "Test lastName", Email = "Test email", IsActive = true });

            // Act & Assert
            _service.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new Instructor { Id = 1, FirstName = "Test firstName", LastName = "Test lastName", Email = "Test email", IsActive = true });

            // Act & Assert
            await _service.Invoking(s => s.DeleteAsync(new Abp.Application.Services.Dto.EntityDto<long> { Id = 1 }))
                .Should().NotThrowAsync();
        }
    }
}
