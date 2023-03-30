using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace Server.InterfaceFiles
{
    /// <summary>
    /// Interaction logic for CreateAccountPage.xaml
    /// </summary>
    public partial class ResultPage : Page
    {
        private ProgramServer server;

        public ResultPage(ProgramServer server, string textToShow)
        {
            InitializeComponent();
            this.server = server;
            DisplayText.Text = textToShow;
        }

        private void UsernameLoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Main.Content = new ImagePredictionHistoryWindowPage(this.server);
        }

        private void ClientList(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientListWindowPage(this.server);
        }

        private void AccountLogs(object sender, RoutedEventArgs e)
        {
            Main.Content = new AccountLogsWindowPage(this.server);
        }

        private void ClientAccountsWindowPage(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientAccountsWindowPage(this.server);
        }
    }
}
