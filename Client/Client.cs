
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Ink;

namespace Client
{
    public class ProgramClient
    {
        public string clientUserName { get; set; }
        public DateTime loginDate;
        public bool authentcated { get; set; }

        public ProgramClient() {
        
           clientUserName= string.Empty;
           loginDate = DateTime.Now; 
           authentcated= false;

        }

        public bool authenticateUser(Packet sendPacket)
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);


            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            
            sender.Connect(localEndPoint);
            ///DO SOMETHING COOL WAITING FOR CONNECTION
            Console.WriteLine("Socket connected to -> {0} ",
                          sender.RemoteEndPoint.ToString());

            sendPacket.SerializeData();
            int bytesSent = sender.Send(sendPacket.getTailBuffer());

            byte[] buffer = new byte[bytesSent];
            int byteRecv = sender.Receive(buffer);

            Packet recvPacket = new Packet(buffer);
            userLoginData loginData = recvPacket.deserializeUserLoginData();
            
            
            
            // Close Socket using
            // the method Close()
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();

            if (recvPacket.GetHead().getState() == states.Recv)
            {
                this.clientUserName = loginData.getUserName();
                this.authentcated = true;
                this.loginDate = DateTime.Now;
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
