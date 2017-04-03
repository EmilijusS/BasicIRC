using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicIRC
{
    public class NewChannelEventArgs
    {
        public string channel;
        public List<string> users;

        public NewChannelEventArgs(string channel, List<string> users)
        {
            this.channel = channel;
            this.users = users;
        }
    }
}
