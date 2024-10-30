using System;
using System.Windows.Forms;

namespace QLNhaThuoc
{
    public partial class fEmployeeInfo : Form
    {
        public fEmployeeInfo()
        {
            InitializeComponent();
        }

        private void fEmployeeInfo_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true; // vẫn giữ nút thu nhỏ nếu cần

            this.fEmployeeInfo_LoadData();   
        }

        private void fEmployeeInfo_LoadData()
        {
            MyUser employeeInfo = MyUser.Instance;
            if (!employeeInfo.IsEmployee) return;

            txtEmployeeID.Text = employeeInfo.ID;
            txtEmployeeName.Text = employeeInfo.Name;
            txtEmployeePhoneNumber.Text = employeeInfo.PhoneNumber;
            dtpEmployeeBirthday.Value = employeeInfo.Birthday;
            rtxtEmployeeAddress.Text = employeeInfo.Address;
        }
    }
}
