using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EchoServer
{
    class Server
    {
        public Server()
        {
            //Preparing server EndPoint
            IPEndPoint ie = new IPEndPoint(IPAddress.Any, 31416);
            //Creating socket
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Binding socket to the port and any net interface
            //If the port is busy. It'll trigger an exception
            s.Bind(ie);

            //Waiting for connection and stablishing row of pending clients
            s.Listen(10);

            //Wait and accept the client's connection (blocking socket)
            Socket sClient = s.Accept();

            //Obtaining client info, casting is necessary because of RemoteEndPoint being a more generic EndPoint class
            IPEndPoint ieClient = (IPEndPoint)sClient.RemoteEndPoint;

            Console.WriteLine("Client connected: {0} at port {1}", ieClient.Address, ieClient.Port);

            NetworkStream ns = new NetworkStream(sClient);

            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            string welcome = "Welcome to The Echo Logic, Odd, Desiderable," +
                " Incredible, and Javaless Echo Server (T.E.L.O.D.I.J.E. Server)";

            sw.WriteLine(welcome);

            sw.Flush();

            string msg;
            while (true)
            {
                try
                {
                    msg = sr.ReadLine();
                    Console.WriteLine(msg !=null? msg:"Client disconnected");
                    sw.WriteLine(msg);
                    sw.Flush();
                }
                catch (IOException)
                {
                    break;
                }
            }
            sw.Close();
            sr.Close();
            ns.Close();
            sClient.Close();
            s.Close();
        }
    }
}
