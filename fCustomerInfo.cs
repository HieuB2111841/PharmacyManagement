using QLNhaThuoc.Models;
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
    public partial class fCustomerInfo : Form
    {
        private User _customer;
        private bool _isEditMode = false;

        public fCustomerInfo()
        {
            InitializeComponent();
        }

        public void ToAddFrom()
        {
            gbCustomerInfo.Text = "Nhập thông tin khách hàng";
            lCustomerID.Text = "Mã khách hàng (Được tạo tự động)";
            pCustomerPassword.Visible = true;

            _isEditMode = false;
        }

        public void ToEditFrom(string customerID)
        {
            _customer = new User(customerID);

            txtCustomerID.Text = _customer.ID;
            txtCustomerName.Text = _customer.Name;
            dtpCustomerBirthday.Value = _customer.Birthday;
            txtCustomerPhoneNumber.Text = _customer.PhoneNumber;
            rtxtCustomerAddress.Text = _customer.Address;
            pCustomerPassword.Visible = false;
            this.Size = new Size(this.Size.Width, 443);

            _isEditMode = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (IsEdited())
            {
                if (MessageBox.Show("Bạn có muốn hủy thay đổi không?", "Hủy thay đổi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidate())
            {
                // Edit Mode
                if (_isEditMode)
                {
                    this.EditCustomer();
                }
                // Add Mode
                else
                {
                    this.AddCustomer();
                }
            }
        }


        private void EditCustomer()
        {
            DataTable data = MyPublics.Instance.CallProcedure("EditUser",
                    out string message,
                    ("@p_MaUser", txtCustomerID.Text),
                    ("@p_SoDienThoai", txtCustomerPhoneNumber.Text),
                    ("@p_TenUser", txtCustomerName.Text),
                    ("@p_DiaChi", rtxtCustomerAddress.Text),
                    ("@p_NgaySinh", dtpCustomerBirthday.Value.ToString("yyyy-MM-dd")));

            if (message.Equals("Success"))
            {
                MessageBox.Show("Sửa thông tin người dùng thành công", "Thông báo", MessageBoxButtons.OK);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(message, "Lỗi trong quá trình sửa thông tin người dùng", MessageBoxButtons.OK);
            }
        }

        private void AddCustomer()
        {
            DataTable data = MyPublics.Instance.CallProcedure("AddUser",
                    out string message,
                    ("@p_SoDienThoai", txtCustomerPhoneNumber.Text),
                    ("@p_Pwd", txtCustomerPassword.Text),
                    ("@p_TenUser", txtCustomerName.Text),
                    ("@p_DiaChi", rtxtCustomerAddress.Text),
                    ("@p_NgaySinh", dtpCustomerBirthday.Value.ToString("yyyy-MM-dd")));

            if (message.Equals("Success"))
            {
                MessageBox.Show("Tạo tài khoản người dùng thành công", "Thông báo", MessageBoxButtons.OK);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(message, "Lỗi trong quá trình tạo tài khoản", MessageBoxButtons.OK);
            }
        }


        private bool IsValidate()
        {
            // User name
            if (string.IsNullOrEmpty(txtCustomerName.Text))
            {
                MessageBox.Show("Tên khách hàng không được trống", "Lỗi", MessageBoxButtons.OK);
                return false;
            }

            // User Phone Number
            if (txtCustomerPhoneNumber.Text.Length != 10)
            {
                MessageBox.Show("Số điện thoại chứa 10 ký tự", "Lỗi", MessageBoxButtons.OK);
                return false;
            }

            // User Address
            if (rtxtCustomerAddress.Text.Length < 10)
            {
                MessageBox.Show("Địa chỉ chứa ít nhất 10 ký tự", "Lỗi", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private bool IsEdited()
        {
            if (_isEditMode)
            {
                if (txtCustomerID.Text != _customer.ID) return true;
                if (txtCustomerName.Text != _customer.Name) return true;
                if (dtpCustomerBirthday.Value != _customer.Birthday) return true;
                if (txtCustomerPhoneNumber.Text != _customer.PhoneNumber) return true;
                if (rtxtCustomerAddress.Text != _customer.Address) return true;

                return false;
            }
            else
            {
                if (txtCustomerID.Text != string.Empty) return true;
                if (txtCustomerName.Text != string.Empty) return true;
                if (dtpCustomerBirthday.Value != new DateTime(2000, 1, 1)) return true;
                if (txtCustomerPhoneNumber.Text != string.Empty) return true;
                if (rtxtCustomerAddress.Text != string.Empty) return true;

                return false;
            }
        }
    }
}
