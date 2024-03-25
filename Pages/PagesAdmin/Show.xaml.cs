using System;
using System.Collections.Generic;
using System.Data;
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

namespace WPFModernVerticalMenu.Pages.PagesAdmin
{
    /// <summary>
    /// Interaction logic for Show.xaml
    /// </summary>
    public partial class Show : Page
    {
        DataProcessing.DataProcessing SQLite = new DataProcessing.DataProcessing();
        public Show()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadIdFilm();
            LoadIdRoom();
            LoadIdStartTime();
            LoadDataShow();
        }

        void LoadIdFilm()
        {
            List<Object> list = SQLite.ReadColumnData("select MaPhim from Phim");
            cbbIdFilm.ItemsSource = list;
        }

        void LoadIdRoom()
        {
            List<Object> list = SQLite.ReadColumnData("select MaPhong from PhongChieu");
            cbbIdRoom.ItemsSource = list;
        }

        void LoadIdStartTime()
        {
            List<Object> list = SQLite.ReadColumnData("select MaGioChieu from GioChieu");
            cbbIdStartTime.ItemsSource = list;
        }

        void LoadDataShow()
        {
            DataTable dt = SQLite.ReadData("select * from Show_BuoiChieu");
            dtgShow.ItemsSource = dt.AsDataView();
            dtgShow.Columns[0].Header = "Mã Show";
            dtgShow.Columns[1].Header = "Mã phim";
            dtgShow.Columns[2].Header = "Mã phòng";
            dtgShow.Columns[3].Header = "Ngày chiếu";
            dtgShow.Columns[4].Header = "Mã Giờ chiếu";
            dtgShow.Columns[5].Header = "Số vé đã bán";
        }

