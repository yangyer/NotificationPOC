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
        public MainWindow()
        {
            InitializeComponent();

            var hubCn = new HubConnection("http://solarwinddemo.azurewebsites.net/");
            IHubProxy proxy = hubCn.CreateHubProxy("MyHub");
            proxy.On<string>("hello", (message) => 
            {
                TaskbarIcon notifyIcon = (TaskbarIcon)FindResource("MyNotifyIcon");

                notifyIcon.ShowBalloonTip("Alert", message, BalloonIcon.Info);
            });
            hubCn.Start().Wait();
        }

        private void btnClickMe_OnClick(object sender, RoutedEventArgs e)
        {
            TaskbarIcon notifyIcon = (TaskbarIcon)FindResource("MyNotifyIcon");

            notifyIcon.ShowBalloonTip("Test", "testing 123", BalloonIcon.Info);
        }
    }
}
