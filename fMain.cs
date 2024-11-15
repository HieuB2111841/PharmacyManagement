using QLNhaThuoc.Utils;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLNhaThuoc
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
            this.SetComponents();
        }

        private void SetComponents()
        {
            Font headerFont = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(163)));
            this.dgvMedicines.ColumnHeadersDefaultCellStyle.Font = headerFont;
            this.dgvCustomers.ColumnHeadersDefaultCellStyle.Font = headerFont;
            this.dgvCustomerHistoryPurchases.ColumnHeadersDefaultCellStyle.Font = headerFont;
            this.dgvImports.ColumnHeadersDefaultCellStyle.Font = headerFont;
            this.dgvImportDetails.ColumnHeadersDefaultCellStyle.Font = headerFont;
            this.dgvBills.ColumnHeadersDefaultCellStyle.Font = headerFont;
            this.dgvBillDetails.ColumnHeadersDefaultCellStyle.Font = headerFont;

            Font cellFont = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(163)));
            this.dgvMedicines.DefaultCellStyle.Font = cellFont;
            this.dgvCustomers.DefaultCellStyle.Font = cellFont;
            this.dgvCustomerHistoryPurchases.DefaultCellStyle.Font = cellFont;
            this.dgvImports.DefaultCellStyle.Font = cellFont;
            this.dgvImportDetails.DefaultCellStyle.Font = cellFont;
            this.dgvBills.DefaultCellStyle.Font = cellFont;
            this.dgvBillDetails.DefaultCellStyle.Font = cellFont;
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
        private void tsmiAccountInfo_Click(object sender, EventArgs e)
        {
            fEmployeeInfo employeeInfoForm = new fEmployeeInfo();
            employeeInfoForm.ShowDialog();
        }

        private void tsmiAccountLogout_Click(object sender, EventArgs e)
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
            this.dgvMedicines_LoadData();
        }

        private void dgvMedicines_LoadData()
        {
            DataTable medicinesTable = MyPublics.Instance.CallProcedure("usp_hienThiDanhSachThuoc",
                out string message,
                ("@in_count", "200"),
                ("@in_offset", "0"));

            dgvMedicines.DataSource = medicinesTable;
            this.dgvMedicines_FormatColumn();
            if (medicinesTable.Rows.Count <= 0)
            {
                this.tabMedicines_ResetSideBar();
                if (message != MyPublics.SUCCESS_MESSAGE)
                    MessageBox.Show($"Lỗi: {message}", "Lỗi", MessageBoxButtons.OK);
            }
        }

        private void dgvMedicines_FormatColumn()
        {
            // Cột tên
            dgvMedicines.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvMedicines.Columns[3].Visible = false; // Ẩn cột nhà cung cấp
            dgvMedicines.Columns[4].Visible = false; // Ẩn cột hãng sản xuất
            dgvMedicines.Columns[5].Visible = false; // Ẩn cột công dụng
        }

        private void dgvMedicines_Scroll(object sender, ScrollEventArgs e)
        {

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
                txtMedicineStoredQuantity.Text = row.Cells[6].Value.ToString();
            }
        }

        private void tabMedicines_ResetSideBar()
        {
            txtMedicineID.Text = string.Empty;
            txtMedicineName.Text = string.Empty;
            txtMedicineType.Text = string.Empty;
            txtMedicineManufacturer.Text = string.Empty;
            txtMedicineSupplier.Text = string.Empty;
            rtxtMedicineEffect.Text = string.Empty;
            txtMedicineStoredQuantity.Text = string.Empty;
        }

        private void btnMedicineSearch_Click(object sender, EventArgs e)
        {
            string name = txtMedicineNameSearch.Text.Trim();
            string type = txtMedicineTypeSearch.Text.Trim();
            string supplier = txtMedicineSupplierSearch.Text.Trim();
            string manufaturer = txtMedicineManufacturerSearch.Text.Trim();


            DataTable medicinesTable = MyPublics.Instance.CallProcedure("Tim_Thuoc",
                ("@ma_thuoc", ""),
                ("@ten_thuoc", name),
                ("@ten_loai", type),
                ("@ten_ncc", supplier),
                ("@ten_hangsx", manufaturer));


            dgvMedicines.DataSource = medicinesTable;
            this.dgvMedicines_FormatColumn();

            if(medicinesTable.Rows.Count <= 0)
            {
                this.tabMedicines_ResetSideBar();
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

        private void btnMedicineAdd_Click(object sender, EventArgs e)
        {
            fMedicineInfo medicineForm = new fMedicineInfo();
            medicineForm.ToAddFrom();
            DialogResult res = medicineForm.ShowDialog();
            if(res == DialogResult.OK)
            {
                this.dgvMedicines_LoadData();
            }

        }
        private void btnMedicineEdit_Click(object sender, EventArgs e)
        {
            fMedicineInfo medicineForm = new fMedicineInfo();
            medicineForm.ToEditFrom(txtMedicineID.Text);
            DialogResult res = medicineForm.ShowDialog();
            if (res == DialogResult.OK)
            {
                this.dgvMedicines_LoadData();
            }
        }

        private void btnMedicineDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show($"Bạn có muốn xóa thuốc {txtMedicineID.Text} không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataTable data = MyPublics.Instance.CallProcedure("DeleteThuoc",
                    out string message,
                    ("@p_MaThuoc", txtMedicineID.Text));

                if(message == MyPublics.SUCCESS_MESSAGE)
                {
                    MessageBox.Show($"Xóa thuốc '{txtMedicineID.Text}' thành công", "Thông báo", MessageBoxButtons.OK);
                    this.dgvMedicines_LoadData();
                }
                else
                {
                    if(message.Contains("foreign key constraint fails"))
                    {
                        MessageBox.Show($"Không thể xóa thuốc", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi khi xóa thuốc: {message}", "Lỗi", MessageBoxButtons.OK);
                    }
                }
            }
        }

        #endregion

        #region Customers Tab
        private void tabCustomers_Enter(object sender, EventArgs e)
        {
            this.dgvCustomer_LoadData();
            dtpCustomerPurchaseToSearch.Value = DateTime.Today;
        }

        private void dgvCustomer_LoadData()
        {
            DataTable customerTable =
                MyPublics.Instance.CallProcedure("usp_hienThiDanhSachKhachHang",
                    out string message,
                    ("@in_count", "200"),
                    ("@in_offset", "0"));

            dgvCustomers.DataSource = customerTable;
            this.dgvCustomers_Formatting();
            if (customerTable.Rows.Count <= 0)
            {
                this.tabCustomers_ResetSideBar();
                if (message != MyPublics.SUCCESS_MESSAGE)
                    MessageBox.Show($"Lỗi: {message}", "Lỗi", MessageBoxButtons.OK);
            }
        }

        private void dgvCustomers_Formatting()
        {
            // Cột Họ Tên
            dgvCustomers.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Cột địa chỉ
            dgvCustomers.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dgvCustomers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Format cột ngày sinh
            if (e.ColumnIndex == 2)
            {
                if (e.Value == DBNull.Value || e.Value == null)
                {
                    e.Value = "undefine";
                    e.FormattingApplied = true;
                }
                else
                {
                    e.Value = ((DateTime)e.Value).ToString("dd/MM/yyyy");
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

        private void tabCustomers_ResetSideBar()
        {
            txtCustomerID.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtCustomerPhoneNumber.Text = string.Empty;
            rtxtCustomerAddress.Text = string.Empty;

            dtpCustomerBirthday.Value = new DateTime(2000, 1, 1);
            dgvCustomerHistoryPurchases.DataSource = null;
        }

        private void dgvCustomerHistoryPurchases_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Format cột ngày mua
            if (e.ColumnIndex == 1)
            {
                e.Value = ((DateTime)e.Value).ToString("dd/MM/yyyy");
            }

            // Format cột tổng tiền
            if (e.ColumnIndex == 2)
            {
                e.Value = StringUtils.FormatMoneyNumber(e.Value.ToString());
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void chkCustomerIsPurchaseSearch_CheckedChanged(object sender, EventArgs e)
        {
            if(chkCustomerIsPurchaseSearch.Checked)
            {
                dtpCustomerPurchaseFromSearch.Enabled = true;
                dtpCustomerPurchaseToSearch.Enabled = true;
            }
            else
            {
                dtpCustomerPurchaseFromSearch.Enabled = false;
                dtpCustomerPurchaseToSearch.Enabled = false;
            }
        }

        private void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            string customerInfo = txtCustomerNameOrPhoneSearch.Text.Trim();
            string dateFrom = dtpCustomerPurchaseFromSearch.Value.ToString("yyyy-MM-dd");
            string dateTo = dtpCustomerPurchaseToSearch.Value.ToString("yyyy-MM-dd");

            DataTable customerTable;
            string message;

            if (chkCustomerIsPurchaseSearch.Checked)
                customerTable = MyPublics.Instance.CallProcedure("Tim_Khach_Hang_Co_Ngay_Mua",
                    out message,
                    ("@searchValue", customerInfo),
                    ("@fromDate", dateFrom),
                    ("@toDate", dateTo));
            else
            {
                customerTable = MyPublics.Instance.CallProcedure("Tim_Khach_Hang",
                    out message,
                    ("@searchValue", customerInfo));
            }

            dgvCustomers.DataSource = customerTable;
            this.dgvCustomers_Formatting();


            if (customerTable.Rows.Count <= 0)
            {
                tabCustomers_ResetSideBar();
                if (message != MyPublics.SUCCESS_MESSAGE)
                    MessageBox.Show($"Lỗi: {message}", "Lỗi", MessageBoxButtons.OK);
            }
        }

        private void btnCustomerResetSearch_Click(object sender, EventArgs e)
        {
            txtCustomerNameOrPhoneSearch.Text = string.Empty;
            dtpCustomerPurchaseFromSearch.Value = new DateTime(2000, 1, 1);
            dtpCustomerPurchaseToSearch.Value = DateTime.Today;

            this.dgvCustomer_LoadData();
        }

        private void btnCustomerAdd_Click(object sender, EventArgs e)
        {
            fCustomerInfo customerInfoForm = new fCustomerInfo();
            customerInfoForm.ToAddFrom();
            DialogResult res = customerInfoForm.ShowDialog();
            if (res == DialogResult.OK)
            {
                this.dgvCustomer_LoadData();
            }
        }

        private void btnCustomerEdit_Click(object sender, EventArgs e)
        {
            fCustomerInfo customerInfoForm = new fCustomerInfo();
            customerInfoForm.ToEditFrom(txtCustomerID.Text);
            DialogResult res = customerInfoForm.ShowDialog();
            if(res == DialogResult.OK)
            {
                this.dgvCustomer_LoadData();
            }
        }
        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show($"Bạn có muốn xóa người dùng {txtCustomerID.Text} không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataTable data = MyPublics.Instance.CallProcedure("DeleteUser",
                    out string message,
                    ("@p_MaUser", txtCustomerID.Text));

                if(message == MyPublics.SUCCESS_MESSAGE)
                {
                    MessageBox.Show($"Xóa người dùng {txtCustomerID.Text} thành công", "Thông báo", MessageBoxButtons.OK);
                    this.dgvCustomer_LoadData();
                }
                else
                {
                    if (message.Contains("foreign key constraint fails"))
                    {
                        MessageBox.Show($"Không thể người dùng", "Thông báo", MessageBoxButtons.OK);
                    }
                    else MessageBox.Show($"Lỗi khi xóa người dùng {message}", "Lỗi", MessageBoxButtons.OK);
                }
            }
        }

        #endregion

        #region Imports Tab
        private void tabImports_Enter(object sender, EventArgs e)
        {
            this.dgvImports_LoadData();
        }

        private void dgvImports_LoadData()
        {
            DataTable importTable = MyPublics.Instance.CallProcedure("usp_hienThiDanhSachPhieuNhap",
                out string message,
                ("@in_count", "200"),
                ("@in_offset", "0"));

            dgvImports.DataSource = importTable;
            this.dgvImports_Format();

            if (importTable.Rows.Count <= 0)
            {
                this.tabImports_ResetSideBar();
                if (message != MyPublics.SUCCESS_MESSAGE)
                    MessageBox.Show($"Lỗi: {message}", "Lỗi", MessageBoxButtons.OK);
            }
        }

        private void dgvImports_Format()
        {
            // Cột Mã
            dgvImports.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

            // Cột Tên nhân viên
            dgvImports.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Cột Tên nhà cung cấp
            dgvImports.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dgvImports_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Cột ngày nhập
            if(e.ColumnIndex == 3)
            {
                e.Value = ((DateTime)e.Value).ToString("dd/MM/yyyy");
            }

            // Cột tổng tiền
            if(e.ColumnIndex == 4)
            {
                e.Value = StringUtils.FormatMoneyNumber(e.Value.ToString());
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void dgvImports_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem hàng và cột có hợp lệ không
            if (e.RowIndex >= 0)
            {
                // Lấy DataGridViewRow của hàng được chọn
                DataGridViewRow row = dgvImports.Rows[e.RowIndex];

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

        private void tabImports_ResetSideBar()
        {
            txtImportID.Text = string.Empty;
            txtImportEmployeeID.Text = string.Empty;
            txtImportSupplierID.Text = string.Empty;
            dtpImportDate.Value = new DateTime(2000, 1, 1);

            txtImportDetailsMedicineName.Text = string.Empty;
            txtImportDetailsMedicineQuantity.Text = string.Empty;
            txtImportDetailsMedicinePrice.Text = string.Empty;

            txtImportTotalPrice.Text = string.Empty;

            dgvImportDetails.DataSource = null;
        }

        private void dgvImportDetails_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvImportDetails.Rows[e.RowIndex];

            txtImportDetailsMedicineName.Text = row.Cells[1].Value.ToString();
            txtImportDetailsMedicineQuantity.Text = row.Cells[2].Value.ToString();
            txtImportDetailsMedicinePrice.Text = StringUtils.FormatMoneyNumber(row.Cells[3].Value.ToString());
        }
        private void dgvImportDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Format cột tổng tiền
            if (e.ColumnIndex == 3)
            {
                e.Value = StringUtils.FormatMoneyNumber(e.Value.ToString());
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void btnImportSearch_Click(object sender, EventArgs e)
        {
            string id = txtImportIDSearch.Text.Trim();
            string employee = txtImportEmployeeSearch.Text.Trim();
            string supplier = txtImportSupplierSearch.Text.Trim();
            string dateFrom = dtpImportDateFromSearch.Value.ToString("yyyy-MM-dd");
            string dateTo = dtpImportDateToSearch.Value.ToString("yyyy-MM-dd");

            if(id != string.Empty)
            {
                if(int.TryParse(id, out int idNumber))
                {

                }
                else
                {
                    if (id[0] != 'I') MessageBox.Show("Mã phiếu nhập lỗi");
                }
            }

            DataTable importTable =
                MyPublics.Instance.CallProcedure("Tim_Phieu_Nhap",
                    out string message,
                    ("@ma_phieu_nhap", id),
                    ("@nhan_vien", employee),
                    ("@ncc", supplier),
                    ("@fromDate", dateFrom),
                    ("@toDate", dateTo));

            dgvImports.DataSource = importTable;
            this.dgvImports_Format();

            if (importTable.Rows.Count <= 0)
            {
                this.tabImports_ResetSideBar();
                if (message != MyPublics.SUCCESS_MESSAGE)
                    MessageBox.Show($"Lỗi: {message}", "Lỗi", MessageBoxButtons.OK);
            }
        }

        private void btnImportResetSearch_Click(object sender, EventArgs e)
        {
            txtImportIDSearch.Text = string.Empty;
            txtImportEmployeeSearch.Text = string.Empty; 
            txtImportSupplierSearch.Text = string.Empty;

            dtpImportDateFromSearch.Value = new DateTime(2000, 1, 1);
            dtpImportDateToSearch.Value = DateTime.Today;

            dgvImports_LoadData();
        }

        private void btnImportAdd_Click(object sender, EventArgs e)
        {
            fImportInfo importInfoForm = new fImportInfo();
            DialogResult res = importInfoForm.ShowDialog();
            if(res == DialogResult.OK)
            {
                this.dgvImports_LoadData();
            }
        }

        private void btnImportDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show($"Bạn có muốn xóa phiếu nhập '{txtImportID.Text}' này không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataTable data = MyPublics.Instance.CallProcedure("DeletePhieuNhap",
                    out string message,
                    ("@p_MaPN", txtImportID.Text));

                if(message == MyPublics.SUCCESS_MESSAGE)
                {
                    MessageBox.Show($"Xóa phiếu nhập '{txtImportID.Text}' thành công", "Thông báo", MessageBoxButtons.OK);
                    this.dgvImports_LoadData();
                }
                else
                {
                    MessageBox.Show($"Lỗi: {message}", "Lỗi", MessageBoxButtons.OK);
                }
            }
        }
        #endregion

        #region Bills Tab
        private void tabBills_Enter(object sender, EventArgs e)
        {
            this.dgvBills_LoadData();

        }

        private void dgvBills_LoadData()
        {
            DataTable billsTable = MyPublics.Instance.CallProcedure("usp_hienThiDanhSachPhieuXuat",
                out string message,
                ("@in_count", "200"),
                ("@in_offset", "0"));

            dgvBills.DataSource = billsTable;
            this.dgvBills_Formating();
            if (billsTable.Rows.Count <= 0)
            {
                tabBills_ResetSideBar();

                if (message != MyPublics.SUCCESS_MESSAGE)
                    MessageBox.Show($"Lỗi: {message}", "Lỗi", MessageBoxButtons.OK);
            }
        }
        private void dgvBills_Formating()
        {
            // Cột mã phiếu 
            dgvBills.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;

            // Cột tên nhân viên
            dgvBills.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Cột tên khách hàng
            dgvBills.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dgvBills_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Cột ngày tháng 
            if(e.ColumnIndex == 3)
            {
                e.Value = ((DateTime)e.Value).ToString("dd/MM/yyyy");
            }

            if(e.ColumnIndex == 4)
            {
                e.Value = StringUtils.FormatMoneyNumber(e.Value.ToString());
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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

                decimal totalPrice = MyPublics.Instance.CallFunction<decimal>("tinhTongSoTienPhieuXuat",
                    ("@in_MaPX", txtBillID.Text));
                txtBillTotalPrice.Text = StringUtils.FormatNumber(totalPrice);
            }
        }

        private void tabBills_ResetSideBar()
        {
            txtBillID.Text = string.Empty;
            txtBillEmployeeID.Text = string.Empty;
            txtBillCustomerID.Text = string.Empty;
            dtpBillDate.Value = new DateTime(2000, 1, 1);

            txtBillDetailsMedicineName.Text = string.Empty;
            txtBillDetailsMedicineQuantity.Text = string.Empty;
            txtBillDetailsMedicinePrice.Text = string.Empty;

            dgvBillDetails.DataSource = null;
        }

        private void dgvBillDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Format cột đơn giá
            if (e.ColumnIndex == 3)
            {
                e.Value = StringUtils.FormatMoneyNumber(e.Value.ToString());
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
                txtBillDetailsMedicinePrice.Text = StringUtils.FormatMoneyNumber(row.Cells[3].Value.ToString());
            }
        }
        private void btnBillSearch_Click(object sender, EventArgs e)
        {
            string id = txtBillIDSearch.Text.Trim();
            string customer = txtBillCustomerNameOrIDSearch.Text.Trim();
            string employee = txtBillEmployeeNameOrIDSearch.Text.Trim();
            string fromDate = dtpBillDateFromSearch.Value.ToString("yyyy-MM-dd");
            string toDate = dtpBillDateToSearch.Value.ToString("yyyy-MM-dd");

            if (id != string.Empty)
            {
                if (int.TryParse(id, out int idNumber))
                {

                }
                else
                {
                    if (id[0] != 'D') MessageBox.Show("Mã phiếu xuất lỗi");
                }
            }

            DataTable billTable =
                MyPublics.Instance.CallProcedure("Tim_Hoa_Don",
                    out string message,
                    ("@ma_phieu_xuat", id),
                    ("@nhan_vien", employee),
                    ("@khach_hang", customer),
                    ("@fromDate", fromDate),
                    ("@toDate", toDate));

            dgvBills.DataSource = billTable;
            if (billTable.Rows.Count <= 0)
            {
                this.tabBills_ResetSideBar();
                if (message != MyPublics.SUCCESS_MESSAGE)
                    MessageBox.Show($"Lỗi: {message}", "Lỗi", MessageBoxButtons.OK);
            }
        }

        private void btnBillResetSearch_Click(object sender, EventArgs e)
        {
            txtBillIDSearch.Text = string.Empty;
            txtBillCustomerNameOrIDSearch.Text = string.Empty;
            txtBillEmployeeNameOrIDSearch.Text = string.Empty;

            dtpBillDateFromSearch.Value = new DateTime(2000, 1, 1);
            dtpBillDateToSearch.Value = DateTime.Today;

            this.dgvBills_LoadData();
        }


        private void btnBillAdd_Click(object sender, EventArgs e)
        {
            fBillInfo billInfoForm = new fBillInfo();
            DialogResult res = billInfoForm.ShowDialog();
            if(res == DialogResult.OK)
            {
                this.dgvBills_LoadData();
            }
        }

        private void btnBillDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Bạn có muốn xóa phiếu xuất '{txtBillID.Text}' này không?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataTable data = MyPublics.Instance.CallProcedure("DeletePhieuXuat",
                    out string message,
                    ("@p_MaPX", txtBillID.Text));

                if (message == MyPublics.SUCCESS_MESSAGE)
                {
                    MessageBox.Show($"Xóa phiếu xuất '{txtBillID.Text}' thành công", "Thông báo", MessageBoxButtons.OK);
                    this.dgvBills_LoadData();
                }
                else
                {
                    MessageBox.Show($"Lỗi: {message}", "Lỗi", MessageBoxButtons.OK);
                }
            }
        }

        #endregion

    }
}
