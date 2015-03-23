using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.AspNet.SignalR.Client;
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
using System.Windows.Shapes;

namespace SolarWind.Wpf
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        public Options()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var hubCn = new HubConnection(txtServerUrl.Text);
            IHubProxy proxy = hubCn.CreateHubProxy("MyHub");
            proxy.On<string>("hello", (message) =>
            {
                TaskbarIcon notifyIcon = (TaskbarIcon)FindResource("MyNotifyIcon");
                MainWindow._messages.Add(message);
                notifyIcon.ShowBalloonTip("Alert", message, BalloonIcon.Info);
            });
            hubCn.Start().Wait();

            App.ServerUrl = txtServerUrl.Text;
            App.Current.MainWindow.Show();

            this.Close();
        }
    }
}
