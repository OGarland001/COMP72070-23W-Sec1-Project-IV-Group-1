﻿using Server.InterfaceFiles;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProgramServer newServer;

        public MainWindow()
        {
            Thread[] threads = new Thread[1];

            threads[0] = new Thread(new ThreadStart(() => {
                newServer = new ProgramServer();
                this.Dispatcher.Invoke(() =>
                {
                    this.DataContext = newServer;
                });
                newServer.run(11069);
            }));

            threads[0].Start();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientAccountsWindowPage(this.newServer);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Main.Content = new AccountLogsWindowPage(this.newServer);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientListWindowPage(this.newServer);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Main.Content = new ImagePredictionHistoryWindowPage(this.newServer);
        }

    }
}
