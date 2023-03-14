
namespace Client_Test_Suite
{
    [TestClass]
    public class ClientUnitTests
    {
        [TestMethod]
        public void CLT_UNIT_TEST_001_Testing_Data_Is_Encrypted_ExpectedOutput_Data_Encrypted()
        {
            //Goal
            
            //Arrange
            //Act
            //Assert


        }
        [TestMethod]
        public void CLT_UNIT_TEST_002_Client_Recieved_Through_Para_Constructor_ExpectedOutput_FullyCreatedPacket()
        {

            //Arrange
            //Act
            //Assert


        }
        [TestMethod]
        public void CLT_UNIT_TEST_003_Client_Can_Update_The_HeaderLength_When_Added_ExpectedOutput_Length()
        {
            //Arrange
            //Act
            //Assert

        }

        [TestMethod]
        public void CLT_UNIT_TEST_004_Client_Can_Download_A_Image_ExpectedOutput_ImageFile()
        {
            //Arrange
            //Act
            //Assert

        }
        [TestMethod]
        public void CLT_UNIT_TEST_005_Client_Can_Login_To_The_Server_ExpectedOutput_Sucessful_Login()
        {
            //Arrange
            //Act
            //Assert

        }
        [TestMethod]
        public void CLT_UNIT_TEST_006_Client_Can_View_The_Processed_Image_ExpectedOutput_Sucessful_Processed_Image()
        {
            //Arrang

            //Act

            //Assert


        }
        [TestMethod]
        public void CLT_UNIT_TEST_007__Client_requests_for_logs_of_client__ExpctedOuput_Client_recieves_the_client_logs()
        {
            /* 
            //Arrang
            - create a log packet
            - send the packet to server from the packet class
            - client will wait for a packet from the server for its log
            - print out the recieved packet in client

            //Act
            - 

            //Assert
            
            */
        }
        [TestMethod]
        public void CLT_UNIT_TEST_008__Client_accepts_image_from_user__ExpctedOuput_Client_sends_accepted_image_to_server()
        {
            /* 
            //Arrang
            - create a image packet from the packet class
            - take incoming image packet and store it in client
            - client will then send the image packet to server 

            //Act
            - 

            //Assert
            
            */
        }
        [TestMethod]
        public void CLT_UNIT_TEST_009__Client_creates_account_for_new_user_login__ExpctedOuput_Account_structure_updated_adding_new_created_user()
        {
            /* 
            //Arrang
            - create user login data
            - send data to client
            - get client to accept the login and authenticate its legit

            //Act


            //Assert
            
            */
        }
        [TestMethod]
        public void CLT_UNIT_TEST_010__GUI_Shows_Client_Features__FeaturesAllShown()
        {
            /* system level test
            //Arrang
            gui features must be printed to screen

            //Act


            //Assert
            features of clients gui is going to be printed correctly
            */
            //Arrange
            //Act
            //Assert

        }
    }
}