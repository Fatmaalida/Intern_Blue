using Xunit;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moq;
using Xunit;
using PulseChat.Application.Services;
using PulseChat.Application.Interfaces;
using PulseChat.Application.DTOs;
using PulseChat.Domain.Entities;
using System;
using System.Threading.Tasks;


public class MessageServiceTests
{
    private readonly Mock<IMessageRepository> _messageRepoMock;
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly MessageService _service;

    public MessageServiceTests()
    {
        _messageRepoMock = new();
        _userRepoMock = new();
        _service = new MessageService(_messageRepoMock.Object, _userRepoMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrow_WhenUserNotFound()
    {
        // Arrange
        var dto = new MessageCreateDto { UserId = Guid.NewGuid(), GroupId = Guid.NewGuid(), Content = "test" };
        _userRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.CreateAsync(dto));
    }
}


