using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    internal class PlayerCountHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly InteractionService _commands;
        private readonly IServiceProvider _services;
        private const string serverOnlineEmoji = "\u2705";
        private const string serverOfflineEmoji = "\u274C";
        public PlayerCountHandler(DiscordSocketClient client, InteractionService commands, IServiceProvider services)
        {
            _client = client;
            _commands = commands;
            _services = services;
            _client.Ready += _client_Ready;
        }

        private Task _client_Ready()
        {
            while (true)
            {
                GetPlayerCount();
                Task.Delay(600000).Wait();
            }
        }

        private void GetPlayerCount()
        {
            string title = string.Empty;
            foreach (GameDataModel game in GameDataModel.GameData)
            {
                if (game.UpdatePlayerCount)
                {
                    if (DateTime.UtcNow.AddMinutes(-5) > game.LastUpdated)
                    {
                        // We will asssume that the server is down if it has not updated in the last 5 minutes.
                        title = $"{serverOfflineEmoji} {game.GameName} - Offline";
                    } else
                    {
                        if (game.PlayerCount == 0)
                        {
                            title = $"{serverOnlineEmoji} {game.GameName} - 0 Players";
                        }
                        else if (game.PlayerCount == 1)
                        {
                            title = $"{serverOnlineEmoji} {game.GameName} - 1 Player";
                        } else
                        {
                            title = $"{serverOnlineEmoji} {game.GameName} - {game.PlayerCount} Players";
                        }
                    }
                    var voiceChannel = _client.GetChannel(ulong.Parse(game.PlayerCountChannel)) as SocketVoiceChannel;
                    if (voiceChannel.Name != title)
                    {
                        // We need to update the title.
                        _ = voiceChannel.ModifyAsync(x =>
                        {
                            x.Name = title;
                        });
                    }
                }
            }
        }
    }
}
