using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Server.InterfaceFiles
{
    /// <summary>
    /// Interaction logic for CreateAccountPage.xaml
    /// </summary>
    public partial class DeleteAccountPage : Page
    {
        private ProgramServer server;
        public DeleteAccountPage(ProgramServer server)
        {
            InitializeComponent();
            this.server = server;   
        }

        private void UsernameLoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            string line;
            bool deletedUserName = false;
          
            try
            {
                string fileName = "../../../Users.txt";
                string tempFileName = "temp.txt";
                string lineToDelete = UsernameLoginTextBox.Text;

                // Open the original file for reading
                using (StreamReader reader = new StreamReader(fileName))
                {
                    // Open a temporary file for writing
                    using (StreamWriter writer = new StreamWriter(tempFileName))
                    {
                        // Read the file line by line
                        while ((line = reader.ReadLine()) != null)
                        {
                            // If the line doesn't match, write it to the temporary file
                            if (line != lineToDelete && !deletedUserName)
                            {
                                writer.WriteLine(line);
                            }
                            else if(deletedUserName)
                            {
                                deletedUserName = false;
                            }
                            else
                            {
                                deletedUserName = true;
                            }
                        }
                    }
                }

                // Replace the original file with the temporary file
                File.Delete(fileName);
                File.Move(tempFileName, fileName);
                Main.Content = new ResultPage(this.server, "The user " + UsernameLoginTextBox.Text + " was deleted from the valid clients list and is now unauthorized to login to this server unless re-authenticated");

            }
            catch
            {
                Main.Content = new ResultPage(this.server, "An error occured while attempting to delete a user");
            }
        }

        private void Log_In_Click(object sender, RoutedEventArgs e)
        {
       
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Main.Content = new ImagePredictionHistoryWindowPage(this.server);
        }

        private void ClientList(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientListWindowPage(this.server);
        }

        private void AccountLogs(object sender, RoutedEventArgs e)
        {
            Main.Content = new AccountLogsWindowPage(this.server);
        }

        private void ClientAccountsWindowPage(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientAccountsWindowPage(this.server);
        }
    }
}
