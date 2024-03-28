using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFModernVerticalMenu.User;
using System.Windows.Threading;

namespace WPFModernVerticalMenu.Pages.PagesUser
{
    /// <summary>
    /// Interaction logic for XemFilm.xaml
    /// </summary>
    public partial class XemFilm : Window
    {
        DispatcherTimer timer;
        string urlVideo = "";
        public XemFilm(string linkVideo)
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(timer_Tick);
            urlVideo = linkVideo;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Slider_Video.Value = mediaElement.Position.TotalSeconds;
        }

        private void StopVideo_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PauseVideo_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }

        private void StartVideo_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
        }

        private void Slider_Vol_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Volume = (double)Slider_Vol.Value;

        }

        private void Slider_Video_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Position = TimeSpan.FromSeconds(Slider_Video.Value);
        }

        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (mediaElement.NaturalDuration.HasTimeSpan)
            {
                TimeSpan ts = mediaElement.NaturalDuration.TimeSpan;
                Slider_Video.Maximum = ts.TotalSeconds;
                timer.Start();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            try {
                mediaElement.Source = new Uri(urlVideo);

                mediaElement.LoadedBehavior = MediaState.Manual;
                mediaElement.UnloadedBehavior = MediaState.Manual;
                mediaElement.Volume = (double)Slider_Vol.Value;
                mediaElement.Play();

            } catch (Exception ex) {
                System.Console.WriteLine("Error Message: " + ex.Message);
            }
            
        }
    }
}
