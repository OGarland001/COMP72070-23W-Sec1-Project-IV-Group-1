using Server;
using Client;

namespace Integration_Tests
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void INT_TEST_002_AuthenticateandVerifyTheUser()
        {
            string userName = "userLogin";
            string password = "password123";

            Client.userLoginData userData = new Client.userLoginData();

            userData.setUserName(userName);
            userData.setPassword(password); 


            Client.Packet packet = new Client.Packet();

            packet.setHead('1', '2', Client.states.Auth);

            byte[] userDataBuffer = userData.serializeData();

            packet.setData(userDataBuffer.Length, userDataBuffer);




        }
    }
}