using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Inai.App.Config;
using Inai.Core.Models;
using OpenAI;
using OpenAI.Chat;
using System.Collections.ObjectModel;
using System.Data;

namespace Inai.App.ViewModels
{
    public partial class ChatViewModel : ObservableObject
    {
        private readonly OpenAIClient _client;

        [ObservableProperty]
        private string _userMessage = string.Empty;

        public ObservableCollection<ChatMessage> Messages { get; } = new();

        public ChatViewModel()
        {
            _client = new OpenAIClient(Secrets.OpenAIKey);
        }

        [RelayCommand]
        private async Task SendMessageAsync()
        {
            if (string.IsNullOrWhiteSpace(UserMessage))
                return;

            // Add user message
            var userChat = new ChatMessage
            {
                Message = UserMessage,
                IsFromAI = false,
                CreatedAt = DateTime.UtcNow
            };
            Messages.Add(userChat);

            var input = UserMessage;
            UserMessage = string.Empty;

            // Show temporary "AI is typing..." message
            var aiTyping = new ChatMessage
            {
                Message = "Typing...",
                IsFromAI = true
            };
            Messages.Add(aiTyping);

            try
            {
                // Create a chat completion request
                var chatResponse = await _client.Chat.GetCompletionAsync(
                    model: "gpt-4o-mini",
                    messages: new[]
                    {
                        new ChatRequestMessage(Role.User, input)
                    }
                );

                // Remove "Typing..."
                Messages.Remove(aiTyping);

                // Add AI response
                var responseText = chatResponse.Content[0].Text;
                var aiChat = new ChatMessage
                {
                    Message = responseText,
                    IsFromAI = true,
                    CreatedAt = DateTime.UtcNow
                };
                Messages.Add(aiChat);
            }
            catch (Exception ex)
            {
                Messages.Remove(aiTyping);
                Messages.Add(new ChatMessage
                {
                    Message = $"Error: {ex.Message}",
                    IsFromAI = true
                });
            }
        }
    }
}
