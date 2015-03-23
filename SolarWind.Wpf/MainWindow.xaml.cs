using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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

namespace SolarWind.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<string> _messages = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Option_Click(object sender, RoutedEventArgs e)
        {
            Options opt = new Options();
            opt.Show();
            this.Hide();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            lblStatus.Text = string.IsNullOrWhiteSpace(App.ServerUrl) ? "Not connected" : "Connected";
        }
    }
}
