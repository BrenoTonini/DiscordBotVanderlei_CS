using DiscordBotVanderlei.Commands.SlashCommands;
using DiscordBotVanderlei.Config;
using DiscordBotVanderlei.Events;
using DSharpPlus.SlashCommands;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus;
using System.Threading.Tasks;

namespace DiscordBotVanderlei
{
    class Program
    {
        private static DiscordClient Client { get; set; }
        private static CommandsNextExtension Commands { get; set; }
        private static string openAI_APIToken;


        static async Task Main(string[] args)
        {
            JSONReader jsonReader = new JSONReader();
            await jsonReader.ReadJSON();

            openAI_APIToken = jsonReader.GetOpenAIToken();

            DiscordConfiguration discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.GetDiscordToken(),
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(discordConfig);

            Client.Ready += Client_Ready;
            Client.MessageCreated += Client_MessageCreated;

            CommandsNextConfiguration commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { jsonReader.GetPrefix() },
                EnableDms = true,
                EnableDefaultHelp = false,
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            SlashCommandsExtension SlashCommandsConfig = Client.UseSlashCommands();

            SlashCommandsConfig.RegisterCommands<Escolha>();

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }

        private static async Task Client_MessageCreated(DiscordClient sender, DSharpPlus.EventArgs.MessageCreateEventArgs args)
        {
            if (args.Message.MentionedUsers.Any(u => u.Id == sender.CurrentUser.Id))
            {
                await WhenMentioned.ReplyUser(args.Message, openAI_APIToken);
            }
        }
    }
}
