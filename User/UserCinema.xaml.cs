using MaterialDesignThemes.Wpf;
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
using WPFModernVerticalMenu.Pages.PagesUser;
using WPFModernVerticalMenu.Signin;

namespace WPFModernVerticalMenu.User
{
    /// <summary>
    /// Interaction logic for UserCinema.xaml
    /// </summary>
    /// 

    public class ImageData
    {
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
    }

    public class SendInfo
    {
        public string MaPhim { get; set; }
        public string TenPhim { get; set; }
    }

    public partial class UserCinema : Window
    {
        public string idKhachHang = "";
        private int currentProfile = 0;
        private List<ImageData> imageDataList;
        private List<Image> homeCinemaImages;
        private List<TextBlock> homeNameFilmTextBlocks = new List<TextBlock>();
        private List<TextBlock> homeNameDiretorTextBlocks = new List<TextBlock>();

        public UserCinema(string idkhach)
        {
            InitializeComponent();
            idKhachHang = idkhach;
            imageDataList = GetImageData();
            homeCinemaImages = new List<Image> { HomeCinemaImage1, HomeCinemaImage2, HomeCinemaImage3 };
            homeNameFilmTextBlocks.Add(NameFilm1);
            homeNameFilmTextBlocks.Add(NameFilm2);
            homeNameFilmTextBlocks.Add(NameFilm3);

            homeNameDiretorTextBlocks.Add(NameDirector1);
            homeNameDiretorTextBlocks.Add(NameDirector2);
            homeNameDiretorTextBlocks.Add(NameDirector3);
            for (int i = 0; i < 3 && i < imageDataList.Count; i++)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(imageDataList[i].ImagePath));
                homeCinemaImages[i].Source = bitmapImage;
                homeNameFilmTextBlocks[i].Text = imageDataList[i].Name;
                homeNameDiretorTextBlocks[i].Text = imageDataList[i].Director;
            }
        }

        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            Login newLogin = new Login();
            newLogin.Show();
            Close();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int index = (int)slider.Value;

            switch(index)
            {
                case 0:
                    grid0.Visibility = Visibility.Visible;
                    grid1.Visibility = Visibility.Collapsed;
                    grid2.Visibility = Visibility.Collapsed;
                    grid3.Visibility = Visibility.Collapsed;
                    grid4.Visibility = Visibility.Collapsed;
                    currentProfile = 0;
                    break;
                case 1:
                    grid0.Visibility = Visibility.Collapsed;
                    grid1.Visibility = Visibility.Visible;
                    grid2.Visibility = Visibility.Collapsed;
                    grid3.Visibility = Visibility.Collapsed;
                    grid4.Visibility = Visibility.Collapsed;
                    currentProfile = 1;
                    break;
                case 2:
                    grid0.Visibility = Visibility.Collapsed;
                    grid1.Visibility = Visibility.Collapsed;
                    grid2.Visibility = Visibility.Visible;
                    grid3.Visibility = Visibility.Collapsed;
                    grid4.Visibility = Visibility.Collapsed;
                    currentProfile = 2;
                    break;
                case 3:
                    grid0.Visibility = Visibility.Collapsed;
                    grid1.Visibility = Visibility.Collapsed;
                    grid2.Visibility = Visibility.Collapsed;
                    grid3.Visibility = Visibility.Visible;
                    grid4.Visibility = Visibility.Collapsed;
                    currentProfile = 3;
                    break;
                case 4:
                    grid0.Visibility = Visibility.Collapsed;
                    grid1.Visibility = Visibility.Collapsed;
                    grid2.Visibility = Visibility.Collapsed;
                    grid3.Visibility = Visibility.Collapsed;
                    grid4.Visibility = Visibility.Visible;
                    currentProfile = 4;
                    break;
            }
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            currentProfile--;
            if (currentProfile < 0)
            {
                currentProfile = 4;
            }
            slider.Value = currentProfile;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            currentProfile++;
            if (currentProfile >= 5)
            {
                currentProfile = 0;
            }
            slider.Value = currentProfile;
        }

        private string connectionString = "Data Source=QuanLyRapPhim.db;Version=3;";

        public List<ImageData> GetImageData()
        {
            List<ImageData> imageDataList = new List<ImageData>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT HinhAnh, TenPhim, DaoDien FROM Phim";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ImageData imageData = new ImageData();
                            imageData.ImagePath = reader["HinhAnh"].ToString();
                            imageData.Name = reader["TenPhim"].ToString();
                            imageData.Director = reader["DaoDien"].ToString();
                            imageDataList.Add(imageData);
                        }
                    }
                }
            }
            return imageDataList;
        }

        private void BuyTicket1_Click(object sender, RoutedEventArgs e)
        {
            string tenFilm = NameFilm1.Text.ToString();
            string maPhim = "";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Phim WHERE TenPhim = '" + tenFilm + "'";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            maPhim = reader["MaPhim"].ToString();
                        }
                    }
                }
            }

            MuaVe newMuaVe = new MuaVe(maPhim, tenFilm, idKhachHang);
            newMuaVe.Show();
            Close();
        }

        private void BuyTicket2_Click(object sender, RoutedEventArgs e)
        {
            string tenFilm = NameFilm2.Text.ToString();
            string maPhim = "";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Phim WHERE TenPhim = '" + tenFilm + "'";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            maPhim = reader["MaPhim"].ToString();
                        }
                    }
                }
            }

            MuaVe newMuaVe = new MuaVe(maPhim, tenFilm, idKhachHang);
            newMuaVe.Show();
            Close();
        }

        private void BuyTicket3_Click(object sender, RoutedEventArgs e)
        {
            string tenFilm = NameFilm3.Text.ToString();
            string maPhim = "";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Phim WHERE TenPhim = '" + tenFilm + "'";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            maPhim = reader["MaPhim"].ToString();
                        }
                    }
                }
            }

            MuaVe newMuaVe = new MuaVe(maPhim, tenFilm, idKhachHang);
            newMuaVe.Show();
            Close();
        }

        private void Description1_Click(object sender, RoutedEventArgs e)
        {
            string tenFilm = NameFilm1.Text.ToString();
            string maPhim = "";
            string theLoaiPhim = "";
            string daoDienPhim = "";
            string namDvChinh = "";
            string nuDvChinh = "";
            string ngayKCPhim = "";
            string ngayKTPhim = "";
            string noiDungPhim = "";
            string imgPhim = "";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Phim WHERE TenPhim = '" + tenFilm + "'";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            maPhim = reader["MaPhim"].ToString();
                            daoDienPhim = reader["DaoDien"].ToString();
                            theLoaiPhim = reader["MaTheLoai"].ToString();
                            ngayKCPhim = reader["NgayKhoiChieu"].ToString();
                            ngayKTPhim = reader["NgayKetThuc"].ToString();
                            nuDvChinh = reader["NuDVChinh"].ToString();
                            namDvChinh = reader["NamDVChinh"].ToString();
                            noiDungPhim = reader["NDChinh"].ToString();
                            imgPhim = reader["HinhAnh"].ToString();
                        }
                    }
                }
            }

            DirectorFilm newDirectorFilm = new DirectorFilm(tenFilm, maPhim, idKhachHang, daoDienPhim, theLoaiPhim, ngayKCPhim, ngayKTPhim, nuDvChinh, namDvChinh, noiDungPhim, imgPhim);
            newDirectorFilm.Show();
            Close();
        }

        private void Description2_Click(object sender, RoutedEventArgs e)
        {
            string tenFilm = NameFilm2.Text.ToString();
            string maPhim = "";
            string theLoaiPhim = "";
            string daoDienPhim = "";
            string namDvChinh = "";
            string nuDvChinh = "";
            string ngayKCPhim = "";
            string ngayKTPhim = "";
            string noiDungPhim = "";
            string imgPhim = "";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Phim WHERE TenPhim = '" + tenFilm + "'";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            maPhim = reader["MaPhim"].ToString();
                            daoDienPhim = reader["DaoDien"].ToString();
                            theLoaiPhim = reader["MaTheLoai"].ToString();
                            ngayKCPhim = reader["NgayKhoiChieu"].ToString();
                            ngayKTPhim = reader["NgayKetThuc"].ToString();
                            nuDvChinh = reader["NuDVChinh"].ToString();
                            namDvChinh = reader["NamDVChinh"].ToString();
                            noiDungPhim = reader["NDChinh"].ToString();
                            imgPhim = reader["HinhAnh"].ToString();

                        }
                    }
                }
            }

            DirectorFilm newDirectorFilm = new DirectorFilm(tenFilm, maPhim, idKhachHang, daoDienPhim, theLoaiPhim, ngayKCPhim, ngayKTPhim, nuDvChinh, namDvChinh, noiDungPhim, imgPhim);
            newDirectorFilm.Show();
            Close();
        }

        private void Description3_Click(object sender, RoutedEventArgs e)
        {
            string tenFilm = NameFilm3.Text.ToString();
            string maPhim = "";
            string theLoaiPhim = "";
            string daoDienPhim = "";
            string namDvChinh = "";
            string nuDvChinh = "";
            string ngayKCPhim = "";
            string ngayKTPhim = "";
            string noiDungPhim = "";
            string imgPhim = "";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Phim WHERE TenPhim = '" + tenFilm + "'";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            maPhim = reader["MaPhim"].ToString();
                            daoDienPhim = reader["DaoDien"].ToString();
                            theLoaiPhim = reader["MaTheLoai"].ToString();
                            ngayKCPhim = reader["NgayKhoiChieu"].ToString();
                            ngayKTPhim = reader["NgayKetThuc"].ToString();
                            nuDvChinh = reader["NuDVChinh"].ToString();
                            namDvChinh = reader["NamDVChinh"].ToString();
                            noiDungPhim = reader["NDChinh"].ToString();
                            imgPhim = reader["HinhAnh"].ToString();

                        }
                    }
                }
            }

            DirectorFilm newDirectorFilm = new DirectorFilm(tenFilm, maPhim, idKhachHang, daoDienPhim, theLoaiPhim, ngayKCPhim, ngayKTPhim, nuDvChinh, namDvChinh, noiDungPhim, imgPhim);
            newDirectorFilm.Show();
            Close();
        }
    }
}
