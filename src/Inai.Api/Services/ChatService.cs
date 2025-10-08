using Inai.Api.Data;
using Inai.Core.Models;
using Microsoft.EntityFrameworkCore;
using OpenAI;
using OpenAI.Chat;
using System.Data;

namespace Inai.Api.Services;

public class ChatService
{
    private readonly InaiDbContext _db;
    private readonly OpenAIClient _openAI;

    public ChatService(InaiDbContext db, IConfiguration config)
    {
        _db = db;
        var apiKey = config["OpenAI:ApiKey"];
        _openAI = new OpenAIClient(apiKey);
    }

    public async Task<IEnumerable<ChatMessage>> GetChatHistoryAsync(Guid userId) =>
        await _db.ChatMessages
                 .Where(c => c.UserId == userId)
                 .OrderBy(c => c.CreatedAt)
                 .ToListAsync();

    public async Task<ChatMessage> SendMessageAsync(Guid userId, string message, bool isFromAI = false)
    {
        var chat = new ChatMessage
        {
            UserId = userId,
            Message = message,
            IsFromAI = isFromAI
        };
        _db.ChatMessages.Add(chat);
        await _db.SaveChangesAsync();
        return chat;
    }

    public async Task<ChatMessage> GetAIResponseAsync(Guid userId, string userMessage)
    {
        // get previous 5 messages to maintain context
        var history = await _db.ChatMessages
            .Where(c => c.UserId == userId)
            .OrderBy(c => c.CreatedAt)
            .TakeLast(5)
            .ToListAsync();

        var messages = new List<ChatRequestMessage>();

        foreach (var msg in history)
        {
            messages.Add(new ChatRequestMessage(
                msg.IsFromAI ? Role.Assistant : Role.User,
                msg.Message));
        }

        // include the current user message
        messages.Add(new ChatRequestMessage(Role.User, userMessage));

        var response = await _openAI.Chat.GetCompletionAsync(
            model: "gpt-4o-mini",
            messages: messages
        );

        var aiText = response.Content.FirstOrDefault()?.Text ?? "I'm not sure how to respond.";

        return await SendMessageAsync(userId, aiText, true);
    }
}