        private void dtgShow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRowView = dtgShow.SelectedItem as DataRowView;
            if (dataRowView != null)
            {
                txtIdShow.Text = dataRowView[0].ToString();
                if (cbbIdFilm.Items.Contains(dataRowView[1].ToString()))
                {
                    cbbIdFilm.SelectedItem = dataRowView[1].ToString();
                }
                else
                {
                    cbbIdFilm.SelectedItem = null;
                }
                if (cbbIdRoom.Items.Contains(dataRowView[2].ToString()))
                {
                    cbbIdRoom.SelectedItem = dataRowView[2].ToString();
                }
                else
                {
                    cbbIdRoom.SelectedItem = null;
                }

                txtStartDay.Text = dataRowView[3].ToString();

                if (cbbIdStartTime.Items.Contains(dataRowView[4].ToString()))
                {
                    cbbIdStartTime.SelectedItem = dataRowView[4].ToString();
                }
                else
                {
                    cbbIdStartTime.SelectedItem = null;
                }

                txtSumOfTicket.Text = dataRowView[5].ToString();

                if(txtSumOfTicket.Text == "")
                {
                    txtSumOfMoney.Text = "0";
                }
                else
                {
                    List<Object> list = SQLite.ReadColumnData("select DongGia from GioChieu where MaGioChieu = '" + dataRowView[4].ToString().Trim() + "'"); //lấy giá tiền cảu giờ chiếu
                    int som = int.Parse(dataRowView[5].ToString().Trim()) * Convert.ToInt32(list[0]);
                    txtSumOfMoney.Text = som.ToString();
                }

                txtIdShow.IsEnabled = false;
            }
            else
            {
                txtIdShow.Text = "";
                txtStartDay.Text = "";
                txtSumOfMoney.Text = "";
                cbbIdFilm.SelectedItem = null;
                cbbIdRoom.SelectedItem = null;
                cbbIdStartTime.SelectedItem = null;
                txtSumOfTicket.Text = "";
                txtIdShow.IsEnabled = true;
            }
        }

        private void AddShow_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdShow.IsEnabled == false)
            {
                txtIdShow.Text = "";
                txtIdShow.Focus();
                txtStartDay.Text = "";
                cbbIdFilm.SelectedItem = null;
                cbbIdRoom.SelectedItem = null;
                cbbIdStartTime.SelectedItem = null;
                txtSumOfTicket.Text = "";
                txtSumOfMoney.Text = "";
                txtIdShow.IsEnabled = true;
            }
            else if (txtIdShow.Text == "" ||
            cbbIdFilm.SelectedItem == null ||
            cbbIdRoom.SelectedItem == null ||
            txtStartDay.Text == "" ||
            cbbIdStartTime.SelectedItem == null ||
            txtStartDay.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin");
            }
            else
            {
                DateTime start;

                if (!DateTime.TryParseExact(txtStartDay.Text, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out start))
                {
                    MessageBox.Show("nhập đúng định dạng ngày tháng: (yyyy/MM/dd) cho ngày khởi chiếu");
                    return;
                }
                else
                {
                    String sql = "insert into Show_BuoiChieu (MaShow, MaPhim, MaPhong, NgayChieu, MaGioChieu, SoVeDaBan) " +
                        "values ('" + txtIdShow.Text + "', '" + cbbIdFilm.SelectedItem.ToString() + "', '" + cbbIdRoom.SelectedItem.ToString() + "', '" + txtStartDay.Text + "', '" +
                        cbbIdStartTime.SelectedItem + "', 0)";
                    SQLite.ChangeData(sql);
                    LoadDataShow();
                    txtIdShow.Text = "";
                    txtIdShow.Focus();
                    cbbIdFilm.SelectedItem = null;
                    cbbIdRoom.SelectedItem = null;
                    cbbIdStartTime.SelectedItem = null;
                    txtStartDay.Text = "";
                    txtSumOfMoney.Text = "";
                    txtSumOfTicket.Text = "";
                    MessageBox.Show("Thêm phim thành công");
                }
            }
        }

        private void UpdateShow_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dtrv = dtgShow.SelectedItem as DataRowView;
            if (txtIdShow.IsEnabled == false)
            {
                txtIdShow.IsEnabled = true;
            }
            else if (dtgShow.SelectedItem == null)
            {
                MessageBox.Show("Hãy chọn show muốn thay đổi dữ liệu");
            }
            else
            {
                if (txtIdShow.Text == "")
                {
                    MessageBox.Show("Phải nhập Mã show");
                }
                else
                {
                    MessageBoxResult quesion = MessageBox.Show("Xác nhận thay đổi dữ liệu?", "thay đổi thông tin phim", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (quesion == MessageBoxResult.Yes)
                    {
                        String sql = "update Show_BuoiChieu set MaShow = '" + txtIdShow.Text + "', MaPhim = '" + cbbIdFilm.SelectedItem.ToString().Trim() + "', MaPhong = '" +
                        cbbIdRoom.SelectedItem.ToString().Trim() + "', NgayChieu = '" + txtStartDay.Text.Trim() + "', MaGioChieu = '" + cbbIdStartTime.SelectedItem.ToString().Trim() + "' where MaShow = '" + dtrv[0].ToString().Trim() + "'";
                        SQLite.ChangeData(sql);
                        LoadDataShow();
                        txtIdShow.Text = "";
                        cbbIdFilm.SelectedItem = null;
                        cbbIdRoom.SelectedItem = null;
                        cbbIdStartTime.SelectedItem = null;
                        txtStartDay.Text = "";
                        txtSumOfMoney.Text = "";
                        txtSumOfTicket.Text = "";
                        MessageBox.Show("Sửa dữ liệu thành công");
                    }
                }
            }
        }

        private void DeleteShow_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dtrv = dtgShow.SelectedItem as DataRowView;
            if (dtrv == null)
            {
                MessageBox.Show("Hãy chọn show muốn xóa dữ liệu");
            }
            else
            {
                MessageBoxResult question = MessageBox.Show("Bạn có chắc muốn xóa dữ liệu này?", "Xóa xữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (question == MessageBoxResult.Yes)
                {
                    String sql = "delete from Show_BuoiChieu where MaShow = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sql);
                    LoadDataShow();
                    txtIdShow.Text = "";
                    cbbIdFilm.SelectedItem = null;
                    cbbIdRoom.SelectedItem = null;
                    cbbIdStartTime.SelectedItem = null;
                    txtStartDay.Text = "";
                    txtSumOfMoney.Text = "";
                    txtSumOfTicket.Text = "";
                    MessageBox.Show("Xóa dữ liệu thành công");
                }
            }
        }

        private void SearchShow_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdShow.IsEnabled == false)
            {
                txtIdShow.Text = "";
                txtIdShow.Focus();
                cbbIdFilm.SelectedItem = null;
                cbbIdRoom.SelectedItem = null;
                cbbIdStartTime.SelectedItem = null;
                txtStartDay.Text = "";
                txtSumOfMoney.Text = "";
                txtSumOfTicket.Text = "";
                txtIdShow.IsEnabled = true;
            }
            else
            {
                String sql = "select * from Show_BuoiChieu where MaShow is not NULL";
                if (txtIdShow.Text != "")
                {
                    sql += " and MaShow = '" + txtIdShow.Text + "'";
                }
                if (txtStartDay.Text != "")
                {
                    sql += " and NgayChieu = '" + txtStartDay.Text + "'";
                }
                if (txtSumOfTicket.Text != "")
                {
                    sql += " and SoVeDaBan = '" + txtSumOfTicket.Text + "'";
                }
                if (cbbIdFilm.SelectedItem != null)
                {
                    sql += " and MaPhim = '" + cbbIdFilm.SelectedItem + "'";
                }
                if (cbbIdRoom.SelectedItem != null)
                {
                    sql += " and MaPhong = '" + cbbIdRoom.SelectedItem + "'";
                }
                if (cbbIdStartTime.SelectedItem != null)
                {
                    sql += " and MaGioChieu = '" + cbbIdStartTime.SelectedItem + "'";
                }
                DataTable dt = SQLite.ReadData(sql);
                dtgShow.ItemsSource = dt.AsDataView();
                dtgShow.Columns[0].Header = "Mã Show";
                dtgShow.Columns[1].Header = "Mã phim";
                dtgShow.Columns[2].Header = "Mã phòng";
                dtgShow.Columns[3].Header = "Ngày chiếu";
                dtgShow.Columns[4].Header = "Mã Giờ chiếu";
                dtgShow.Columns[5].Header = "Số vé đã bán";
                txtIdShow.Text = "";
                txtIdShow.Focus();
                cbbIdFilm.SelectedItem = null;
                cbbIdRoom.SelectedItem = null;
                cbbIdStartTime.SelectedItem = null;
                txtStartDay.Text = "";
                txtSumOfMoney.Text = "";
                txtSumOfTicket.Text = "";
                MessageBox.Show("Tìm kiếm thành công");

            }
        }
    }
}
