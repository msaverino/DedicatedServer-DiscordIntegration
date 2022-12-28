using DigitalCombatSimulator.DataModel;
using DiscordBotAutomation.DigitalCombatSimulator;
using HtmlAgilityPack;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotAutomation.DigitalCombatSimulator
{
    internal class UpdateHandler
    {
        public static void HandleUpdateChecks()
        {
            while (true)
            {
                int elapsedTime = 0;
                GetDedicatedServerVersion();
                GetLatestVersion();
                if (GameConfiguration.gameData.ServerVersion != GameConfiguration.gameData.LatestVersion)
                    PerformUpdate();
                while (elapsedTime <= 2160) // 6 hours
                {

                    string query = $"SELECT `force_update` FROM `updater` WHERE `game_key`={GameConfiguration.gameData.Id} ;";
                    try
                    {
                        using (var connection = new MySqlConnection(CS.ConnectionString))
                        {
                            connection.Open();
                            using (var command = new MySqlCommand(query, connection))
                            {
                                using (var reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            GameConfiguration.gameData.ForceUpdate = (reader.GetInt32(0) == 1);
                                        }
                                    }
                                }
                            }
                            connection.Close();
                        }
                    } catch
                    {
                        
                    }

                    if (GameConfiguration.gameData.ForceUpdate)
                        break;

                    Task.Delay(10000); // Wait 10 second
                    elapsedTime += 10;
                }
            }
        }
        private static void GetDedicatedServerVersion()
        {
            string configPath = GameConfiguration.gameData.RootPath + "\\autoupdate.cfg";
            // We need to check what version of DCS the server is currently running.
            if (File.Exists(configPath))
            {
                using (StreamReader sr = new StreamReader(configPath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("version"))
                        {
                            var lineArray = line.Split('"');
                            string serverVersion = lineArray[3];
                            // We need to check if the version matches our SQL database.
                            if (GameConfiguration.gameData.ServerVersion != serverVersion)
                            {
                                //Console.WriteLine("The server version has changed.");
                                GameConfiguration.gameData.ServerVersion = serverVersion;
                                // We need to update the SQL database.
                                string query = $"UPDATE `updater` SET `server_version`='{serverVersion}' WHERE `game_key`={GameConfiguration.gameData.Id};";
                                try
                                {
                                    using (var connection = new MySqlConnection(CS.ConnectionString))
                                    {
                                        connection.Open();

                                        // Update the game
                                        using (var command = new MySqlCommand(query, connection))
                                        {
                                            command.ExecuteNonQuery();
                                        }

                                        connection.Close();
                                    }
                                } catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                                
                            }
                             //= lineArray[3];
                            break;
                        }
                    }
                }
            }
        }

        private static void GetLatestVersion()
        {
            string dcsWebsite = "https://www.digitalcombatsimulator.com/en/news/changelog/openbeta";
            string dcsOfficialVersion;
            // Navigate to the website
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(dcsWebsite).Result;

            // Read the content
            string html = response.Content.ReadAsStringAsync().Result;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            //dcsOfficialVersionDate = doc.DocumentNode.Descendants("i").First(x => x.InnerText.Contains(".")).InnerText;
            //dcsOfficialVersionDate = dcsOfficialVersionDate.Split('.')[1] + "/" + dcsOfficialVersionDate.Split('.')[0] + "/" + dcsOfficialVersionDate.Split('.')[2];
            var parent = doc.DocumentNode.Descendants("i").First(x => x.InnerText.Contains(".")).ParentNode;
            var aTag = parent.Descendants("a").First(x => x.InnerText.Contains("Open Beta")).Attributes["href"];
            //dcsOfficialVersionURL = dcsWebsite + aTag.Value;
            var versionText = parent.Descendants("a").First(x => x.InnerText.Contains("Open Beta")).InnerText;
            dcsOfficialVersion = versionText.Split(' ')[1];

            if (dcsOfficialVersion != GameConfiguration.gameData.LatestVersion)
            {
                Console.WriteLine("The Official Version is not correct.");

                // Using the same connection string we used to connect to the database
                using (var mysqlconnection = new MySqlConnection(CS.ConnectionString))
                {
                    try
                    {
                        mysqlconnection.Open();
                        // We want to update the "discord" table and set the "token" and "game" columns to the values in the textboxes
                        string query = $"UPDATE `updater` SET `latest_version`='{dcsOfficialVersion}' WHERE  `game_key`={GameConfiguration.gameData.Id}";
                        using (var command = new MySqlCommand(query, mysqlconnection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }


                // We would want to perform some functions on a safe thread as we're going to have to wait a while here.

         

            }

        }

        private static void PerformUpdate()
        {
            // SAFE THREAD
            GameConfiguration.gameData.UpdateInProgress = true;
            // We want to perform a loop checking the player count every minute.
            // We will perform this for a maximum of 45 minutes before forcing the restart.

            int elapsedTime = 0;
            while (elapsedTime < 45)
            {
                string query = $"SELECT `dedicated_acknowledgement`, `force_update` FROM `updater` WHERE `game_key`={GameConfiguration.gameData.Id} ;";
                try
                {
                    using (var connection = new MySqlConnection(CS.ConnectionString))
                    {
                        connection.Open();
                        using (var command = new MySqlCommand(query, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        GameConfiguration.gameData.DedicatedAcknowledge = (reader.GetInt32(0) == 1);
                                        GameConfiguration.gameData.ForceUpdate = (reader.GetInt32(1) == 1);
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {

                }
                // Check if the player count is = 0.
                // Temporary.
                int playerCount = GameConfiguration.gameData.PlayerCount;
                // End Temporary.


                if ((1 >= playerCount && GameConfiguration.gameData.DedicatedAcknowledge) || GameConfiguration.gameData.ForceUpdate || (DateTime.UtcNow.AddMinutes(-5) > GameConfiguration.gameData.LastUpdated))
                {
                    break; // We want to exit the loop.
                }

                System.Threading.Thread.Sleep(120000); // Needs to be updated to two minutes.(120000)

                elapsedTime += 2;
            }

            // END SAFE THREAD

            //Console.WriteLine("We'll update here.");

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = GameConfiguration.gameData.UpdaterPath;
            startInfo.Arguments = "update " + GameConfiguration.gameData.LatestVersion;
            Process[] runningProcesses = Process.GetProcesses();
            // Cycle each process
            foreach (Process process in runningProcesses)
            {
                if (process.ProcessName == "DCS")
                {
                    process.Kill(); // This is not safe if you have multiple DCS servers running on the same server.
                    // Will possibly research for a better alternative.
                    if (!process.ProcessName.Contains("DCS"))
                        break;
                }
            }

            Thread.Sleep(5000); // Wait 5 seconds to make sure DCS has closed.

            Process.Start(startInfo);
            // Wait 30 seconds (this will allow an administrator to type the password on UAC clients).
            // Note, the workaround is to run this script as Administrator to get around the future UAC prompts.
            Thread.Sleep(10000);
            while (elapsedTime < 240)
            {
                GetDedicatedServerVersion();

                if (GameConfiguration.gameData.ServerVersion == GameConfiguration.gameData.LatestVersion)
                {
                    break;
                }


                Thread.Sleep(120000); // Needs to be updated to 2 minutes. (120000)
                elapsedTime += 2;
            }
            // Find the updater and kill it.
            runningProcesses = Process.GetProcesses();
            foreach (Process process in runningProcesses)
            {
                if (process.ProcessName == "DCS_updater")
                {
                    process.Kill();
                    if (!process.ProcessName.Contains("DCS_updater"))
                        break;
                }
            }
            GameConfiguration.gameData.UpdateInProgress = false;
            //Console.WriteLine("Completed Update");

            StartDCS();

            // We'd send a notification at this point via the GamingDiscordBotAutomation.

        }

        private static void StartDCS()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = GameConfiguration.gameData.ApplicationPath;
            startInfo.Arguments = "";
            Process.Start(startInfo);
            UpdateSQLTable();
        }

        private static void UpdateSQLTable()
        {
            // Using the same connection string we used to connect to the database
            using (var mysqlconnection = new MySqlConnection(CS.ConnectionString))
            {
                try
                {
                    GameConfiguration.gameData.DedicatedCompletedUpdated = DateTime.UtcNow;
                    mysqlconnection.Open();
                    // We want to update the "discord" table and set the "token" and "game" columns to the values in the textboxes
                    string query = $"UPDATE `updater` SET `server_version`=@server_version, `dedicated_completed_update`=@completed_update_time, `update_lock`=0, `force_update`=0,`dedicated_acknwoledgement`=0 WHERE  `game_key`={GameConfiguration.gameData.Id}";
                    using (var command = new MySqlCommand(query, mysqlconnection))
                    {
                        command.Parameters.AddWithValue("@server_version", GameConfiguration.gameData.ServerVersion);
                        command.Parameters.AddWithValue("@completed_update_time", GameConfiguration.gameData.DedicatedCompletedUpdated.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
