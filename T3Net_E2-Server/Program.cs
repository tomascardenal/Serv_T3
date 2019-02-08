using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T3Net_E2_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //Starting a new server on port 1337
            ChatServer cs = new ChatServer(1337);
            cs.CreateServer();
        }
    }
}
