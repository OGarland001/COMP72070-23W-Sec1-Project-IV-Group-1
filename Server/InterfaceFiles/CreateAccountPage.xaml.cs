using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Server.InterfaceFiles
{
    /// <summary>
    /// Interaction logic for CreateAccountPage.xaml
    /// </summary>
    public partial class CreateAccountPage : Page
    {
        private ProgramServer server;
        public CreateAccountPage(ProgramServer server)
        {
            InitializeComponent();
            this.server = server;
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            bool found = false;
            string line;

            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader("../../../Users.txt");

                while ((line = file.ReadLine()) != null)
                {
                    if (line.Equals(UsernameLoginTextBox.Text))
                    {
                        found = true;
                        break;
                    }
                }

                file.Close();

                //if the username is unique allow them to create it
                if (!found)
                {
                    using (StreamWriter writer = new StreamWriter("../../../Users.txt", append: true))
                    {
                        writer.WriteLine(UsernameLoginTextBox.Text);
                        writer.WriteLine(PasswordLoginTextBox.Password.ToString());
                        writer.Close();
                    }

                    Main.Content = new ResultPage(this.server, "The user " + UsernameLoginTextBox.Text + " was created and is now authorized to login to this server");
                }
                //if its not unique say so
                else
                {
                    Main.Content = new ResultPage(this.server, "The user " + UsernameLoginTextBox.Text + " is not unique please view the list of authorized users in the server and select a unique name");
                }

            }
            catch
            {
                Main.Content = new ResultPage(this.server, "An error occured while attempting to create a user");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Main.Content = new ImagePredictionHistoryWindowPage(this.server);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientListWindowPage(this.server);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Main.Content = new AccountLogsWindowPage(this.server);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientAccountsWindowPage(this.server);
        }
    }
}
