using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class LoggingService
    {
        public LoggingService(DiscordSocketClient client, CommandService command, InteractionService interaction)
        {
            client.Log += LogAsync;
            command.Log += LogAsync;
            interaction.Log += LogAsync;

        }

        internal void Log(string username, string message)
        {
            Console.WriteLine(System.DateTime.Now + " " + username + " " + message);
        }

        private Task LogAsync(LogMessage arg)
        {
            //Console.WriteLine(System.DateTime.Now + " " + arg.Message);
            using (StreamWriter sw = File.AppendText($"Logs\\Log_{DateTime.UtcNow.ToString("MM-dd-yyyy")}.log"))
            {
                sw.WriteLine($"{DateTime.Now} : {arg.Message}");
                Console.WriteLine($"{DateTime.Now} : {arg.Message}");
            }
            return Task.CompletedTask;
        }


    }
}
