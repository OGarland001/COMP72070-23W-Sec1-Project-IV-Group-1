using Google.Protobuf.WellKnownTypes;
using Server;
using Server.ML.NET.YoloParser;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using static SkiaSharp.SKPath;


namespace Server_Test_Suite
{
    [TestClass]
    public class ServerUnitTests
    {
        [TestMethod]
        public void SVR_UNIT_TEST_001_DataEncryptedBeforeSend_EncryptedData() //Goal
        {
            //Arrange

            //Act
            //Serialize the packet -- Save output to a data buffer
            //Encrypt buffer -- Encryption Class and save to encryption buffer

            //Assert

            //Assert buffer encryption doesn't equal seriailize buffer 
            //Assert Deserialize -- Parameterized  Packet Constructor and Compare to Packet
        }

        [TestMethod]
        public void SVR_UNIT_TEST_002_PacketParseData_RecievedData_InitializedPacket()
        {
            //Arrange
            //Create Packet -- Packet Structure
            Packet packet = new Packet();
            byte[] buffer = Encoding.ASCII.GetBytes("Hello!");
            //byte[] Sendbuffer = { };
            packet.setData(6, buffer);
            packet.setHead((char)1, (char)2, states.Recv);
           

            //packet.GetTail().setTxBuffer(Sendbuffer);


            //Act
            // Seriailze Packet -- seriailize Data Method
            // Packet Paramterized Constructor -- Pass Buffer into Constructor
            packet.SerializeData();
            Packet RecievePacket = new Packet(packet.getTailBuffer());

            //Assert
            Assert.AreEqual(packet.GetHead().getLength(), RecievePacket.GetHead().getLength());
            // Assert each element is correct for both packets or create a compare method for Packtets
        }

        [TestMethod]
        public void SVR_UNIT_TEST_003_UpdateHeaderLength_RecievedData_HeaderLength()
        {
            //Arrange
            // Create Packet -- Packet Structure
            // Data Buffer for seriailized Data
            // int Length of data
            int length = 10;
            Packet packet = new Packet();
            byte[] buffer = Encoding.ASCII.GetBytes("PacketBody");


            //Act
            // Seriailze Packet -- seriailize Data Method
            // Packet Paramterized Constructor -- Pass Buffer into Constructor
            packet.setData(buffer.Length, buffer);
            packet.setHead((char)1, (char)2, states.Analyze);

            //Assert
            // Asset that Packet header length is equal to length 
            Assert.AreEqual(length, packet.GetHead().getLength());
        }

        [TestMethod]
        public void SVR_UNIT_TEST_004_DynamicPacketCreated_Packet_DynamicPacketData() //after integration
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void SVR_UNIT_TEST_005_VerifyAllServerState_StateTrue_StateTrue()
        {
            //Arrange
            ProgramServer serverIDlE = new ProgramServer();
            ProgramServer serverAUTHENTICATE = new ProgramServer();
            ProgramServer serverRecieve = new ProgramServer();
            ProgramServer serverAnalyze = new ProgramServer();
            ProgramServer serverSending = new ProgramServer();
            ProgramServer serverSave = new ProgramServer();
            //Act
            // Set all states to true
            serverAUTHENTICATE.setAutenticatingState();
            serverRecieve.setReceivingPacketsState();
            serverAnalyze.setAnalyzingImagesState();
            serverSending.setSendingAnalyzedImagesState();
            serverSave.setSavingImagesState();

            //Assert
            // Verify all states are set to true
            Assert.AreEqual(serverIDlE.getCurrentState(), states.Idle);
            Assert.AreEqual(serverAUTHENTICATE.getCurrentState(), states.Auth);
            Assert.AreEqual(serverRecieve.getCurrentState(), states.Recv);
            Assert.AreEqual(serverAnalyze.getCurrentState(), states.Analyze);
            Assert.AreEqual(serverSending.getCurrentState(), states.Sending);
            Assert.AreEqual(serverSave.getCurrentState(), states.Saving);

        }

        [TestMethod]
        public void SVR_UNIT_TEST_006_ServerIsAlwaysIn_StateTrue_StateTrue()
        {
            //Arrange
            // Create Packet
            ProgramServer server = new ProgramServer();

            //Act
            // Check packet is in state by default or run Recogniation method to check for recogniation state is true
            server.RunRecognition();

            //Assert
            // Assert Analyzing State is set to current state not the default
            Assert.AreNotEqual(server.getCurrentState(), states.Idle);
        }



        [TestMethod]
        public void SVR_UNIT_TEST_008_GenerateImage_ImageCreated()

        {
            //Arrange
            //Create Packet
            
            ProgramServer program = new ProgramServer();
            int width = 0;
            int height = 0;

            //Act
            // Generate Image method -- needs to inputs
            program.RunRecognition();
            
            string imageName = @"../../../ML.NET/assets\images\output\Bicycle.jpg";
            Image createdImage = Image.FromFile(imageName);
            width = createdImage.Size.Width;
            height = createdImage.Size.Height;


            //Assert
            // Assert image is present in the file explorer
            Assert.IsTrue(width > 0 && height > 0);
        }

