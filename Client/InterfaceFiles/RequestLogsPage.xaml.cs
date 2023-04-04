﻿using System;
using System.IO;
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
using System.Reflection;

namespace Client.InterfaceFiles
{
    /// <summary>
    /// Interaction logic for RequestLogsPage.xaml
    /// </summary> 
    public partial class RequestLogsPage : Page
    {
        ProgramClient client;
        public RequestLogsPage(ProgramClient client)
        {
            this.client = client;
            InitializeComponent();
            //recieve from file and write to ClientLog.txt

            string filePath = "../../../ClientLog.txt";
            string fileContents = File.ReadAllText(filePath);
            label.Content = fileContents;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new HomePage(ref this.client);
        }
    }
}
