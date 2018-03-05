using System;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace CommunicationLibrary
{
    public class RxEventArgs : EventArgs
    {
        private string message;

        public RxEventArgs(string message)
        {
            this.Message = message;
        }

        public string Message { get => message; set => message = value; }
    }

    public class TxEventArgs : RxEventArgs
    {
        public TxEventArgs(string message) : base(message)
        {

        }
    }

    public class UnableToConnectException : Exception
    {
        public UnableToConnectException(string message, Exception innerException):base(message, innerException)
        {
        }
    }

    public class PitayaComm
    {
        public delegate void RxEventHandler(object sender, RxEventArgs e);
        public event RxEventHandler RxEvent;
        protected virtual void OnRxEvent(RxEventArgs e)
        {
            RxEvent?.Invoke(this, e);
        }

        public delegate void TxEventHandler(object sender, TxEventArgs e);
        public event TxEventHandler TxEvent;
        protected virtual void OnTxEvent(TxEventArgs e)
        {
            TxEvent?.Invoke(this, e);
        }

        // Properties
        private TcpClient socketClient;
        private int port;
        private NetworkStream stream;
        private bool isConnected;
        private bool isExitting;
        private bool isReading;
        private bool isTransmitting;
        private Thread RxThread;

        // Constructor
        public PitayaComm(int port = 5000)
        {
            this.Port = port;
            isConnected = false;
            isExitting = false;
            RxThread = new Thread(RxManager);
        }

        // Accessor Methods
        public bool IsConnected { get => isConnected; set => isConnected = value; }
        public int Port { get => port; set => port = value; }
        public NetworkStream Stream { get => stream; set => stream = value; }
        public TcpClient Client { get => socketClient; set => socketClient = value; }

        // Public Methods
        public void Connect()
        {
            try
            {
                socketClient = new TcpClient();
                socketClient.Connect("rp-f04948.local", Port);
                Stream = socketClient.GetStream();
                isConnected = true;
                RxThread.Start();
            }
            catch (Exception e)
            {
                throw new UnableToConnectException("Either the pitaya isn't connected, or the server isn't running.", e);
            }
        }

        public void Close()
        {
            if (isConnected)
            {
                isExitting = true;
                while (isReading || isTransmitting) ;
                Stream.Close();
                socketClient.Dispose();
                isConnected = false;
            }
            else
                throw new InvalidOperationException("Cannot close a non-existing connection.");
        }

        public void Transmit(byte[] message)
        {
            if (isConnected && !isExitting)
            {
                isTransmitting = true;
                Stream.Write(message, 0, message.Length);
                OnTxEvent(new TxEventArgs(Encoding.ASCII.GetString(message)));
                isTransmitting = false;
            }
            else
                throw new InvalidOperationException("Socket either isn't connected or is closed.");
        }

        // Private Methods
        private void RxManager()
        {
            if (isConnected)
            {
                while (!isExitting)
                {
                    byte[] message = new byte[256];
                    while (socketClient.Connected)
                    {
                        isReading = true;
                        int count = Stream.Read(message, 0, message.Length);
                        if (count > 0)
                        {
                            //Stream.Read(message, 0, count);
                            OnRxEvent(new RxEventArgs(Encoding.ASCII.GetString(message)));
                        }
                        isReading = false;
                    }
                }
            }
            else
                throw new InvalidOperationException("Cannot start the RxManager without being connected.");
        }
    }
}
