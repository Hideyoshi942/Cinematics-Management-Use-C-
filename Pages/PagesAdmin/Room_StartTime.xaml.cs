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
    /// Interaction logic for Room_StartTime.xaml
    /// </summary>
    public partial class Room_StartTime : Page
    {
        DataProcessing.DataProcessing SQLite = new DataProcessing.DataProcessing();

        public Room_StartTime()
        {
            InitializeComponent();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataRoom();
            LoadDataStartTime();
        }

        void LoadDataRoom()
        {
            DataTable dtb = SQLite.ReadData("Select * from PhongChieu");
            dtgRoom.ItemsSource = dtb.AsDataView();
            dtgRoom.Columns[0].Header = "Mã phòng";
            dtgRoom.Columns[1].Header = "Tên phòng";
        }

        void LoadDataStartTime()
        {
            DataTable dtb = SQLite.ReadData("Select * from GioChieu");
            dtgStartTime.ItemsSource = dtb.AsDataView();
            dtgStartTime.Columns[0].Header = "Mã giờ chiếu";
            dtgStartTime.Columns[1].Header = "Đồng giá";
        }

        private void dtgRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dtrv = dtgRoom.SelectedItem as DataRowView;
            if (dtrv != null)
            {
                txtIdRoom.Text = dtrv[0].ToString();
                txtNameRoom.Text = dtrv[1].ToString();
                txtIdRoom.IsEnabled = false;
            }
            else
            {
                txtIdRoom.Text = "";
                txtNameRoom.Text = "";
                txtIdRoom.IsEnabled = true;
            }
        }

        private void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdRoom.IsEnabled == false)
            {
                txtIdRoom.IsEnabled = true;
                txtIdRoom.Text = "";
                txtNameRoom.Text = "";
            }
            else if (txtIdRoom.Text == "")
            {
                MessageBox.Show("Chưa nhập mã phòng");
            }
            else if (txtNameRoom.Text == "")
            {
                MessageBox.Show("Chưa nhập tên phòng");
            }
            else
            {
                String sql = "insert into PhongChieu (MaPhong, TenPhong) values ('" + txtIdRoom.Text.Trim() + "', '" + txtNameRoom.Text.Trim() + "')";
                SQLite.ChangeData(sql);
                LoadDataRoom();
                txtIdRoom.Text = "";
                txtNameRoom.Text = "";
                MessageBox.Show("Thêm dữ liệu thành công");
            }
        }

        private void UpdateRoom_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dtrv = dtgRoom.SelectedItem as DataRowView;
            if (dtrv == null)
            {
                MessageBox.Show("hãy chọn dữ liệu cần sửa");

            }
            else if (txtIdRoom.IsEnabled == false)
            {
                txtIdRoom.IsEnabled = true;
                txtNameRoom.IsEnabled = true;
            }
            else if (txtIdRoom.IsEnabled == true)
            {
                MessageBoxResult question = MessageBox.Show("Nếu thay đổi dữ liệu này, những thông tin về Show diễn liên quan có thể bị thay đổi", "Xóa xữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (question == MessageBoxResult.Yes)
                {
                    String sql = "update PhongChieu Set MaPhong = '" + txtIdRoom.Text.Trim() + "', TenPhong = '" + txtNameRoom.Text.Trim() + "' where MaPhong = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sql);
                    String sqlShow = "update Show_BuoiChieu set MaPhong = '" + txtIdRoom.Text.Trim() + "' where MaPhong = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sqlShow);
                    LoadDataRoom();
                    txtIdRoom.Text = "";
                    txtNameRoom.Text = "";
                    MessageBox.Show("Đã thay đổi dữ liệu thành công");
                }
            }
        }

        private void DeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dtrv = dtgRoom.SelectedItem as DataRowView;
            if (dtrv != null)
            {
                MessageBoxResult question = MessageBox.Show("Nếu xóa dữ liệu này, những thông tin về Show diễn liên quan có thể bị thay đổi", "Xóa xữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (question == MessageBoxResult.Yes)
                {
                    String sql = "delete from PhongChieu where MaPhong = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sql);
                    String sqlFilm = "update Show_BuoiChieu set MaPhong = NULL where MaPhong = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sqlFilm);
                    LoadDataRoom();
                    txtIdRoom.Text = "";
                    txtNameRoom.Text = "";
                    MessageBox.Show("Đã Xóa dữ liệu thành công");
                    txtIdRoom.IsEnabled = true;
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn dữ liệu cần xóa");
            }
        }


        private void dtfStartTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dtrv = dtgStartTime.SelectedItem as DataRowView;
            if (dtrv != null)
            {
                txtIdStartTime.Text = dtrv[0].ToString();
                txtCost.Text = dtrv[1].ToString();
                txtIdStartTime.IsEnabled = false;
            }
            else
            {
                txtIdStartTime.Text = "";
                txtCost.Text = "";
                txtIdStartTime.IsEnabled = true;
            }
        }

        private void AddStartTime_Click(object sender, RoutedEventArgs e)
        {
            int result;
            if (txtIdStartTime.IsEnabled == false)
            {
                txtIdRoom.IsEnabled = true;
                txtIdRoom.Text = "";
                txtNameRoom.Text = "";
            }
            else if (txtIdStartTime.Text == "")
            {
                MessageBox.Show("Chưa nhập mã phòng");
            }
            else if (txtCost.Text == "")
            {
                MessageBox.Show("Chưa nhập giá (đòng giá)");
            }
            else if (!int.TryParse(txtCost.Text.Trim(), out result))
            {
                MessageBox.Show("Đồng giá phải là số nguyên");
            }
            else
            {
                String sql = "insert into GioChieu (MaGioChieu, DongGia) values ('" + txtIdStartTime.Text.Trim() + "', " + txtCost.Text.Trim() + ")";
                SQLite.ChangeData(sql);
                LoadDataStartTime();
                txtIdStartTime.Text = "";
                txtCost.Text = "";
                MessageBox.Show("Thêm dữ liệu thành công");
            }
        }

        private void UpdateStartTime_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dtrv = dtgStartTime.SelectedItem as DataRowView;
            if (dtrv == null)
            {
                MessageBox.Show("hãy chọn dữ liệu cần sửa");

            }
            else if (txtIdStartTime.IsEnabled == false)
            {
                txtIdStartTime.IsEnabled = true;
                txtCost.IsEnabled = true;
            }
            else if (txtIdStartTime.IsEnabled == true)
            {
                MessageBoxResult question = MessageBox.Show("Nếu thay đổi dữ liệu này, những thông tin về Show diễn liên quan có thể bị thay đổi", "Xóa xữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (question == MessageBoxResult.Yes)
                {
                    String sql = "update GioChieu Set MaGioChieu = '" + txtIdStartTime.Text.Trim() + "', DongGia = " + txtCost.Text.Trim() + " where MaGioChieu = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sql);
                    String sqlShow = "update Show_BuoiChieu set MaGioChieu = '" + txtIdStartTime.Text.Trim() + "' where MaGioChieu = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sqlShow);
                    LoadDataStartTime();
                    txtIdStartTime.Text = "";
                    txtCost.Text = "";
                    MessageBox.Show("Đã thay đổi dữ liệu thành công");
                }
            }
        }

        private void DeleteStartTime_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dtrv = dtgStartTime.SelectedItem as DataRowView;
            if (dtrv != null)
            {
                MessageBoxResult question = MessageBox.Show("Nếu xóa dữ liệu này, những thông tin về Show diễn liên quan có thể bị thay đổi", "Xóa xữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (question == MessageBoxResult.Yes)
                {
                    String sql = "delete from GioChieu where MaGioChieu = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sql);
                    String sqlShow = "update Show_BuoiChieu set MaGioChieu = NULL where MaGioChieu = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sqlShow);
                    LoadDataStartTime();
                    txtIdStartTime.Text = "";
                    txtCost.Text = "";
                    MessageBox.Show("Đã Xóa dữ liệu thành công");
                    txtIdStartTime.IsEnabled = true;
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn dữ liệu cần xóa");
            }
        }
    }
}
