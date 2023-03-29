
using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Windows.Ink;

namespace Client
{
    public class ProgramClient
    {
        private userLoginData clientData;
        public DateTime loginDate;
        public bool authentcated { get; set; }

        public ProgramClient()
        {

            clientData.setUserName(string.Empty);
            clientData.setPassword(string.Empty);
            loginDate = DateTime.Now;
            authentcated = false;

        }

        public bool authenticateUser(Packet sendPacket)
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 696969);


            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);


            sender.Connect(localEndPoint);
            ///DO SOMETHING COOL WAITING FOR CONNECTION
            

            sendPacket.SerializeData();
            int bytesSent = sender.Send(sendPacket.getTailBuffer());

            byte[] buffer = new byte[bytesSent];
            int byteRecv = sender.Receive(buffer);

            Packet recvPacket = new Packet(buffer);

            this.clientData = recvPacket.deserializeUserLoginData();



            // Close Socket using
            // the method Close()
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();

            if (recvPacket.GetHead().getState() == states.Recv)
            {
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
