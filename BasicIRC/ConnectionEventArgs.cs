using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicIRC
{
    public class ConnectionEventArgs : EventArgs
    {
        public string message;

        public ConnectionEventArgs(string message)
        {
            this.message = message;
        }
    }
}
