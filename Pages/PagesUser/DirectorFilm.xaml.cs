using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
using WPFModernVerticalMenu.User;

namespace WPFModernVerticalMenu.Pages.PagesUser
{
    /// <summary>
    /// Interaction logic for DirectorFilm.xaml
    /// </summary>
    /// 
    public partial class DirectorFilm : Window
    {
        public string idKhachHang = "";
        public string maPhimDirector = "";
        private string connectionString = "Data Source=QuanLyRapPhim.db;Version=3;";
        public DirectorFilm(string maPhim, string tenPhim, string idkhach, string theloai, string daodien, string ngaychieu, string ngaykt, string nuchinh, string namchinh, string noidung, string hinhAnhPhim)
        {
            InitializeComponent();
            idKhachHang = idkhach;
            maPhimDirector = maPhim;
            NameFilmDirector.Text = tenPhim;
            CategoryFilmDirector.Text = theloai;
            DirectorFilmDirector.Text = daodien;
            ManFilmDirector.Text = namchinh;
            WomanFilmDirector.Text = nuchinh;
            DayStartFilmDirector.Text = ngaychieu;
            DayEndFilmDirector.Text = ngaykt;
            ContentFilm.Text = noidung;
            BitmapImage bitmapImage = new BitmapImage(new Uri(hinhAnhPhim));
            ImageDirector.Source = bitmapImage;
        }

        private void BackHomeCinema_Click(object sender, RoutedEventArgs e)
        {
            UserCinema newUserCinema = new UserCinema(idKhachHang);
            newUserCinema.Show();
            Close();
        }
    }
}
