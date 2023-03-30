using System.Windows;
using System.Windows.Controls;

namespace Client.InterfaceFiles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ProgramClient client = new ProgramClient();
        public MainWindow()
        {
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

            client.authenticateUser(userdataPacket);


            if (client.authentcated)
            {
                Main.Content = new HomePage();
            }
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new CreateAccountPage();
        }

        private void Main_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}
