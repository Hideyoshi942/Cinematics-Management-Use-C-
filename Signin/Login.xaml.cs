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
using System.Windows.Shapes;
using WPFModernVerticalMenu.AdminCinema;
using WPFModernVerticalMenu.User;

namespace WPFModernVerticalMenu.Signin
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        DataProcessing.DataProcessing SQLite = new DataProcessing.DataProcessing();

        public Login()
        {
            InitializeComponent();
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
            {
                textEmail.Visibility = Visibility.Collapsed;
            } else
            {
                textEmail.Visibility = Visibility.Visible;
            }
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            if (email.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập email!");
                return;
            }
            if (password.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!");
                return;
            }

            string sql = "SELECT * FROM Khach WHERE Email = '" + email + "' AND Password = '" + password + "'";
            DataTable userData = SQLite.ReadData(sql);

            if (userData.Rows.Count > 0)
            {
                string loai = userData.Rows[0]["Loai"].ToString();
                string idkhach = userData.Rows[0]["MaKhach"].ToString();
                

                if (loai == "False")
                {
                    MessageBox.Show("Đăng nhập thành công với tài khoản người dùng");
                    UserCinema newUserCinema = new UserCinema(idkhach);
                    newUserCinema.Show();
                }
                else
                {
                    MessageBox.Show("Đăng nhập thành công với tài khoản quản trị viên");
                    Admin newAdminCinema = new Admin();
                    newAdminCinema.Show();
                }
                Close();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!");
                return;
            }

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Signup newSignUp = new Signup();
            newSignUp.Show();
            Close();
        }
    }
}
