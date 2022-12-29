using Discord.Commands;
using Discord.Interactions;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using MySqlConnector;

namespace DiscordBot
{
    class Program
    {

        private DiscordSocketClient _client; // The client is the main class for the bot. 
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
            } catch (Exception ex)
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
                    //.AddSingleton<CommandHandlingService>() // This is the class we will create to handle commands.
                    //.AddSingleton<LoggingService>() // This is the class we will create to handle logging.
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
            //provider.GetRequiredService<LoggingService>();
            //provider.GetRequiredService<DCSHandler>();

            var sCommands = provider.GetRequiredService<InteractionService>();
            //await provider.GetRequiredService<InteractionHandler>().InitializeAsync();

            _client.Ready += _client_Ready;

            // Sign into the Discord client.
            await _client.LoginAsync(TokenType.Bot, token);
            // Start the client
            await _client.StartAsync();


            // Wait infinitely so the bot doesn't close.
            await Task.Delay(-1);
        }

        private Task _client_Ready()
        {
            throw new NotImplementedException();
        }
    }
}