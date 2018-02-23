using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace RedPitayaTCPProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TcpClient client = new TcpClient();
            client.Connect("rp-f04948.local", 5000);
            NetworkStream stream = client.GetStream();
            Thread RxThread = new Thread(RxManager);
            RxThread.Start();

            while(client.Connected)
            {
                string entry = Console.ReadLine();
                stream.Write(Encoding.ASCII.GetBytes(entry), 0, entry.Length);
            }

            void RxManager()
            {
                byte[] message = new byte[256];
                while(client.Connected)
                {
                    int count = stream.Read(message, 0, message.Length);
                    if(count != 0)
                    {
                        stream.Read(message, 0, count);
                        Console.WriteLine(Encoding.ASCII.GetString(message));
                    }
                }
            }
        }
    }
}
