using System;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Client();
        }

        static void Client()
        {
            TcpClient tcpclnt = new TcpClient();
            Console.WriteLine("Connecting.....");

            while (true)
            {
                try
                {
                    tcpclnt.Connect("192.168.100.7", 8000);
                    Console.WriteLine("Connected...");

                    Console.Write("\n Enter the string to be sent: ");

                    String str = Console.ReadLine();
                    Stream stm = tcpclnt.GetStream();

                    ASCIIEncoding asen = new ASCIIEncoding();
                    byte[] ba = asen.GetBytes(str);
                    Console.WriteLine("Sending...");

                    stm.Write(ba, 0, ba.Length);

                    byte[] bb = new byte[1024];
                    int k = stm.Read(bb, 0, 1024);

                    for (int i = 0; i < k; i++)
                    {
                        Console.Write(Convert.ToChar(bb[i]));
                    }

                    tcpclnt.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error..... " + e.StackTrace);
                }
            }
        }

    }
}
