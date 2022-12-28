using DigitalCombatSimulator.DataModel;
using MySqlConnector;

namespace DiscordBotAutomation.DigitalCombatSimulator
{
    internal class GameConfiguration
    {

        private static string connectionString = CS.ConnectionString;
        public static GameDataModel gameData = new GameDataModel();

        internal static void ConfigureDCSAutomation()
        {
            // We need to configure the bot.
            // All information is stored in a SQL database.
            // Since this is the initial run, we will assume the user hasn't configured the information within the SQL database.
            Console.WriteLine("This is the initial setup...");
            Console.WriteLine("Please make sure to build the database priror to continuing.");
            Console.WriteLine("-------------------------------------------------------------");

            // Game Databse
            Console.WriteLine("Please enter the Game Name.");
            string gameName = ValidateString("Please enter a game name.");

            // Networking Database
            Console.WriteLine("Please enter the server IP. This can be the IP (192.168.1.1) or a DNS name (dcs.tfpgaming.com)");
            string serverIP = ValidateString("Please enter an IP Address.");

            Console.WriteLine("Please enter the port that the server runs on.");
            int serverPort = ValidateInt("Please enter a port.");

            Console.WriteLine("Please enter the port to send information on.");
            int sendPort = ValidateInt("Please enter a port.");

            Console.WriteLine("Please enter the port for receiving data.");
            int receivePort = ValidateInt("Please enter a port.");

            Console.WriteLine("Please enter the port that two way communication happens on.");
            int twoWayPort = ValidateInt("Please enter a port for two way communication.");

            // Updater Databse
            Console.WriteLine("Do you want to automatically update the server?");
            bool autoUpdate = ValidateBool("Please confirm if you want to automatically update the dedicated server.");
            string updaterPath;
            if (autoUpdate)
            {
                Console.WriteLine("Please enter the updater path. Example: C:\\Temp");
                updaterPath = ValidateString("Please enter a path");
            }
            else
            {
                updaterPath = "Not Needed.";
            }


            // Roles
            Console.WriteLine("Please enter roles who should have the ability to perform admin features.");
            Console.WriteLine("An example is to restart the server, or force an update.");
            Console.WriteLine("Example: admins,owners,moderators,dcs admins");
            string authorizedAdmins = ValidateString("Enter admin roles.");

            // Discord Notifications
            Console.WriteLine("Should we notify admins in the event their is an error?");
            bool notifyAdmins = ValidateBool("Notify Admins?");

            string adminChannel;
            if (notifyAdmins)
            {
                Console.WriteLine("Enter the admin channel you wish to use.");
                adminChannel = ValidateString("Enter the admin channel for notifications.");
            }
            else
            {
                adminChannel = "Not Needed";
            }

            Console.WriteLine("Should we notify users about important information (updates, restarts etc)?");
            bool notifyUsers = ValidateBool("Notify users?");

            string userChannel;
            if (notifyUsers)
            {
                Console.WriteLine("Enter the user channel you wish to use.");
                userChannel = ValidateString("Enter the user channel for notifications.");
            }
            else
            {
                userChannel = "Not Needed";
            }

            Console.WriteLine("Should update player count?");
            bool updatePlayerCount = ValidateBool("Update player count?");

            string playerCountChannel;
            if (updatePlayerCount)
            {
                Console.WriteLine("Enter the channel you wish to use to update player count.");
                playerCountChannel = ValidateString("Enter the user channel for the player count.");
            }
            else
            {
                playerCountChannel = "Not Needed";
            }

            long gameKey;
            // Build the SQL database.

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Update the game
                using (var command = new MySqlCommand("INSERT INTO games(game_name) VALUES(@game_name)", connection))
                {
                    command.Parameters.AddWithValue("@game_name", gameName);
                    command.ExecuteNonQuery();
                    gameKey = command.LastInsertedId;
                }

                // Networking Information
                using (var command = new MySqlCommand("INSERT INTO networking(game_key,server_ip,server_port,query_port_send,query_port_receive,query_port_two_way) VALUES(@game_key,@server_ip,@server_port,@query_port_send,@query_port_receive,@query_port_two_way)", connection))
                {
                    command.Parameters.AddWithValue("@game_key", gameKey);
                    command.Parameters.AddWithValue("@server_ip", serverIP);
                    command.Parameters.AddWithValue("@server_port", serverPort);
                    command.Parameters.AddWithValue("@query_port_send", sendPort);
                    command.Parameters.AddWithValue("@query_port_receive", receivePort);
                    command.Parameters.AddWithValue("@query_port_two_way", twoWayPort);
                    command.ExecuteNonQuery();
                }

                // Player Count
                using (var command = new MySqlCommand("INSERT INTO player_count(game_key,online_players,last_updated,most_online,most_online_date) VALUES(@game_key,@online_players,@last_updated,@most_online,@most_online_date)", connection))
                {
                    command.Parameters.AddWithValue("@game_key", gameKey);
                    command.Parameters.AddWithValue("@online_players", 0);
                    command.Parameters.AddWithValue("@last_updated", DateTime.UtcNow);
                    command.Parameters.AddWithValue("@most_online", 0);
                    command.Parameters.AddWithValue("@most_online_date", DateTime.UtcNow);
                    command.ExecuteNonQuery();
                }

                // Updater
                using (var command = new MySqlCommand("INSERT INTO updater(game_key,enable_updater,updater_path,updater_arguments,root_path,application_path,steam_game,server_version,latest_version,update_lock,force_update,dedicated_acknowledgement,dedicated_acknowledgement_date,dedicated_completed_update) VALUES(@game_key,@enable_updater,@updater_path,@updater_arguments,@root_path,@application_path,@steam_game,@server_version,@latest_version,@update_lock,@force_update,@dedicated_acknowledgement,@dedicated_acknowledgement_date,@dedicated_completed_update)", connection))
                {
                    int enableUpdateCommand;
                    int steamGameCommand = 0;
                    int updateLockCommand = 0;
                    int forceUpdateLock = 0;
                    int dedicatedAcknowledgementCommand = 0;

                    if (autoUpdate)
                        enableUpdateCommand = 1;
                    else
                        enableUpdateCommand = 0;


                    command.Parameters.AddWithValue("@game_key", gameKey);
                    command.Parameters.AddWithValue("@enable_updater", enableUpdateCommand);
                    command.Parameters.AddWithValue("@updater_path", updaterPath);
                    command.Parameters.AddWithValue("@updater_arguments", "Update");
                    command.Parameters.AddWithValue("@root_path", "C:\\Temp\\Temp");
                    command.Parameters.AddWithValue("@application_path", "C:\\Temp\\Temp\\DCS.exe");
                    command.Parameters.AddWithValue("@steam_game", "0");
                    command.Parameters.AddWithValue("@server_version", "0.0");
                    command.Parameters.AddWithValue("@latest_version", "0.0");
                    command.Parameters.AddWithValue("@update_lock", "0");
                    command.Parameters.AddWithValue("@force_update", "0");
                    command.Parameters.AddWithValue("@dedicated_acknowledgement", "0");
                    command.Parameters.AddWithValue("@dedicated_acknowledgement_date", DateTime.UtcNow);
                    command.Parameters.AddWithValue("@dedicated_completed_update", DateTime.UtcNow);
                    command.ExecuteNonQuery();
                }

                // Discord Roles
                using (var command = new MySqlCommand("INSERT INTO discord_roles(game_key,role) VALUES(@game_key,@role)", connection))
                {
                    command.Parameters.AddWithValue("@game_key", gameKey);
                    command.Parameters.AddWithValue("@role", authorizedAdmins);
                    command.ExecuteNonQuery();
                }

                // Notificaitons
                using (var command = new MySqlCommand("INSERT INTO discord_notification(game_key,admin_channel,notify_admins,user_notification_channel,notify_users,player_count_channel,update_player_count) VALUES(@game_key,@admin_channel,@notify_admins,@user_notification_channel,@notify_users,@player_count_channel,@update_player_count)", connection))
                {

                    int notifyAdminsCommand = 0;
                    int notifyUsersCommand = 0;
                    int updatePlayerCountCommand = 0;
                    if (notifyAdmins)
                        notifyAdminsCommand = 1;
                    if (notifyUsers)
                        notifyUsersCommand = 1;
                    if (updatePlayerCount)
                        updatePlayerCountCommand = 1;


                    command.Parameters.AddWithValue("@game_key", gameKey);
                    command.Parameters.AddWithValue("@admin_channel", adminChannel);
                    command.Parameters.AddWithValue("@notify_admins", notifyAdminsCommand);
                    command.Parameters.AddWithValue("@user_notification_channel", userChannel);
                    command.Parameters.AddWithValue("@notify_users", notifyUsersCommand);
                    command.Parameters.AddWithValue("@player_count_channel", playerCountChannel);
                    command.Parameters.AddWithValue("@update_player_count", updatePlayerCountCommand);
                    command.ExecuteNonQuery();
                }

                string text = gameKey.ToString();
                string filePath = "bot_configuration";

                File.WriteAllText(filePath, text);

                connection.Close();
            }



        }

