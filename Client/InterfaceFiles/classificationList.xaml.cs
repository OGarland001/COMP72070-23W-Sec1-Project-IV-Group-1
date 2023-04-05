using System;
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

namespace Client.InterfaceFiles
{
    /// <summary>
    /// Interaction logic for ClassificationList.xaml
    /// </summary>
    public partial class ClassificationList : Page
    {
        public ProgramClient client = new ProgramClient();
        public ClassificationList(ref ProgramClient client)
        {
            this.client = client;
            InitializeComponent();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new HomePage(this.client);
        }
    }
}