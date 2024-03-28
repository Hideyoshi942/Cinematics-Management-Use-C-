using Microsoft.SqlServer.Server;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
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
using System.Xml.Linq;

namespace WPFModernVerticalMenu.Pages.PagesAdmin
{
    /// <summary>
    /// Interaction logic for Film.xaml
    /// </summary>
    public partial class Film : Page
    {
        DataProcessing.DataProcessing SQLite = new DataProcessing.DataProcessing();

        public Film()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadIdCountry();
            LoadIdCategory();
            LoadIdManufactor();
            LoadDataFilm();
        }

        private void LoadIdCountry()
        {
            List<Object> list = SQLite.ReadColumnData("select MaNSX from NuocSanXuat");
            cbbIdCountry.ItemsSource = list;
        }

        private void LoadIdManufactor()
        {
            List<Object> list = SQLite.ReadColumnData("select MaHSX from HangSanXuat");
            cbbIdManufactor.ItemsSource = list;
        }

        private void LoadIdCategory()
        {
            List<Object> list = SQLite.ReadColumnData("select MaTheLoai from TheLoai");
            ccbIdCategory.ItemsSource = list;
        }

        private void LoadDataFilm()
        {
            DataTable dt = SQLite.ReadData("select * from phim");
            dtgFilm.ItemsSource = dt.AsDataView();
            dtgFilm.Columns[0].Header = "Mã phim";
            dtgFilm.Columns[1].Header = "Tên phim";
            dtgFilm.Columns[2].Header = "Mã NSX";
            dtgFilm.Columns[3].Header = "Mã HSX";
            dtgFilm.Columns[4].Header = "Đạo diễn";
            dtgFilm.Columns[5].Header = "Mã thể loại";
            dtgFilm.Columns[6].Header = "Ngày khởi chiếu";
            dtgFilm.Columns[7].Header = "Ngày kết thúc";
            dtgFilm.Columns[8].Header = "Nữ dv chính";
            dtgFilm.Columns[9].Header = "Nam dv chính";
            dtgFilm.Columns[10].Header = "ND chính";
            dtgFilm.Columns[11].Header = "Video";
            dtgFilm.Columns[12].Header = "Hình ảnh";
        }

