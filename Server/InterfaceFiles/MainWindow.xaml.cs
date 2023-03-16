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

namespace Server
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var accountsWindow = new ClientAccountsWindow();

            accountsWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var accountsLogWindow = new AccountLogsWindow();

            accountsLogWindow.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var clientsWindow = new ClientListWindow();

            clientsWindow.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var predictionWindow = new ImagePredictionHistoryWindow();

            predictionWindow.Show();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine("Do Nothin");
        }
    }
}
