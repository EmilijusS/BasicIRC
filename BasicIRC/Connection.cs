using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BasicIRC
{
    public class Connection
    {
        public event EventHandler<ConnectionEventArgs> DataReceived;
        private TcpClient client;
        private NetworkStream stream;
        private bool isListening;

        public bool Connect(string server, int port = 6667)
        {
            client = new TcpClient(server, port);

            if(!client.Connected)
            {
                return false;
            }

            stream = client.GetStream();

            return true;
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
                try
                {
                    bytes = stream.Read(data, 0, data.Length);
                }
                catch (Exception e)
                {
                    stream.Close();
                    client.Close();
                    break;
                }

                if (bytes > 0)
                {
                    DataReceived?.Invoke(this, new ConnectionEventArgs(Encoding.ASCII.GetString(data, 0, bytes)));
                }

                Thread.Sleep(100);
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
