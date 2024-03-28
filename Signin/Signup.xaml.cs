using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFModernVerticalMenu.DataProcessing;

namespace WPFModernVerticalMenu.Signin
{
    /// <summary>
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : Window
    {
        DataProcessing.DataProcessing SQLite = new DataProcessing.DataProcessing();

        public Signup()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void textUser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtUser.Focus();
        }

        private void txtUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUser.Text) && txtUser.Text.Length > 0)
            {
                textUser.Visibility = Visibility.Collapsed;
            }
            else
            {
                textUser.Visibility = Visibility.Visible;
            }
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
            }
            else
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

        private void textConfimPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtConfimPassword.Focus();

        }

        private void txtConfimPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0)
            {
                textConfimPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textConfimPassword.Visibility = Visibility.Visible;
            }
        }

        private void PackIcon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login newLogin = new Login();
            newLogin.Show();
            Close();
        }

        public bool IsValidEmail(string email)
        {
            string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        public bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(password);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string userName = txtUser.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Password;
            string confirmPassword = txtConfimPassword.Password;
            if (userName.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên người dùng!");
                return;
            }

            if (email.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập email!");
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Định dạng email phải có @gmail.com");
                return;
            }

            if (password.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!");
                return;
            }

            if (!IsValidPassword(password))
            {
                MessageBox.Show("Mật khẩu phải có độ dài 8 ký tự bao gồm chữ in hoa, chữ thường, số, ký tự đặc biệt.");
                return;
            }

            if (confirmPassword.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập xác nhận mật khẩu!");
                return;
            }

            if (confirmPassword != password)
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu chính xác!");
                return;
            }

            string sql = "Select * from Khach where Email '" + email + "'";
            if (sql.Count() == 0)
            {
                MessageBox.Show("Email này đã được đăng ký, vui lòng dùng email khác");
            } else
            {
                Random random = new Random();
                int  randomNumber = random.Next(1, 1000);
                string loai = "False";
                string sqlInsert = "Insert into Khach (MaKhach, TenKhach, Email, Password, ConfirmPassword, Loai) values ('" + randomNumber.ToString() + "', '"  + userName.Trim() + "', '" + email.Trim() + "', '" + password.Trim() + "', '" + confirmPassword.Trim() + "', '" +  loai + "')";
                SQLite.ChangeData(sqlInsert);
                txtUser.Text = "";
                txtEmail.Text = "";
                txtPassword.Password = "";
                txtConfimPassword.Password = "";
                MessageBox.Show("Đăng ký thành công");
            }
        }
    }
}
