
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
using System.Windows.Media;
using System.Reflection;

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

        public bool authenticateUser(Packet sendPacket, TcpClient client)
        {

            //connect the client to the server
            client.Connect(IPAddress.Loopback, 11001);
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

        public bool sendImage(string filepath, TcpClient client)
        {
            try
            {
                client.Connect(IPAddress.Loopback, 11001);
                NetworkStream stream = client.GetStream();

                byte[] imageBuffer = File.ReadAllBytes(filepath);

                int index = 0;
                bool lastPacketSent = false;
                while (!lastPacketSent)
                {
                    Packet sendPacket = new Packet();
                    sendPacket.setHead('1', '2', states.Sending);
                    byte[] dataBuf = new byte[500];

                    int bytesToCopy = Math.Min(dataBuf.Length, imageBuffer.Length - index);
                    Array.Copy(imageBuffer, index, dataBuf, 0, bytesToCopy);
                    index += bytesToCopy;

                    // Check if this is the last packet
                    if (bytesToCopy == 0)
                    {
                        Packet lastPacket = new Packet();
                        lastPacketSent = true;
                        sendPacket.setHead('1', '2', states.Analyze);
                        byte[] noData = new byte[0];
                        lastPacket.setData(noData.Length, noData);
                        lastPacket.SerializeData();
                        stream.Write(lastPacket.getTailBuffer(), 0, lastPacket.getTailBuffer().Length);
                        continue;
                    }

                    sendPacket.setData(bytesToCopy, dataBuf);
                    sendPacket.SerializeData();
                    stream.Write(sendPacket.getTailBuffer(), 0, sendPacket.getTailBuffer().Length);

                    byte[] recvBuf = new byte[1024];
                    int amount = stream.Read(recvBuf, 0, recvBuf.Length);

                    byte[] array = new byte[amount];
                    Array.Copy(recvBuf, array, amount);
                    Packet recvPacket = new Packet(array);

                    if (lastPacketSent)
                    {
                        if (recvPacket.GetHead().getState() != states.Recv)
                        {
                            Exception e = new Exception();
                            throw e;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (recvPacket.GetHead().getState() != states.Saving)
                        {
                            Exception e = new Exception();
                            throw e;
                        }
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }





    }
}

