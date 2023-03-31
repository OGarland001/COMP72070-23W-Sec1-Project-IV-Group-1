using System.Windows;
using System.Windows.Controls;
using System;


namespace Server.InterfaceFiles
{
    /// <summary>
    /// Interaction logic for CreateAccountPage.xaml
    /// </summary>
    public partial class FindAccountPage : Page
    {
        private ProgramServer server;

        public FindAccountPage(ProgramServer server)
        {
            InitializeComponent();
            this.server = server;   
        }

        private void UsernameLoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            bool found = false;
            string line;
            int count = 1;

            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader("../../../Users.txt");

                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains(UsernameLoginTextBox.Text) && count % 2 != 0)
                    {
                        found = true;
                        break;
                    }
                    count++;
                }

                if (!found)
                {
                    Main.Content = new ResultPage(this.server, "The user has not been found in the server logs");
                }
                else
                {
                    Main.Content = new ResultPage(this.server, "The potential user " + UsernameLoginTextBox.Text + " was found and has\nbeen connected to this server in the past.\n\nUser:\n" + line);
                }

                file.Close();
            }
            catch 
            {
                Main.Content = new ResultPage(this.server, "An error occured while attempting to find a user");
            }
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
