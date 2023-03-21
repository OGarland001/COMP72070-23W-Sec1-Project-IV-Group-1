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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void requestlogs_Click(object sender, RoutedEventArgs e)
        {
            //to make request button go to servers account logs window to display logs
            //var accountsWindow = new AccountLogsWindow();

            //accountsWindow.Show();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new MainPage();
            
        }

        private void Upload_an_Image_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Analyze_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Main_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void Main_Navigated_1(object sender, NavigationEventArgs e)
        {

        }

        
    }
}
