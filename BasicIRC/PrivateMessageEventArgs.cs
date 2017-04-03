using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicIRC
{
    public class PrivateMessageEventArgs : EventArgs
    {
        public string channel;
        public string nick;
        public string message;
        
        public PrivateMessageEventArgs(string channel, string nick, string message)
        {
            this.channel = channel;
            this.nick = nick;
            this.message = message;
        }
    }
}
