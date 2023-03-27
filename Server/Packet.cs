using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#pragma warning disable SYSLIB0011

namespace Server
{
    //loginDataStruct
    [Serializable]
    public struct userLoginData
    {
        private string UserName;
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

            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {

                //weird warning but not sure if it works yet microsoft says its good
                formatter.Serialize(stream, this);
                byte[] data = new byte[stream.Length];
                data = stream.ToArray();
                stream.Close();

                return data;
            }

        }
    }

    //Enum States
    public enum states
    {
        Idle, Auth, Recv, Analyze, Saving, Sending
    }
    [Serializable]
    public struct Head
    {
        //ID from sender
        private char SenderID;
        //ID from reciever
        private char RecieverID;
        //Length of data
        private int Length;
        //enum states for each server state.
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
    [Serializable]
    public struct Body
    {
        //byte array for data
        public byte[] data;

        public void setData(byte[] buffer)
        {
            data = buffer;
        }
        public byte[] getData() { return data; }
    };
    [Serializable]
    public struct Tail
    {
        //byte array for the send buffer
        public byte[] TxBuffer;
        public void setTxBuffer(byte[] buffer)
        {
            TxBuffer = buffer;
        }
        public byte[] getTxBuffer() { return TxBuffer; }
    }


    //Class Defination
    [Serializable]
    public class Packet
    {
        private Head head;
        private Body body;
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
            //ParaConstructor of the data buffer.
            using (MemoryStream ms = new MemoryStream(data))
            {

                IFormatter br = new BinaryFormatter();
                //weird warning but not sure if it works yet microsoft says its good
                Packet recvPacket = new Packet((Packet)br.Deserialize(ms));
                this.setHead(recvPacket.GetHead().getSenderID(), recvPacket.GetHead().getReciverID(), recvPacket.GetHead().getState());
                this.setData(recvPacket.GetHead().getLength(), recvPacket.GetBody().getData());
            }

        }

        public void SerializeData()
        {
            //serialize the data attribute to the Txbuffer inside tail.

            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {

                //weird warning but not sure if it works yet microsoft says its good
                formatter.Serialize(stream, this);
                this.tail.setTxBuffer(stream.ToArray());
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
            using (MemoryStream ms = new MemoryStream(this.body.getData()))
            {

                IFormatter br = new BinaryFormatter();

                userLoginData loginData = (userLoginData)br.Deserialize(ms);

                return loginData;
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

        public userLoginData GetuserData() {
            return userData;
        }

        public void SetuserData(string username, string password)
        {
            userData.setUserName(username);
            userData.setPassword(password);
        }

        public bool SaveuserData()
        {
            var filePath = "users.txt";
            bool error = false;

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
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

        public bool LoaduserData()
        {
            var filePath = "users.txt";
            bool found = false;

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
                            found = true;
                            break;
                        }

                        line1 = reader.ReadLine();
                        line2 = reader.ReadLine();
                    }
                   


                    reader.Close();
                }
            }
            catch(IOException e)
            {
                Console.WriteLine("An error occurred while writing to the file: " + e.Message);
            }
            
            return found;
        }

    }

      
    




}