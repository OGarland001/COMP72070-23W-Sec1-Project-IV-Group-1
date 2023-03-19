
using Client;
using System.Text;

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
            Packet packet1 = new Packet();
            
           

            string letters = "data";
            int length = letters.Length;
            byte[] data = new byte[length];
            data = Encoding.ASCII.GetBytes(letters);

            //Act

            //serialize the data -- seralize data method
            //create a packet structure with the serialized data Act like -- para constructor for packet
            packet1.setHead('1', '2', states.Idle);
            packet1.setData(length, data);

            packet1.SerializeData();

            Packet recv = new Packet(packet1.GetTail().getTxBuffer());
            //Assert

            Assert.AreEqual(packet1.GetHead().getSenderID(), recv.GetHead().getSenderID());

            Assert.AreEqual(packet1.GetHead().getReciverID(), recv.GetHead().getReciverID());

            Assert.AreEqual(packet1.GetHead().getLength(), recv.GetHead().getLength());

            Assert.AreEqual(packet1.GetBody().getData(), recv.GetBody().getData());

        }
        [TestMethod]
        public void CLT_UNIT_TEST_003_Client_Can_Update_The_HeaderLength_When_Added_ExpectedOutput_Length()
        {
            //Arrange
            Packet packet = new Packet();
            byte[] data= new byte[5];
            int length = 5;


            //Act
            packet.GetHead().setReciverID('1');
            packet.GetHead().setSenderID('2');
            packet.setData(length, data);
            //Assert
            Assert.AreEqual(5, packet.GetHead().getLength());

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