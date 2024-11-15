using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace QLNhaThuoc.Utils
{
    public static class WinformUtils
    {
        public static List<(string ColumnName, int RowIndex)> GetEmptyCells(this DataGridView dataGridView)
        {
            var emptyCells = new List<(string ColumnName, int RowIndex)>();

            // Duyệt qua từng hàng trong DataGridView
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // Bỏ qua hàng cuối cùng nếu đó là hàng trống mới để thêm dữ liệu
                if (row.IsNewRow) continue;

                // Duyệt qua từng ô trong hàng
                foreach (DataGridViewCell cell in row.Cells)
                {
                    // Kiểm tra nếu ô đó là trống (null hoặc rỗng)
                    if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
                    {
                        // Lấy tên cột và số hàng của ô trống
                        string columnName = dataGridView.Columns[cell.ColumnIndex].Name;
                        int rowIndex = cell.RowIndex;

                        // Thêm thông tin vào danh sách
                        emptyCells.Add((columnName, rowIndex));
                    }
                }
            }

            return emptyCells;
        }


        public static void ExportDataGridViewToExcel(DataGridView dataGridView)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                worksheet.Name = "ExportedData";

                // Xuất tiêu đề cột
                for (int i = 1; i < dataGridView.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dataGridView.Columns[i - 1].HeaderText;
                }

                // Xuất dữ liệu từ DataGridView
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView.Rows[i].Cells[j].Value.ToString();
                    }
                }

                // Lưu file Excel
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Save an Excel File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Đóng Excel
                workbook.Close();
                excelApp.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
