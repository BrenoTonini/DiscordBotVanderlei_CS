using DSharpPlus.Entities;
using OpenAI_API;
using OpenAI_API.Completions;

namespace DiscordBotVanderlei.Events
{
    public class WhenMentioned
    {
        
        public static async Task ReplyUser(DiscordMessage message, string openAi_APIToken) 
        {
            if (message.Content == null || message.Author == null || message.Channel == null) { return; }

            string prompt = message.Content;
            DiscordChannel channel = message.Channel;
            DiscordUser user = message.Author;
            
            try
            {
                OpenAIAPI _openAIAPI = new OpenAIAPI(openAi_APIToken);
                string generatedResponse = "";

                CompletionRequest completionRequest = new CompletionRequest();
                completionRequest.Temperature = 0.6;
                completionRequest.Prompt = message.Content;
                completionRequest.MaxTokens = 100;
                completionRequest.Model = OpenAI_API.Models.Model.DefaultModel;

                var completions = _openAIAPI.Completions.CreateCompletionAsync(completionRequest);

                foreach (var completion in completions.Result.Completions)
                {
                    generatedResponse += completion.Text;
                }

                string fullResponse = $"<@{user.Id}> {generatedResponse}";
                await message.Channel.SendMessageAsync(fullResponse);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calling OpenAI API: {ex.Message}");
                await message.Channel.SendMessageAsync("Não to conseguindo pensar por agora, me pergunta de novo mais tarde.");
            }
        }
    }
}
