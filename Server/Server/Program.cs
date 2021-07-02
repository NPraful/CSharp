using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Serve();
        }

        static void Serve()
        {
            IPAddress ipAdress = IPAddress.Parse("192.168.100.7");

            // Initializes the Listener
            TcpListener myList = new TcpListener(ipAdress, 8000);
            Socket s;
            while (true)
            {
                try
                {
                    // Start Listeneting at the specified port
                    myList.Start();
                    s = myList.AcceptSocket();
                    Console.WriteLine("Server running... @ Port: 8000");
                    Console.WriteLine("Local End point: " + myList.LocalEndpoint);
                    Console.WriteLine("Waiting for connections...");
                    // When accepted
                    Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);

                    byte[] b = new byte[1024];
                    int k = s.Receive(b);
                    Console.WriteLine("Recieved...");

                    for (int i = 0; i < k; i++)
                    {
                        Console.Write(Convert.ToChar(b[i]));
                    }

                    ASCIIEncoding asen = new ASCIIEncoding();
                    s.Send(asen.GetBytes("Automatic message: " + "String received by server!"));
                    Console.WriteLine("\n Automatic message sent!");

                    s.Close();
                    myList.Stop();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error..... " + e.StackTrace);
                }
            }
        }
    }
}
