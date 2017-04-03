using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BasicIRC
{
    public class Parser
    {
        public event EventHandler<MessageEventArgs> Error;
        public event EventHandler<EventArgs> Connected;
        public event EventHandler<MessageEventArgs> JoinedChannel;

        private Connection connection;
        private string nick;

        public Parser()
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

        private void DataReceived(MessageEventArgs e)
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
                            Error?.Invoke(this, new MessageEventArgs(command.Substring(command.LastIndexOf(':') + 1)));
                            break;
                    }

                    switch(message[1])
                    {
                        case "331":
                        case "332":
                            // Extracts word starting with '#'
                            JoinedChannel?.Invoke(this, new MessageEventArgs(
                                command.Substring(command.IndexOf('#') + 1, command.Substring(command.IndexOf('#') + 1).IndexOf(' '))));
                            break;
                    }
                }
            }                    
        }

        public void SendData(string message, string channel)
        {
            if(message.ToLower().StartsWith("/join"))
            {
                var split = message.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for(int i = 1; i < split.Length; ++i)
                {
                    MsgJoin(split[i]);
                }
            }
            else if (message.ToLower().StartsWith("/part"))
            {
                var split = message.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 1; i < split.Length; ++i)
                {
                    MsgPart(split[i]);
                }
            }
            else if(channel != null)
            {
                MsgChat(message, channel);
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
            connection.Send("QUIT\r\n");
        }

        private void MsgJoin(string channel)
        {
            connection.Send($"JOIN {channel}\r\n");
        }

        private void MsgPart(string channel)
        {
            connection.Send($"PART {channel}\r\n");
        }

        private void MsgChat(string message, string channel)
        {
            connection.Send($"PRIVMSG #{channel} :{message}\r\n");
        }
    }
}
