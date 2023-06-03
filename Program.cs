using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace lohostaria
{
    class Program
    {
        private readonly DiscordSocketClient _client;
        static void Main(string[] args)
            => new Program()
                .MainAsync()
                .GetAwaiter()
                .GetResult();

        public Program()
        {
            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
            };
            _client = new DiscordSocketClient(config);
            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;
            _client.MessageReceived += MessageReceivedAsync;
            _client.InteractionCreated += InteractionCreatedAsync;
        }

        public async Task MainAsync()
        {
            string token = File.ReadAllText("token.txt");
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await Task.Delay(Timeout.Infinite);
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }
        private Task ReadyAsync()
        {
            Console.WriteLine($"{_client.CurrentUser} is connected!");

            return Task.CompletedTask;
        }
        private async Task MessageReceivedAsync(SocketMessage message)
        {
            string prefix = "l.";

            // The bot should never respond to itself.
            if (message.Author.Id == _client.CurrentUser.Id)
                return;
            /*
            if (message.Content == prefix + "")
            {
                await message.Channel.SendMessageAsync("");
            }
            */

            if (message.Content == prefix + "ping")
            {
                // Create a new ComponentBuilder, in which dropdowns & buttons can be created.
                /*
                var cb = new ComponentBuilder()
                    .WithButton("text", "id", ButtonStyle.Primary);
                */
                await message.Channel.SendMessageAsync("pong!" /*, components: cb.Build()*/);
            }



            if (message.Content == prefix + "help")
            {
                await message.Channel.SendMessageAsync("I'm sorry I'm currently in development");
            }
            if (message.Content == prefix + "whoami")
            {
                await message.Channel.SendMessageAsync((message.Author).ToString());
            }
            if (message.Content == prefix + "project-detail")
            {
                await message.Channel.SendMessageAsync("This project is my hobby project of creating a bot");
            }
        }
        private async Task InteractionCreatedAsync(SocketInteraction interaction)
        {
            /*
            if (interaction is SocketMessageComponent component)
            {
                if (component.Data.CustomId == "id")
                {
                    await interaction.RespondAsync("respond");
                }

                else
                    Console.WriteLine("An ID has been received that has no handler!");
            }
            */
        }
    }
}