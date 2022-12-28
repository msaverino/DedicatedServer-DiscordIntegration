using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DiscordBotAutomation.DigitalCombatSimulator;
using DigitalCombatSimulator.DataModel;
using System.Data.Common;
using MySqlConnector;

namespace DiscordBotAutomation.DigitalCombatSimulator
{
    internal class PlayerCountHandler
    {

        private static string connectionString = CS.ConnectionString;

        public static Task UpdatePlayerCount()
        {

            while (true)
            {
                if (!GameConfiguration.gameData.UpdateInProgress)
                {
                    Console.WriteLine("Checking online players.");
                    int listeningPort = GameConfiguration.gameData.QueryReceivePort;
                    int sendingPort = GameConfiguration.gameData.QuerySendPort;
                    string serverIp = GameConfiguration.gameData.ServerIP;

                    UdpClient toClient = new UdpClient();
                    UdpClient toServer = new UdpClient();
                    // We can test various things here
                    Byte[] sendBytes = Encoding.ASCII.GetBytes("getplayercount");
                    try
                    {
                        toServer.Send(sendBytes, sendBytes.Length, serverIp, sendingPort);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    toServer.Close();

                    toClient.Client.ReceiveTimeout = 5000; // Timeout of 5 seconds.

                    toClient.Client.Bind(new IPEndPoint(IPAddress.Any, listeningPort));
                    try
                    {

                        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, listeningPort);
                        byte[] data = toClient.Receive(ref endPoint);
                        // Print the response.
                        var response = Encoding.ASCII.GetString(data);

                        if (Int32.TryParse(response, out int playerCount))
                        {
                            GameConfiguration.gameData.PlayerCount = playerCount;
                            GameConfiguration.gameData.LastUpdated = DateTime.UtcNow;
                            // We need to update the database
                            using (var connection = new MySqlConnection(CS.ConnectionString))
                            {
                                connection.Open();
                                string query = "UPDATE `player_count` SET `online_players`=@player_count, `last_updated`=@utc_date WHERE  `game_key`=@game_key;";
                                using (var command = new MySqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@player_count", GameConfiguration.gameData.PlayerCount);
                                    command.Parameters.AddWithValue("@game_key", GameConfiguration.gameData.Id);
                                    command.Parameters.AddWithValue("@utc_date", GameConfiguration.gameData.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss"));
                                    Console.WriteLine(GameConfiguration.gameData.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss"));
                                    command.ExecuteNonQuery();
                                }
                            }

                            if (playerCount > GameConfiguration.gameData.MostOnline)
                            {
                                GameConfiguration.gameData.MostOnline = playerCount;
                                GameConfiguration.gameData.DateOfMostOnline = DateTime.UtcNow;

                                using (var connection = new MySqlConnection(CS.ConnectionString))
                                {
                                    connection.Open();
                                    string query = "UPDATE `player_count` SET `most_online`=@player_count, `most_online_date`=@utc_date WHERE  `game_key`=@game_key;";
                                    using (var command = new MySqlCommand(query, connection))
                                    {
                                        command.Parameters.AddWithValue("@player_count", GameConfiguration.gameData.PlayerCount);
                                        command.Parameters.AddWithValue("@game_key", GameConfiguration.gameData.Id);
                                        command.Parameters.AddWithValue("@utc_date", GameConfiguration.gameData.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss"));
                                        Console.WriteLine(GameConfiguration.gameData.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss"));
                                        command.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: " + response);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    toClient.Close();
                    Task.Delay(1000).Wait();
                }

            }
        }
    }
}
