using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BasicIRC
{
    public class IRC
    {
        public event EventHandler<EventArgs> Error;
        public event EventHandler<EventArgs> Connected;

        private Connection connection;
        private string nick;

        public IRC()
        {
            connection = new Connection();
        }

        public bool Start(string server, string nick)
        {
            var serverInfo = server.Split(':');
            int port;

            if (serverInfo.Length > 1 && int.TryParse(serverInfo[1], out port))
            {
                if (!connection.Connect(serverInfo[0], port))
                {
                    connection.Close();
                    return false;
                }                   
            }
            else
            {
                if (!connection.Connect(server))
                {
                    connection.Close();
                    return false;
                }
            }

            MsgNick(nick);
            MsgUser(nick);
            this.nick = nick;

            connection.DataReceived += (o, e) => DataReceived(e);
            new Thread(() => connection.Listen()).Start();

            return true;            
        }

        private void DataReceived(ConnectionEventArgs e)
        {
            var commands = e.message.Split(new char[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);

            foreach(var command in commands)
            {
                Console.WriteLine(command);
                var message = command.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);              

                if (message.Length > 1)
                {
                    if (message[0].Equals("PING"))
                        MsgPong(message[1]);

                    switch (message[1][0])
                    {
                        //Successful connection
                        case '0':
                            Connected?.Invoke(this, new EventArgs());
                            break;
                        //ERR message
                        case '4':
                            Error?.Invoke(this, new EventArgs());
                            break;
                    }
                }
            }                    
        }

        public void CloseConnection()
        {
            MsgQuit();
            connection.Close();
        }

        private void MsgNick(string nick)
        {
            connection.Send($"NICK {nick}\r\n");
        }

        private void MsgUser(string nick)
        {
            connection.Send($"USER {nick} 0 * :{nick}\r\n");
        }

        private void MsgPong(string reply)
        {
            connection.Send($"PONG {reply}\r\n");
        }

        private void MsgQuit()
        {
            connection.Send("QUIT");
        }
    }
}
