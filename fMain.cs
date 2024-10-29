using Google.Protobuf.WellKnownTypes;
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
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        #region From
        private void fMain_Load(object sender, EventArgs e)
        {
            dtpCustomerPurchaseToSearch.Value = DateTime.Today;
            dtpImportDateToSearch.Value = DateTime.Today;
            dtpBillDateToSearch.Value = DateTime.Today;
        }

        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
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
        #endregion

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
            if (dgvMedicines.DataSource == null)
                this.dgvMedicines_LoadData();
        }

        private void dgvMedicines_LoadData()
        {
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

        private void btnMedicineSearch_Click(object sender, EventArgs e)
        {
            string name = txtMedicineNameSearch.Text;
            string type = txtMedicineTypeSearch.Text;
            string supplier = txtMedicineSupplierSearch.Text;
            string manufaturer = txtMedicineManufacturerSearch.Text;


            DataTable medicinesTable = MyPublics.Instance.CallProcedure("Tim_Thuoc",
                ("@ma_thuoc", ""),
                ("@ten_thuoc", name),
                ("@ten_loai", type),
                ("@ten_ncc", supplier),
                ("@ten_hangsx", manufaturer));

            if (medicinesTable.Rows.Count > 0)
            {
                dgvMedicines.DataSource = medicinesTable;
                dgvMedicines.Columns[5].Visible = false; // Ẩn cột công dụng
            }
            
        }
        private void btnMedicineResetSearch_Click(object sender, EventArgs e)
        {
            txtMedicineNameSearch.Text = string.Empty;
            txtMedicineTypeSearch.Text = string.Empty;
            txtMedicineSupplierSearch.Text = string.Empty;
            txtMedicineManufacturerSearch.Text = string.Empty;

            this.dgvMedicines_LoadData();
        }
        #endregion

        #region Customers Tab
        private void tabCustomers_Enter(object sender, EventArgs e)
        {
            if (dgvCustomers.DataSource == null)
                this.dgvCustomer_LoadData();
            dtpCustomerPurchaseToSearch.Value = DateTime.Today;
        }

        private void dgvCustomer_LoadData()
        {
            DataTable customerTable =
                MyPublics.Instance.CallProcedure("usp_hienThiDanhSachKhachHang",
                    ("@in_count", "20"),
                    ("@in_offset", "0"));

            if(customerTable.Rows.Count > 0)
            {
                dgvCustomers.DataSource = customerTable;

                // Cột Họ Tên
                dgvCustomers.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                // Cột địa chỉ
                dgvCustomers.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
                }

                DataTable historyTable = MyPublics.Instance.CallProcedure("usp_lichSuMuaCuaKhachHang", ("@in_MaKhachHang", txtCustomerID.Text));
                dgvCustomerHistoryPurchases.DataSource = historyTable;
            }
        }

        private void dgvCustomerHistoryPurchases_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Format cột tổng tiền
            if (e.ColumnIndex == 2)
            {
                e.Value = StringUtils.FormatNumber(e.Value.ToString());
            }
        }

        private void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            string name = txtCustomerNameOrPhoneSearch.Text;
            string dateFrom = dtpCustomerPurchaseFromSearch.Value.ToString("yyyy-MM-dd");
            string dateTo = dtpCustomerPurchaseToSearch.Value.ToString("yyyy-MM-dd");

            DataTable customerTable =
                MyPublics.Instance.CallProcedure("Tim_Khach_Hang",
                    ("@searchValue", name),
                    ("@fromDate", dateFrom),
                    ("@toDate", dateTo));

            if (customerTable.Rows.Count > 0)
            {
                dgvCustomers.DataSource = customerTable;

                // Cột Họ Tên
                dgvCustomers.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                // Cột địa chỉ
                dgvCustomers.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            else
            {
                MessageBox.Show("no data");
            }
        }

        private void btnCustomerResetSearch_Click(object sender, EventArgs e)
        {
            txtCustomerNameOrPhoneSearch.Text = string.Empty;
            dtpCustomerPurchaseFromSearch.Value = new DateTime(2000, 1, 1);
            dtpCustomerPurchaseToSearch.Value = DateTime.Today;

            this.dgvCustomer_LoadData();
        }

        #endregion

        #region Imports Tab
        private void tabImports_Enter(object sender, EventArgs e)
        {
            if (dvgImports.DataSource == null)
                this.tabImports_LoadData();
            
        }

        private void tabImports_LoadData()
        {
            DataTable importTable = MyPublics.Instance.CallProcedure("usp_hienThiDanhSachPhieuNhap",
                ("@in_count", "20"),
                ("@in_offset", "0"));

            if (importTable.Rows.Count > 0)
            {
                dvgImports.DataSource = importTable;
            }
            else
            {
                MessageBox.Show("no data");
            }
        }

        private void dvgImports_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void dvgImports_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem hàng và cột có hợp lệ không
            if (e.RowIndex >= 0)
            {
                // Lấy DataGridViewRow của hàng được chọn
                DataGridViewRow row = dvgImports.Rows[e.RowIndex];

                txtImportID.Text = row.Cells[0].Value.ToString();
                txtImportEmployeeID.Text = row.Cells[1].Value.ToString();
                txtImportSupplierID.Text = row.Cells[2].Value.ToString();
                dtpImportDate.Text = row.Cells[3].Value.ToString();

                DataTable detailsTable = MyPublics.Instance.CallProcedure("usp_hienThiChiTietPhieuNhap", ("@in_MaPhieuNhap", txtImportID.Text));
                dgvImportDetails.DataSource = detailsTable;

                // Cột tên thuốc chiếm phần còn lại của view
                dgvImportDetails.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                // Không có dữ liệu trong phiếu nhập
                if (dgvImportDetails.Rows.Count <= 0)
                {
                    txtImportDetailsMedicineName.Text = string.Empty;
                    txtImportDetailsMedicineQuantity.Text = string.Empty;
                    txtImportDetailsMedicinePrice.Text = string.Empty;
                }

                decimal totalPrice = MyPublics.Instance.CallFunction<decimal>("tinhTongSoTienPhieuNhap",
                    ("@in_MaPN", txtImportID.Text));
                txtImportTotalPrice.Text = StringUtils.FormatNumber(totalPrice);
            }
        }

        private void dgvImportDetails_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvImportDetails.Rows[e.RowIndex];

            txtImportDetailsMedicineName.Text = row.Cells[1].Value.ToString();
            txtImportDetailsMedicineQuantity.Text = row.Cells[2].Value.ToString();
            txtImportDetailsMedicinePrice.Text = StringUtils.FormatNumber(row.Cells[3].Value.ToString());
        }
        private void dgvImportDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Format cột tổng tiền
            if (e.ColumnIndex == 3)
            {
                e.Value = StringUtils.FormatNumber(e.Value.ToString());
            }
        }

        private void btnImportSearch_Click(object sender, EventArgs e)
        {
            //string id = txtImportID.Text;
            //string employee = txtImportEmployeeSearch.Text;
            //string supplier = txtImportSupplierSearch.Text;
            //string date = dtpImportDateFromSearch.Value.ToString("yyyy-MM-dd");

            //DataTable importTable =
            //    MyPublics.Instance.CallProcedure("Tim_Phieu_Nhap",
            //        ("@ma_phieu_nhap", id),
            //        ("@nhan_vien", employee),
            //        ("@ncc", supplier),
            //        ("@ngay_nhap_tu", "null"),
            //        ("@ngay_nhap_den", "null"));

            //if (importTable.Rows.Count > 0)
            //{
            //    dvgImports.DataSource = importTable;
            //}
            //else
            //{
            //    MessageBox.Show("no data");
            //}
        }

        private void btnImportResetSearch_Click(object sender, EventArgs e)
        {
            txtImportID.Text = string.Empty;
            txtImportEmployeeSearch.Text = string.Empty; 
            txtImportSupplierSearch.Text = string.Empty;

            dtpImportDateFromSearch.Value = new DateTime(2000, 1, 1);
            dtpImportDateToSearch.Value = DateTime.Today;

            tabImports_LoadData();
        }

        #endregion

        #region Bills Tab
        private void tabBills_Enter(object sender, EventArgs e)
        {
            if (dgvBills.DataSource == null)
                this.dgvBills_LoadData();

        }

        private void dgvBills_LoadData()
        {
            DataTable billsTable = MyPublics.Instance.CallProcedure("usp_hienThiDanhSachPhieuXuat",
                ("@in_count", "20"),
                ("@in_offset", "0"));

            if (billsTable.Rows.Count > 0)
            {
                dgvBills.DataSource = billsTable;
                dgvBills.Columns[4].Visible = false;
            }
            else
            {
                txtBillID.Text = string.Empty;
                txtBillEmployeeID.Text = string.Empty;
                txtBillCustomerID.Text = string.Empty;
                dtpBillDate.Text = string.Empty;

                MessageBox.Show("no data");
            }
        }

        private void dgvBills_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem hàng và cột có hợp lệ không
            if (e.RowIndex >= 0)
            {
                // Lấy DataGridViewRow của hàng được chọn
                DataGridViewRow row = dgvBills.Rows[e.RowIndex];

                txtBillID.Text = row.Cells[0].Value.ToString();
                txtBillEmployeeID.Text = row.Cells[1].Value.ToString();
                txtBillCustomerID.Text = row.Cells[2].Value.ToString();
                dtpBillDate.Text = row.Cells[3].Value.ToString();

                DataTable detailsTable = MyPublics.Instance.CallProcedure("usp_hienThiChiTietPhieuXuat", ("@in_MaPhieuXuat", txtBillID.Text));
                dgvBillDetails.DataSource = detailsTable;

                // Cột tên thuốc chiếm phần còn lại của view
                dgvBillDetails.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                // Không có dữ liệu trong hóa đơn
                if (detailsTable.Rows.Count <= 0)
                {
                    txtBillDetailsMedicineName.Text = string.Empty;
                    txtBillDetailsMedicineQuantity.Text = string.Empty;
                    txtBillDetailsMedicinePrice.Text = string.Empty;
                }


                txtBillTotalPrice.Text = StringUtils.FormatNumber(row.Cells[4].Value.ToString());
            }
            
        }

        private void dgvBillDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Format cột đơn giá
            if (e.ColumnIndex == 3)
            {
                e.Value = StringUtils.FormatNumber(e.Value.ToString());
            }
        }

        private void dgvBillDetails_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem hàng và cột có hợp lệ không
            if (e.RowIndex >= 0)
            {
                // Lấy DataGridViewRow của hàng được chọn
                DataGridViewRow row = dgvBillDetails.Rows[e.RowIndex];

                txtBillDetailsMedicineName.Text = row.Cells[1].Value.ToString();
                txtBillDetailsMedicineQuantity.Text = row.Cells[2].Value.ToString();
                txtBillDetailsMedicinePrice.Text = StringUtils.FormatNumber(row.Cells[3].Value.ToString());
            }
        }
        private void btnBillSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnBillResetSearch_Click(object sender, EventArgs e)
        {
            txtBillIDSearch.Text = string.Empty;
            txtBillCustomerIDSearch.Text = string.Empty;
            txtBillEmployeeIDSearch.Text = string.Empty;

            dtpBillDateFromSearch.Value = new DateTime(2000, 1, 1);
            dtpBillDateToSearch.Value = DateTime.Today;

            this.dgvBills_LoadData();
        }


        #endregion

    }
}
