
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Client.InterfaceFiles
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
   
    public partial class MainPage : Page
    {
        public ProgramClient client = new ProgramClient();
        public MainPage()
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

            userdataPacket.setData(loginData.serializeData().Length, loginData.serializeData());

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

        private void Main_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
