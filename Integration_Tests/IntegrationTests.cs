using Client;
using Server;

namespace Integration_Tests
{
    [TestClass]
    public class IntegrationTests
    {
        private ProgramClient client;
        private ProgramServer server;

        [TestMethod]
        public void INT_TEST_001_RequestandRecieveLogFilesFromServer()
        {
           //Test 1 and 3
          
        }
        [TestMethod]
        public void INT_TEST_002_AuthenticateandVerifyTheUser()
        {

            string userName = "tester";
            string password = "password";

            Client.userLoginData userData = new Client.userLoginData();

            userData.setUserName(userName);
            userData.setPassword(password);


            Client.Packet packet = new Client.Packet();

            packet.setHead('1', '2', Client.states.Auth);

            byte[] userDataBuffer = userData.serializeData();

            packet.setData(userDataBuffer.Length, userDataBuffer);




            try
            {
                server = new ProgramServer();
                client = new ProgramClient();

                Thread[] threads = new Thread[2];
                threads[0] = new Thread(new ThreadStart(() => { server.run(); }));

                threads[1] = new Thread(new ThreadStart(() => { client.authenticateUser(packet); }));

                threads[0].Start();
                threads[1].Start();



            }
            catch (Exception ex) { Console.WriteLine(ex.Message); };

            //Checks the username within the text file in integration test project
            Assert.IsTrue(server.checkUsername("../Users.txt"));


        }
        [TestMethod]
        public void INT_TEST_004_VerifySerlization_Deserialization_Client_Server()
        {


        }

        [TestMethod]
        public void INT_TEST_006_ServerStatesAreRecievedAndSaved()
        {


        }

        [TestMethod]
        public void INT_TEST_007_ServerCanReturnAnalyzedImage()
        {
            

        }
        [TestMethod]
        public void INT_TEST_008_ClientLogIsRecordedandFormattedProperly()
        {


        }

        [TestMethod]
        public void INT_TEST_009_EachLogHasAnAccurateTimeStamp()
        {


        }

        [TestMethod]
        public void INT_TEST_010_ClientCanConnectToServer()
        {


        }
        [TestMethod]
        public void INT_TEST_011_SucessfulTransmissionOfClientLoginPacket()
        {


        }
        [TestMethod]
        public void INT_TEST_012_SucessfulTransmissionOfClientSignupPacket()
        {


        }
        [TestMethod]
        public void INT_TEST_013_ServerAccuratleyLogsImageRequestsAndTransmissions()
        {


        }
        [TestMethod]
        public void INT_TEST_014_ServerCanSucessfullySignupAndSaveUserSignUpRequest()
        {


        }
        [TestMethod]
        public void INT_TEST_017_PacketHeaderShowsProperSenderAndReciverIDBetweenSends()
        {


        }

        [TestMethod]
        public void INT_TEST_018_EnsureThatServerStaysInOneState()
        {


        }


        [TestMethod]
        public void INT_TEST_023_ServerCanManuallyDisconnectAClient()
        {


        }

        [TestMethod]
        public void INT_TEST_024_SendandRecieveImages_ReturnFullySavedImageOnServer()
        {
            Client.userLoginData userData = new Client.userLoginData();
            userData.setUserName("tester");
            userData.setPassword("password");
            try
            {

                // Start the server thread
                Thread serverThread = new Thread(() =>
                {
                    server = new ProgramServer();
                    server.SetuserData(userData.getUserName(), userData.getPassword());
                    server.run();

                });


                // Start the client thread
                Thread clientThread = new Thread(() =>
                {
                    client = new ProgramClient();
                   
                    client.sendImage("../../../Tester.jpg");

                });

                serverThread.Start();
                clientThread.Start();

                clientThread.Join();
                


            }
            catch (Exception e) { Console.WriteLine(e.Message); Assert.Fail(); };
            string count = (userData.getSendCount()).ToString();
            string path = @"../../../Users/" + userData.getUserName() + "/assets/images/" + userData.getUserName() + count + ".jpg";
            bool result = File.Exists(path);
            Assert.IsTrue(result);


        }
    }
}