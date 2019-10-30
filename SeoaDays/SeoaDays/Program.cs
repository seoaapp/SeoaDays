using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace SeoaDays
{
    class Program
    {
      DiscordSocketClient client = new DiscordSocketClient();
      public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();
        public async Task MainAsync()
        {
           string token = System.IO.File.ReadAllText("token.txt");
            foreach (char t in token)
            {
                token = token.Replace("\n", string.Empty);
            }
            await client.LoginAsync(TokenType.Bot, token); 
            await client.StartAsync();
            client.MessageReceived += Client_MessageReceived;
            client.Log += Client_Log;
            client.Ready += Client_Ready;
            client.GuildAvailable += Client_GuildAvailable;
            await Task.Delay(-1);
        }

        async Task Client_MessageReceived(SocketMessage message)
        {
            Console.WriteLine(message);
        }

        Task Client_Log(LogMessage arg)
        {
            Console.WriteLine(arg.Message);
            return Task.CompletedTask;
        }

        Task Client_Ready()
        {
            return Task.CompletedTask;
        }

        Task Client_GuildAvailable(SocketGuild arg)
        {
            arg.DefaultChannel.SendMessageAsync("");
            return Task.CompletedTask;
        }

    }
}
