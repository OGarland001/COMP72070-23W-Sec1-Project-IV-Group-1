using Microsoft.Win32;
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
        public HomePage()
        {
            InitializeComponent();
        }

        private void requestlogs_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new RequestLogsPage();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new MainPage();

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
            //call the server / send image to the server via a packet
            // Send a request to the server to start sending the image packets
            //byte[] request = Encoding.ASCII.GetBytes("SendImage");
            //client.GetStream().Write(request, 0, request.Length);
            ////client is the tcpClient "name" for the connection
            

            ////recieve the processed packet and print out the new image

            ////byte[] buffer;
            //List<byte> imageBytes = new List<byte>();
            //int bytesRead;
            //while ((bytesRead = client.GetStream().Read(request, 0, request.Length)) > 0)
            //{
            //    imageBytes.AddRange(request.Take(bytesRead));
            //}

            //// Create a BitmapImage from the received bytes
            //BitmapImage image = new BitmapImage();
            //image.BeginInit();
            //image.StreamSource = new MemoryStream(imageBytes.ToArray());
            //image.EndInit();

            //// Display the image in GUI
            //imagePicture.Source = image;

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