        private static string ValidateString(string instructions)
        {
            string userInput = Console.ReadLine();
            while (userInput == null || userInput == "")
            {
                Console.WriteLine("You've entered invalid text. Please try again.");
                Console.WriteLine(instructions);
                userInput = Console.ReadLine();
            }
            return userInput;
        }

        private static int ValidateInt(string instructions)
        {
            int num;
            while (true)
            {
                Console.WriteLine("Enter an integer:");
                string input = Console.ReadLine();

                if (Int32.TryParse(input, out num))
                {
                    // input is a valid integer, break out of the loop
                    break;
                }

                Console.WriteLine(instructions);
            }
            return Convert.ToInt32(num);
        }

        private static bool ValidateBool(string instructions)
        {
            bool flag;
            while (true)
            {
                Console.WriteLine("Enter a boolean value (true, false, yes, no, y, or n):");
                string input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "true":
                    case "yes":
                    case "y":
                        flag = true;
                        break;
                    case "false":
                    case "no":
                    case "n":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        continue;
                }

                // flag now contains the boolean value entered by the user
                break;
            }
            return flag;
        }

        internal static void GetServerSettings()
        {
            // We need to read the config to get our ID.
            string key_id = File.ReadAllLines("bot_configuration").First();

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
                    "JOIN player_count pc ON g.game_key = pc.game_key " +
                    "WHERE g.game_key = @game_key ";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@game_key", key_id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
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
                                gameData.PlayerCount = reader.GetInt32(27);
                                gameData.LastUpdated = reader.GetDateTime(28);
                                gameData.MostOnline = reader.GetInt32(29);
                                gameData.DateOfMostOnline = reader.GetDateTime(30);
                            }
                        } else
                        {
                            Console.WriteLine("We couldn't find the information inside of SQL.");
                            Console.WriteLine("Please validate that everything is setup correctly.");
                            Console.WriteLine("If you would like ot reconfigure the bot, please delete the bot_configuration file.");
                            Console.WriteLine("I'm to lazy to make a proper uninstaller.");
                        }
                    }
                }

            }
        }
    }
}