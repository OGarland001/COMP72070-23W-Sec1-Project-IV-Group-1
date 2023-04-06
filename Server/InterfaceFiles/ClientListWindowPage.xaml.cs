﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Server.InterfaceFiles
{
    /// <summary>
    /// Interaction logic for ClientListWindowPage.xaml
    /// </summary>
    public partial class ClientListWindowPage : Page
    {
        private ProgramServer server;
        public ClientListWindowPage(ProgramServer server)
        {
            this.server = server;
            InitializeComponent();
            statesDisp();
            ClientUsername.Text = this.server.GetuserData().getUserName();
            userLoginData blankUser = new userLoginData();
            if (this.server.GetuserData().getUserName() == blankUser.getUserName())
            {
                ConCircle.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("Red");
                ConStatus.Text = "Not Connected";
                ConStatus.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("Red");
            }
            else
            {
                ConCircle.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF10AB3D");
                ConStatus.Text = "Connected";
                ConStatus.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF10AB3D");
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientAccountsWindowPage(this.server);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Main.Content = new AccountLogsWindowPage(this.server);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Main.Content = new ClientListWindowPage(this.server);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Main.Content = new ImagePredictionHistoryWindowPage(this.server);
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //MANULLAY DISCONNECT CLIENT
            this.server.disconnectClient();
            Main.Content = new ClientListWindowPage(this.server);
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
