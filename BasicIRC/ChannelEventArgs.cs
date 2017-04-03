using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicIRC
{
    public class ChannelEventArgs : EventArgs
    {
        public string channel;
        public string nick;

        public ChannelEventArgs(string channel, string nick)
        {
            this.channel = channel;
            this.nick = nick;
        }
    }
}
