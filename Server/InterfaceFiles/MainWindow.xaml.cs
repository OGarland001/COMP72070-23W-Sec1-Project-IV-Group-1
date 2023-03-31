using Server.InterfaceFiles;
using System.Windows;

namespace Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProgramServer newServer;
        public MainWindow()
        {
            //START MAIN PROGRAM - Client Connection
            newServer = new ProgramServer();
            //newServer.run();
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientAccountsWindowPage(this.newServer);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Main.Content = new AccountLogsWindowPage(this.newServer);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientListWindowPage(this.newServer);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Main.Content = new ImagePredictionHistoryWindowPage(this.newServer);
        }
    }
}
