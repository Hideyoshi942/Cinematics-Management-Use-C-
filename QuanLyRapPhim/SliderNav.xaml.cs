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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyRapPhim
{
    /// <summary>
    /// Interaction logic for SliderNav.xaml
    /// </summary>
    public partial class SliderNav : Window
    {
        bool SliderNavshow = true;
        public SliderNav()
        {
            InitializeComponent();
        }

        private void logo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(SliderNavshow)
            {
                Storyboard storyboard = (Storyboard)this.Resources["Storyboard1"];
                storyboard.Begin(logo);
                SliderNavshow = false;
            }
            else
            {
                Storyboard storyboard = (Storyboard)this.Resources["Storyboard2"];
                storyboard.Begin(logo);
                SliderNavshow = true;
            }
        }
        
    }
}
