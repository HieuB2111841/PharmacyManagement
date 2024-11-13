using MySql.Data.MySqlClient;
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
    public partial class fSignUp : Form
    {
        public fSignUp()
        {
            InitializeComponent();
        }

        private void fSignUp_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true; // vẫn giữ nút thu nhỏ nếu cần
            ResetForm();
            txtName.Focus();
            if (MyPublics.Instance.ConnectDatabase(out string error))
            {

            }
            else
            {
                MessageBox.Show(error, "Error");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void ResetForm()
        {
            txtName.Text = string.Empty;
            dtpBirthday.Value = new DateTime(2000,1,1);
            txtAddress.Text = string.Empty;
            txtAccount.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
        }

        private bool ValidateSignUp()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAddress.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtAccount.Text) || !txtAccount.Text.All(char.IsDigit) || txtAccount.Text.Length != 10)
            {
                MessageBox.Show("Vui lòng nhập số điện thoại hợp lệ (10 chữ số).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccount.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPassword.Text) || txtPassword.Text.Length < 5)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu và phải có ít nhất 5 ký tự.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtConfirmPassword.Text) || txtConfirmPassword.Text.Length < 5 || txtConfirmPassword.Text != txtPassword.Text)
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return false;
            }
            return true;
        }


        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (!ValidateSignUp()) return;

            // Lấy dữ liệu từ các ô nhập liệu
            string phone = txtAccount.Text.Trim();
            string password = txtPassword.Text.Trim();
            string name = txtName.Text.Trim();
            string address = txtAddress.Text.Trim();
            DateTime birthDate = dtpBirthday.Value;

            // Gọi Stored Procedure `sign_up_customer`
            bool result = MyPublics.Instance.CallFunction<bool>("sign_up",
                                                                out string message,
                                                                ("@p_SoDienThoai", phone),
                                                                ("@p_Pwd", password),
                                                                ("@p_TenUser", name),
                                                                ("@p_DiaChi", address),
                                                                ("@p_NgaySinh", birthDate.ToString("yyyy-MM-dd")));

            // Kiểm tra xem đăng ký có thành công không
            if(message == MyPublics.SUCCESS_MESSAGE)
            {
                if (result)
                {
                    MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetForm();
                }
            }
            else
            {
                if (message.Contains("Duplicate"))
                {
                    MessageBox.Show("Số điện thoại đã được sử dụng. Xin vui lòng dùng số điện thoại khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
