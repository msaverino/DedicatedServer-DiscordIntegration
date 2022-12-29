using BattleNET;
using QueryMaster.GameServer;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using BattleNET;

namespace DiscordBotAutomation.Dayz
{
    internal class RCon
    {

        private BattlEyeLoginCredentials _credentials;
        private BattlEyeClient _client;

        public void Connect (IPAddress host, int port, string password)
        {
            _credentials = new BattlEyeLoginCredentials
            {
                Host = host,
                Port = port,
                Password = password
            };
            _client = new BattlEyeClient(_credentials); // Authenticate with RCON.
            _client.BattlEyeConnected += HandleConnection;
        }

        private void HandleConnection(BattlEyeConnectEventArgs args)
        {
            switch (args.ConnectionResult)
            {
                case BattlEyeConnectionResult.Success:
                    Console.WriteLine("Connected.");
                    break;
                case BattlEyeConnectionResult.InvalidLogin:
                    Console.WriteLine("Invalid Credentials.");
                    break;
                case BattlEyeConnectionResult.ConnectionFailed:
                    Console.WriteLine("Failed to connect.");
                    break;
                default:
                    Console.WriteLine("Unkown Error.");
                    break;
            }
        }

        private DateTime lastSentCommand;

        private int SendCommand(BattlEyeCommand command)
        {

            while ((DateTime.UtcNow - lastSentCommand).TotalMilliseconds <= 50) { Thread.Sleep(10); } // This will help prevent us from sending to many packets

            int 

            lastSentCommand = DateTime.UtcNow;
        }
    }
}
