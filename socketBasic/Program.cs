﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace socketBasic
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            using (var listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {



                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 23000);
                try
                {

                    listenerSocket.Bind(iPEndPoint);

                    listenerSocket.Listen(1); // how many clients can connect

                    Console.WriteLine("Waiting for incoming connections");

                    var client = listenerSocket.Accept(); // Syncronous operation

                    Console.WriteLine("Client connected IP End Point;"
                        + client.RemoteEndPoint.ToString());


                    var buffer = new byte[128];
                    int numberOfReceivedBytes = 0;
                    while (true)
                    {
                        numberOfReceivedBytes = client.Receive(buffer);

                        //Console.WriteLine(numberOfReceivedBytes + " bytes received");

                            var receivedText = Encoding.ASCII.GetString(buffer, 0, numberOfReceivedBytes);
                            if (numberOfReceivedBytes > 0)
                                Console.WriteLine($"Data received: {receivedText}");
                            client.Send(buffer);
                       
                            Array.Clear(buffer, 0, buffer.Length);
                            numberOfReceivedBytes = 0;
                        }
                   
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Connection closed");
                    //Console.WriteLine(exception.ToString());
                }

            }
            Console.WriteLine("press a key to exit");
            Console.ReadKey();


        }

    }
}
