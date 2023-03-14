namespace Server_Test_Suite
{
    [TestClass]
    public class ServerUnitTests
    {
        [TestMethod]
        public void TestName01_Inputs_ExpectedOutputs()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void TestName02_Inputs_ExpectedOutputs()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void TestName03_Inputs_ExpectedOutputs()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void TestName04_Inputs_ExpectedOutputs()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void TestName05_Inputs_ExpectedOutputs()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void TestName06_Inputs_ExpectedOutputs()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void TestName07_Inputs_ExpectedOutputs()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void TestName08_Inputs_ExpectedOutputs()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void TestName09_Inputs_ExpectedOutputs()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod]
        public void TestName010_Inputs_ExpectedOutputs()
        {
            //Arrange

            //Act

            //Assert
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