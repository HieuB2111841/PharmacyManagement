using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace QLNhaThuoc
{
    public partial class fChangePassword : Form
    {
        public fChangePassword()
        {
            InitializeComponent();
        }

        private void fChangePassword_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            txtAccount.Text = MyUser.Instance.PhoneNumber;
            txtAccount.Enabled = false;
            txtOldPassword.Focus();
            if (MyPublics.Instance.ConnectDatabase(out string error))
            {

            }
            else
            {
                MessageBox.Show(error, "Error");
            }
        }

        private bool ValidateChangePassword()
        {
            if (string.IsNullOrEmpty(txtOldPassword.Text) || txtOldPassword.Text.Length < 5)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu cũ hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOldPassword.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNewPassword.Text) || txtNewPassword.Text.Length < 5)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới và phải có ít nhất 5 ký tự.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtConfirmPassword.Text) || txtConfirmPassword.Text.Length < 5 || txtConfirmPassword.Text != txtNewPassword.Text)
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu mới hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return false;
            }
            return true;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!ValidateChangePassword()) return;

            string maUser = MyUser.Instance.ID; 
            string oldPassword = txtOldPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();

            bool result = MyPublics.Instance.CallFunction<bool>("DoiMatKhau",
                ("@MaUser", maUser),
                ("@old_pwd", oldPassword),
                ("@new_pwd", newPassword));

            if (result)
            {
                MessageBox.Show("Đổi mật khẩu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Mật khẩu cũ không đúng. Vui lòng thử lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
