using DigitalCombatSimulator.DataModel;
using MySqlConnector;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace DiscordBotAutomation.DigitalCombatSimulator
{
    public class Program
    {
        private static readonly string connectionString = CS.ConnectionString;
        public static void Main()
        {
            // We need to check if the bot has been configured
            // For this, we will store a file inside the working directory called "bot_configured"
            if (System.IO.File.Exists("bot_configuration"))
                GameConfiguration.GetServerSettings();
            else
                GameConfiguration.ConfigureDCSAutomation();

            Task.Run(() => UpdateHandler.HandleUpdateChecks());
            //_ = UpdateHandler.HandleUpdateChecks();
            // Testing
            Console.WriteLine(GameConfiguration.gameData.GameName);
            Console.WriteLine(GameConfiguration.gameData.Id);

            Task.Run(() => PlayerCountHandler.UpdatePlayerCount());

            Console.ReadLine();
        }
        
        
    }
}
