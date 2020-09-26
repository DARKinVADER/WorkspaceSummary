using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using System.Xml.Serialization;
using Xml2CSharp;

namespace TCRestApiGetWorkspaces
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnExecute_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            var response = httpClient.GetAsync(txtUrl.Text).Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ArrayOfstring));
                ArrayOfstring workspaceList = (ArrayOfstring)xmlSerializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(content)));
                lstWorkspace.ItemsSource = workspaceList.String;
            }
            else
            {
                MessageBox.Show("Error", "Failed to call Tosca REST API!");
            }

        }
    }
}
