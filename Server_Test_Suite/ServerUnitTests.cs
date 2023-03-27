using Server;
using System.Text;


namespace Server_Test_Suite
{
    [TestClass]
    public class ServerUnitTests
    {
        [TestMethod]
        public void SVR_UNIT_TEST_001_DataEncryptedBeforeSend_EncryptedData()
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
            packet.setHead((char)1, (char)2, states.Analyze);
           

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
        public void SVR_UNIT_TEST_004_DynamicPacketCreated_Packet_DynamicPacketData()//----------------------------------------------------------HERE BRODIN
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void SVR_UNIT_TEST_005_VerifyAllServerState_StateTrue_StateTrue()//----------------------------------------------------------HERE BRODIN
        {
            //Arrange
            //Create Packet
            //Act
            // Set all states to true

            //Assert
            // Verify all states are set to true
        }

        [TestMethod]
        public void SVR_UNIT_TEST_006_ServerIsAlwaysIn_StateTrue_StateTrue()//----------------------------------------------------------HERE BRODIN
        {
            //Arrange
            // Create Packet

            //Act
            // Check packet is in state by default or run Recogniation method to check for recogniation state is true

            //Assert
            // Assert Default state is true and rest are false
        }



        [TestMethod]
        public void SVR_UNIT_TEST_008_GenerateImage_ImageCreated()//----------------------------------------------------------HERE BRODIN

        {
            //Arrange
            //Create Packet
            string imageName = "../../../ML.NET/assets\\images";
            ProgramServer program = new ProgramServer();
            int length = 0;


            //Create Packet 
            
            //Act
            // Generate Image method -- needs to inputs
            program.RunRecognition();
            //Image createdImage = Image.FromFile(imageName);
            //length = createdImage.Size.Width;


            //Assert
            // Assert image is present in the file explorer
            Assert.IsTrue(length > 0);
        }

        [TestMethod]
        public void SVR_UNIT_TEST_009_SaveClientPasswordUsernameToFile_CredentialsSaved()
        {
            //Arrange
            //Data buffer for username and password
            //Create login Packet for new user

            //Act
            //Call Database access method to save new client's credentials

            //Assert
            // Read Database method to ensure client is actually saved
        }

        [TestMethod]
        public void SVR_UNIT_TEST_010_AdminLogin_UsernamePassword_PacketAutheticated()
        {
            //Arrange
            // Data buffer username and password
            // Create Login Packet

            //Act
            // Check login packet with database/file is there

            //Assert
            // Assert that file username and password are same as inputted 
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

            //Client server connection established
            //Client requests authentication - invalid authentication

            //Act

            //Attempt to perform authentication in server

            //Assert

            //Did a response get generated that the user was not unique
        }
        [TestMethod]
        public void SVR_UNIT_TEST_013_ImageDetections_DogImage_DogClassifciation()
        {
            //Arrange

            //Server integrated with the API image detection
            //Send the image to the api

            //Act

            //collect the response from the api

            //Assert

            //does the API response match the expectation
        }

        [TestMethod]
        public void SVR_UNIT_TEST_014_SaveUserCredentials_NewClient_CredentialsSaved()
        {
            //Arrange

            //Client server connection established
            //Client requests authentication - valid authentication

            //Act

            //Perform authentication in server

            //Assert

            //Did the users credentials get saved to the file?
        }

        [TestMethod]
        public void SVR_UNIT_TEST_015_AuthenticateClient_NewClient_ClientAuthenticated()
        {
            //Arrange

            //Client server connection established
            //Client requests authentication - valid authentication

            //Act

            //Perform authentication in server

            //Assert

            //Did the user get authorized?
        }

        [TestMethod]
        public void SVR_UNIT_TEST_016_RecvImage_ClientSendsImage_ServerReceivesImageAndCanOpenIt()
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

            //no arrange

            //Act

            //open all test logs and files

            //Assert

            //verify all files are openable
        }

        [TestMethod]
        public void SVR_UNIT_TEST_018_DisconnetClient_SelectDisconnect_ClientDisconnects()
        {
            //Arrange

            //establish client server connetion

            //Act

            //trigger disconnect function in the server

            //Assert

            //ensure the client closes - server waits for new connections

        }

        [TestMethod]
        public void SVR_UNIT_TEST_019_SaveStateChanges_StateChange_ChangeShownInFile()
        {
            //Arrange

            //Inititate and state change

            //Act

            //change states (different state then what was initialized)

            //Assert

            //open the file and search for the line reflecting this state change
        }

    }
}