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
using Path = System.IO.Path;

namespace Client.InterfaceFiles
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        ProgramClient client;
        public HomePage(ProgramClient client)
        {
            this.client = client;
            InitializeComponent();
            if (!this.client.checkConnection())
            {
                Main.Content = new MainPage(ref this.client);
            }
        }

        private void requestlogs_Click(object sender, RoutedEventArgs e)
        {
            
            if (this.client.receiveUserlogs())
            {
                //pop up saying it set it to the image
                MessageBox.Show("User Log Received!");
            }
            else
            {
                MessageBox.Show("No User Log Found");
            }

            Main.Content = new RequestLogsPage(this.client);

        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new MainPage(ref this.client);

        }

        private void Upload_an_Image_Click(object sender, RoutedEventArgs e)
        {
            if (!this.client.checkConnection())
            {
                Main.Content = new MainPage(ref this.client);
            }
            Microsoft.Win32.OpenFileDialog image = new OpenFileDialog();
            image.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            image.FilterIndex = 1;

            if (image.ShowDialog() == true)
            {
                imagePicture.Source = new BitmapImage(new Uri(image.FileName));
                //imagePicture.Source = uploadedImage;

                //enable button and set opacity to normal
                Analyze.IsEnabled = true;
                Analyze.Opacity = 0.66;
            }

            //if image uploaded allow analyze click
            Analyze.IsEnabled = true;
        }

        private void Analyze_Click(object sender, RoutedEventArgs e)
        {
            //dispose the previous bitmapImage and set it to the loading image
            outputPicture.Source = null;
            outputPicture.Source = new BitmapImage(new Uri(Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, @"UserImages\loading.jpg")));
            
            if (!this.client.checkConnection())
            {
                Main.Content = new MainPage(ref this.client);
            }
            //take the image that is uploaded to the screen imagePicture, and then convert to a byte array and setup the packet structure and send 100 bytes at a time to the server

            //imagePicture path
            string path = imagePicture.Source.ToString();
            path = path.Substring(8);
            MessageBox.Show(path);
            try
            {
                this.client.sendImage(@path);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            if (this.client.receiveImage())
            {
                outputPicture.Source = null;
                // set the bitmap as the source of the outputPicture object
                BitmapImage image = new BitmapImage();
                using (var stream = new FileStream(Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, @"UserImages\Output.jpg"), FileMode.Open, FileAccess.Read))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                }
                outputPicture.Source = image;

                //pop up saying it set it to the image
                MessageBox.Show("Image has been set to the output image");
            }
            else
            {
                MessageBox.Show("Nothing Found :(");
            }

            

        }

        private void Main_Navigated(object sender, NavigationEventArgs e)
        {
            //reset uploaded image to default
            //uploadedImage = null;
            imagePicture.Source = null;
            Analyze.IsEnabled = false;
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            if (!this.client.checkConnection())
            {
                Main.Content = new MainPage(ref this.client);
            }
        }

        private void List_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClassificationList(ref this.client);
        }
    }
}