using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Server.InterfaceFiles
{
    /// <summary>
    /// Interaction logic for ClientListWindowPage.xaml
    /// </summary>
    public partial class ClientListWindowPage : Page
    {
        private ProgramServer server;
        public ClientListWindowPage(ProgramServer server)
        {
            this.server = server;
            InitializeComponent();
            ClientUsername.Text = this.server.GetuserData().getUserName();
            userLoginData blankUser = new userLoginData();
            if (this.server.GetuserData().getUserName() == blankUser.getUserName())
            {
                ConCircle.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("Red");
                ConStatus.Text = "Not Connected";
                ConStatus.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("Red");
            }
            else
            {
                ConCircle.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF10AB3D");
                ConStatus.Text = "Connected";
                ConStatus.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF10AB3D");
            }
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
            //MANULLAY DISCONNECT CLIENT
            this.server.disconnectClient();
        }
    }
}
