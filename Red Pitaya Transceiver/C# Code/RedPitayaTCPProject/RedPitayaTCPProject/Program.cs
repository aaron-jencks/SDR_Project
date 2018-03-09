using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using CommunicationLibrary;
using ReceptionLibrary;

namespace RedPitayaTCPProject
{
    class Program
    {
        static void Main(string[] args)
        {
            PitayaComm comms = new PitayaComm();
            comms.Connect();
            Console.WriteLine("Hello World!");
            comms.RxEvent += OnRxEvent;

            while(comms.IsConnected)
            {
                string entry = Console.ReadLine();
                if (entry == "exit")
                    comms.Close();
                else
                    comms.Transmit(Encoding.ASCII.GetBytes(entry+"\r\f"));
            }

            void OnRxEvent(object sender, RxEventArgs e)
            {
                Console.WriteLine("Received a message!");
            }
        }
    }
}
