
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
            //reading a image file 255 bytes at a time.
            FileInfo fileInfo = new FileInfo(filepath);

            byte[] data = new byte[byte.MaxValue];
            try
            {
                if (!fileInfo.Exists)
                {
                    return false;
                }
         
            using (BinaryReader reader = new BinaryReader(new FileStream(filepath, FileMode.Open)))
            {

                    NetworkStream stream = client.GetStream();
                    //keep reading data until end of file
                   
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    reader.BaseStream.Seek(50, SeekOrigin.Begin);
                    reader.Read(data, 0, data.Length);

                    Packet sendPacket = new Packet();
                    sendPacket.setHead('1', '2', states.Sending);
                    sendPacket.setData(data.Length, data);

                    sendPacket.SerializeData();

                    byte[] buffer = sendPacket.getTailBuffer();
                    stream.Write(buffer, 0, buffer.Length);
                }

                    // create the last packet
                    Packet lastPacket = new Packet();
                    lastPacket.setHead('1', '2', states.Analyze);
                    lastPacket.setData((int)reader.BaseStream.Length % byte.MaxValue, data);

                    lastPacket.SerializeData();

                    byte[] lastBuffer = lastPacket.getTailBuffer();
                    stream.Write(lastBuffer, 0, lastBuffer.Length);

                    

                }
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
                //send the last packet with the end of file flag.
            return true;
        }

    }
}

