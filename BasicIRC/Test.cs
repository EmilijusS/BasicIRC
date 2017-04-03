using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BasicIRC
{
    class Test
    {
        public void Start()
        {
            int port = 6667;
            byte[] data;
            TcpClient client = new TcpClient("chat.freenode.net", port);
            NetworkStream stream = client.GetStream();
            string input;

            Thread listen = new Thread(() => GetResponse(stream));
            listen.Start();

            data = Encoding.ASCII.GetBytes("NICK squotro\r\n");

            stream.Write(data, 0, data.Length);

            data = Encoding.ASCII.GetBytes("USER squotro 0 * :Real name\r\n");

            stream.Write(data, 0, data.Length);

            while(true)
            {
                input = Console.ReadLine();

                input += "\r\n";
                data = Encoding.ASCII.GetBytes(input);
                stream.Write(data, 0, data.Length);
            }
          
            stream.Close();
            client.Close();
        }

        void GetResponse(NetworkStream stream)
        {
            int bytes;
            var data = new Byte[1024];

            while(true)
            {
                bytes = stream.Read(data, 0, data.Length);

                if(bytes > 0)
                {
                    Console.WriteLine(Encoding.ASCII.GetString(data, 0, bytes));
                }

                Thread.Sleep(1000);
            }
        }
    }
}
