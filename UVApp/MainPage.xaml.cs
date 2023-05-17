using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UVApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
           
            var myPosition = await GetLocationInfo.GetPosition();
            Root myUVInfo = await OpenUVInfo.GetUVData(myPosition.Coordinate.Point.Position.Latitude, myPosition.Coordinate.Point.Position.Longitude);
            RootObject myWeatherInfo = await OpenWeatherMap.GetWeatherData(myPosition.Coordinate.Point.Position.Latitude, myPosition.Coordinate.Point.Position.Longitude);
            UVTextBlock.Text = "Current UV Index: " + myUVInfo.result.uv.ToString();
            UVTextDescBlock.Text = getUVDesc(myUVInfo.result.uv);
            Text_ST1.Text = myUVInfo.result.safe_exposure_time.st1.ToString();
            Text_ST2.Text = myUVInfo.result.safe_exposure_time.st2.ToString();
            Text_ST3.Text = myUVInfo.result.safe_exposure_time.st3.ToString();
            Text_ST4.Text = myUVInfo.result.safe_exposure_time.st4.ToString();
            Text_ST5.Text = myUVInfo.result.safe_exposure_time.st5.ToString();
            Text_ST6.Text = myUVInfo.result.safe_exposure_time.st6.ToString();
            UVStackPanel.Background = new SolidColorBrush(getUVColor(myUVInfo.result.uv));
            ReportedLocation.Text = "Your reported location is: " + myPosition.Coordinate.Point.Position.Latitude.ToString() + ", " + myPosition.Coordinate.Point.Position.Longitude.ToString();
            ReportedLocationDesc.Text = myWeatherInfo.weather[0].description;
        }

        private Color getUVColor(double uv)
        {
            Color uvColorCase;

            if (uv < 3)
            {
                uvColorCase = Color.FromArgb(255, 85, 139, 47);
            }
            else if (uv >= 3 && uv < 6)
            {
                uvColorCase = Color.FromArgb(255, 249, 168, 37);
            }
            else if (uv >= 6 && uv < 8)
            {
                uvColorCase = Color.FromArgb(255, 239, 108, 0);
            }
            else if (uv >= 8 && uv < 11)
            {
                uvColorCase = Color.FromArgb(255, 183, 28, 28);
            }
            else
            {
                uvColorCase = Color.FromArgb(255, 106, 27, 154);
            }

            return uvColorCase;
        }

        private String getUVDesc(double uv)
        {
            String uvColorCase;

            if (uv < 3)
            {
                uvColorCase = "Low";
            }
            else if (uv >= 3 && uv < 6)
            {
                uvColorCase = "Moderate";
            }
            else if (uv >= 6 && uv < 8)
            {
                uvColorCase = "High";
            }
            else if (uv >= 8 && uv < 11)
            {
                uvColorCase = "Very High";
            }
            else
            {
                uvColorCase = "Extreme";
            }

            return uvColorCase;
        }

    }
}
