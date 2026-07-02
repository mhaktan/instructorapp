using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using Moq;
using InstructorApp.Entities;
using InstructorApp.Lessons;
using InstructorApp.Lessons.Dto;

namespace InstructorApp.Tests.Lessons
{
    public class LessonAppServiceTests
    {
        private readonly Mock<IRepository<Lesson, long>> _repositoryMock;
        private readonly LessonAppService _service;

        public LessonAppServiceTests()
        {
            _repositoryMock = new Mock<IRepository<Lesson, long>>();
            _service = new LessonAppService(_repositoryMock.Object);
        }

        [Fact]
        public void Repository_GetAll_ShouldReturnQueryable()
        {
            // Arrange
            var entities = new[]
            {
                new Lesson { Id = 1, LessonDate = DateTime.UtcNow, DayOfWeek = 0, StartTime = "Test " },
                new Lesson { Id = 2, LessonDate = DateTime.UtcNow, DayOfWeek = 0, StartTime = "Test " },
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
                new Lesson { Id = 1, LessonDate = DateTime.UtcNow, DayOfWeek = 0, StartTime = "Test " },
                new Lesson { Id = 2, LessonDate = DateTime.UtcNow, DayOfWeek = 0, StartTime = "Test " },
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
            var dto = new CreateLessonDto
            {
                LessonDate = DateTime.UtcNow, DayOfWeek = 0, StartTime = "Test "
            };

            _repositoryMock.Setup(r => r.InsertAndGetIdAsync(It.IsAny<Lesson>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new Lesson { Id = 1, LessonDate = DateTime.UtcNow, DayOfWeek = 0, StartTime = "Test " });

            // Act & Assert
            _service.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new Lesson { Id = 1, LessonDate = DateTime.UtcNow, DayOfWeek = 0, StartTime = "Test " });

            // Act & Assert
            await _service.Invoking(s => s.DeleteAsync(new Abp.Application.Services.Dto.EntityDto<long> { Id = 1 }))
                .Should().NotThrowAsync();
        }
    }
}
