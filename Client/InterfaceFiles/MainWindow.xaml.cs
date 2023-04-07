using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;

namespace Client.InterfaceFiles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ProgramClient client;
        public MainWindow()
        {
            client = new ProgramClient(11069);
            InitializeComponent();
        }

        private void UsernameLoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            //username textbox
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            //Authecate then navigate to next page
            Packet userdataPacket = new Packet();
            //CALL THE CLIENT SEND TO SERVER METHOD AND RETURN A TRUE OR FALSE IF IT WAS AUTHENTICATED
            userdataPacket.setHead('1', '2', states.Auth);
            userLoginData loginData = new userLoginData();

            loginData.setUserName(UsernameLoginTextBox.Text);
            loginData.setPassword(PasswordLoginTextBox.Password.ToString());

            byte[] data = new byte[loginData.getUserName().Length + loginData.getPassword().Length];
            data = loginData.serializeData();

            userdataPacket.setData(data.Length, data);

            //display a message box showing a waiting message till the user is connected and authenticated
            MessageBox.Show("Please wait while we connect you to the server");
            TcpClient clientTcp = new TcpClient(); 
            client.authenticateUser(userdataPacket);
            MessageBox.Show("Connected!");

            if (client.authentcated)
            {
                Main.Content = new HomePage(this.client);
            }
            else
            {
                MessageBox.Show("Invalid Username or Password");
            }
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new CreateAccountPage(ref this.client);
        }

        private void Main_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}
