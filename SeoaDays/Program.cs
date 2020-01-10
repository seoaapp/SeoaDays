using System;
using System.IO;
using System.Xml.Linq;
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
            //Get token (토큰 얻기)
           string token = System.IO.File.ReadAllText("token.txt");
           Console.WriteLine(token);
            //Login (로그인)
            await client.LoginAsync(TokenType.Bot, token); 
            await client.StartAsync();
            client.MessageReceived += Client_MessageReceived;
            client.Log += Client_Log;
            client.Ready += Client_Ready;
            client.GuildAvailable += Client_GuildAvailable;
            await Task.Delay(-1);
        }

        async Task Client_MessageReceived(SocketMessage msg)
        {
            if (!msg.Author.IsBot)
            {
                string message = msg.Content;
                string[] spacing = message.Split(' ');
                if (message[0] == '=') //Prefix of SeoaDays is '=' (SeoaDays의 접두사는 '=')
                {
                    AddUser(msg.Author.Id);
                    ScheduleEN scheduleEN = new ScheduleEN();
                    ScheduleKO scheduleKO = new ScheduleKO();
                    if (spacing[0] == "=schedule")
                    {
                        if (spacing[1] == "add")
                        {
                            //=schedule add [Name] [Date] [Content]
                            string content = null;
                            int i = 0;
                            foreach (string count in spacing)
                            {
                                if (i >= 4)
                                {
                                    content += count;
                                }
                                i++;
                            }
                            string add = scheduleEN.ScheduleAdd(msg.Author.Id, spacing[2], spacing[3], content);
                            await msg.Channel.SendMessageAsync(add);
                        }
                    }
                    else if (spacing[0] == "=일정")
                    {
                        if (spacing[1] == "추가")
                        {
                            //=일정 추가 [이름] [날짜] [내용]
                            string content = null;
                            int i = 0;
                            foreach (string count in spacing)
                            {
                                if (i >= 4)
                                {
                                    content += count;
                                }
                                i++;
                            }
                            string add = scheduleKO.ScheduleAdd(msg.Author.Id, spacing[2], spacing[3], content);
                            await msg.Channel.SendMessageAsync(add);
                        }
                    }
                }
            }
        }

        void AddUser(ulong id)
        {
            XDocument xdoc = new XDocument(new XDeclaration("1.0", "UTF-8", null));
            try
            {
                xdoc = XDocument.Load("data.xml");
            }
            catch
            {
                XElement root = new XElement("data");
                xdoc.Add(root);
            }
            if (xdoc.Root.Element("_" + id.ToString()) == null)
            {
                XElement newUser = new XElement("_" + id.ToString(), "");
                xdoc.Root.Add(newUser);
                xdoc.Save("data.xml");
            }
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
