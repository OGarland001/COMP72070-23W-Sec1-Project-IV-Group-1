using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Path = System.IO.Path;
using System.Windows.Shapes;

namespace Server.InterfaceFiles
{
    /// <summary>
    /// Interaction logic for ImagePredictionHistoryWindowPage.xaml
    /// </summary>
    public partial class ImagePredictionHistoryWindowPage : Page
    {
        private ProgramServer server;
        public ImagePredictionHistoryWindowPage(ProgramServer server)
        {
            this.server = server;
            InitializeComponent();
            statesDisp();
            // Get the current directory
            string combinedAnalyzedPath = this.server.getCurrentAnalyzedImage();
            string combinedOriginalPath = this.server.getCurrentOriginalImage();
            AnalyzedImage.Source = new BitmapImage(new Uri(Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, combinedAnalyzedPath)));
            OrginialImage.Source = new BitmapImage(new Uri(Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, combinedOriginalPath)));
            //Infolist.
            //this.server.SetuserData("Tyler", "Scscaccsa");
            
            Username.Text = this.server.GetuserData().getUserName() + " Sent an image";
         

            if (server.getDetectedObjects().GetLength(0) > 0) 
            {
                string[,] detectedObjects = server.getDetectedObjects();

                var data = new List<string>();

                for (int i = 0; i < detectedObjects.GetLength(0); i++)
                {
                    string row = "";
                    for (int j = 0; j < detectedObjects.GetLength(1); j++)
                    {
                        row += detectedObjects[i, j] + "\n";
                    }
                    data.Add(row.TrimEnd());
                }

                ListBox.ItemsSource = data;
            }
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Predicition.Content = new ClientAccountsWindowPage(this.server);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Predicition.Content = new AccountLogsWindowPage(this.server);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Predicition.Content = new ClientListWindowPage(this.server);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Predicition.Content = new ImagePredictionHistoryWindowPage(this.server);
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            Predicition.Content = new ImagePredictionHistoryWindowPage(this.server);
        }

        private void statesDisp()
        {
            //Ensure all states are marked as off

            // Get the first Rectangle element in the XAML
            Rectangle rect = (Rectangle)FindName("Idle");
            // Get the RadialGradientBrush from the Rectangle's Fill property
            RadialGradientBrush brush = (RadialGradientBrush)rect.Fill;
            // Update the color of the first GradientStop object
            brush.GradientStops[1].Color = (Color)ColorConverter.ConvertFromString("#FF515151");
            // Get the 1 Rectangle element in the XAML
            Rectangle rect1 = (Rectangle)FindName("Auth");
            // Get the RadialGradientBrush from the Rectangle's Fill property
            RadialGradientBrush brush1 = (RadialGradientBrush)rect1.Fill;
            // Update the color of the first GradientStop object
            brush1.GradientStops[1].Color = (Color)ColorConverter.ConvertFromString("#FF515151");
            // Get the 2 Rectangle element in the XAML
            Rectangle rect2 = (Rectangle)FindName("NewAuth");
            // Get the RadialGradientBrush from the Rectangle's Fill property
            RadialGradientBrush brush2 = (RadialGradientBrush)rect2.Fill;
            // Update the color of the first GradientStop object
            brush2.GradientStops[1].Color = (Color)ColorConverter.ConvertFromString("#FF515151");
            // Get the 3 Rectangle element in the XAML
            Rectangle rect3 = (Rectangle)FindName("Recv");
            // Get the RadialGradientBrush from the Rectangle's Fill property
            RadialGradientBrush brush3 = (RadialGradientBrush)rect3.Fill;
            // Update the color of the first GradientStop object
            brush3.GradientStops[1].Color = (Color)ColorConverter.ConvertFromString("#FF515151");
            // Get the 4 Rectangle element in the XAML
            Rectangle rect4 = (Rectangle)FindName("Analyze");
            // Get the RadialGradientBrush from the Rectangle's Fill property
            RadialGradientBrush brush4 = (RadialGradientBrush)rect4.Fill;
            // Update the color of the first GradientStop object
            brush4.GradientStops[1].Color = (Color)ColorConverter.ConvertFromString("#FF515151");
            // Get the 5 Rectangle element in the XAML
            Rectangle rect5 = (Rectangle)FindName("Saving");
            // Get the RadialGradientBrush from the Rectangle's Fill property
            RadialGradientBrush brush5 = (RadialGradientBrush)rect5.Fill;
            // Update the color of the first GradientStop object
            brush5.GradientStops[1].Color = (Color)ColorConverter.ConvertFromString("#FF515151");
            // Get the 6 Rectangle element in the XAML
            Rectangle rect6 = (Rectangle)FindName("Sending");
            // Get the RadialGradientBrush from the Rectangle's Fill property
            RadialGradientBrush brush6 = (RadialGradientBrush)rect6.Fill;
            // Update the color of the first GradientStop object
            brush6.GradientStops[1].Color = (Color)ColorConverter.ConvertFromString("#FF515151");
            // Get the 7 Rectangle element in the XAML
            Rectangle rect7 = (Rectangle)FindName("Discon");
            // Get the RadialGradientBrush from the Rectangle's Fill property
            RadialGradientBrush brush7 = (RadialGradientBrush)rect7.Fill;
            // Update the color of the first GradientStop object
            brush7.GradientStops[1].Color = (Color)ColorConverter.ConvertFromString("#FF515151");
            // Get the 8 Rectangle element in the XAML
            Rectangle rect8 = (Rectangle)FindName("RecvLog");
            // Get the RadialGradientBrush from the Rectangle's Fill property
            RadialGradientBrush brush8 = (RadialGradientBrush)rect8.Fill;
            // Update the color of the first GradientStop object
            brush8.GradientStops[1].Color = (Color)ColorConverter.ConvertFromString("#FF515151");

            //set the current state
            // Get the dynamic Rectangle element in the XAML
            Rectangle dynamicRect = (Rectangle)FindName(this.server.getCurStringState());
            // Get the RadialGradientBrush from the Rectangle's Fill property
            RadialGradientBrush dynamicBrush = (RadialGradientBrush)dynamicRect.Fill;
            // Update the color of the first GradientStop object
            dynamicBrush.GradientStops[1].Color = (Color)ColorConverter.ConvertFromString("#FF0CFF00");
        }
    }
}
