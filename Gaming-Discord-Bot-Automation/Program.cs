using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySqlConnector;

namespace DiscordBot
{
    class Program
    {

        private DiscordSocketClient _client;
        private string token;
        private string connectionString = CS.ConnectionString;

        static void Main(string[] args)
        {
            Program program = new Program();
            program.MainAsync().Wait();
        }

        async Task MainAsync()
        {

            using var mysqlconnection = new MySqlConnection(connectionString);
            try
            {
                mysqlconnection.Open();
                //MessageBox.Show("Connection successful!");
                // We want to read the table "discord" and get the "token" and "game" columns
                string query = "SELECT * FROM discord";
                MySqlCommand command;
                using (command = new MySqlCommand(query, mysqlconnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            token = reader.GetString(1);
                        }
                    }
                }
                mysqlconnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            using IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                services
                .AddSingleton(x => new DiscordSocketClient(new DiscordSocketConfig
                {
                    LogLevel = LogSeverity.Verbose,
                    MessageCacheSize = 1000,
                    GatewayIntents = Discord.GatewayIntents.All, // We can reduce this later when we know what we need.
                    UseInteractionSnowflakeDate = false
                }))
                .AddSingleton(new CommandService(new CommandServiceConfig
                {
                    LogLevel = LogSeverity.Info,
                    DefaultRunMode = Discord.Commands.RunMode.Async,
                    CaseSensitiveCommands = false
                }))
                .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
                .AddSingleton<InteractionHandler>()
                .AddSingleton<PlayerCountHandler>()
                //.AddSingleton<CommandHandlingService>() // This is the class we will create to handle commands.
                .AddSingleton<LoggingService>() // This is the class we will create to handle logging.
                //.AddSingleton<DCSHandler>()
                ).Build();

            await RunAsync(host);
        }

        private async Task RunAsync(IHost host)
        {
            using IServiceScope serviceScope = host.Services.CreateScope(); // This is the scope for the services.
            IServiceProvider provider = serviceScope.ServiceProvider;

            _client = provider.GetRequiredService<DiscordSocketClient>();
            // This is the method we will create to initialize the command handling service.
            //await provider.GetRequiredService<CommandHandlingService>().InitializeAsync();
            // This is the method we will create to initialize the logging service.
            provider.GetRequiredService<LoggingService>();
            provider.GetRequiredService<PlayerCountHandler>();

            var sCommands = provider.GetRequiredService<InteractionService>();
            //await provider.GetRequiredService<InteractionHandler>().InitializeAsync();

            //_client.Ready += _client_Ready;

            // Sign into the Discord client.
            await _client.LoginAsync(TokenType.Bot, token);
            // Start the client
            await _client.StartAsync();

            _ = Task.Run(() => UpdateDatabaseInformation());


            // Wait infinitely so the bot doesn't close.
            await Task.Delay(-1);
        }

