using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Printing.IndexedProperties;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using ProtoBuf;

#pragma warning disable SYSLIB0011

namespace Client
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
    }

    //Enum States
    public enum states
    {
        Idle, Auth, NewAuth, Recv, Analyze, Saving, Sending
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

        public Body()
        {
            data = new byte[500];
        }

        public void setData(byte[] buffer)
        {
            data = buffer;
        }
        public byte[] getData() { return data; }
    }    [ProtoContract]
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
}
