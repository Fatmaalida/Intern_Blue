using Xunit;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using PulseChat.Application.DTOs;
using PulseChat.Application.Interfaces;
using PulseChat.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PulseChat.Domain.Entities;


namespace PulseChat.Tests
{
    public class GroupServiceTests
    {
        private readonly Mock<IGroupRepository> _groupRepositoryMock;
        private readonly IGroupService _groupService;

        public GroupServiceTests()
        {
            _groupRepositoryMock = new Mock<IGroupRepository>();
            _groupService = new GroupService(_groupRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateGroupAsync_ShouldReturnCreatedGroup()
        {
            // Arrange
            var dto = new GroupCreateDto { Name = "Test Group" };
            var createdGroup = new Group { Id = Guid.NewGuid(), Name = dto.Name };

            _groupRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Group>()))
                .ReturnsAsync(createdGroup);

            // Act
            var result = await _groupService.CreateGroupAsync(dto);

            // Assert
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(createdGroup.Id, result.Id);
        }

        [Fact]
        public async Task GetAllGroupsAsync_ShouldReturnGroupList()
        {
            // Arrange
            var groups = new List<Group>
            {
                new Group { Id = Guid.NewGuid(), Name = "Group 1" },
                new Group { Id = Guid.NewGuid(), Name = "Group 2" }
            };

            _groupRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(groups);

            // Act
            var result = await _groupService.GetAllGroupsAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, g => g.Name == "Group 1");
        }
    }
}

