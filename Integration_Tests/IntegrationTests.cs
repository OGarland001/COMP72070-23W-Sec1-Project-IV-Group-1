using Client;
using Server;
using System.Windows;

namespace Integration_Tests
{
    [TestClass]
    public class IntegrationTests
    {

        [TestMethod]
        public void INT_TEST_001_RequestandRecieveLogFilesFromServer()
        {
            Client.userLoginData userData = new Client.userLoginData();
            userData.setUserName("tester");
            userData.setPassword("password");
            string Expected = "Username: testerUser signed in:";
            int lengthExpected = Expected.Length;

            try
            {

                // Start the server thread
                Thread serverThread = new Thread(() =>
                {
                    ProgramServer server = new ProgramServer();
                    server.SetuserData(userData.getUserName(), userData.getPassword());
                    server.run();

                });


                // Start the client thread
                Thread clientThread = new Thread(() =>
                {
                    ProgramClient client = new ProgramClient();

                    client.receiveUserlogs();

                });

                serverThread.Start();
                clientThread.Start();

                clientThread.Join();



            }
            catch (Exception e) { Console.WriteLine(e.Message); Assert.Fail(); };
            string count = (userData.getSendCount()).ToString();
            string path = @"../../../ClientLog.txt";
            string fileLine = File.ReadLines(path).First();
            
            Assert.AreEqual(fileLine.Substring(0,lengthExpected), Expected);
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

           
            string filePath = @"../../../TestAuth.txt";


            try
            {

            

                Thread serverThread = new Thread(new ThreadStart(() => {
                    ProgramServer server = new ProgramServer();
                    server.run(); 
                }));

                Thread clientThread = new Thread(new ThreadStart(() => { ProgramClient client = new ProgramClient();
                    if (client.authenticateUser(packet))
                    {
                        //write to a txt file the message "user packet was successfully serialized and deseriailzed"



                        // Write some text to the file
                        using (StreamWriter writer = File.CreateText(filePath))
                        {
                            writer.WriteLine("Authenicated");
                            writer.Close();
                        }

                    }
                    else
                    {

                        // Write some text to the file
                        using (StreamWriter writer = File.CreateText(filePath))
                        {
                            writer.WriteLine("Failed");
                            writer.Close();
                        }

                    }
                }));

              
                serverThread.Start();

                clientThread.Start();

                clientThread.Join();
                
                


            }
            catch (Exception ex) { Console.WriteLine(ex.Message); };

            // Read the text from the file
            using (StreamReader reader = File.OpenText(filePath))
            {
                string text = reader.ReadLine();
                reader.Close();
                Assert.AreEqual("Authenicated", text);

            }


        }
        [TestMethod]
        public void INT_TEST_004_VerifySerlization_Deserialization_Client_Server()
        {
            string userName = "tester";
            string password = "password";

            Client.userLoginData userData = new Client.userLoginData();

            userData.setUserName(userName);
            userData.setPassword(password);

            string filePath = @"../../../TestFile.txt";
            Client.Packet packet = new Client.Packet();

            packet.setHead('1', '2', Client.states.Auth);

            byte[] userDataBuffer = userData.serializeData();

            packet.setData(userDataBuffer.Length, userDataBuffer);




            try
            {
                
                

                Thread serverThread = new Thread(new ThreadStart(() => { ProgramServer server = new ProgramServer(); server.run(); }));
                

                Thread clientThread = new Thread(new ThreadStart(() =>
                {
                   ProgramClient client = new ProgramClient();
                    if (client.authenticateUser(packet))
                    {
                        //write to a txt file the message "user packet was successfully serialized and deseriailzed"



                        // Write some text to the file
                        using (StreamWriter writer = File.CreateText(filePath))
                        {
                            writer.WriteLine("User Login Packet was sucessfully serialized and deserialized");
                            writer.Close();
                        }

                    }
                    else
                    {

                        // Write some text to the file
                        using (StreamWriter writer = File.CreateText(filePath))
                        {
                            writer.WriteLine("Failed");
                            writer.Close();
                        }

                    }
                }));

               serverThread.Start();
                clientThread.Start();
         
                clientThread.Join();

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); };
            // Read the text from the file
            using (StreamReader reader = File.OpenText(filePath))
            {
                string text = reader.ReadLine();
                reader.Close();
                Assert.AreEqual("User Login Packet was sucessfully serialized and deserialized", text);

            }


        }

        [TestMethod]
        public void INT_TEST_006_ServerStatesAreRecievedAndSaved()
        {


        }

        [TestMethod]
        public void INT_TEST_007_ServerCanReturnAnalyzedImage()
        {

            Client.userLoginData userData = new Client.userLoginData();
            userData.setUserName("tester");
            userData.setPassword("password");
            try
            {
               
               
                // Start the server thread
                Thread serverThread = new Thread(() =>
                {
                    ProgramServer server = new ProgramServer();
                    server.SetuserData(userData.getUserName(), userData.getPassword()); 
                    server.run(); });


                serverThread.Start();
                ProgramClient client = new ProgramClient();

                Thread clientThread = new Thread(new ThreadStart(() =>
                {

                    client.sendImage("../../../Tester.jpg");
                    
                    client.receiveImage();
                    
                }));

                
                clientThread.Start();

                clientThread.Join();
                
               
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); };
            // Read the text from the file

           
            

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
                    ProgramServer server = new ProgramServer();
                    server.SetuserData(userData.getUserName(), userData.getPassword());
                    server.run();

                });


                // Start the client thread
                Thread clientThread = new Thread(() =>
                {
                   ProgramClient client = new ProgramClient();

                    client.sendImage("../../../Tester.jpg");
                    
                    client.receiveImage();

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