using Microsoft.AspNetCore.Mvc;
using PulseChat.Application.DTOs;
using PulseChat.Application.Interfaces;

namespace PulseChat.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;
    private readonly IWebHostEnvironment _env;

    public MessageController(IMessageService messageService, IWebHostEnvironment env)
    {
        _messageService = messageService;
        _env = env;
    }

    // GET: api/message/group/{groupId}
    [HttpGet("group/{groupId}")]
    public async Task<IActionResult> GetMessagesByGroupId(Guid groupId)
    {
        var messages = await _messageService.GetMessagesByGroupIdAsync(groupId);
        return Ok(messages);
    }

    // POST: api/message
    [HttpPost]
    public async Task<IActionResult> CreateMessage([FromBody] MessageCreateDto dto)
    {
        var created = await _messageService.CreateAsync(dto);
        return Ok(created);
    }

    // POST: api/message/send-with-file
    [HttpPost("send-with-file")]
    public async Task<IActionResult> SendMessageWithFile([FromForm] Guid senderId,
                                                         [FromForm] Guid groupId,
                                                         [FromForm] string content,
                                                         [FromForm] IFormFile? file)
    {
        string? fileUrl = null;

        if (file != null)
        {
            var uploadsPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");
            Directory.CreateDirectory(uploadsPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            fileUrl = $"/uploads/{fileName}";
        }

        var dto = new MessageCreateDto
        {
            SenderId = senderId,
            GroupId = groupId,
            Content = content,
            FileUrl = fileUrl
        };

        var created = await _messageService.CreateAsync(dto);
        return Ok(created);
    }

    // PUT: api/message/{messageId}
    [HttpPut("{messageId}")]
    public async Task<IActionResult> EditMessage(Guid messageId, [FromBody] EditMessageDto dto)
    {
        var updated = await _messageService.UpdateMessageAsync(messageId, dto.NewContent);


        if (updated == null)
            return NotFound("Message not found or could not be updated.");

        return Ok(updated);
    }

    // DELETE: api/message/{messageId}?senderId={senderId}
    [HttpDelete("{messageId}")]
    public async Task<IActionResult> DeleteMessage(Guid messageId, [FromQuery] Guid senderId)
    {
        var success = await _messageService.DeleteMessageAsync(messageId, senderId);

        if (!success)
            return Forbid("You are not authorized to delete this message.");

        return NoContent();
    }

    // GET: api/message/search?groupId=...&searchTerm=...&pageNumber=1&pageSize=10
    [HttpGet("search")]
    public async Task<IActionResult> SearchMessages(
        [FromQuery] Guid groupId,
        [FromQuery] string? searchTerm,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _messageService.SearchMessagesAsync(groupId, searchTerm, pageNumber, pageSize);
        return Ok(result);
    }

}
