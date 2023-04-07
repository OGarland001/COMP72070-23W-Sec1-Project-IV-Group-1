using System.Windows;
using System.Windows.Controls;


namespace Client.InterfaceFiles
{
    /// <summary>
    /// Interaction logic for ClassificationList.xaml
    /// </summary>
    public partial class ClassificationList : Page
    {
        public ProgramClient client = new ProgramClient(11069);
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