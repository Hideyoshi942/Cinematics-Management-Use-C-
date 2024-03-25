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
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer : Page
    {
        DataProcessing.DataProcessing SQLite = new DataProcessing.DataProcessing();
        public Customer()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataCustomer();
        }

        void LoadDataCustomer()
        {
            DataTable dtb = SQLite.ReadData("Select * from Khach");
            dtgCustomer.ItemsSource = dtb.AsDataView();
            dtgCustomer.Columns[0].Header = "Mã Khách";
            dtgCustomer.Columns[1].Header = "Tên Khách";
            dtgCustomer.Columns[2].Header = "Số điện thoại";
        }

        private void dtgCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRowView = dtgCustomer.SelectedItem as DataRowView;
            if (dataRowView != null)
            {
                txtidCustomer.Text = dataRowView[0].ToString();
                txtNameCustomer.Text = dataRowView[1].ToString();
                txtPhoneNumber.Text = dataRowView[2].ToString();

                txtidCustomer.IsEnabled = false;
            }
            else
            {
                txtidCustomer.Text = "";
                txtNameCustomer.Text = "";
                txtPhoneNumber.Text = "";
                txtidCustomer.IsEnabled = true;
            }
        }

        private void SearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (txtidCustomer.IsEnabled == false)
            {
                txtidCustomer.Text = "";
                txtidCustomer.Focus();
                txtNameCustomer.Text = "";
                txtPhoneNumber.Text = "";
                txtidCustomer.IsEnabled = true;
            }
            else
            {
                String sql = "select * from Khach where MaKhach is not NULL";
                if (txtidCustomer.Text != "")
                {
                    sql += " and MaKhach = '" + txtidCustomer.Text + "'";
                }
                if (txtNameCustomer.Text != "")
                {
                    sql += " and TenKhach = '" + txtNameCustomer.Text + "'";
                }
                if (txtPhoneNumber.Text != "")
                {
                    sql += " and SoDienThoai = '" + txtPhoneNumber.Text + "'";
                }
                DataTable dt = SQLite.ReadData(sql);
                dtgCustomer.ItemsSource = dt.AsDataView();
                dtgCustomer.Columns[0].Header = "Mã khách";
                dtgCustomer.Columns[1].Header = "Tên khách";
                dtgCustomer.Columns[2].Header = "Số điện thoại";
                txtidCustomer.Focus();
                txtidCustomer.Text = "";
                txtNameCustomer.Text = "";
                txtPhoneNumber.Text = "";
                MessageBox.Show("Tìm kiếm thành công");
            }
        }

        private void ShowData_Click(object sender, RoutedEventArgs e)
        {
            LoadDataCustomer();
        }
        
    }
}
