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
        private ProgramClient client;
        public HomePage(ref ProgramClient client)
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
            Main.Content = new MainPage(ref this.client);

        }

        private void Upload_an_Image_Click(object sender, RoutedEventArgs e)
        {
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
        }

        private void Analyze_Click(object sender, RoutedEventArgs e)
        {
            //take the image that is uploaded to the screen imagePicture, and then convert to a byte array and setup the packet structure and send 100 bytes at a time to the server
            
            //imagePicture path
            string path = imagePicture.Source.ToString();
            path = path.Substring(8);
            MessageBox.Show(path);
            try
            {
                client.sendImage(@path);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }



            client.receiveImage();

            //// create a new BitmapImage object with the image file as the source
            BitmapImage bitmap = new BitmapImage(new Uri("../UserImage/Output.jpg", UriKind.Relative));

            // set the bitmap as the source of the outputPicture object
            outputPicture.Source = bitmap;

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