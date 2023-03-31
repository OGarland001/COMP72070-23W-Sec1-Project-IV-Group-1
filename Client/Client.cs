
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Ink;
using ProtoBuf;

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
            //// Establish the remote endpoint for the socket.
            Int32 port = 11001;

            using TcpClient client = new TcpClient(IPAddress.Loopback.ToString(), port);

            sendPacket.SerializeData();

            byte[] buffer = sendPacket.getTailBuffer();

            NetworkStream stream = client.GetStream();

            stream.Write(buffer, 0, buffer.Length);

            buffer = new byte[1024];

            int i;
            // Loop to receive all the data sent by the client.
            i = stream.Read(buffer, 0, buffer.Length);
            byte[] data = new byte[i];
            Array.Copy(buffer, data, i);

            Packet responsePacket = new Packet(data);

            if (responsePacket.GetHead().getState() == states.Recv)
            {
                client.Close();
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

