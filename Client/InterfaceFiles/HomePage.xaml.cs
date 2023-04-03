﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
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
using System.Net.Sockets;

namespace Client.InterfaceFiles
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private ProgramClient client;
        public HomePage(ProgramClient client)
        {
            InitializeComponent();
            this.client = client;
        }

        private void requestlogs_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new RequestLogsPage(this.client);
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new MainPage(this.client);

        }

        private void Upload_an_Image_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog image = new OpenFileDialog();
            image.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            image.FilterIndex = 1;

            if (image.ShowDialog() == true)
            {
                imagePicture.Source = new BitmapImage(new Uri(image.FileName));
                //imagePicture.Source = uploadedImage;

                //enable button and set opacity to normal
                Analyze.IsEnabled = true;
                Analyze.Opacity = 0.66;
            }
        }

        private void Analyze_Click(object sender, RoutedEventArgs e)
        {
            //take the image that is uploaded to the screen imagePicture, and then convert to a byte array and setup the packet structure and send 100 bytes at a time to the server
            
            //imagePicture path
            string path = imagePicture.Source.ToString();
            
            client.sendImage(path);

        }

        private void Main_Navigated(object sender, NavigationEventArgs e)
        {
            //reset uploaded image to default
            //uploadedImage = null;
            imagePicture.Source = null;
            Analyze.IsEnabled = false;
        }

    }
}