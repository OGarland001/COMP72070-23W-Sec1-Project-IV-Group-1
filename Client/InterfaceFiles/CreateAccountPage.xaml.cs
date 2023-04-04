using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.InterfaceFiles
{
    /// <summary>
    /// Interaction logic for CreateAccountPage.xaml
    /// </summary>
    public partial class CreateAccountPage : Page
    {
        public ProgramClient client = new ProgramClient();
        public CreateAccountPage(ref ProgramClient client)
        {
            this.client = client;

            InitializeComponent();
            
        }

        private void UsernameLoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            //Authecate then navigate to next page
            Packet userdataPacket = new Packet();
            //CALL THE CLIENT SEND TO SERVER METHOD AND RETURN A TRUE OR FALSE IF IT WAS AUTHENTICATED
            userdataPacket.setHead('1', '2', states.NewAuth);
            userLoginData loginData = new userLoginData();

            loginData.setUserName(UsernameLoginTextBox.Text);
            loginData.setPassword(PasswordLoginTextBox.Password.ToString());

            byte[] data = new byte[loginData.getUserName().Length + loginData.getPassword().Length];
            data = loginData.serializeData();

            userdataPacket.setData(data.Length, data);

            //display a message box showing a waiting message till the user is connected and authenticated
            MessageBox.Show("Please wait while we connect you to the server");
            client.authenticateUser(userdataPacket);
            MessageBox.Show("Connected!");

            if (client.authentcated)
            {
                Main.Content = new HomePage(ref this.client);
            }
            else
            {
                MessageBox.Show("Invalid Username or Password");
            }
            
        }

        private void Log_In_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new MainPage(ref this.client);

       
        }

        private void Main_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