        [TestMethod]
        public void SVR_UNIT_TEST_009_SaveClientPasswordUsernameToFile_CredentialsSaved()
        {
            //Arrange
            //Data buffer for username and password
            //Create login Packet for new user
            string username = "NewUser1222";
            string password = "User444$$";

            Packet packet = new Packet();
            userLoginData loginData;


            //Act
            // Check login packet with database/file is there
            loginData.setUserName(username);
            loginData.setPassword(password);

            byte[] body = new byte[username.Length + password.Length];

            body = loginData.serializeData();

            packet.setHead((char)3, (char)4, states.Sending);
            packet.setData(body.Length, body);
            packet.SerializeData();
            Packet RecievePacket = new Packet(packet.getTailBuffer());

            login userlogin = new login(RecievePacket);

            bool Error = userlogin.SaveuserData("users.txt");

            string Correct = userlogin.SignInUser("users.txt");

            //Assert
            
            Assert.AreEqual("User signed in", Correct);
            Assert.IsFalse(Error);
        }

        [TestMethod]
        public void SVR_UNIT_TEST_010_AdminLogin_UsernamePassword_PacketAutheticated()
        {
            //Arrange
            // Data buffer username and password
            // Create Login Packet
            string username = "Tester88";
            string password = "!QAZ1qaz";

            Packet packet = new Packet();
            userLoginData loginData;


            //Act
            // Check login packet with database/file is there
            loginData.setUserName(username);
            loginData.setPassword(password);

            byte[] body = new byte[username.Length + password.Length];

            body = loginData.serializeData();

            packet.setHead((char)1, (char)2, states.Auth);
            packet.setData(body.Length, body);
            packet.SerializeData();
            Packet RecievePacket = new Packet(packet.getTailBuffer());

            login userlogin = new login(RecievePacket);
            ///userlogin.SaveuserData("users.txt");
            string Correct = userlogin.SignInUser("TestingUsers.txt");

            //Assert
            Assert.AreEqual("User signed in",Correct);
        }

        [TestMethod]
        public void SVR_UNIT_TEST_011_DownloadImage_DownloadRequest_ImageDownloaded()
        {
            //Arrange

            //server generated image - can the image be downloaded

            //Act

            //download the image

            //Assert

            //Can the user open the image from the specified directory
        }

        [TestMethod]
        public void SVR_UNIT_TEST_012_NotUniqueUser_NewUserRequestWithoutUnique_IdentifiedAsNotUnique()
        {
            //Arrange
            login existinglogin = new login();
            login login = new login();
            string username = "Tester88";
            string password = "Yellow$E2";
            string existingUsername = username;
            string existingPassword = "RandomPassword21!";

            //Client server connection established
            //Client requests authentication - invalid authentication

            //Act

            login.SetuserData(username, password);
            existinglogin.SetuserData(existingUsername, existingPassword);
            existinglogin.SaveuserData("users.txt");
            string result = login.RegisterUser("users.txt");
            //Attempt to perform authentication in server

            //Assert
            Assert.AreEqual("Username must be unique", result);
            //Did a response get generated that the user was not unique
        }
        
        [TestMethod]
        public void SVR_UNIT_TEST_013_ImageDetections_DogImage_DogClassifciation()
        {
            //Arrange

            //Server integrated with the API image detection
            //Send the image to the api
            ProgramServer server = new ProgramServer();


            // Collect the response from the API
            string[,] objects = server.RunRecognition();

            if (objects != null)
            {
                Assert.AreEqual("dog", objects[1, 0]);
            }
        }

        [TestMethod]
        public void SVR_UNIT_TEST_014_SaveUserCredentials_NewClient_CredentialsSaved()
        {
            //Arrange

            //Client server connection established
            //Client requests authentication - valid authentication

            login login = new login();
            string username = "RandomUser1";
            string password = "Password990$";


            //Act
            //Perform authentication in server
            login.SetuserData(username, password);
            string result = login.RegisterUser("users.txt");

            //Assert
            Assert.AreEqual("User registered", result);

            //clear contents of file so username is unique everytime
            System.IO.File.WriteAllText("users.txt", string.Empty);
            //Did the users credentials get saved to the file?
        }

        [TestMethod]
        public void SVR_UNIT_TEST_015_AuthenticateClient_NewClient_ClientAuthenticated()
        {
            //Arrange
            //Client requests authentication - valid authentication
            login login = new login();
            string username = "Tester9six1";
            string password = "123456";

            //Act
            //Perform authentication in server
            login.SetuserData(username, password);
            string result = login.RegisterUser("users.txt");

            //Assert
            //Did the user get authorized?
            Assert.AreEqual("User Registered", result);
        }

        [TestMethod]
        public void SVR_UNIT_TEST_016_RecvImage_ClientSendsImage_ServerReceivesImageAndCanOpenIt() //after integration
        {
            //Arrange

            //Client server connections
            //Client sends image packet

            //Act

            //recv image packet

            //Assert

            //attempt to open image packet
        }

        [TestMethod]
        public void SVR_UNIT_TEST_017_ViewFiles_OpenFiles_FilesOpen()
        {
            //Arrange
            ProgramServer server = new ProgramServer(); 

            //Act
            string readInfo = server.openFile("testFile.txt");

            //Assert
            Assert.AreEqual("This is a test", readInfo);
        }

        [TestMethod]
        public void SVR_UNIT_TEST_019_SaveStateChanges_StateChange_ChangeShownInFile()
        {
            //Arrange
            ProgramServer server = new ProgramServer();

            //Act
            server.setAutenticatingState();

            //Assert
            //open the file and search for the line reflecting this state change
            string readInfo = server.openFile("ServerLog.txt");
            Assert.IsTrue(readInfo.Contains("Username: Server state changed to authenticating: Time of day"));
        }
    }
}