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
        public CreateAccountPage()
        {
            InitializeComponent();
        }

        private void UsernameLoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new HomePage();
        }

        private void Log_In_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new MainPage();

       
        }
    }
}
