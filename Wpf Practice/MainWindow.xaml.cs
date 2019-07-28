using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using DarkSkyApi;
using System.Xml;

namespace Wpf_Practice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initialising the API & Required Stuffs
        /// </summary>
        DarkSkyApi.DarkSkyService darkSky = new DarkSkyService("Your API Key here");
        DarkSkyApi.Models.Forecast x;
        //This finds files in the Resources and generates a Stream.
        XmlDocument xmlDocument = new XmlDocument();
        double latitude = 0, longitude = 0;
        bool CityChanged = false;
        Unit unit = Unit.UK;
        string State = "Maharashtra", City = "Nagpur";
        string WeatherIcon = @"Images/sun.png";
        Animations Anims = new Animations();


        /// <summary>
        /// Main Program Start
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            
            WindowClock.BeginAnimation(HeightProperty, Anims.StartAnimation);
            MouseDown += MainWindow_MouseDown;
            TimeUpdater();
            Updater(true);
        }
        


        /// <summary>
        /// Gets the Weather Date and Stores into x.
        /// Updates Temprature & Weather Icon As per results.
        /// </summary>
        public async void Updater(bool FormLoaded)
        {
            //if (darkSky.ApiCallsMade.Value <= 3)
            {
                try
                {
                    if (CityChanged || FormLoaded)
                    {
                        FormLoaded = false;
                        CityChanged = false;
                        #region Getting Lat & Lon after Selection

                        //Making Sure the xml is Embedded Resource and Path will be automatically relevant.
                        //Also Check for Properties Panel for the xml File.
                        xmlDocument.Load("Cities.xml");
                        //getting the Latitude and Longitude
                        XmlNode node = xmlDocument.SelectSingleNode(String.Format("/Cities/name[{0:0}]/lat", Properties.Settings.Default.City));
                        latitude = Convert.ToDouble(node.InnerText);

                        node = xmlDocument.SelectSingleNode(String.Format("/Cities/name[{0:0}]/lon", Properties.Settings.Default.City));
                        longitude = Convert.ToDouble(node.InnerText);

                        node = xmlDocument.SelectSingleNode(String.Format("/Cities/name[{0:0}]/name", Properties.Settings.Default.City));
                        City = node.InnerText;

                        node = xmlDocument.SelectSingleNode(String.Format("/Cities/name[{0:0}]/state", Properties.Settings.Default.City));
                        State = node.InnerText;

                        #endregion
                    }
                    //Setting Window Opacity From settings
                    WindowClock.Opacity = Properties.Settings.Default.Opacity;
                    //Setting ComboBox Index
                    CityComboBox.SelectedIndex = Properties.Settings.Default.City - 1;

                    //Initialisation and Connection to the Weather Api.
                    x = await darkSky.GetWeatherDataAsync(latitude, longitude, unit);
                    TxtTemprature.BeginAnimation(OpacityProperty, Anims.RevAnim);
                    TxtTemprature.Text = (x.Currently.Temperature.ToString() + " °C");
                    TxtTempratureLow.Text = (x.Currently.ApparentTemperature.ToString());

                    switch (x.Currently.Icon)
                    {
                        case "clear-day":
                            WeatherIcon = "Images\\sun.png";
                            break;
                        case "clear-night":
                            WeatherIcon = "Images\\013-moon.png";
                            break;
                        case "rain":
                            WeatherIcon = "Images\\012-raining.png";
                            break;
                        case "wind":
                            WeatherIcon = "Images\\004-wind-1.png";
                            break;
                        case "partly-cloudy-day":
                        case "partly-cloudy-night":
                        case "cloudy":
                            WeatherIcon = "Images\\Cloudy.png";
                            break;
                        case "thunderstorm":
                            WeatherIcon = "Images\\006-storm-1.png";
                            break;
                        default:
                            WeatherIcon = "Images\\sun.png";
                            break;
                    }
                    TxtCity.Text = City;
                    TxtState.Text = State;
                    //WeatherImage.BeginAnimation(OpacityProperty, FrwdAnim);
                    WeatherImage.Source = new BitmapImage(new Uri(WeatherIcon,UriKind.Relative));
                    WeatherImage.BeginAnimation(OpacityProperty, Anims.RevAnim);
                    TxtDate.Text = DateTime.Now.ToString("dd MMMM yyyy");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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


        private void LocationChooser(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed && Keyboard.IsKeyDown(Key.LeftAlt) )
            {
                ComboBoxStack.Visibility = Visibility.Visible;
                WindowClock.BeginAnimation(HeightProperty, Anims.HtIncAnim);
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

        private void OpacitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            WindowClock.Opacity = 0.3 + e.NewValue/10;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            WindowClock.BeginAnimation(HeightProperty, Anims.ExitAnimation);
            this.Close();
        }

        private void btnInfo_Clicked(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.linkedin.com/in/thakrekuldeep/");
            btnApply_Click(sender, e);
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Opacity = WindowClock.Opacity;
            Properties.Settings.Default.Save();
            ComboBoxStack.Visibility = Visibility.Hidden;
            WindowClock.BeginAnimation(HeightProperty, Anims.HtDecAnim);
            Updater(false);
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.City = CityComboBox.SelectedIndex + 1;
            Properties.Settings.Default.Save();
            CityChanged = true;
        }
    }
}
