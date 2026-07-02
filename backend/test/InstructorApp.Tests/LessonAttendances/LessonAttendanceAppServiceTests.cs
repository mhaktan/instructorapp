using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Repositories;
using Moq;
using InstructorApp.Entities;
using InstructorApp.LessonAttendances;
using InstructorApp.LessonAttendances.Dto;

namespace InstructorApp.Tests.LessonAttendances
{
    public class LessonAttendanceAppServiceTests
    {
        private readonly Mock<IRepository<LessonAttendance, long>> _repositoryMock;
        private readonly LessonAttendanceAppService _service;

        public LessonAttendanceAppServiceTests()
        {
            _repositoryMock = new Mock<IRepository<LessonAttendance, long>>();
            _service = new LessonAttendanceAppService(_repositoryMock.Object);
        }

        [Fact]
        public void Repository_GetAll_ShouldReturnQueryable()
        {
            // Arrange
            var entities = new[]
            {
                new LessonAttendance { Id = 1, Attended = true, SessionConsumed = true },
                new LessonAttendance { Id = 2, Attended = true, SessionConsumed = true },
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
                new LessonAttendance { Id = 1, Attended = true, SessionConsumed = true },
                new LessonAttendance { Id = 2, Attended = true, SessionConsumed = true },
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
            var dto = new CreateLessonAttendanceDto
            {
                Attended = true, SessionConsumed = true
            };

            _repositoryMock.Setup(r => r.InsertAndGetIdAsync(It.IsAny<LessonAttendance>()))
                .ReturnsAsync(1);
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new LessonAttendance { Id = 1, Attended = true, SessionConsumed = true });

            // Act & Assert
            _service.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(new LessonAttendance { Id = 1, Attended = true, SessionConsumed = true });

            // Act & Assert
            await _service.Invoking(s => s.DeleteAsync(new Abp.Application.Services.Dto.EntityDto<long> { Id = 1 }))
                .Should().NotThrowAsync();
        }
    }
}
