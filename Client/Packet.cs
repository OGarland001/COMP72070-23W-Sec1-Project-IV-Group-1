using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Client
{
    //Enum States
    public enum states
    {
        Idle, Auth, Recv, Analyze
    }
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
    public class Packet
    {
        private Head head;
        private Body body;
        private Tail tail;
        

        public Packet() { 
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

        public Tail GetTail() { return tail;}




    }
}
