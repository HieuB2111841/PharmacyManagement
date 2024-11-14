using QLNhaThuoc.Utils;
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
    public partial class fCustomer : Form
    {
        public fCustomer()
        {
            InitializeComponent();
            this.SetComponents();
        }
        private void SetComponents()
        {
            Font headerFont = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(163)));
            this.dgvHistory.ColumnHeadersDefaultCellStyle.Font = headerFont;
            this.dgvInfoBillDetails.ColumnHeadersDefaultCellStyle.Font = headerFont;

            Font cellFont = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(163)));
            this.dgvHistory.DefaultCellStyle.Font = cellFont;
            this.dgvInfoBillDetails.DefaultCellStyle.Font = cellFont;
        }

        #region From 

        private void fCustomer_Load(object sender, EventArgs e)
        {
            dtpBillPurchaseToSearch.Value = DateTime.Now;
        }

        private void fCustomer_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không?", "Thoát ứng dụng", MessageBoxButtons.OKCancel);
            if (result != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region Menu Strip
        private void tsmiFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiChangePassword_Click(object sender, EventArgs e)
        {
            fChangePassword fChangePassword = new fChangePassword();
            fChangePassword.ShowDialog();
        }

        private void tsmiLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Tab Pag
        private void tcMain_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            Graphics g = e.Graphics;
            Brush textBrush = new SolidBrush(Color.Black);

            // Lấy tab hiện tại
            TabPage tabPage = tabControl.TabPages[e.Index];
            Rectangle tabBounds = tabControl.GetTabRect(e.Index);

            // Thiết lập màu nền
            if (e.State == DrawItemState.Selected)
            {
                // Nền xanh lá nhạt
                g.FillRectangle(Brushes.Aquamarine, e.Bounds);
            }

            // Lấy font của TabControl
            Font tabFont = new Font("Segoe UI", 12.0f, FontStyle.Bold, GraphicsUnit.Pixel);

            // Đặt chữ cho tab
            StringFormat stringFlags = new StringFormat();
            stringFlags.Alignment = StringAlignment.Center;
            stringFlags.LineAlignment = StringAlignment.Center;

            // Vẽ chữ trong tab
            g.DrawString(tabPage.Text, tabFont, textBrush, tabBounds, new StringFormat(stringFlags));
        }
        #endregion

        #region Home Tab
        private void tabHome_Enter(object sender, EventArgs e)
        {
            lHomeHello.Text = $"Xin chào {MyUser.Instance.Name}";
        }
        #endregion

        #region Profiles tab
        private void tabProfiles_Enter(object sender, EventArgs e)
        {
            txtProfileName.Text = MyUser.Instance.Name;
            txtProfilePhoneNumber.Text = MyUser.Instance.PhoneNumber;
            rtxtProfileAddress.Text = MyUser.Instance.Address;

            if(MyUser.Instance.Birthday != DateTime.MinValue)
            {
                dtpProfileBirthday.Value = MyUser.Instance.Birthday;
            }
        }

        private void tabProfiles_Leave(object sender, EventArgs e)
        {
            txtProfileName.ReadOnly = true;
            dtpProfileBirthday.Enabled = false;
            rtxtProfileAddress.ReadOnly = true;
            txtProfilePhoneNumber.Enabled = false;
            btnProfileEdit.Visible = true;
            btnProfileSave.Visible = false;
            btnProfileCancel.Visible = false;
        }

        private void btnProfileEdit_Click(object sender, EventArgs e)
        {
            txtProfileName.ReadOnly = false;
            dtpProfileBirthday.Enabled = true;
            rtxtProfileAddress.ReadOnly = false;
            txtProfilePhoneNumber.Enabled = true;
            btnProfileEdit.Visible = false;
            btnProfileSave.Visible = true;
            btnProfileCancel.Visible = true;
        }

        private bool ValidateUserProfile()
        {
            if (string.IsNullOrWhiteSpace(txtProfileName.Text))
            {
                MessageBox.Show("Tên người dùng không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProfileName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtProfilePhoneNumber.Text) || txtProfilePhoneNumber.Text.Length != 10)
            {
                MessageBox.Show("Số điện thoại phải có 10 chữ số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProfilePhoneNumber.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(rtxtProfileAddress.Text))
            {
                MessageBox.Show("Địa chỉ không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                rtxtProfileAddress.Focus();
                return false;
            }

            return true;
        }

        private void btnProfileSave_Click(object sender, EventArgs e)
        {
            // Kiểm tra tính hợp lệ của dữ liệu nhập vào
            if (!ValidateUserProfile()) return;

            string userId = MyUser.Instance.ID.Trim();
            string phoneNumber = txtProfilePhoneNumber.Text.Trim();
            string userName = txtProfileName.Text.Trim();
            string address = rtxtProfileAddress.Text.Trim();
            string birthday = dtpProfileBirthday.Value.ToString("yyyy-MM-dd");

            // Gọi stored procedure để cập nhật thông tin người dùng
            string message;
            DataTable dataTable = MyPublics.Instance.CallProcedure("EditUser",
                out message,
                ("@p_MaUser", userId),
                ("@p_SoDienThoai", phoneNumber),
                ("@p_TenUser", userName),
                ("@p_DiaChi", address),
                ("@p_NgaySinh", birthday)
            );

            // Kiểm tra kết quả trả về và hiển thị thông báo
            if (message == MyPublics.SUCCESS_MESSAGE)
            {
                MessageBox.Show("Cập nhật thông tin thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Đặt lại các controls thành chế độ chỉ đọc
                txtProfileName.ReadOnly = true;
                dtpProfileBirthday.Enabled = false;
                rtxtProfileAddress.ReadOnly = true;
                txtProfilePhoneNumber.Enabled = false;

                // Ẩn nút Lưu và Hủy, hiển thị nút Chỉnh sửa
                btnProfileSave.Visible = false;
                btnProfileCancel.Visible = false;
                btnProfileEdit.Visible = true;
            }
            else
            {
                MessageBox.Show("Cập nhật không thành công: " + message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProfileCancel_Click(object sender, EventArgs e)
        {
            txtProfileName.ReadOnly = true;
            txtProfileName.Text = MyUser.Instance.Name;
            dtpProfileBirthday.Enabled = false;

            // Kiểm tra nếu Birthday là kiểu DateTime trước khi gán giá trị
            if (MyUser.Instance.Birthday != DateTime.MinValue)
                dtpProfileBirthday.Value = MyUser.Instance.Birthday;
            else
                dtpProfileBirthday.Value = new DateTime(2000, 1, 1);

            rtxtProfileAddress.ReadOnly = true;
            rtxtProfileAddress.Text = MyUser.Instance.Address;
            txtProfilePhoneNumber.Enabled = false;
            txtProfilePhoneNumber.Text = MyUser.Instance.PhoneNumber;

            btnProfileEdit.Visible = true;
            btnProfileSave.Visible = false;
            btnProfileCancel.Visible = false;
        }
        #endregion

        #region History Tab
        private void tabHistory_Enter(object sender, EventArgs e)
        {
            if (dgvHistory.DataSource != null) return;
            this.dgvHistory_LoadData();
        }

        private void tabHistory_ResetSideBar()
        {
            txtInfoBillID.Text = string.Empty;
            dtpInfoBillDate.Value = new DateTime(2000, 1, 1);
            txtInfoBillEmployeeName.Text = string.Empty;

            dgvInfoBillDetails.DataSource = null;
            
            txtInfoBillTotalPrice.Text = string.Empty; 
        }

        private void dgvHistory_LoadData()
        {
            DataTable historyData = MyPublics.Instance.CallProcedure("usp_hienThiLichSuMuaThuoc",
                out string message,
                ("@in_maKhachHang", MyUser.Instance.ID));

            dgvHistory.DataSource = historyData;
            if (historyData.Rows.Count <= 0)
            {
                this.tabHistory_ResetSideBar();
                if (message != MyPublics.SUCCESS_MESSAGE)
                {
                    MessageBox.Show($"Lỗi {message}", "Lỗi", MessageBoxButtons.OK);
                }
            }
        }

        private void dgvHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // cột tổng tiền
            if(e.ColumnIndex == 3)
            {
                e.Value = StringUtils.FormatMoneyNumber(e.Value.ToString());
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void dgvHistory_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem hàng và cột có hợp lệ không
            if (e.RowIndex >= 0)
            {
                // Lấy DataGridViewRow của hàng được chọn
                DataGridViewRow row = dgvHistory.Rows[e.RowIndex];

                txtInfoBillID.Text = row.Cells[0].Value.ToString();
                dtpInfoBillDate.Text = row.Cells[1].Value.ToString();
                txtInfoBillEmployeeName.Text = row.Cells[2].Value.ToString();

                DataTable billDetailsTable = MyPublics.Instance.CallProcedure("usp_hienThiChiTietHoaDon", ("@in_maHoaDon", txtInfoBillID.Text));
                
                dgvInfoBillDetails.DataSource = billDetailsTable;
                if(billDetailsTable.Rows.Count > 0)
                {
                    dgvInfoBillDetails.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgvInfoBillDetails.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                string totalPrice = row.Cells[3].Value.ToString();
                txtInfoBillTotalPrice.Text = StringUtils.FormatMoneyNumber(totalPrice);
            }
        }

        private void dgvInfoBillDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // cột đơn giá
            if(e.ColumnIndex == 2)
            {
                e.Value = StringUtils.FormatMoneyNumber(e.Value.ToString());
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void btnBillSearch_Click(object sender, EventArgs e)
        {
            string id = MyUser.Instance.ID;
            string billID = txtInfoBillID.Text.Trim();
            string fromDate = dtpBillPurchaseFromSearch.Value.ToString("yyyy-MM-dd");
            string toDate = dtpBillPurchaseToSearch.Value.ToString("yyyy-MM-dd");

            DataTable historyData = MyPublics.Instance.CallProcedure("Tim_Hoa_Don_Cua_Khanh_Hang",
                out string message,
                ("@ma_khach_hang", id),
                ("@ma_phieu_xuat", billID),
                ("@fromDate", fromDate),
                ("@toDate", toDate));

            dgvHistory.DataSource = historyData;
            if (historyData.Rows.Count <= 0)
            {
                this.tabHistory_ResetSideBar();
                if (message != MyPublics.SUCCESS_MESSAGE)
                {
                    MessageBox.Show($"Lỗi {message}", "Lỗi", MessageBoxButtons.OK);
                }
            }
        }

        private void btnBillResetSearch_Click(object sender, EventArgs e)
        {
            txtBillIDSearch.Text = string.Empty;
            dtpBillPurchaseFromSearch.Value = new DateTime(2000, 1, 1);
            dtpBillPurchaseToSearch.Value = DateTime.Now;

            this.dgvHistory_LoadData();
        }
        #endregion

    }
}
