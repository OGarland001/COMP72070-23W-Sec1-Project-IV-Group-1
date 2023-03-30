using Microsoft.Win32;
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
    /// Interaction logic for AfterPhotoPage.xaml
    /// </summary>
    public partial class AfterPhotoPage : Page
    {
        public AfterPhotoPage()
        {
            
            InitializeComponent();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new MainPage();
        }

        private void requestlogs_Click(object sender, RoutedEventArgs e)
        {
            //to make request button go to servers account logs window to display logs
            //var accountsWindow = new AccountLogsWindow();

            //accountsWindow.Show();
        }

        private void Upload_an_Image_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Analyze_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this button is analyze side / just for show
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            //this button upload side / just for show
        }
    }
}
