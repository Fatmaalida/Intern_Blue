using PulseChat.Application.DTOs;
using PulseChat.Application.Interfaces;
using PulseChat.Domain.Entities;

namespace PulseChat.Application.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;

    public MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
    }

    public async Task<List<MessageDto>> GetMessagesByGroupIdAsync(Guid groupId)
    {
        var messages = await _messageRepository.GetMessagesByGroupIdAsync(groupId);
        return messages
            .Where(m => !m.IsDeleted) // Soft delete filtrelemesi
            .Select(m => new MessageDto
            {
                Id = m.Id,
                Content = m.Content,
                SentAt = m.SentAt,
                UserName = m.User?.UserName ?? "Unknown",
                FileUrl = m.FileUrl
            }).ToList();
    }

    public async Task<MessageDto> CreateAsync(MessageCreateDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user == null) throw new Exception("User not found.");

        var message = new Message
        {
            Content = dto.Content,
            GroupId = dto.GroupId,
            UserId = dto.UserId,
            SentAt = DateTime.UtcNow,
            FileUrl = dto.FileUrl
        };

        await _messageRepository.AddAsync(message);

        return new MessageDto
        {
            Id = message.Id,
            Content = message.Content,
            SentAt = message.SentAt,
            UserName = user.UserName,
            FileUrl = message.FileUrl
        };
    }

    public async Task<MessageDto> UpdateMessageAsync(Guid messageId, string newContent)
    {
        var message = await _messageRepository.GetByIdAsync(messageId);
        if (message == null || message.IsDeleted)
            throw new Exception("Message not found or deleted.");

        message.Content = newContent;
        message.UpdatedAt = DateTime.UtcNow;

        await _messageRepository.UpdateAsync(message);

        var user = await _userRepository.GetByIdAsync(message.UserId);

        return new MessageDto
        {
            Id = message.Id,
            Content = message.Content,
            SentAt = message.SentAt,
            UserName = user?.UserName ?? "Unknown",
            FileUrl = message.FileUrl
        };
    }

    public async Task<bool> DeleteMessageAsync(Guid messageId, Guid senderId)
    {
        var message = await _messageRepository.GetByIdAsync(messageId);
        if (message == null || message.UserId != senderId)
            return false;

        message.IsDeleted = true;
        message.DeletedAt = DateTime.UtcNow;

        await _messageRepository.UpdateAsync(message); // Soft delete
        return true;
    }

    public async Task<PagedResultDto<MessageDto>> SearchMessagesAsync(Guid groupId, string? searchTerm, int page, int pageSize)
    {
        var allMessages = await _messageRepository.GetMessagesByGroupIdAsync(groupId);

        var filtered = string.IsNullOrWhiteSpace(searchTerm)
            ? allMessages
            : allMessages.Where(m => m.Content.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

        var paged = filtered
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PagedResultDto<MessageDto>
        {
            Items = paged.Select(m => new MessageDto
            {
                Id = m.Id,
                Content = m.Content,
                SentAt = m.SentAt,
                UserName = m.User?.UserName ?? "Unknown"
            }).ToList(),
            TotalCount = filtered.Count,
            Page = page,
            PageSize = pageSize
        };

        return result;
    }



}
