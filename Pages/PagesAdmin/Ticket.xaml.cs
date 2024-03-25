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
    /// Interaction logic for Ticket.xaml
    /// </summary>
    public partial class Ticket : Page
    {
        DataProcessing.DataProcessing SQLite = new DataProcessing.DataProcessing();
        public Ticket()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataTicket();
            LoadIdKhach();
            LoadIdShow();
        }

        void LoadDataTicket()
        {
            List<String> listNumber = new List<String>() { "1", "2", "3", "4", "5" };
            cbbNumberOFSeat.ItemsSource = listNumber;
            List<String> listRow = new List<string>() { "A", "B", "C", "D", "E" };
            cbbRowOfSeat.ItemsSource = listRow;
            DataTable dtb = SQLite.ReadData("Select * from Ve");
            dtgTicket.ItemsSource = dtb.AsDataView();
            dtgTicket.Columns[0].Header = "Mã vé";
            dtgTicket.Columns[1].Header = "Mã Show";
            dtgTicket.Columns[2].Header = "Hàng ghế";
            dtgTicket.Columns[3].Header = "Số ghế";
            dtgTicket.Columns[4].Header = "Mã khách";
        }

        void LoadIdShow()
        {
            List<Object> list = SQLite.ReadColumnData("select MaShow from Show_BuoiChieu");
            cbbIdShow.ItemsSource = list;
        }

        void LoadIdKhach()
        {
            List<Object> list = SQLite.ReadColumnData("select MaKhach from Khach");
            cbbIdCustomer.ItemsSource = list;
        }

        private void dtgTicket_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRowView = dtgTicket.SelectedItem as DataRowView;
            if (dataRowView != null)
            {
                txtIdTicket.Text = dataRowView[0].ToString();

                if (cbbIdShow.Items.Contains(dataRowView[1].ToString()))
                {
                    cbbIdShow.SelectedItem = dataRowView[1].ToString();
                }
                else
                {
                    cbbIdShow.SelectedItem = null;
                }

                if (cbbIdCustomer.Items.Contains(dataRowView[4].ToString()))
                {
                    cbbIdCustomer.SelectedItem = dataRowView[4].ToString();
                }
                else
                {
                    cbbIdCustomer.SelectedItem = null;
                }

                if (cbbRowOfSeat.Items.Contains(dataRowView[2].ToString()))
                {
                    cbbRowOfSeat.SelectedItem = dataRowView[2].ToString();
                }
                else
                {
                    cbbRowOfSeat.SelectedItem = null;
                }

                if (cbbNumberOFSeat.Items.Contains(dataRowView[3].ToString()))
                {
                    cbbNumberOFSeat.SelectedItem = dataRowView[3].ToString();
                }
                else
                {
                    cbbNumberOFSeat.SelectedItem = null;
                }

                txtIdTicket.IsEnabled = false;
            }
            else
            {
                txtIdTicket.Text = "";
                cbbIdShow.SelectedItem = null;
                cbbIdCustomer.SelectedItem = null;
                cbbNumberOFSeat.SelectedItem = null;
                cbbRowOfSeat.SelectedItem = null;
                txtIdTicket.IsEnabled = true;
            }
        }

        private void SearchTiket_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdTicket.IsEnabled == false)
            {
                txtIdTicket.Text = "";
                txtIdTicket.Focus();
                cbbIdShow.SelectedItem = null;
                cbbIdCustomer.SelectedItem = null;
                cbbNumberOFSeat.SelectedItem = null;
                cbbRowOfSeat.Text = "";
                txtIdTicket.IsEnabled = true;
            }
            else
            {
                String sql = "select * from Ve where MaVe is not NULL";
                if (txtIdTicket.Text != "")
                {
                    sql += " and MaVe = '" + txtIdTicket.Text + "'";
                }
                if (cbbIdShow.SelectedItem != null)
                {
                    sql += " and MaShow = '" + cbbIdShow.SelectedItem + "'";
                }
                if (cbbNumberOFSeat.SelectedItem != null)
                {
                    sql += " and SoGhe = '" + cbbNumberOFSeat.SelectedItem + "'";
                }
                if (cbbRowOfSeat.SelectedItem != null)
                {
                    sql += " and HangGhe = '" + cbbRowOfSeat.SelectedItem + "'";
                }
                if (cbbIdCustomer.SelectedItem != null)
                {
                    sql += " and MaKhach = '" + cbbIdCustomer.SelectedItem + "'";
                }
                DataTable dt = SQLite.ReadData(sql);
                dtgTicket.ItemsSource = dt.AsDataView();
                dtgTicket.Columns[0].Header = "Mã vé";
                dtgTicket.Columns[1].Header = "Mã Show";
                dtgTicket.Columns[2].Header = "Hàng ghế";
                dtgTicket.Columns[3].Header = "Số ghế";
                dtgTicket.Columns[4].Header = "Mã khách";
                txtIdTicket.Text = "";
                txtIdTicket.Focus();
                cbbIdCustomer.SelectedItem = null;
                cbbIdShow.SelectedItem = null;
                cbbNumberOFSeat.SelectedItem = null;
                cbbRowOfSeat.SelectedItem = null;
                MessageBox.Show("Tìm kiếm thành công");

            }
        }

        private void ShowData_Click(object sender, RoutedEventArgs e)
        {
            LoadDataTicket();
            LoadIdKhach();
            LoadIdShow();
        }
    }
}
