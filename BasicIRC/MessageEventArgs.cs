using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicIRC
{
    public class MessageEventArgs : EventArgs
    {
        public string message;

        public MessageEventArgs(string message)
        {
            this.message = message;
        }
    }
}
