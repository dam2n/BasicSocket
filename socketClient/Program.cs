using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace socketClient
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            var client = new Socket(AddressFamily.InterNetwork, 
            SocketType.Stream, ProtocolType.Tcp);

            IPAddress ipAddress;
            int port;

            try
            {
                Console.WriteLine("Client socket (press any key)");
                Console.ReadKey();
                Console.Write("Enter Server IP address: ");

                string strIPAddress = Console.ReadLine();
                Console.Write("Enter Server Port: ");
                string strPort = Console.ReadLine();

                if (!IPAddress.TryParse(strIPAddress, out ipAddress))
                {
                    Console.WriteLine("Invalid IP address");
                    return;
                }

                if (!int.TryParse(strPort, out port))
                {
                    Console.WriteLine("Invalid port ");
                    return;
                }

                Console.WriteLine($"IP Address: {ipAddress} - Port: {port}");
                client.Connect(ipAddress, port);

                Console.WriteLine("Connected to the server");
                string inputText = "";
                while (true)
                {
                    inputText = Console.ReadLine();
                    if (inputText.Equals("EXIT"))

                    {
                        break;
                    }

                    var bufferSend = Encoding.ASCII.GetBytes(inputText);
                    client.Send(bufferSend);

                    var bufferReceived = new byte[128];
                    int numberOfBytesReceived = client.Receive(bufferReceived);
                    Console.WriteLine("Data received: {0}",
                    Encoding.ASCII.GetString(bufferReceived, 0, numberOfBytesReceived));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

            finally
            {
                if (client != null && client.Connected)
                {
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                    client.Dispose();
                }
            }

            Console.WriteLine("Press a key to exit");
            Console.ReadKey();
        }
    }
}
