using QLNhaThuoc.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace QLNhaThuoc
{
    public partial class fImportInfo : Form
    {
        public fImportInfo()
        {
            InitializeComponent();
        }


        private void fImportInfo_Load(object sender, EventArgs e)
        {
            if (!MyUser.Instance.IsEmployee)
            {
                MessageBox.Show("Bạn không có quyền thêm phiếu nhập!", "Thông báo", MessageBoxButtons.OK);
                this.Close();
            }

            txtEmployeeID.Text = MyUser.Instance.ID;
            txtEmployeeName.Text = MyUser.Instance.Name;
            lSupplierID.Text = "Mã nhà cung cấp (VD: F0001)";
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
            if(IsValidate())
            {
                string detailsJson = JsonUtils.ConvertDataGridViewToJson(dgvImportDetails);

                DataTable data = MyPublics.Instance.CallProcedure("InsertPhieuNhapWithDetails", 
                    out string message,
                    ("@MaNhanVien", txtEmployeeID.Text),
                    ("@MaNhaCungCap", txtSupplierID.Text),
                    ("@NgayNhap", DateTime.Now.ToString("yyyy-MM-dd")),
                    ("@ChiTietPhieuNhap", detailsJson));

                if(message == "Success")
                {
                    MessageBox.Show("Thêm phiếu nhập thành công", "Thông báo", MessageBoxButtons.OK);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    if (message.Contains("foreign key constraint fails"))
                    {
                        if (message.Contains("MaNhaCungCap"))
                        {
                            MessageBox.Show($"Nhà cung cấp không tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        if(message.Contains("MaThuoc"))
                        {
                            MessageBox.Show($"Thuốc không tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                        MessageBox.Show($"Lỗi: {message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool IsValidate()
        {
            if(txtSupplierID.Text.Length != 5)
            {
                MessageBox.Show("Mã nhà cung cấp chứa 5 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtSupplierID.Text[0] != 'F')
            {
                MessageBox.Show("Mã nhà cung cấp bắt đầu bằng ký tự 'F'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            List<(string ColumnName, int RowIndex)> emptyCells = dgvImportDetails.GetEmptyCells();
            if(emptyCells.Count > 0)
            {
                MessageBox.Show("Có ô trống trong danh sách chi tiết", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            (string ColumnName, int RowIndex, string mess)? incorectCell = dgvImportDetails_CellContentNotCorect();
            if (incorectCell != null)
            {
                MessageBox.Show($"Cột {incorectCell?.ColumnName}, hàng thứ {incorectCell?.RowIndex + 1}: {incorectCell?.mess}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private (string ColumnName, int RowIndex, string mess)? dgvImportDetails_CellContentNotCorect()
        {

            // Duyệt qua từng hàng trong DataGridView
            foreach (DataGridViewRow row in dgvImportDetails.Rows)
            {
                // Bỏ qua hàng cuối cùng nếu đó là hàng trống mới để thêm dữ liệu
                if (row.IsNewRow) continue;

                // Duyệt qua từng ô trong hàng
                foreach (DataGridViewCell cell in row.Cells)
                {
                    // Cột mã thuốc
                    if(cell.ColumnIndex == 0)
                    {
                        if(cell.Value.ToString().Length != 5)
                        {
                            // Lấy tên cột và số hàng của ô trống
                            string columnName = dgvImportDetails.Columns[cell.ColumnIndex].Name;
                            int rowIndex = cell.RowIndex;
                            return (columnName, rowIndex, "Mã thuốc chứa 5 ký tự");
                        }
                        if (cell.Value.ToString()[0] != 'M')
                        {
                            // Lấy tên cột và số hàng của ô trống
                            string columnName = dgvImportDetails.Columns[cell.ColumnIndex].Name;
                            int rowIndex = cell.RowIndex;
                            return (columnName, rowIndex, "Mã thuốc bắt đầu bằng ký tự 'M'");
                        }
                    }

                    // Cột số lượng
                    if (cell.ColumnIndex == 1)
                    {
                        if (int.TryParse(cell.Value.ToString(), out int res))
                        {
                            if (res < 0)
                            {
                                // Lấy tên cột và số hàng của ô trống
                                string columnName = dgvImportDetails.Columns[cell.ColumnIndex].Name;
                                int rowIndex = cell.RowIndex;
                                return (columnName, rowIndex, "Số lượng phải lớn hơn 0");
                            }

                        }
                        else
                        {
                            // Lấy tên cột và số hàng của ô trống
                            string columnName = dgvImportDetails.Columns[cell.ColumnIndex].Name;
                            int rowIndex = cell.RowIndex;
                            return (columnName, rowIndex, "Số lượng không phải số");
                        }
                    }

                    // Cột đơn giá
                    if (cell.ColumnIndex == 2)
                    {
                        if (int.TryParse(cell.Value.ToString(), out int res))
                        {
                            if(res < 0)
                            {
                                // Lấy tên cột và số hàng của ô trống
                                string columnName = dgvImportDetails.Columns[cell.ColumnIndex].Name;
                                int rowIndex = cell.RowIndex;
                                return (columnName, rowIndex, "Đơn giá phải lớn hơn 0");
                            }
                            
                        }
                        else
                        {
                            // Lấy tên cột và số hàng của ô trống
                            string columnName = dgvImportDetails.Columns[cell.ColumnIndex].Name;
                            int rowIndex = cell.RowIndex;
                            return (columnName, rowIndex, "Đơn giá không phải số");
                        }
                    }
                }
            }
            return null;
        }

        private bool IsEdited()
        {
            if (txtSupplierID.Text != string.Empty) return true;
            if (dgvImportDetails.Rows.Count > 1) return true;
            return false;
        }
    }
}
