using ProtoBuf;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Shapes;

#pragma warning disable SYSLIB0011

namespace Server
{
    //loginDataStruct
    [ProtoContract]
    public struct userLoginData
    {
        [ProtoMember(1)]
        private string UserName;
        [ProtoMember(2)]
        private string Password;

        public string getUserName()
        {
            return this.UserName;
        }
        public string getPassword()
        {
            return this.Password;
        }
        public void setUserName(string userName) { this.UserName = userName; }
        public void setPassword(string password) { this.Password = password; }

        public byte[] serializeData()
        {
            //serialize the data attribute to the Txbuffer inside tail.
            try
            {
                using (var stream = new MemoryStream())
                {
                    Serializer.Serialize(stream, this);
                    return stream.ToArray();
                }
            }
            catch
            {
                throw;
            }
        }
        public void createUserFile()
        {
            string path = @"../../../Users/" + getUserName() + "/" + getUserName() + ".txt";
            if (!File.Exists(path))
            {
                using (FileStream fs = File.Create(path))
                {
                    fs.Close();
                }
            }

            StreamWriter writer = new StreamWriter(path);
            {
                int count = 0;
                writer.WriteLine(count.ToString());
                writer.Close();
            }



        }
        public int getSendCount()
        {
            string path = @"../../../Users/" + getUserName() + "/" + getUserName() + ".txt";
            if (File.Exists(path))
            {
                StreamReader streamReader = new StreamReader(path);
                {
                    string data = streamReader.ReadLine();
                    if (data != null)
                    {
                        int count = Int32.Parse(data);
                        streamReader.Close();
                        return count;
                    }

                }
            }
            else
            {
                return 0;
            }

            return 0;


        }
        public void saveSendCount()
        {
            //open a file and read the number and increment and save the number again
            string path = @"../../../Users/" + getUserName() + "/" + getUserName() + ".txt";
            if (File.Exists(path))
            {
                int count = getSendCount();
                count++;


                StreamWriter writer = new StreamWriter(path);
                {
                    writer.WriteLine(count.ToString());
                    writer.Close();
                }
            }
            else
            {
                int count = getSendCount();
                count++;

                File.Create(path);
                StreamWriter writer = new StreamWriter(path);
                {
                    writer.WriteLine(count.ToString());
                    writer.Close();
                }
            }
        }
    }


    //Enum States
    public enum states
    {
        Idle, Auth, NewAuth, Recv, Analyze, Saving, Sending, Discon, RecvLog
    }
    [ProtoContract]
    public struct Head
    {
        //ID from sender
        [ProtoMember(1)]
        private char SenderID;
        //ID from reciever
        [ProtoMember(2)]
        private char RecieverID;
        //Length of data
        [ProtoMember(3)]
        private int Length;
        //enum states for each server state.
        [ProtoMember(4)]
        private states State;

        public char getSenderID()
        {
            return SenderID;
        }
        public void setSenderID(char id)
        {
            SenderID = id;
        }

        public char getReciverID()
        {
            return RecieverID;
        }

        public void setReciverID(char id)
        {
            RecieverID = id;
        }

        public int getLength()
        {
            return Length;
        }

        public void setLength(int len)
        {
            Length = len;
        }

        public states getState()
        {
            return State;
        }
        public void setState(states state)
        {
            State = state;
        }
    };
    [ProtoContract]
    public struct Body
    {
        //byte array for data
        [ProtoMember(1)]
        public byte[] data;
        

        public void setData(byte[] buffer)
        {
            data = buffer;
        }
        public byte[] getData() { return data; }
    };
    [ProtoContract]
    public struct Tail
    {
        //byte array for the send buffer
        [ProtoMember(1)]
        public byte[] TxBuffer;
        public void setTxBuffer(byte[] buffer)
        {
            TxBuffer = buffer;
        }
        public byte[] getTxBuffer() { return TxBuffer; }
    }


    //Class Defination
    [ProtoContract]
    public class Packet
    {
        [ProtoMember(1)]
        private Head head;
        [ProtoMember(2)]
        private Body body;
        [ProtoMember(3)]
        private Tail tail;


