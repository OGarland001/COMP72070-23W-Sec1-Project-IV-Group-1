using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Server.InterfaceFiles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ViewUsersPage : Page
    {
        private ProgramServer server;
        public ViewUsersPage(ProgramServer server)
        {
            this.server = server;
            InitializeComponent();
            string filePath = "../../../Users.txt";
            string fileContents = File.ReadAllText(filePath);
            label.Content = fileContents;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientAccountsWindowPage(server);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Main.Content = new AccountLogsWindowPage(server);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientListWindowPage(server);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Main.Content = new ImagePredictionHistoryWindowPage(server);
        }
    }
}