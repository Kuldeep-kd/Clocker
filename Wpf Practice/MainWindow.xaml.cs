using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_Practice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();

            WeatherImage.Source = new BitmapImage(new Uri(@"Images/Cloudy.png", UriKind.Relative));
            TempratureImage.Source = new BitmapImage(new Uri(@"Images/Temp.png", UriKind.Relative));
            Clock.Opacity = 0.7;

            MouseDown += MainWindow_MouseDown;
            TimeUpdater();
        }
    

        async void TimeUpdater()
        {
            while (true)
            {
                TxtHour.Text = string.Format("{00:00}",( ( (DateTime.Now.Hour>12) ? DateTime.Now.Hour-12 :  DateTime.Now.Hour)));
                TxtMinutes.Text = string.Format("{00:00}", DateTime.Now.Minute);
                TxtSeconds.Text = string.Format("{00:00}", DateTime.Now.Second);
                await Task.Delay(1000);    
            }
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if(e.LeftButton == MouseButtonState.Pressed)
                    DragMove();
            }
            catch (Exception)
            { }
        }
    }
}
