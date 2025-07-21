using Microsoft.EntityFrameworkCore;
using PulseChat.Application.Interfaces;
using PulseChat.Domain.Entities;
using PulseChat.Persistence.Context;
using System;

namespace PulseChat.Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly AppDbContext _context;

    public MessageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Message?> GetByIdAsync(Guid id)
    {
        return await _context.Messages
            .Include(m => m.User)
            .Include(m => m.Group)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<Message>> GetMessagesByGroupIdAsync(Guid groupId)
    {
        return await _context.Messages
            .Where(m => m.GroupId == groupId)
            .Include(m => m.User)
            .OrderBy(m => m.SentAt)
            .ToListAsync();
    }

    public async Task AddAsync(Message message)
    {
        await _context.Messages.AddAsync(message);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Message message)
    {
        _context.Messages.Update(message);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var message = await _context.Messages.FindAsync(id);
        if (message != null)
        {
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<(List<Message> messages, int totalCount)> SearchMessagesAsync(Guid groupId, string? keyword, int page, int pageSize)
    {
        var query = _context.Messages
            .Where(m => m.GroupId == groupId);

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(m => m.Content.Contains(keyword));
        }

        var totalCount = await query.CountAsync();

        var messages = await query
            .OrderByDescending(m => m.SentAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(m => m.User)
            .ToListAsync();

        return (messages, totalCount);
    }



}
