using Server;
using Client;

namespace Integration_Tests
{
    [TestClass]
    public class IntegrationTests
    {
        private ProgramClient client;
        private ProgramServer server;
        [TestMethod]
        public void INT_TEST_002_AuthenticateandVerifyTheUser()
        {

            string userName = "admin";
            string password = "password";

            Client.userLoginData userData = new Client.userLoginData();

            userData.setUserName(userName);
            userData.setPassword(password); 


            Client.Packet packet = new Client.Packet();

            packet.setHead('1', '2', Client.states.Auth);

            byte[] userDataBuffer = userData.serializeData();

            packet.setData(userDataBuffer.Length, userDataBuffer);

            client = new ProgramClient();

            server = new ProgramServer();
            try
            {
                Thread[] threads = new Thread[2];


                threads[0] = new Thread(new ThreadStart(() => { server.run(); }));

                threads[1] = new Thread(new ThreadStart(() => { client.authenticateUser(packet); }));

                threads[0].Start();
                threads[1].Start();

                Assert.IsTrue(client.authentcated);

            }
            catch(Exception ex) { Console.WriteLine(ex.Message); };




            

        }
    }
}