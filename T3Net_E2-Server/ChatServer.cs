using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace T3Net_E2_Server
{
    /// <summary>
    /// Represents a multithreaded chat server
    /// </summary>
    class ChatServer
    {
        /// <summary>
        /// The server endpoint
        /// </summary>
        private IPEndPoint serverIE;
        /// <summary>
        /// The server socket
        /// </summary>
        private Socket serverSocket;
        /// <summary>
        /// The server port
        /// </summary>
        private int serverPort;
        /// <summary>
        /// Connected user list
        /// </summary>
        public Dictionary<string, Socket> ClientList;
        /// <summary>
        /// Lock for multithreading
        /// </summary>
        public object Locker = new object();

        /// <summary>
        /// Initalizes a chatserver on the indicated port
        /// </summary>
        /// <param name="port"></param>
        public ChatServer(int port)
        {
            this.serverPort = port;
            ClientList = new Dictionary<string, Socket>();
        }

        /// <summary>
        /// Creates the server and starts waiting for clients
        /// </summary>
        public void CreateServer()
        {
            Console.WriteLine(Properties.strings.SRV_WELCOME,this.serverPort);
            serverIE = new IPEndPoint(IPAddress.Any, this.serverPort);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                serverSocket.Bind(serverIE);
                serverSocket.Listen(10);
                //Wait for clients, throw a new thread with the client
                while (true)
                {
                    Socket client = serverSocket.Accept();
                    ChatroomClientManager chatClient = new ChatroomClientManager(client, this);
                    Thread clientThread = new Thread(chatClient.run);
                    clientThread.Start();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(Properties.strings.SRV_SOCKETEXCEPTION,e.Message);
            }
        }


    }
}