        private void dtgFilm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRowView = dtgFilm.SelectedItem as DataRowView;
            if(dataRowView != null)
            {
                txtIdFilm.Text = dataRowView[0].ToString();
                txtNameFilm.Text = dataRowView[1].ToString();
                if (cbbIdCountry.Items.Contains(dataRowView[2].ToString()))
                {
                    cbbIdCountry.SelectedItem = dataRowView[2].ToString();
                }
                else
                {
                    cbbIdCountry.SelectedItem= null;
                }
                if (cbbIdManufactor.Items.Contains(dataRowView[3].ToString()))
                {
                    cbbIdManufactor.SelectedItem = dataRowView[3].ToString();
                }
                else
                {
                    cbbIdManufactor.SelectedItem= null;
                }

                txtDirector.Text = dataRowView[4].ToString();

                if (ccbIdCategory.Items.Contains(dataRowView[5].ToString()))
                {
                    ccbIdCategory.SelectedItem = dataRowView[5].ToString();
                }
                else { 
                    ccbIdCategory.SelectedItem= null; 
                }

                txtStartDay.Text = dataRowView[6].ToString();
                txtEndDay.Text = dataRowView[7].ToString();
                txtActress.Text = dataRowView[8].ToString();
                txtACtor.Text = dataRowView[9].ToString();
                txtContent.Text = dataRowView[10].ToString();
                txtVideo.Text = dataRowView[11].ToString();
                txtImage.Text = dataRowView[12].ToString();
                txtIdFilm.IsEnabled = false;
            }
            else
            {
                txtIdFilm.Text = "";
                txtNameFilm.Text = "";
                txtDirector.Text = "";
                cbbIdCountry.SelectedItem = null;
                ccbIdCategory.SelectedItem = null;
                cbbIdManufactor.SelectedItem = null;
                txtActress.Text = "";
                txtACtor.Text = "";
                txtContent.Text = "";
                txtStartDay.Text = "";
                txtEndDay.Text = "";
                txtIdFilm.IsEnabled = true;

            }
        }

        private void AddFilm_Click(object sender, RoutedEventArgs e)
        {
            if(txtIdFilm.IsEnabled == false)
            {
                txtIdFilm.Text = "";
                txtIdFilm.Focus();
                txtNameFilm.Text = "";
                txtDirector.Text = "";
                cbbIdCountry.SelectedItem = null;
                ccbIdCategory.SelectedItem = null;
                cbbIdManufactor.SelectedItem = null;
                txtActress.Text = "";
                txtACtor.Text = "";
                txtContent.Text = "";
                txtStartDay.Text = "";
                txtEndDay.Text = "";
                txtVideo.Text = "";
                txtImage.Text = "";
                txtIdFilm.IsEnabled = true;
            }
            else if(txtIdFilm.Text == "" ||
            txtNameFilm.Text == "" ||
            txtDirector.Text == ""||
            cbbIdCountry.SelectedItem == null ||
            ccbIdCategory.SelectedItem == null ||
            cbbIdManufactor.SelectedItem == null ||
            txtActress.Text == "" ||
            txtACtor.Text == "" ||
            txtContent.Text == "" ||
            txtStartDay.Text == "" ||
            txtEndDay.Text == "" ||
            txtVideo.Text == "" ||
            txtImage.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin");
            }
            else
            {
                DateTime start, end;

                if(!DateTime.TryParseExact(txtStartDay.Text, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out start))
                {
                    MessageBox.Show("nhập đúng định dạng ngày tháng: (yyyy/MM/dd) cho ngày khởi chiếu");
                    return;
                }
                else if(!DateTime.TryParseExact(txtEndDay.Text, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out end))
                {
                    MessageBox.Show("nhập đúng định dạng ngày tháng: (yyyy/MM/dd) cho ngày kết thúc");
                    return;
                }
                else if (start > end)
                {
                    MessageBox.Show("ngày bắt đầu không thể sau ngày kết thúc");
                    return;
                }
                else
                {
                    String sql = "insert into Phim (MaPhim, TenPhim, MaNuocSanXuat, MaHangSanXuat, DaoDien, MaTheLoai, NgayKhoiChieu, NgayKetThuc, NuDVChinh, NamDVChinh, NDChinh, Video, HinhAnh) " +
                        "values ('" + txtIdFilm.Text + "', '" + txtNameFilm.Text + "', '" + cbbIdCountry.SelectedItem.ToString() + "', '" + cbbIdManufactor.SelectedItem.ToString() + "', '" + txtDirector.Text + "', '" +
                        ccbIdCategory.SelectedItem + "', '" + txtStartDay.Text + "', '" + txtEndDay.Text + "' , '" + txtActress.Text + "' , '" + txtACtor.Text + "', '" + txtContent.Text + "', '" + txtVideo.Text + "', '" + txtImage.Text + "')";
                    SQLite.ChangeData(sql);
                    LoadDataFilm();
                    txtIdFilm.Text = "";
                    txtIdFilm.Focus();
                    txtNameFilm.Text = "";
                    txtDirector.Text = "";
                    cbbIdCountry.SelectedItem = null;
                    ccbIdCategory.SelectedItem = null;
                    cbbIdManufactor.SelectedItem = null;
                    txtActress.Text = "";
                    txtACtor.Text = "";
                    txtContent.Text = "";
                    txtStartDay.Text = "";
                    txtEndDay.Text = "";
                    txtVideo.Text = "";
                    txtImage.Text = "";
                    MessageBox.Show("Thêm phim thành công");
                }
            }
        }

        private void UpdateFilm_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dtrv = dtgFilm.SelectedItem as DataRowView;
            if(txtIdFilm.IsEnabled == false)
            {
                txtIdFilm.IsEnabled = true;
            }
            else if(dtgFilm.SelectedItem == null)
            {
                MessageBox.Show("Hãy chọn phim muốn thay đổi dữ liệu");
            }
            else
            {
                if (txtIdFilm.Text == "")
                {
                    MessageBox.Show("Phải nhập Mã phim");
                }
                else
                {
                    MessageBoxResult quesion = MessageBox.Show("Xác nhận thay đổi dữ liệu?", "thay đổi thông tin phim", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if(quesion == MessageBoxResult.Yes)
                    {
                        String sql = "update Phim set MaPhim = '" + txtIdFilm.Text.Trim() + "', TenPhim = '" + txtNameFilm.Text.Trim() + "', MaNuocSanXuat = '" + cbbIdCountry.SelectedItem.ToString().Trim() + "', MaHangSanXuat = '" +
                        cbbIdManufactor.SelectedItem.ToString().Trim() + "', DaoDien = '" + txtDirector.Text.Trim() + "', MaTheLoai = '" + ccbIdCategory.SelectedItem.ToString().Trim() + "', NgayKhoiChieu = '" + txtStartDay.Text.Trim() +
                        "', NgayKetThuc = '" + txtEndDay.Text.Trim() + "', NuDVChinh = '" + txtActress.Text.Trim() + "', NamDVChinh = '" + txtACtor.Text.Trim() + "', NDChinh = '" + txtContent.Text.Trim() + "', Video = '" +
                        txtVideo.Text.Trim() + "', HinhAnh = '" + txtImage.Text.Trim() + "' where MaPhim = '" + dtrv[0].ToString().Trim() + "'";
                        SQLite.ChangeData(sql);
                        LoadDataFilm();
                        txtIdFilm.Text = "";
                        txtNameFilm.Text = "";
                        txtDirector.Text = "";
                        cbbIdCountry.SelectedItem = null;
                        ccbIdCategory.SelectedItem = null;
                        cbbIdManufactor.SelectedItem = null;
                        txtActress.Text = "";
                        txtACtor.Text = "";
                        txtContent.Text = "";
                        txtStartDay.Text = "";
                        txtEndDay.Text = "";
                        txtVideo.Text = "";
                        txtImage.Text = "";
                        MessageBox.Show("Sửa dữ liệu thành công");
                    }
                }
            }
        }

        private void DeleteFilm_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dtrv = dtgFilm.SelectedItem as DataRowView;
            if (dtrv == null)
            {
                MessageBox.Show("Hãy chọn phim muốn xóa dữ liệu");
            }
            else
            {
                MessageBoxResult question = MessageBox.Show("Bạn có chắc muốn xóa dữ liệu này?", "Xóa xữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (question == MessageBoxResult.Yes)
                {
                    String sql = "delete from Phim where MaPhim = '" + dtrv[0].ToString().Trim() + "'";
                    SQLite.ChangeData(sql);
                    LoadDataFilm();
                    txtIdFilm.Text = "";
                    txtNameFilm.Text = "";
                    txtDirector.Text = "";
                    cbbIdCountry.SelectedItem = null;
                    ccbIdCategory.SelectedItem = null;
                    cbbIdManufactor.SelectedItem = null;
                    txtActress.Text = "";
                    txtACtor.Text = "";
                    txtContent.Text = "";
                    txtStartDay.Text = "";
                    txtEndDay.Text = "";
                    txtVideo.Text = "";
                    txtImage.Text = "";
                    MessageBox.Show("Xóa dữ liệu thành công");
                }
            }
        }

        private void SearchFilm_Click(object sender, RoutedEventArgs e)
        {
            if(txtIdFilm.IsEnabled == false)
            {
                txtIdFilm.Text = "";
                txtIdFilm.Focus();
                txtNameFilm.Text = "";
                txtDirector.Text = "";
                cbbIdCountry.SelectedItem = null;
                ccbIdCategory.SelectedItem = null;
                cbbIdManufactor.SelectedItem = null;
                txtActress.Text = "";
                txtACtor.Text = "";
                txtContent.Text = "";
                txtStartDay.Text = "";
                txtEndDay.Text = "";
                txtVideo.Text = "";
                txtImage.Text = "";
                txtIdFilm.IsEnabled = true;
            }
            else
            {
                String sql = "select * from Phim where MaPhim is not NULL";
                if(txtIdFilm.Text != "")
                {
                    sql += " and MaPhim = '" + txtIdFilm.Text + "'";
                }
                if(txtNameFilm.Text != "")
                {
                    sql += " and TenPhim = '" + txtNameFilm.Text + "'";
                }
                if (txtDirector.Text != "")
                {
                    sql += " and DaoDien = '" + txtDirector.Text + "'";
                }
                if (txtActress.Text != "")
                {
                    sql += " and NuDVChinh = '" + txtActress.Text + "'";
                }
                if (txtACtor.Text != "")
                {
                    sql += " and NamDVChinh = '" + txtACtor.Text + "'";
                }
                if (txtContent.Text != "")
                {
                    sql += " and NDChinh = '" + txtContent.Text + "'";
                }
                if (txtStartDay.Text != "")
                {
                    sql += " and NgayKhoiChieu = '" + txtStartDay.Text + "'";
                }
                if (txtEndDay.Text != "")
                {
                    sql += " and NgayKetThuc = '" + txtEndDay.Text + "'";
                }
                if (txtVideo.Text != "")
                {
                    sql += " and Video = '" + txtVideo.Text + "'";
                }
                if (txtImage.Text != "")
                {
                    sql += " and HinhAnh = '" + txtImage.Text + "'";
                }
                if(ccbIdCategory.SelectedItem != null)
                {
                    sql += " and MaTheLoai = '" + ccbIdCategory.SelectedItem + "'";
                }
                if (cbbIdCountry.SelectedItem != null)
                {
                    sql += " and MaNuocSanXuat = '" + cbbIdCountry.SelectedItem + "'";
                }
                if (cbbIdManufactor.SelectedItem != null)
                {
                    sql += " and MaHangSanXuat = '" + cbbIdManufactor.SelectedItem + "'";
                }
                DataTable dt = SQLite.ReadData(sql);
                dtgFilm.ItemsSource = dt.AsDataView();
                dtgFilm.Columns[0].Header = "Mã phim";
                dtgFilm.Columns[1].Header = "Tên phim";
                dtgFilm.Columns[2].Header = "Mã NSX";
                dtgFilm.Columns[3].Header = "Mã HSX";
                dtgFilm.Columns[4].Header = "Đạo diễn";
                dtgFilm.Columns[5].Header = "Mã thể loại";
                dtgFilm.Columns[6].Header = "Ngày khởi chiếu";
                dtgFilm.Columns[7].Header = "Ngày kết thúc";
                dtgFilm.Columns[8].Header = "Nữ dv chính";
                dtgFilm.Columns[9].Header = "Nam dv chính";
                dtgFilm.Columns[10].Header = "ND chính";
                dtgFilm.Columns[11].Header = "Video";
                dtgFilm.Columns[12].Header = "Hình ảnh";
                txtIdFilm.Text = "";
                txtIdFilm.Focus();
                txtNameFilm.Text = "";
                txtDirector.Text = "";
                cbbIdCountry.SelectedItem = null;
                ccbIdCategory.SelectedItem = null;
                cbbIdManufactor.SelectedItem = null;
                txtActress.Text = "";
                txtACtor.Text = "";
                txtContent.Text = "";
                txtStartDay.Text = "";
                txtEndDay.Text = "";
                txtVideo.Text = "";
                txtImage.Text = "";
                MessageBox.Show("Tìm kiếm thành công");

            }
        }

        private void selectVideo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Video Files (*.mp4;*.avi;*.wmv;*.flv)|*.mp4;*.avi;*.wmv;*.flv|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                txtVideo.Text = openFileDialog.FileName;
            }

        }

        private void selectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                txtImage.Text = openFileDialog.FileName;
            }
        }
    }

}
