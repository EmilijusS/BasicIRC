using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicIRC
{
    public class IRC
    {
        private Connection connection;

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
                    return false;
            }
            else
            {
                if (!connection.Connect(server))
                    return false;
            }



            return true;
            
        }
    }
}