        private void UpdateDatabaseInformation()
        {
            while (true)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT " +
                        "g.game_key, g.game_name, " +
                        "n.server_ip, n.server_port, n.query_port_send, n.query_port_receive, n.query_port_two_way, " +
                        "u.enable_updater, u.updater_path, u.updater_arguments, u.root_path, u.application_path, u.steam_game, u.server_version, u.latest_version, u.update_lock, u.force_update, u.dedicated_acknowledgement, dedicated_acknowledgement_date, dedicated_completed_update, " +
                        "dr.role, " +
                        "dn.admin_channel, dn.notify_admins, dn.user_notification_channel, dn.notify_users, dn.player_count_channel, dn.update_player_count, " +
                        "pc.online_players, pc.last_updated, pc.most_online, pc.most_online_date " +
                        "FROM games g " +
                        "JOIN updater u ON g.game_key = u.game_key " +
                        "JOIN discord_notification dn ON g.game_key = dn.game_key " +
                        "JOIN discord_roles dr ON g.game_key = dr.game_key " +
                        "JOIN networking n ON g.game_key = n.game_key " +
                        "JOIN player_count pc ON g.game_key = pc.game_key ";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GameDataModel gameData = new GameDataModel();
                                gameData.Id = reader.GetInt32(0);
                                gameData.GameName = reader.GetString(1);
                                gameData.ServerIP = reader.GetString(2);
                                gameData.ServerPort = reader.GetInt32(3);
                                gameData.QuerySendPort = reader.GetInt32(4);
                                gameData.QueryReceivePort = reader.GetInt32(5);
                                gameData.TwoWayPort = reader.GetInt32(6);
                                gameData.EnableUpdater = (reader.GetInt32(7) == 1);
                                gameData.UpdaterPath = reader.GetString(8);
                                gameData.UpdaterArguments = reader.GetString(9);
                                gameData.RootPath = reader.GetString(10);
                                gameData.ApplicationPath = reader.GetString(11);
                                gameData.IsSteamGame = (reader.GetInt32(12) == 1);
                                gameData.ServerVersion = reader.GetString(13);
                                gameData.LatestVersion = reader.GetString(14);
                                gameData.UpdateLock = (reader.GetInt32(15) == 1);
                                gameData.ForceUpdate = (reader.GetInt32(16) == 1);
                                gameData.DedicatedAcknowledge = (reader.GetInt32(17) == 1);
                                gameData.DedicatedAcknowledgeDate = reader.GetDateTime(18);
                                gameData.DedicatedCompletedUpdated = reader.GetDateTime(19);
                                gameData.Staff = reader.GetString(20).Split(",").ToList();
                                gameData.AdminChannel = reader.GetString(21);
                                gameData.NotifyAdmins = (reader.GetInt32(22) == 1);
                                gameData.UserNotificationChannel = reader.GetString(23);
                                gameData.NotifyUsers = (reader.GetInt32(24) == 1);
                                gameData.PlayerCountChannel = reader.GetString(25);
                                gameData.UpdatePlayerCount = (reader.GetInt32(26) == 1);
                                gameData.PlayerCount = reader.GetInt32(27);
                                gameData.LastUpdated = reader.GetDateTime(28);
                                gameData.MostOnline = reader.GetInt32(29);
                                gameData.DateOfMostOnline = reader.GetDateTime(30);
                                // We need to check if the game is already in the list, if it is, we need to update the data, if not, we need to add it.
                                if (GameDataModel.GameData.Any(x => x.Id == gameData.Id))
                                {
                                    // We need to update the data.
                                    GameDataModel gameDataToUpdate = GameDataModel.GameData.FirstOrDefault(x => x.Id == gameData.Id);
                                    gameDataToUpdate.Id = gameData.Id;
                                    gameDataToUpdate.GameName = gameData.GameName;
                                    gameDataToUpdate.ServerIP = gameData.ServerIP;
                                    gameDataToUpdate.ServerPort = gameData.ServerPort;
                                    gameDataToUpdate.QuerySendPort = gameData.QuerySendPort;
                                    gameDataToUpdate.QueryReceivePort = gameData.QueryReceivePort;
                                    gameDataToUpdate.TwoWayPort = gameData.TwoWayPort;
                                    gameDataToUpdate.EnableUpdater = gameData.EnableUpdater;
                                    gameDataToUpdate.UpdaterPath = gameData.UpdaterPath;
                                    gameDataToUpdate.UpdaterArguments = gameData.UpdaterArguments;
                                    gameDataToUpdate.RootPath = gameData.RootPath;
                                    gameDataToUpdate.ApplicationPath = gameData.ApplicationPath;
                                    gameDataToUpdate.IsSteamGame = gameData.IsSteamGame;
                                    gameDataToUpdate.ServerVersion = gameData.ServerVersion;
                                    gameDataToUpdate.LatestVersion = gameData.LatestVersion;
                                    gameDataToUpdate.UpdateLock = gameData.UpdateLock;
                                    gameDataToUpdate.ForceUpdate = gameData.ForceUpdate;
                                    gameDataToUpdate.DedicatedAcknowledge = gameData.DedicatedAcknowledge;
                                    gameDataToUpdate.DedicatedAcknowledgeDate = gameData.DedicatedAcknowledgeDate;
                                    gameDataToUpdate.DedicatedCompletedUpdated = gameData.DedicatedCompletedUpdated;
                                    gameDataToUpdate.Staff = gameData.Staff;
                                    gameDataToUpdate.AdminChannel = gameData.AdminChannel;
                                    gameDataToUpdate.NotifyAdmins = gameData.NotifyAdmins;
                                    gameDataToUpdate.UserNotificationChannel = gameData.UserNotificationChannel;
                                    gameDataToUpdate.NotifyUsers = gameData.NotifyUsers;
                                    gameDataToUpdate.PlayerCountChannel = gameData.PlayerCountChannel;
                                    gameDataToUpdate.UpdatePlayerCount = gameData.UpdatePlayerCount;
                                    gameDataToUpdate.PlayerCount = gameData.PlayerCount;
                                    gameDataToUpdate.LastUpdated = gameData.LastUpdated;
                                    gameDataToUpdate.MostOnline = gameData.MostOnline;
                                    gameDataToUpdate.DateOfMostOnline = gameData.DateOfMostOnline;
                                }
                                else
                                {
                                    // We need to add the data.
                                    GameDataModel.GameData.Add(gameData);
                                }
                            }
                        }
                    }
                    connection.Close();
                }
                Task.Delay(10000).Wait();
            }
        }

        //private Task _client_Ready()
        //{
        //    //throw new NotImplementedException();
        //}
    }
}