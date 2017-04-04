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
        public event EventHandler<NewChannelEventArgs> JoinedChannel;
        public event EventHandler<ChannelEventArgs> UserJoined;
        public event EventHandler<MessageEventArgs> LeftChannel;
        public event EventHandler<ChannelEventArgs> UserLeft;
        public event EventHandler<PrivateMessageEventArgs> ReceivedMessage;

        private Connection connection;
        private string nick;
        private string lastMessage;
        private List<string> users;

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

            connection.DataReceived += DataReceived;
            new Thread(() => connection.Listen()).Start();

            return true;            
        }

        private void DataReceived(object o, MessageEventArgs e)
        {
            string originNick;
            var commands = e.message.Split(new char[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);

            // Servers like to send commands split in separate messages
            if(lastMessage != null)
            {
                commands[0] = lastMessage + commands[0];
            }

            if(!e.message.EndsWith("\n"))
            {
                lastMessage = commands[commands.Length - 1];
                commands = commands.Take(commands.Length - 1).ToArray();
            }
            else
            {
                lastMessage = null;
            }

            foreach(var command in commands)
            {
                //Console.WriteLine(command);
                var message = command.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);  
                
                if(message[0].Contains('!'))
                {
                    originNick = message[0].Substring(1, message[0].IndexOf('!') - 1);
                }
                else
                {
                    originNick = message[0].Substring(1);
                }            

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
                        case "353":
                            AddUsers(command);
                            break;
                        //End of names list, guaranteed to get after joining channel
                        case "366":
                            JoinChannel(command);
                            break;
                        case "PART":
                            if(originNick.Equals(nick))
                                LeftChannel?.Invoke(this, new MessageEventArgs(command.Substring(command.IndexOf('#') + 1)));
                            else
                                UserLeft?.Invoke(this, new ChannelEventArgs(command.Substring(command.IndexOf('#') + 1), originNick));
                            break;
                        case "JOIN":
                            if(originNick != nick)
                                UserJoined?.Invoke(this, new ChannelEventArgs(command.Substring(command.IndexOf('#') + 1), originNick));
                            break;
                        case "PRIVMSG":
                            ReceivedMessage?.Invoke(this, new PrivateMessageEventArgs(message[2].Substring(1), originNick, command.Substring(1 + command.IndexOf(':', command.IndexOf(':') + 1))));
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
                ReceivedMessage?.Invoke(this, new PrivateMessageEventArgs(channel, nick, message));
            }
        }

        public void CloseConnection()
        {
            MsgQuit();
            connection.DataReceived -= DataReceived;
            connection.Close();
        }

        private void AddUsers(string command)
        {
            var names = command.Substring(1 + command.IndexOf(':', command.IndexOf(':') + 1)).Split(' ');

            if (users == null)
                users = new List<string>();

            users.AddRange(names);
        }

        private void JoinChannel(string command)
        {
            // Extracts first word starting with '#'
            string channel = command.Substring(command.IndexOf('#') + 1, command.Substring(command.IndexOf('#') + 1).IndexOf(' '));

            users.Sort();

            JoinedChannel?.Invoke(this, new NewChannelEventArgs(channel, users));

            users = null;
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
