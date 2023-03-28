
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
            bool authenticatedUser = false;

            //CALL THE CLIENT SEND TO SERVER METHOD AND RETURN A TRUE OR FALSE IF IT WAS AUTHENTICATED

            //authenticate passed
            authenticatedUser = true;

            if (authenticatedUser)
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
