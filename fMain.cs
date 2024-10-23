using Google.Protobuf.WellKnownTypes;
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
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        #region Tab Page
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

        #region Medicines Tab
        private void tabMedicines_Enter(object sender, EventArgs e)
        {
            if(dgvMedicines.DataSource != null) return;

            DataTable medicinesTable = MyPublics.Instance.CallProcedure("usp_hienThiDanhSachThuoc",
                ("@in_count", "20"),
                ("@in_offset", "0"));

            if (medicinesTable.Rows.Count > 0)
            {
                dgvMedicines.DataSource = medicinesTable;
                dgvMedicines.Columns[5].Visible = false; // Ẩn cột công dụng
            }
            else
            {
                MessageBox.Show("no data");
            }
        }

        private void dgvMedicines_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem hàng và cột có hợp lệ không
            if (e.RowIndex >= 0)
            {
                // Lấy DataGridViewRow của hàng được chọn
                DataGridViewRow row = dgvMedicines.Rows[e.RowIndex];

                txtMedicineID.Text = row.Cells[0].Value.ToString();
                txtMedicineName.Text = row.Cells[1].Value.ToString();
                txtMedicineType.Text = row.Cells[2].Value.ToString();
                txtMedicineManufacturer.Text = row.Cells[3].Value.ToString();
                txtMedicineSupplier.Text = row.Cells[4].Value.ToString();
                rtxtMedicineEffect.Text = row.Cells[5].Value.ToString();

            }
        }
        #endregion

        #region Customers Tab
        private void tabCustomers_Enter(object sender, EventArgs e)
        {
            if (dgvCustomers.DataSource != null) return;

            DataTable customerTable = 
                MyPublics.Instance.CallProcedure("usp_hienThiDanhSachKhachHang", 
                    ("@in_count", "20"),
                    ("@in_offset", "0"));

            if (customerTable.Rows.Count > 0)
            {
                dgvCustomers.DataSource = customerTable;
                dgvCustomers.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvCustomers.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            else
            {
                MessageBox.Show("no data");
            }
        }

        private void dgvCustomers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Format cột ngày sinh
            if (e.ColumnIndex == 2)
            {
                if (e.Value == DBNull.Value || e.Value == null)
                {
                    e.Value = "Chưa có ngày";
                    e.FormattingApplied = true;
                }
            }
        }

        private void dgvCustomers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem hàng và cột có hợp lệ không
            if (e.RowIndex >= 0)
            {
                // Lấy DataGridViewRow của hàng được chọn
                DataGridViewRow row = dgvCustomers.Rows[e.RowIndex];

                txtCustomerID.Text = row.Cells[0].Value.ToString();
                txtCustomerName.Text = row.Cells[1].Value.ToString();
                txtCustomerPhoneNumber.Text = row.Cells[3].Value.ToString();
                rtxtCustomerAddress.Text = row.Cells[4].Value.ToString();

                string birthday = row.Cells[2].Value.ToString();
                if (birthday == string.Empty)
                {
                    dtpCustomerBirthday.Value = new DateTime(2000, 1, 1);
                }
                else
                {
                    dtpCustomerBirthday.Text = row.Cells[2].Value.ToString();
                    dtpCustomerBirthday.CustomFormat = "dd/MM/yyyy";
                }

                DataTable historyTable = MyPublics.Instance.CallProcedure("usp_lichSuMuaCuaKhachHang", ("@in_MaKhachHang", txtCustomerID.Text));
                dgvCustomerHistoryPurchases.DataSource = historyTable;
            }
        }
        #endregion


    }
}
