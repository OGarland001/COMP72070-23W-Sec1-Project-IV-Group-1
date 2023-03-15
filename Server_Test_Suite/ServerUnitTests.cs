namespace Server_Test_Suite
{
    [TestClass]
    public class ServerUnitTests
    {
        [TestMethod]
        public void SVR_UNIT_TEST_001_DataEncryptedBeforeSend_EncryptedData()
        {
            //Arrange
            //Create Packet 


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
            //Data Buffer For Serialized Data

            //Act
            // Seriailze Packet -- seriailize Data Method
            // Packet Paramterized Constructor -- Pass Buffer into Constructor

            //Assert
            // Assert each element is correct for both packets or create a compare method for Packtets
        }

        [TestMethod]
        public void SVR_UNIT_TEST_003_UpdateHeaderLength_RecievedData_HeaderLength()
        {
            //Arrange
            // Create Packet -- Packet Structure
            // Data Buffer for seriailized Data
            // int Length of data

            //Act
            // Seriailze Packet -- seriailize Data Method
            // Packet Paramterized Constructor -- Pass Buffer into Constructor

            //Assert
            // Asset that Packet header length is equal to length 
        }

        [TestMethod]
        public void SVR_UNIT_TEST_004_DynamicPacketCreated_Packet_DynamicPacketData()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void SVR_UNIT_TEST_005_VerifyAllServerState_StateTrues_AllStatesPresent()
        {
            //Arrange
            //Create Packet
            //Act
            // Set all states to true

            //Assert
            // Verify all states are set to true
        }

        [TestMethod]
        public void SVR_UNIT_TEST_006_ServerIsAlwaysIn_StateTrue_StateTrue()
        {
            //Arrange
            // Create Packet

            //Act
            // Check packet is in state by default or run Recogniation method to check for recogniation state is true

            //Assert
            // Assert Default state is true and rest are false
        }

       

        [TestMethod]
        public void SVR_UNIT_TEST_008_GenerateImage_ImageCreated()

        {
            //Arrange
            //Create Packet 


            //Act
            // Generate Image method -- needs to inputs

            //Assert
            // Assert image is present in the file explorer
        }

        [TestMethod]
        public void SVR_UNIT_TEST_009_SaveClientPasswordUsernameToDatabase_CredentialsSaved()
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
        public void TestName011_Inputs_ExpectedOutputs()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void TestName012_Inputs_ExpectedOutputs()
        {
            //Arrange

            //Act

            //Assert
        }
        [TestMethod]
        public void TestName013_Inputs_ExpectedOutputs()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void TestName014_Inputs_ExpectedOutputs()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void TestName015_Inputs_ExpectedOutputs()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void TestName016_Inputs_ExpectedOutputs()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void TestName017_Inputs_ExpectedOutputs()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void TestName018_Inputs_ExpectedOutputs()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void TestName019_Inputs_ExpectedOutputs()
        {
            //Arrange

            //Act

            //Assert
        }

    }
}