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
    /// Interaction logic for Country_Manufactoure_Category.xaml
    /// </summary>
    public partial class Country_Manufactoure_Category : Page
    {
        DataProcessing.DataProcessing SQLite = new DataProcessing.DataProcessing();

        public Country_Manufactoure_Category()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataCountry();
            LoadDataManufactor();
            LoadDataCategory();
        }

        void LoadDataCountry()
        {
            DataTable dtb = SQLite.ReadData("Select * from NuocSanXuat");
            dtgCountry.ItemsSource = dtb.AsDataView();
            dtgCountry.Columns[0].Header = "Mã nước sản xuất";
            dtgCountry.Columns[1].Header = "Tên nước sản xuất";
        }

        private void dtgCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dtrv = dtgCountry.SelectedItem as DataRowView;
            if (dtrv != null)
            {
                txtIdCountry.Text = dtrv[0].ToString();
                txtNameCountry.Text = dtrv[1].ToString();
                txtIdCountry.IsEnabled = false;
            }
            else
            {
                txtIdCountry.Text = "";
                txtNameCountry.Text = "";
                txtIdCountry.IsEnabled = true;
            }
        }

        private void AddCountry_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdCountry.IsEnabled == false) {
                txtIdCountry.IsEnabled = true;
                txtIdCountry.Text = "";
                txtNameCountry.Text = "";
            }
            else if (txtIdCountry.Text == "")
            {
                MessageBox.Show("Chưa nhập mã nước sản xuất");
            }
            else if (txtNameCountry.Text == "")
            {
                MessageBox.Show("Chưa nhập tên nước sản xuất");
            }
            else
            {
                String sql = "insert into NuocSanXuat (MaNSX, TenNSX) values ('" + txtIdCountry.Text.Trim() + "', '" + txtNameCountry.Text.Trim() + "')";
                SQLite.ChangeData(sql);
                LoadDataCountry();
                txtIdCountry.Text = "";
                txtNameCountry.Text = "";
                MessageBox.Show("Thêm dữ liệu thành công");
            }
        }

        private void UpdateCountry_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dtrv = dtgCountry.SelectedItem as DataRowView;
            if (dtrv == null)
            {
                MessageBox.Show("hãy chọn dữ liệu cần sửa");

            }
            else if(txtIdCountry.IsEnabled == false)
            {
                txtIdCountry.IsEnabled = true;
                txtNameCountry.IsEnabled = true;
            }
            else if(txtIdCountry.IsEnabled == true)
            {
                MessageBoxResult question = MessageBox.Show("Nếu thay đổi dữ liệu này, những thông tin về phim liên quan có thể bị thay đổi", "Xóa xữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(question == MessageBoxResult.Yes)
                {
                    String sql = "update NuocSanXuat Set MaNSX = '" + txtIdCountry.Text.Trim() + "', TenNSX = '" + txtNameCountry.Text.Trim() + "' where MaNSX = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sql);
                    String sqlFilm = "update Phim set MaNuocSanXuat = '" + txtIdCountry.Text.Trim() + "' where MaNuocSanXuat = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sqlFilm);
                    LoadDataCountry();
                    txtIdCountry.Text = "";
                    txtNameCountry.Text = "";
                    MessageBox.Show("Đã thay đổi dữ liệu thành công");
                }
            }
        }

        private void DeleteCountry_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dtrv = dtgCountry.SelectedItem as DataRowView;
            if(dtrv != null )
            {
                MessageBoxResult question = MessageBox.Show("Nếu xóa dữ liệu này, những thông tin về phim liên quan có thể bị thay đổi", "Xóa xữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (question == MessageBoxResult.Yes)
                {
                    String sql = "delete from NuocSanXuat where MaNSX = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sql);
                    String sqlFilm = "update Phim set MaNuocSanXuat = NULL where MaNuocSanXuat = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sqlFilm);
                    LoadDataCountry();
                    txtIdCountry.Text = "";
                    txtNameCountry.Text = "";
                    MessageBox.Show("Đã Xóa dữ liệu thành công");
                    txtIdCountry.IsEnabled = true;
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn dữ liệu cần xóa");
            }
        }

        void LoadDataManufactor()
        {
            DataTable dtb = SQLite.ReadData("select * from HangSanXuat");
            dtgManufactor.ItemsSource = dtb.AsDataView();
            dtgManufactor.Columns[0].Header = "Mã hãng sản xuất";
            dtgManufactor.Columns[1].Header = "Tên hãng sản xuất";
        }

        private void dtgManufactor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dtrv = dtgManufactor.SelectedItem as DataRowView;
            if (dtrv != null)
            {
                txtIdManufactor.Text = dtrv[0].ToString();
                txtNameManufactor.Text = dtrv[1].ToString();
                txtIdManufactor.IsEnabled = false;
            }
            else
            {
                txtIdManufactor.Text = "";
                txtNameManufactor.Text = "";
                txtIdManufactor.IsEnabled = true;
            }
        }

        private void addManufactor_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdManufactor.IsEnabled == false)
            {
                txtIdManufactor.IsEnabled = true;
                txtIdManufactor.Text = "";
                txtNameManufactor.Text = "";
            }
            else if (txtIdManufactor.Text == "")
            {
                MessageBox.Show("Chưa nhập mã hãng sản xuất");
            }
            else if (txtNameManufactor.Text == "")
            {
                MessageBox.Show("Chưa nhập tên hãng sản xuất");
            }
            else
            {
                String sql = "insert into HangSanXuat (MaHSX, TenHSX) values ('" + txtIdManufactor.Text.Trim() + "', '" + txtNameManufactor.Text.Trim() + "')";
                SQLite.ChangeData(sql);
                LoadDataManufactor();
                txtIdManufactor.Text = "";
                txtNameManufactor.Text = "";
                MessageBox.Show("Thêm dữ liệu thành công");
            }
        }

        private void updateManufactor_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dtrv = dtgManufactor.SelectedItem as DataRowView;
            if (dtrv == null)
            {
                MessageBox.Show("hãy chọn dữ liệu cần sửa");

            }
            else if (txtIdManufactor.IsEnabled == false)
            {
                txtIdManufactor.IsEnabled = true;
                txtNameManufactor.IsEnabled = true;
            }
            else if (txtIdManufactor.IsEnabled == true)
            {
                MessageBoxResult question = MessageBox.Show("Nếu thay đổi dữ liệu này, những thông tin về phim liên quan có thể bị thay đổi", "Xóa xữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (question == MessageBoxResult.Yes)
                {
                    String sql = "update HangSanXuat Set MaHSX = '" + txtIdManufactor.Text.Trim() + "', TenHSX = '" + txtNameManufactor.Text.Trim() + "' where MaHSX = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sql);
                    String sqlFilm = "update Phim set MaHangSanXuat = '" + txtIdManufactor.Text.Trim() + "' where MaHangSanXuat = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sqlFilm);
                    LoadDataManufactor();
                    txtIdManufactor.Text = "";
                    txtNameManufactor.Text = "";
                    MessageBox.Show("Đã thay đổi dữ liệu thành công");
                }
                
            }
        }

        private void deleteManufactor_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dtrv = dtgManufactor.SelectedItem as DataRowView;
            if (dtrv != null)
            {
                MessageBoxResult question = MessageBox.Show("Nếu xóa dữ liệu này, những thông tin về phim liên quan có thể bị thay đổi", "Xóa xữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (question == MessageBoxResult.Yes)
                {
                    String sql = "delete from HangSanXuat where MaHSX = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sql);
                    String sqlFilm = "update Phim set MaHangSanXuat = NULL where MaHangSanXuat = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sqlFilm);
                    LoadDataManufactor();
                    txtIdManufactor.Text = "";
                    txtNameManufactor.Text = "";
                    MessageBox.Show("Đã Xóa dữ liệu thành công");
                    txtIdManufactor.IsEnabled = true;
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn dữ liệu cần xóa");
            }
        }

        void LoadDataCategory()
        {
            DataTable dtb = SQLite.ReadData("select * from TheLoai");
            dtgCategory.ItemsSource = dtb.AsDataView();
            dtgCategory.Columns[0].Header = "Mã thể loại";
            dtgCategory.Columns[1].Header = "Tên thể loại";
        }

        private void dtgCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dtrv = dtgCategory.SelectedItem as DataRowView;
            if (dtrv != null)
            {
                txtIdCategory.Text = dtrv[0].ToString();
                txtNameCategory.Text = dtrv[1].ToString();
                txtIdCategory.IsEnabled = false;
            }
            else
            {
                txtIdCategory.Text = "";
                txtNameCategory.Text = "";
                txtIdCategory.IsEnabled = true;
            }
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdCategory.IsEnabled == false)
            {
                txtIdCategory.IsEnabled = true;
                txtIdCategory.Text = "";
                txtNameCategory.Text = "";
            }
            else if (txtIdCategory.Text == "")
            {
                MessageBox.Show("Chưa nhập mã thể loại xuất");
            }
            else if (txtNameCategory.Text == "")
            {
                MessageBox.Show("Chưa nhập tên thể loại xuất");
            }
            else
            {
                String sql = "insert into TheLoai (MaTheLoai, TenTheLoai) values ('" + txtIdCategory.Text.Trim() + "', '" + txtNameCategory.Text.Trim() + "')";
                SQLite.ChangeData(sql);
                LoadDataCategory();
                txtIdCategory.Text = "";
                txtNameCategory.Text = "";
                MessageBox.Show("Thêm dữ liệu thành công");
            }
        }

        private void UpdateCategory_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dtrv = dtgCategory.SelectedItem as DataRowView;
            if (dtrv == null)
            {
                MessageBox.Show("hãy chọn dữ liệu cần sửa");

            }
            else if (txtIdCategory.IsEnabled == false)
            {
                txtIdCategory.IsEnabled = true;
                txtNameCategory.IsEnabled = true;
            }
            else if (txtIdCategory.IsEnabled == true)
            {
                if (txtIdCategory.Text == "" || txtNameCategory.Text == "")
                {
                    MessageBox.Show("nhập thiếu Mã thể loại hoặc Tên thể loại");
                    return;
                }
                MessageBoxResult question = MessageBox.Show("Nếu thay đổi dữ liệu này, những thông tin về phim liên quan có thể bị thay đổi", "Xóa xữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (question == MessageBoxResult.Yes)
                {
                    String sql = "update TheLoai Set MaTheLoai = '" + txtIdCategory.Text.Trim() + "', TenTheLoai = '" + txtNameCategory.Text.Trim() + "' where MaTheLoai = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sql);
                    String sqlFilm = "update Phim set MaTheLoai = '" + txtIdCategory.Text.Trim() + "' where MaTheLoai = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sqlFilm);
                    LoadDataCategory();
                    txtIdCategory.Text = "";
                    txtNameCategory.Text = "";
                    MessageBox.Show("Đã thay đổi dữ liệu thành công");
                }
            }
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dtrv = dtgCategory.SelectedItem as DataRowView;
            if (dtrv != null)
            {
                MessageBoxResult question = MessageBox.Show("Bạn có chắc muốn xóa dữ liệu này?", "Xóa xữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (question == MessageBoxResult.Yes)
                {
                    String sql = "delete from TheLoai where MaTheLoai = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sql);
                    String sqlFilm = "update Phim set MaTheLoai = NULL where MaTheLoai = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sqlFilm);
                    LoadDataCategory();
                    txtIdCategory.Text = "";
                    txtNameCategory.Text = "";
                    MessageBox.Show("Đã Xóa dữ liệu thành công");
                    txtIdCategory.IsEnabled = true;
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn dữ liệu cần xóa");
            }
        }
    }
}
