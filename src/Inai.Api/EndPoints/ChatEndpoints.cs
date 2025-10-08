using Inai.Api.Services;

namespace Inai.Api.Endpoints;

public static class ChatEndpoints
{
    public static void MapChat(this WebApplication app)
    {
        app.MapGet("/chat/{userId:guid}", async (Guid userId, ChatService service) =>
            Results.Ok(await service.GetChatHistoryAsync(userId)));

        app.MapPost("/chat/send", async (Guid userId, string message, ChatService service) =>
        {
            var userMessage = await service.SendMessageAsync(userId, message);
            var aiResponse = await service.GetAIResponseAsync(userId, message);
            return Results.Ok(new { userMessage, aiResponse });
        });
    }
}
