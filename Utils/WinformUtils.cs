using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

    }
}
