using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BasicIRC
{
    class Connection
    {
        public event EventHandler<ConnectionEventArgs> dataReceived;
        private TcpClient client;
        private NetworkStream stream;
        private bool isListening;

        public void Connect(string server, int port = 6667)
        {
            client = new TcpClient(server, port);
            stream = client.GetStream();
        }

        public void Send(string message)
        {
            byte[] data;

            data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        public void Listen()
        {
            int bytes;
            var data = new byte[512];
            isListening = true;

            while (isListening)
            {
                bytes = stream.Read(data, 0, data.Length);

                if (bytes > 0)
                {
                    dataReceived?.Invoke(this, new ConnectionEventArgs(Encoding.ASCII.GetString(data, 0, bytes)));
                }

                Thread.Sleep(1000);
            }
        }

        public void Close()
        {
            isListening = false;
            stream.Close();
            client.Close();
        }
    }
}
