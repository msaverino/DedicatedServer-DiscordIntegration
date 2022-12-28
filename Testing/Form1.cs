using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Testing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_submit_Click(object sender, EventArgs e)
        {
            int listeningPort;
            int sendingPort;
            string serverIp = textBox_ip.Text;

            string listeningPortString = textBox_receivePort.Text;
            string sendingPortString = textBox_port.Text;

            bool isListeningPortValid = int.TryParse(listeningPortString, out listeningPort);
            bool isSendingPortValid = int.TryParse(sendingPortString, out sendingPort);
            
            if (isListeningPortValid && isSendingPortValid)
            {
                UdpClient toClient = new UdpClient();
                UdpClient toServer = new UdpClient();
                // We can test various things here
                Byte[] sendBytes = Encoding.ASCII.GetBytes(textBox_message.Text);
                try
                {
                    toServer.Send(sendBytes, sendBytes.Length, serverIp, sendingPort);
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                toServer.Close();

                toClient.Client.ReceiveTimeout = 5000; // Timeout of 5 seconds.

                toClient.Client.Bind(new IPEndPoint(IPAddress.Any, listeningPort));
                try
                {

                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 4001);
                    byte[] data = toClient.Receive(ref endPoint);
                    // Print the response.
                    var response = Encoding.ASCII.GetString(data);
                    label_response.Text = response;
                    if (checkBox_display_messageBox.Checked)
                    {
                        MessageBox.Show(response);
                    }
                    //usersOnline = Convert.ToInt32(response);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                toClient.Close();
            } else
            {
                // We want to notify the user that one of the ports was not setup correctly.
                MessageBox.Show("You didn't enter a correct port.");
            }
        }
    }
}