
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
           
            Console.WriteLine("Send completed");


            return true;
        }

    }
}
