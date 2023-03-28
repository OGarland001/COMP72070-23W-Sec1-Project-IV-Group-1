using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections.TCP;
using System;
using System.Net.NetworkInformation;

namespace Client
{
    public class Client
    {
        string clientUserName { get; set; }
        DateTime loginDate;
        bool authentcated { get; set; }

        public Client() {
        
           clientUserName= string.Empty;
           loginDate = DateTime.Now; 
           authentcated= false;

        }

        bool authenticateUser(Packet sendPacket)
        {
            TCPConnection.GetConnection(new ConnectionInfo("127.0.0.1", 10000)).SendObject("PacketObj" , sendPacket);
            Console.WriteLine("Send completed");


            return true;
        }

    }
}
