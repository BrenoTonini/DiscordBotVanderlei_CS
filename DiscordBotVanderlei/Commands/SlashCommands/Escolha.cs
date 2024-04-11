using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;


namespace DiscordBotVanderlei.Commands.SlashCommands
{
    internal class Escolha : ApplicationCommandModule
    {
        private static readonly Random randomChoice = new Random();

        [SlashCommand("escolha", "Pergunte ao vanderlei.")]
        public async Task Escolher(InteractionContext ctx, [Option("Opções", "Dê opções ao vanderlei.")]string text)
        {
            string[] delimiters = { "ou", ",", "?" };
            string[] words = text.ToLower().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length < 2)
            {
                string issueMessage = "Faz a pergunta direito mula";
                await ctx.Interaction.CreateResponseAsync(DSharpPlus.InteractionResponseType.ChannelMessageWithSource, new DSharpPlus.Entities.DiscordInteractionResponseBuilder().WithContent(issueMessage).AsEphemeral(true));
                return;
            }

            int choice = randomChoice.Next(0, words.Length);
            string chosenOption = words[choice].Trim();

            DiscordEmbedBuilder choiceMessage = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Blue,
                Title = ctx.Interaction.User.Username + " perguntou:",
                Description = $"{text} \n \n \n Eu escolho: **{chosenOption}**",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                {
                    Url = ctx.Interaction.User.AvatarUrl
                }
            };

            await ctx.CreateResponseAsync(choiceMessage);
            return;
        }
    }
}