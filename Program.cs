using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDcBot
{
    class Program
    {
        static async Task Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();
        private string _botPrefix;
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();
            _botPrefix ="/";
            string botToken = "NjY0MDEzODg5ODYzODExMDcy.XhReXg.Uyg81VnaX1-Mqh1AbAdeZRYS-LU";

            //event subscription
            _client.Log += Log;

            await RegisterCommandsAsync();

            await _client.LoginAsync(Discord.TokenType.Bot, botToken);
            await _client.StartAsync();
            await Task.Delay(-1);

        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(),_services);
        }
        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message is null || message.Author.IsBot) return;
            int argPos=0;
            if (message.HasStringPrefix(_botPrefix,ref argPos) || message.HasMentionPrefix(_client.CurrentUser,ref argPos))
            {
                var context = new SocketCommandContext(_client, message);
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            } 
        }

        private string getConfig(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }

        
    }
    
}
