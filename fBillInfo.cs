using QLNhaThuoc.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLNhaThuoc
{
    public partial class fBillInfo : Form
    {
        public fBillInfo()
        {
            InitializeComponent();
            this.SetComponents();
        }
        private void SetComponents()
        {
            Font headerFont = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(163)));
            this.dgvBillDetails.ColumnHeadersDefaultCellStyle.Font = headerFont;

            Font cellFont = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(163)));
            this.dgvBillDetails.DefaultCellStyle.Font = cellFont;
        }


        private void fBillInfo_Load(object sender, EventArgs e)
        {
            if (!MyUser.Instance.IsEmployee)
            {
                MessageBox.Show("Bạn không có quyền thêm phiếu nhập!", "Thông báo", MessageBoxButtons.OK);
                this.Close();
            }

            txtEmployeeID.Text = MyUser.Instance.ID;
            txtEmployeeName.Text = MyUser.Instance.Name;
            lCustomerID.Text = "Mã khách hàng (VD: U0001)";
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
                string detailsJson = JsonUtils.ConvertDataGridViewToJson(dgvBillDetails);

                DataTable data = MyPublics.Instance.CallProcedure("InsertPhieuXuatWithDetails", 
                    out string message,
                    ("@MaNhanVien", txtEmployeeID.Text),
                    ("@MaKhachHang", txtCustomerID.Text),
                    ("@NgayXuat", DateTime.Now.ToString("yyyy-MM-dd")),
                    ("@ChiTietPhieuXuat", detailsJson));

                if(message == MyPublics.SUCCESS_MESSAGE)
                {
                    MessageBox.Show("Thêm phiếu nhập thành công", "Thông báo", MessageBoxButtons.OK);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    if (message.Contains("foreign key constraint fails"))
                    {
                        if (message.Contains("MaKhachHang"))
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
            if(txtCustomerID.Text.Length != 5)
            {
                MessageBox.Show("Mã khách hàng chứa 5 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtCustomerID.Text[0] != 'U')
            {
                MessageBox.Show("Mã khách hàng bắt đầu bằng ký tự 'U'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            List<(string ColumnName, int RowIndex)> emptyCells = dgvBillDetails.GetEmptyCells();
            if(emptyCells.Count > 0)
            {
                MessageBox.Show("Có ô trống trong danh sách chi tiết", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            (string ColumnName, int RowIndex, string mess)? incorectCell = dgvBillDetails_CellContentNotCorect();
            if (incorectCell != null)
            {
                MessageBox.Show($"Cột {incorectCell?.ColumnName}, hàng thứ {incorectCell?.RowIndex + 1}: {incorectCell?.mess}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private (string ColumnName, int RowIndex, string mess)? dgvBillDetails_CellContentNotCorect()
        {

            // Duyệt qua từng hàng trong DataGridView
            foreach (DataGridViewRow row in dgvBillDetails.Rows)
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
                            string columnName = dgvBillDetails.Columns[cell.ColumnIndex].Name;
                            int rowIndex = cell.RowIndex;
                            return (columnName, rowIndex, "Mã thuốc chứa 5 ký tự");
                        }
                        if (cell.Value.ToString()[0] != 'M')
                        {
                            // Lấy tên cột và số hàng của ô trống
                            string columnName = dgvBillDetails.Columns[cell.ColumnIndex].Name;
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
                                string columnName = dgvBillDetails.Columns[cell.ColumnIndex].Name;
                                int rowIndex = cell.RowIndex;
                                return (columnName, rowIndex, "Số lượng phải lớn hơn 0");
                            }

                        }
                        else
                        {
                            // Lấy tên cột và số hàng của ô trống
                            string columnName = dgvBillDetails.Columns[cell.ColumnIndex].Name;
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
                                string columnName = dgvBillDetails.Columns[cell.ColumnIndex].Name;
                                int rowIndex = cell.RowIndex;
                                return (columnName, rowIndex, "Đơn giá phải lớn hơn 0");
                            }
                            
                        }
                        else
                        {
                            // Lấy tên cột và số hàng của ô trống
                            string columnName = dgvBillDetails.Columns[cell.ColumnIndex].Name;
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
            if (txtCustomerID.Text != string.Empty) return true;
            if (dgvBillDetails.Rows.Count > 1) return true;
            return false;
        }
    }
}
