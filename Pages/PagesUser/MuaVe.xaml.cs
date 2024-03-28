using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
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
    /// Interaction logic for MuaVe.xaml
    /// </summary>
    /// 
    public partial class MuaVe : Window
    {
        public class VeXemPhim
        {
            public string MaPhim { get; set; }
            public string TenPhim { get; set; }
            public string MaShow { get; set; }
            public string MaPhong { get; set; }
            public string NgayChieu { get; set; }
            public string MaGio { get; set; }
        }
        DataProcessing.DataProcessing SQLite = new DataProcessing.DataProcessing();
        public string maPhimTicket = "";
        public string idKhachHang = "";
        public string maShowTicket = "";
        public string maPhongTicket = "";
        public string maNgayTicket = "";
        public string maGioTicket = "";
        public string linkVideo = "";
        public MuaVe(string maPhim, string tenPhim, string idkhach)
        {
            InitializeComponent();
            idKhachHang = idkhach;
            tblIdFilm.Text = maPhim;
            tblNameFilm.Text = tenPhim;
            maPhimTicket = maPhim;
            loadShow();
            loadRoom();
            loadDay();
            loadHour();
        }

        void loadShow ()
        {
            List<Object> list = SQLite.ReadColumnData("select MaShow from Show_BuoiChieu Where MaPhim = '" + maPhimTicket + "'");
            cbbShow.ItemsSource = list;
        }

        void loadRoom()
        {
            List<Object> list = SQLite.ReadColumnData("select MaPhong from Show_BuoiChieu Where MaPhim = '" + maPhimTicket + "'And MaShow = '" + maShowTicket + "'");
            cbbIdRoom.ItemsSource = list;
        }

        void loadDay()
        {
            List<Object> list = SQLite.ReadColumnData("select NgayChieu from Show_BuoiChieu Where MaPhim = '" + maPhimTicket + "'And MaPhong = '" + maPhongTicket + "'And MaShow = '" + maShowTicket + "'");
            cbbDateShow.ItemsSource = list;
        }

        void loadHour()
        {
            List<Object> list = SQLite.ReadColumnData("select MaGioChieu from Show_BuoiChieu Where MaPhim = '" + maPhimTicket + "'And MaPhong = '" + maPhongTicket + "'And MaShow = '" + maShowTicket + "'");
            cbbIDHourShow.ItemsSource = list;
        }

        private void CancelBuyTicket_Click(object sender, RoutedEventArgs e)
        {
            UserCinema newUserCinema = new UserCinema(idKhachHang);
            newUserCinema.Show();
            Close();
        }

        public void CreateAndSavePDF(VeXemPhim veXemPhim)
        {
            PdfDocument document = new PdfDocument();

            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont font = new XFont("Verdana", 12, XFontStyle.Regular);
            DateTime currentDateTime = DateTime.Now;

            string content = $"Vé xem phim\n" +
                             $"Thời gian mua: {currentDateTime}\n" +
                             $"Mã phim: {veXemPhim.MaPhim}\n" +
                             $"Tên phim: {veXemPhim.TenPhim}\n" +
                             $"Mã show: {veXemPhim.MaShow}\n" +
                             $"Mã phòng: {veXemPhim.MaPhong}\n" +
                             $"Ngày chiếu: {veXemPhim.NgayChieu}\n" +
                             $"Mã giờ: {veXemPhim.MaGio}";

            gfx.DrawString(content, font, XBrushes.Black, new XRect(10, 10, page.Width, page.Height), XStringFormats.TopLeft);

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveDialog.FilterIndex = 0;
            saveDialog.RestoreDirectory = true;

            var result = saveDialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                document.Save(saveDialog.FileName);
            }
        }

        private string connectionString = "Data Source=QuanLyRapPhim.db;Version=3;";

        private void BuyTicket_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "Select Video from Phim Where MaPhim = '" + maPhimTicket + "'";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            
                            linkVideo = reader["Video"].ToString();
                        }
                    }
                }
            }
            if (maShowTicket == "" || maGioTicket == "" || maNgayTicket == "" || maPhongTicket == "")
            {
                MessageBox.Show("Vui lòng điền thông tin!");
            } else
            {
                string veDaMua = "";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = "Select SoVeDaBan from Show_BuoiChieu Where MaPhim = '" + maPhimTicket + "'And MaPhong = '" + maPhongTicket + "'And MaShow = '" + maShowTicket + "'";

                    using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                veDaMua = reader["SoVeDaBan"].ToString();
                            }
                        }
                    }
                }
                int veDaBan = int.Parse(veDaMua);
                veDaBan += 1;
                veDaMua = veDaBan.ToString();

                string sqlUpdate = "UPDATE Show_BuoiChieu SET SoVeDaBan = '" + veDaMua + "' WHERE MaShow = '" + maShowTicket + "'";
                SQLite.ChangeData(sqlUpdate);
                if (MessageBox.Show("Bạn có muốn in hóa đơn không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    VeXemPhim veXemPhim = new VeXemPhim
                    {
                        MaPhim = tblIdFilm.Text,
                        TenPhim = tblNameFilm.Text,
                        MaShow = cbbShow.SelectedItem.ToString(),
                        MaPhong = cbbIdRoom.SelectedItem.ToString(),
                        NgayChieu = cbbDateShow.SelectedItem.ToString(),
                        MaGio = cbbIDHourShow.SelectedItem.ToString()
                    };

                    CreateAndSavePDF(veXemPhim);
                    if (MessageBox.Show("Bạn có muốn xem phim luôn không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        XemFilm newXemFilm = new XemFilm(linkVideo);
                        newXemFilm.Show();
                    }
                    else
                    {
                        UserCinema newUserCinema = new UserCinema(idKhachHang);
                        newUserCinema.Show();
                        
                    }
                }
                else
                {
                    if (MessageBox.Show("Bạn có muốn xem phim luôn không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        XemFilm newXemFilm = new XemFilm(linkVideo);
                        newXemFilm.Show();
                        
                    }
                    else
                    {
                        UserCinema newUserCinema = new UserCinema(idKhachHang);
                        newUserCinema.Show();

                    }
                    
                }
                Close();
            }
        }

        private void cbbShow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = cbbShow.SelectedItem;

            if (selectedItem != null)
            {
                string selectedValue = selectedItem.ToString();
                maShowTicket = selectedValue;
            }
            loadRoom();
        }

        private void cbbIdRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = cbbIdRoom.SelectedItem;

            if (selectedItem != null)
            {
                string selectedValue = selectedItem.ToString();
                maPhongTicket = selectedValue;
            }
            loadDay();
            loadHour();
        }

        private void cbbDateShow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = cbbDateShow.SelectedItem;

            if (selectedItem != null)
            {
                string selectedValue = selectedItem.ToString();
                maNgayTicket = selectedValue;
            }
            loadDay();
        }

        private void cbbIDHourShow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = cbbIDHourShow.SelectedItem;

            if (selectedItem != null)
            {
                string selectedValue = selectedItem.ToString();
                maGioTicket = selectedValue;
            }
            loadHour();
        }
    }
}
