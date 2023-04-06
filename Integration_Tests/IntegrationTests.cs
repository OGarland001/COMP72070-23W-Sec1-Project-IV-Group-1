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
                serverThread.Abort();



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

                serverThread.Abort();




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
                serverThread.Abort();

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
                serverThread.Abort();


            }
            catch (Exception ex) { Console.WriteLine(ex.Message); };
            // Read the text from the file

            Assert.IsTrue(File.Exists("../../../UserImages/Output.jpg"));
            

        }
        [TestMethod]
        public void INT_TEST_008_ClientLogIsRecordedandFormattedProperly()
        {
            Client.userLoginData userData = new Client.userLoginData();
            userData.setUserName("Randomtester");
            userData.setPassword("password");
            string ExpectedImage = "Username: Randomtester  Sent Image received:";
            //string ExpectedAnalyzedImage = "Username: RandomtesterRandomtester1.jpg Anayzed image has been created: Time of day 2023-04-05 3:44:33 PM\""
            int lengthExpectedImage = ExpectedImage.Length;
            //int lengthExpectedAnalyzedImage = ExpectedAnalyzedImage.Length;
            File.WriteAllText("../../../Users/Randomtester/Randomtester.txt", "0");

            try
            {

                Thread serverThread = new Thread(() =>
                {
                    ProgramServer server = new ProgramServer();
                    server.SetuserData(userData.getUserName(), userData.getPassword());
                    server.run();
                });


                serverThread.Start();
                ProgramClient client = new ProgramClient();

                Thread clientThread = new Thread(new ThreadStart(() =>
                {

                    client.sendImage("../../../Tester.jpg");
                    client.receiveImage();
                    client.receiveUserlogs();

                }));

                
                
                clientThread.Start();

                clientThread.Join();
                serverThread.Abort();



            }
            catch (Exception e) { Console.WriteLine(e.Message); Assert.Fail(); };
            string count = (userData.getSendCount()).ToString();
            string path = @"../../../ClientLog.txt";
            string fileLine = File.ReadLines(path).First();

            Assert.AreEqual(fileLine.Substring(0, lengthExpectedImage), ExpectedImage);

        }

        [TestMethod]
        public void INT_TEST_009_EachLogHasAnAccurateTimeStamp()
        {
            Client.userLoginData userData = new Client.userLoginData();
            userData.setUserName("Randomtester");
            userData.setPassword("password");
            string ExpectedImage = "Username: Randomtester  Sent Image received:";
            //string ExpectedAnalyzedImage = "Username: RandomtesterRandomtester1.jpg Anayzed image has been created: Time of day 2023-04-05 3:44:33 PM\""
            //int lengthExpectedImage = ExpectedImage.Length;
            //int lengthExpectedAnalyzedImage = ExpectedAnalyzedImage.Length;
            File.WriteAllText("../../../Users/Randomtester/Randomtester.txt", "0");

            try
            {

                Thread serverThread = new Thread(() =>
                {
                    ProgramServer server = new ProgramServer();
                    server.SetuserData(userData.getUserName(), userData.getPassword());
                    server.run();
                });


                serverThread.Start();
                ProgramClient client = new ProgramClient();

                Thread clientThread = new Thread(new ThreadStart(() =>
                {

                    client.sendImage("../../../Tester.jpg");
                    client.receiveImage();
                    client.receiveUserlogs();

                }));



                clientThread.Start();

                clientThread.Join();
                serverThread.Abort();



            }
            catch (Exception e) { Console.WriteLine(e.Message); Assert.Fail(); };
            string count = (userData.getSendCount()).ToString();
            string path = @"../../../ClientLog.txt";
            string fileLine = File.ReadLines(path).First();
            int index= fileLine.IndexOf('2');

            Assert.AreEqual(fileLine.Substring(index), "2023-04-05 9:16:57 PM");

        }

        [TestMethod]
        public void INT_TEST_010_ClientCanConnectToServer()
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


                serverThread.Start();
                ProgramClient client = new ProgramClient();
               
                Assert.AreNotEqual(null, client);

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); };
           

            
        }
       
        [TestMethod]
        public void INT_TEST_012_SucessfulTransmissionOfClientSignupPacket()
        {
            //MUST DELETE FROM THE FILE BEFORE RUNNING THIS TEST

            string userName = "newUser";
            string password = "newPassword";

            Client.userLoginData userData = new Client.userLoginData();

            userData.setUserName(userName);
            userData.setPassword(password);


            Client.Packet packet = new Client.Packet();

            packet.setHead('1', '2', Client.states.NewAuth);

            byte[] userDataBuffer = userData.serializeData();

            packet.setData(userDataBuffer.Length, userDataBuffer);


            string filePath = @"../../../TestAuth.txt";


            try
            {



                Thread serverThread = new Thread(new ThreadStart(() => {
                    ProgramServer server = new ProgramServer();
                    
                    server.run();
                }));

                Thread clientThread = new Thread(new ThreadStart(() => {
                    ProgramClient client = new ProgramClient();
                    if (client.authenticateUser(packet))
                    {
                        //write to a txt file the message "user packet was successfully serialized and deseriailzed"



                        // Write some text to the file
                        using (StreamWriter writer = File.CreateText(filePath))
                        {
                            writer.WriteLine("New User Added");
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

                serverThread.Abort();




            }
            catch (Exception ex) { Console.WriteLine(ex.Message); };

            // Read the text from the file
            using (StreamReader reader = File.OpenText(filePath))
            {
                string text = reader.ReadLine();
                reader.Close();
                Assert.AreEqual("New User Added", text);

            }

        }
        [TestMethod]
        public void INT_TEST_013_ServerAccuratleyLogsImageRequestsAndTransmissions()
        {
            Client.userLoginData userData = new Client.userLoginData();
            userData.setUserName("Randomtester");
            userData.setPassword("password");
            string Expected = "Username: RandomtesterRandomtester1.jpg Anayzed image has been created:";
            //string ExpectedAnalyzedImage = "Username: RandomtesterRandomtester1.jpg Anayzed image has been created: Time of day 2023-04-05 3:44:33 PM\""
            //int lengthExpectedImage = ExpectedImage.Length;
            //int lengthExpectedAnalyzedImage = ExpectedAnalyzedImage.Length;
            File.WriteAllText("../../../Users/Randomtester/Randomtester.txt", "0");
            bool found = false;

            try
            {

                Thread serverThread = new Thread(() =>
                {
                    ProgramServer server = new ProgramServer();
                    server.SetuserData(userData.getUserName(), userData.getPassword());
                    server.run();
                });


                serverThread.Start();
                ProgramClient client = new ProgramClient();

                Thread clientThread = new Thread(new ThreadStart(() =>
                {

                    client.sendImage("../../../Tester.jpg");
                    client.receiveImage();
                    //client.receiveUserlogs();

                }));



                clientThread.Start();

                clientThread.Join();



            }
            catch (Exception e) { Console.WriteLine(e.Message); Assert.Fail(); };
            string count = (userData.getSendCount()).ToString();
            string path = @"../../../ServerLog.txt";

            //int index = fileLine.IndexOf('2');
            if (System.IO.File.ReadAllText(path).Contains(Expected))
            {
                found = true;

            }


            Assert.IsTrue(found);

        }
        
        [TestMethod]
        public void INT_TEST_017_PacketHeaderShowsProperSenderAndReciverIDBetweenSends()
        {
            Client.userLoginData userData = new Client.userLoginData();
            userData.setUserName("Randomtester");
            userData.setPassword("password");
            File.WriteAllText("../../../Users/Randomtester/Randomtester.txt", "0");
            

            try
            {
                char clientCA;
                char ServerSA;
                char ServerCA;
                char clientSA;

                ProgramServer server = new ProgramServer();
                Thread serverThread = new Thread(() =>
                {
                    
                    
                    server.SetuserData(userData.getUserName(), userData.getPassword());
                    server.run();
                });
                ServerSA = server.getServeraddress();
                ServerCA = server.getClientaddress();

                serverThread.Start();
                ProgramClient client = new ProgramClient();

                Thread clientThread = new Thread(new ThreadStart(() =>
                {

                    client.sendImage("../../../Tester.jpg");
                    client.receiveImage();
                    
                    //client.receiveUserlogs();

                }));
                clientCA = client.getClientaddress();
                clientSA = client.getServeraddress();


                clientThread.Start();

                clientThread.Join();


                Assert.AreEqual(clientSA, ServerSA);
                Assert.AreEqual(clientCA, ServerCA);
            }
            catch (Exception e) { Console.WriteLine(e.Message); Assert.Fail(); };

            

        }

        [TestMethod]
        public void INT_TEST_018_EnsureThatServerStaysInOneState()
        {
            Client.userLoginData userData = new Client.userLoginData();
            userData.setUserName("tester");
            userData.setPassword("password");

            try
            {
                ProgramServer server = new ProgramServer();

                // Start the server thread
                Thread serverThread = new Thread(() =>
                {
                    server.SetuserData(userData.getUserName(), userData.getPassword());
                    server.run();
                });


                serverThread.Start();
                ProgramClient client = new ProgramClient();

                Assert.AreEqual(Server.states.Idle, server.getCurrentState());

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); };

        }


        [TestMethod]
        public void INT_TEST_023_ServerCanManuallyDisconnectAClient()
        {
            Client.userLoginData userData = new Client.userLoginData();
            //Client.userLoginData NoUser = new Client.userLoginData();
            string username;
            userData.setUserName("Randomtester");
            userData.setPassword("password");
           
           
            try
            {
                ProgramServer server = new ProgramServer();

                Thread serverThread = new Thread(() =>
                {
                    
                    server.SetuserData(userData.getUserName(), userData.getPassword());
                    server.disconnectClient();
                    server.run();

                    
                    
                });
                


                serverThread.Start();
                ProgramClient client = new ProgramClient();

                Thread clientThread = new Thread(new ThreadStart(() =>
                {

                    client.checkConnection();

                }));



                clientThread.Start();

                clientThread.Join();
                username = server.GetuserData().getUserName();

                Assert.AreNotEqual("Randomtester", username);

            }
            catch (Exception e) { Console.WriteLine(e.Message); Assert.Fail(); };
            string count = (userData.getSendCount()).ToString();
            
           
            

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
                serverThread.Abort();



            }
            catch (Exception e) { Console.WriteLine(e.Message); Assert.Fail(); };
            string count = (userData.getSendCount()).ToString();
            string path = @"../../../Users/" + userData.getUserName() + "/assets/images/" + userData.getUserName() + count + ".jpg";
            bool result = File.Exists(path);
            Assert.IsTrue(result);


        }
    }
}