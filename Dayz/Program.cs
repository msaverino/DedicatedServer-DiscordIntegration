
namespace DiscordBotAutomation.Dayz
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // We need to get the player count
            new RCon();
        }
    }
}

//// Replace "server_ip" and "server_port" with the IP address and port of the Dayz server you want to connect to
//using QueryMaster;
//using QueryMaster.GameServer;

//string serverAddress = "server_ip:server_port";

//// Create a new Server object and connect to the Dayz server
//Server server = ServerQuery.GetServerInstance(EngineType.Source, "66.150.214.230", 27015);
//server.RconPassword = "rcon_password";

//// Get the server's status
//ServerInfo serverInfo = server.GetInfo();

//// Print the number of players on the server
//Console.WriteLine("There are currently " + serverInfo.Players + " players on the server.");

