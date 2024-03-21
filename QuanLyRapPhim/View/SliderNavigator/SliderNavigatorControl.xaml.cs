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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyRapPhim.View.SliderNavigator
{
    /// <summary>
    /// Interaction logic for SliderNavigatorControl.xaml
    /// </summary>
    public partial class SliderNavigatorControl : UserControl
    {
        bool SliderNavshow = true;
        public SliderNavigatorControl()
        {
            InitializeComponent();
        }

        private void logo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SliderNavshow)
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
