
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
        private TcpClient tcpClient;
        
        public bool authentcated { get; set; }

        public ProgramClient()
        {

            clientData.setUserName(string.Empty);
            clientData.setPassword(string.Empty);
            loginDate = DateTime.Now;
            authentcated = false;

            this.tcpClient = new TcpClient();
            this.tcpClient.Connect(IPAddress.Loopback, 11002);
            

        }

        public bool authenticateUser(Packet sendPacket)
        {
            NetworkStream stream = this.tcpClient.GetStream();
            //connect the client to the server
            sendPacket.SerializeData();

            byte[] buffer = sendPacket.getTailBuffer();

            

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
                this.authentcated = true;
                this.loginDate = DateTime.Now;
                return true;
            }
            else
            {
                return false;
            }

            
        }

        public bool checkConnection()
        {
            Packet sendPacket = new Packet();

            NetworkStream stream = this.tcpClient.GetStream();

            sendPacket.setHead('1', '2', states.Discon);

            //connect the client to the server
            sendPacket.SerializeData();

            byte[] buffer = sendPacket.getTailBuffer();

            stream.Write(buffer, 0, buffer.Length);

            buffer = new byte[1024];

            int i;
            // Loop to receive all the data sent by the client.
            i = stream.Read(buffer, 0, buffer.Length);
            byte[] data = new byte[i];
            Array.Copy(buffer, data, i);
            Packet responsePacket = new Packet(data);

            if (responsePacket.GetHead().getState() == states.Discon)
            {
                this.loginDate = DateTime.Now;
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool sendImage(string filepath)
        { 
                try
                {
                NetworkStream stream = this.tcpClient.GetStream();

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
                            lastPacket.setHead('1', '2', states.Analyze);
                            byte[] noData = new byte[0];
                            lastPacket.setData(noData.Length, noData);
                            lastPacket.SerializeData();
                            stream.Write(lastPacket.getTailBuffer(), 0, lastPacket.getTailBuffer().Length);
                            stream.Flush();
                            continue;
                        }

                        sendPacket.setData(bytesToCopy, dataBuf);
                        sendPacket.SerializeData();
                        stream.Write(sendPacket.getTailBuffer(), 0, sendPacket.getTailBuffer().Length);
                        stream.Flush();
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

        public bool receiveImage()
        {
            string path = "../../../UserImages/Output.jpg";

            try
            {
                NetworkStream stream = this.tcpClient.GetStream();
                byte[] receiveBuffer = new byte[1000];

                using (FileStream file = new FileStream(path, FileMode.Create))
                {

                    while (true)
                    {
                        int bytesRead = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                        byte[] data = new byte[bytesRead];
                        Array.Copy(receiveBuffer, data, bytesRead);

                        Packet receivedPacket = new Packet(data);

                        Packet ackPacket = new Packet();
                        ackPacket.setHead('1', '2', states.Saving);
                        byte[] noData = new byte[0];
                        ackPacket.setData(noData.Length, noData);
                        ackPacket.SerializeData();
                        byte[] buf = ackPacket.getTailBuffer();

                        stream.Write(buf, 0, buf.Length);

                        if (receivedPacket.GetHead().getState() == states.Analyze)
                        {
                            Packet lastPacket = new Packet();
                            lastPacket.setHead('1', '2', states.Saving);
                            lastPacket.setData(noData.Length, noData);
                            lastPacket.SerializeData();
                            byte[] newbuf = lastPacket.getTailBuffer();

                            stream.Write(newbuf, 0, newbuf.Length);
                            break;
                        }

                        byte[] imageData = receivedPacket.GetBody().getData();

                        file.Write(imageData, 0, imageData.Length);
                    }
                    file.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

           
        }

        ~ProgramClient()
        {
            
            this.tcpClient.Close();

        }



    }
}

