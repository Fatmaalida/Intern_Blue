using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PulseChat.Domain.Entities;

namespace PulseChat.Application.Interfaces;

public interface IMessageRepository
{
    Task<Message?> GetByIdAsync(Guid id);
    Task<List<Message>> GetMessagesByGroupIdAsync(Guid groupId);
    Task AddAsync(Message message);
    Task UpdateAsync(Message message);
    Task DeleteAsync(Guid id);
    Task<(List<Message> messages, int totalCount)> SearchMessagesAsync(Guid groupId, string? keyword, int page, int pageSize);

}
