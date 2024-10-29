using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhaThuoc
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void fLogin_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true; // vẫn giữ nút thu nhỏ nếu cần
            txtAccount.Focus();
            if(MyPublics.Instance.ConnectDatabase(out string error))
            {
                
            }
            else
            {
                MessageBox.Show(error, "Error");
            }
        }

        private bool ValidateLogin()
        {
            if (string.IsNullOrEmpty(txtAccount.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccount.Focus();
                return false;
            }
            if (!txtAccount.Text.All(char.IsDigit) || txtAccount.Text.Length != 10)
            {
                MessageBox.Show("Số điện thoại phải là chuỗi 10 chữ số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccount.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }
            return true;
        }

        private void ResetForm()
        {
            txtAccount.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string account = txtAccount.Text;
            string password = txtPassword.Text;

            if (!ValidateLogin()) return;

            // Gọi hàm checking_login từ MySQL
            bool isValid = MyPublics.Instance.CallFunction<bool>("checking_login",
                                                                  ("@p_SoDienThoai", account),
                                                                  ("@p_Pwd", password));
            if (isValid)
            {
                // Đăng nhập thành công, chuyển đến form chính
                fMain fMain = new fMain();
                this.Hide();
                fMain.ShowDialog();
                this.ResetForm();
                this.Show();
            }
            else
            {
                // Đăng nhập thất bại, hiển thị thông báo
                MessageBox.Show("Số điện thoại hoặc mật khẩu không đúng.", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            fSignUp fSignUp = new fSignUp();
            fSignUp.ShowDialog();
            this.Show();
        }

    }
}
