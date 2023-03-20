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

namespace Server.InterfaceFiles
{
    /// <summary>
    /// Interaction logic for ImagePredictionHistoryWindowPage.xaml
    /// </summary>
    public partial class ImagePredictionHistoryWindowPage : Page
    {
        public ImagePredictionHistoryWindowPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientAccountsWindowPage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Main.Content = new AccountLogsWindowPage();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientListWindowPage();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Main.Content = new ImagePredictionHistoryWindowPage();
        }
    }
}
