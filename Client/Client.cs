
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
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Client
{
    public class ProgramClient
    {
        private userLoginData clientData;
        public DateTime loginDate;
        private TcpClient tcpClient;
        private NetworkStream stream;
        public bool authentcated { get; set; }

        public ProgramClient()
        {

            clientData.setUserName(string.Empty);
            clientData.setPassword(string.Empty);
            loginDate = DateTime.Now;
            authentcated = false;

            this.tcpClient = new TcpClient();
            this.tcpClient.Connect(IPAddress.Loopback, 11002);
            this.stream = this.tcpClient.GetStream();
            

        }

        public bool authenticateUser(Packet sendPacket)
        {
            
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
                stream.Flush();
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
                stream.Flush();
                return true;
            }
        }

        public bool sendImage(string filepath)
        { 
                try
                {
                //Resizes the image to 640x457
               System.Drawing.Image img = System.Drawing.Image.FromFile(filepath);

                Bitmap b = new Bitmap(img);
                byte[] imageBuffer2 = ImageToByteArray(img);

                System.Drawing.Image i = resizeImage(b, new Size(640,457));

                //converts image to byte array
                byte[] imageBuffer = ImageToByteArray(i);
                i.Dispose();
                img.Dispose();
                b.Dispose();

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
                                stream.Flush();
                                return true;
                            }
                        }
                        //else
                        //{
                        //    if (recvPacket.GetHead().getState() != states.Saving)
                        //    {
                        //        Exception e = new Exception();
                        //        throw e;
                        //    }
                        //}
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
                


                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                using (FileStream file = new FileStream(path, FileMode.Create))
                {

                    while (true)
                    {

                        byte[] receiveBuffer = new byte[4096];
                        int bytesRead = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                        byte[] data = new byte[bytesRead];
                        Array.Copy(receiveBuffer, data, bytesRead);

                        Packet receivedPacket = new Packet(data);
                        if(receivedPacket.GetHead().getState() == states.Idle)
                        {
                            return false;
                        }
                      

                        if (receivedPacket.GetHead().getState() == states.Analyze)
                        {
                            //Packet lastPacket = new Packet();
                            //lastPacket.setHead('1', '2', states.Recv);
                            //lastPacket.setData(noData.Length, noData);
                            //lastPacket.SerializeData();
                            //byte[] newbuf = lastPacket.getTailBuffer();

                            //stream.Write(newbuf, 0, newbuf.Length);
                            break;
                        }
                        Packet ackPacket = new Packet();
                        ackPacket.setHead('1', '2', states.Saving);
                        byte[] noData = new byte[0];
                        ackPacket.setData(noData.Length, noData);
                        ackPacket.SerializeData();
                        byte[] buf = ackPacket.getTailBuffer();

                        stream.Write(buf, 0, buf.Length);
                        

                        byte[] imageData = receivedPacket.GetBody().getData();
                        if(imageData != null)
                        {
                            file.Write(imageData, 0, imageData.Length);
                        }
                        
                    }
                    file.Close();
                }
                stream.Flush();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

           
        }
        private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        {
            //Get the image current width  
            int sourceWidth = imgToResize.Width;
            //Get the image current height  
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size  
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //Calculate height with new desired size  
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //New Width  
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height  
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height  
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }

        public bool receiveUserlogs()
        {
            string path = "../../../ClientLog.txt";
            System.IO.File.WriteAllText(@"../../../ClientLog.txt", string.Empty);

            try
            {
                Packet sendPacket = new Packet();



                sendPacket.setHead('1', '2', states.RecvLog);

                //connect the client to the server
                sendPacket.SerializeData();

                byte[] buffer = sendPacket.getTailBuffer();
                stream.Write(buffer, 0, buffer.Length);



                using (FileStream file = new FileStream(path, FileMode.Append))
                {

                    while (true)
                    {

                        byte[] receiveBuffer = new byte[1024];
                        int bytesRead = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                        byte[] data = new byte[bytesRead];
                        Array.Copy(receiveBuffer, data, bytesRead);

                        Packet receivedPacket = new Packet(data);
                        if (receivedPacket.GetHead().getState() == states.Idle)
                        {
                            return false;
                        }


                        if (receivedPacket.GetHead().getState() == states.Analyze)
                        {
                            //Packet lastPacket = new Packet();
                            //lastPacket.setHead('1', '2', states.Recv);
                            //lastPacket.setData(noData.Length, noData);
                            //lastPacket.SerializeData();
                            //byte[] newbuf = lastPacket.getTailBuffer();

                            //stream.Write(newbuf, 0, newbuf.Length);
                            break;
                        }
                        Packet ackPacket = new Packet();
                        ackPacket.setHead('1', '2', states.Saving);
                        byte[] noData = new byte[0];
                        ackPacket.setData(noData.Length, noData);
                        ackPacket.SerializeData();
                        byte[] buf = ackPacket.getTailBuffer();

                        stream.Write(buf, 0, buf.Length);


                        byte[] Data = receivedPacket.GetBody().getData();
                        if (Data != null)
                        {
                            file.Write(Data, 0, Data.Length);
                        }

                    }
                    file.Close();
                }
                stream.Flush();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }


        }




        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(imageIn, typeof(byte[]));
            return xByte;
        }

    }
}

