using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using CommunicationLibrary;
using ReceptionLibrary;

namespace CommunicationsDebuggingTestbench
{
    class Program
    {
        static void Main(string[] args)
        {
            PitayaComm comms = new PitayaComm();
            comms.Connect();
            Console.WriteLine("Hello World!");
            comms.RxEvent += OnRxEvent;
            Class1.ScanForFrequencies(ref comms);

            while (comms.IsConnected)
            {
                string entry = Console.ReadLine();
                comms.Transmit(Encoding.ASCII.GetBytes(entry));
            }

            void OnRxEvent(object sender, RxEventArgs e)
            {
                Console.WriteLine("Received a message!");
            }
        }
    }
}

