using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Networking_Tests
{
    class Program
    {
        static void IPEndPExample()
        {
            IPAddress ip = IPAddress.Loopback;
            IPEndPoint ie = new IPEndPoint(ip, 1200);
            Console.WriteLine("IPEndPoint: {0}",ie.ToString());
            Console.WriteLine("AddressFamily: {0}",ie.AddressFamily);
            Console.WriteLine("Address: {0}, Puerto: {1}",ie.Address,ie.Port);
            Console.WriteLine("Ports range: {0}-{1}", IPEndPoint.MinPort, IPEndPoint.MaxPort);
            ie.Port = 80;
            ie.Address = IPAddress.Parse("80.1.12.128");
            Console.WriteLine("New End Point: {0}",ie.ToString());
        }

        static void ShowNetInformation(string name)
        {
            IPHostEntry hostInfo;

            //Try to solve DNS
            hostInfo = Dns.GetHostEntry(name);

            //Showing the name of the host
            Console.WriteLine("Name : {0}", hostInfo.HostName);
            foreach (IPAddress ip in hostInfo.AddressList)
            {
                //Only IPv4 adresses
                //Use AddressFamily.InterNetWorkV6 for viewing IPv6 adresses
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    Console.WriteLine("\t{0,16}", ip.ToString());
                }
            }
            Console.WriteLine("\n");
        }

        static void ShowNetInformation(IPAddress ipAddress)
        {
            IPHostEntry hostInfo = Dns.GetHostEntry(ipAddress);
            ShowNetInformation(hostInfo.HostName);
        }

        static void SocketInstant()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        static void Main(string[] args)
        {
            //Obtaining localhost name and showing it
            string localHost = Dns.GetHostName();
            Console.WriteLine("Localhost name; {0}\n", localHost);

            //Showing net info from localhost and a remote host
            ShowNetInformation(localHost);
            ShowNetInformation("www.google.es");

            //ShowNetInformation(new IPAddress(new byte[] {82,98,160,124}));
            ShowNetInformation(IPAddress.Parse("82.98.160.124"));

            IPEndPExample();

            Console.ReadKey();
        }
    }
}
