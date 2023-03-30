using Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace Client_Test_Suite
{
   
    public class ServerStub
    {
        public bool authenicateLoginPacket(Packet packet)
        {
            if(packet.GetHead().getState() == states.Auth)
            {
                userLoginData recvLoginData = packet.deserializeUserLoginData();

                if (recvLoginData.getUserName() == "user123" && recvLoginData.getPassword() == "Password123")
                {
                    return true;
                }
                
            }
            return false;
        }
        public bool CreateUserCreds(Packet packet)
        {
            Packet packet2Recv = new Packet(packet.getTailBuffer());

            if (packet2Recv.GetHead().getState() == states.NewAuth)
            {
                userLoginData recvLoginData = packet2Recv.deserializeUserLoginData();

                using (StreamWriter writer = new StreamWriter("userDataTest.txt"))
                {
                    string line = "" + recvLoginData.getUserName() + ','+ recvLoginData.getPassword() + "\n";
                    writer.WriteLine(line);
                }
                return true;

            }
            return false;
        }
    }
}