        public Packet()
        {
            //Default Constructor
            head = new Head();
            body = new Body();
            tail = new Tail();
        }
        public Packet(byte[] data)
        {

            try
            {
                using (var stream = new MemoryStream(data))
                {
                    Packet packet = Serializer.Deserialize<Packet>(stream);
                    this.head = packet.head;
                    this.body = packet.body;
                    this.tail = packet.tail;
                }
            }
            catch
            {
                // Log error
                throw;
            }

        }

        public void SerializeData()
        {
            //serialize the data attribute to the Txbuffer inside tail.

            try
            {
                using (var stream = new MemoryStream())
                {
                    Serializer.Serialize(stream, this);
                    this.tail.setTxBuffer(stream.ToArray());
                }
            }
            catch
            {
                throw;
            }


        }
        public Packet(Packet recvPacket)
        {
            this.head = recvPacket.head;
            this.body = recvPacket.body;
            this.tail = recvPacket.tail;
        }

        public Head GetHead() { return head; }

        public Body GetBody() { return body; }

        public Tail GetTail() { return tail; }

        public void setData(int length, byte[] data)
        {
            this.head.setLength(length);
            this.body.data = new byte[length];
            this.body.data = data;
        }

        public void setHead(char sID, char rID, states state)
        {
            this.head.setSenderID(sID);
            this.head.setReciverID(rID);
            this.head.setState(state);
        }

        public byte[] getTailBuffer()
        {
            return this.tail.getTxBuffer();

        }

        public userLoginData deserializeUserLoginData()
        {
            //ParaConstructor of the data buffer.

            try
            {
                using (var stream = new MemoryStream(this.body.getData()))
                {
                    userLoginData loginData = Serializer.Deserialize<userLoginData>(stream);
                    return loginData;
                }
            }
            catch
            {
                // Log error
                throw;
            }



        }



    }

    public class login
    {
        private userLoginData userData;

        public login()
        {
            userData = new userLoginData();
        }

        public login(Packet packet)
        {
            userLoginData temp = packet.deserializeUserLoginData();
            userData.setUserName(temp.getUserName());
            userData.setPassword(temp.getPassword());
        }

        public userLoginData GetuserData()
        {
            return userData;
        }

        public void SetuserData(string username, string password)
        {
            userData.setUserName(username);
            userData.setPassword(password);
        }

        public bool SaveuserData(string filePath)
        {

            bool error = false;

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, append: true))
                {
                    writer.WriteLine(userData.getUserName());
                    writer.WriteLine(userData.getPassword());
                    writer.Close();
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred while writing to the file: " + e.Message);
                error = true;
            }

            return error;
        }

        public string SignInUser(string filePath)
        {


            string message = "Username or password is incorrect";
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line1 = reader.ReadLine();
                    string line2 = reader.ReadLine();

                    while ((line1 != null) && (line2 != null))
                    {
                        if ((line1 == userData.getUserName()) && (line2 == userData.getPassword()))
                        {

                            message = "User signed in";
                            break;
                        }

                        line1 = reader.ReadLine();
                        line2 = reader.ReadLine();
                    }



                    reader.Close();
                }
            }
            catch (IOException e)
            {
                message = "Error Signing in user";
            }

            return message;
        }


        public bool checkUsername(string filePath)
        {
            bool unique = true;

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line1 = reader.ReadLine();

                    while ((line1 != null))
                    {
                        if ((line1 == userData.getUserName()))
                        {
                            unique = false;
                            break;
                        }

                        line1 = reader.ReadLine();
                    }



                    reader.Close();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred while writing to the file: " + e.Message);
            }

            return unique;
        }


        public string RegisterUser(string filePath)
        {
            string message;
            if (checkUsername(filePath) == false)
            {
                //Non unique Username Entered
                message = "Username must be unique";
            }
            else
            {
                if (SaveuserData(filePath) == false)
                {
                    message = "User registered";
                }
                else
                {
                    message = "Error Saving user's credentials";
                }
            }

            return message;
        }
    }
}