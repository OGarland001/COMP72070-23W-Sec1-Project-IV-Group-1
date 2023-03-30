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
