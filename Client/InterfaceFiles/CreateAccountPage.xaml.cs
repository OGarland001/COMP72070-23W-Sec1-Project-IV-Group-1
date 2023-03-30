using System;
using System.Collections.Generic;
using System.Linq;
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
        public CreateAccountPage()
        {

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

            client.authenticateUser(userdataPacket);


            if (client.authentcated)
            {
                Main.Content = new HomePage();
            }
            
        }

        private void Log_In_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new MainPage();

       
        }

        private void Main_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
