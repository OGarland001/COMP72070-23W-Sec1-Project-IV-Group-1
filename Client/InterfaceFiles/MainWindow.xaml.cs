using System.Windows;
using System.Windows.Controls;

namespace Client.InterfaceFiles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UsernameLoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //username textbox TextChanged="UsernameLoginTextBox_TextChanged"
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            //add a check for the credentials of user / if correct then login
            Main.Content = new HomePage();
            // if not do not login
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
