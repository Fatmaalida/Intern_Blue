using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PulseChat.Domain.Entities;

using PulseChat.Application.DTOs;

namespace PulseChat.Application.Interfaces;

public interface IMessageService
{
    Task<List<MessageDto>> GetMessagesByGroupIdAsync(Guid groupId);
    Task<MessageDto> CreateAsync(MessageCreateDto dto);
    Task<MessageDto> UpdateMessageAsync(Guid messageId, string newContent);
    //Task<bool> DeleteMessageAsync(Guid messageId);
    Task<bool> DeleteMessageAsync(Guid messageId, Guid senderId);
    Task<PagedResultDto<MessageDto>> SearchMessagesAsync(Guid groupId, string? searchTerm, int page, int pageSize);
    //Task<PagedResultDto<MessageDto>> SearchMessagesAsync(Guid groupId, string? keyword, int page, int pageSize);




}

