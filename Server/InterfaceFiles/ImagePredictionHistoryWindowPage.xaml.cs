using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Path = System.IO.Path;

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

        
    }
}
