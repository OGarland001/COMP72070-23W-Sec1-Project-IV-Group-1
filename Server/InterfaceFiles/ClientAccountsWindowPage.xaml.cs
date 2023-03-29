using System.Windows;
using System.Windows.Controls;


namespace Server.InterfaceFiles
{
    /// <summary>
    /// Interaction logic for ClientAccountsWindowPage.xaml
    /// </summary>
    public partial class ClientAccountsWindowPage : Page
    {
        private ProgramServer server;
        public ClientAccountsWindowPage(ProgramServer server)
        {
            this.server = server;
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientAccountsWindowPage(this.server);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Main.Content = new AccountLogsWindowPage(this.server);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientListWindowPage(this.server);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Main.Content = new ImagePredictionHistoryWindowPage(this.server);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Main.Content = new CreateAccountPage(this.server);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Main.Content = new DeleteAccountPage(this.server);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Main.Content = new FindAccountPage(this.server);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {

        }
    }
}
