using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace QLNhaThuoc.Utils
{
    public static class JsonUtils
    {
        public static string ConvertDataGridViewToJson(DataGridView dataGridView)
        {
            // Tạo một danh sách các đối tượng từ các hàng của DataGridView
            var rows = new List<Dictionary<string, object>>();

            // Duyệt qua từng hàng trong DataGridView
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // Bỏ qua hàng trống hoặc hàng cuối cùng (nếu là hàng mới để nhập dữ liệu)
                if (row.IsNewRow) continue;

                // Tạo một từ điển để lưu trữ dữ liệu của hàng
                var rowData = new Dictionary<string, object>();

                // Duyệt qua từng ô (cell) trong hàng
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string columnName = dataGridView.Columns[cell.ColumnIndex].Name;
                    // Thêm dữ liệu của ô vào từ điển với tên cột làm khóa
                    rowData[columnName] = cell.Value;
                }

                // Thêm hàng vào danh sách
                rows.Add(rowData);
            }

            // Chuyển đổi danh sách thành JSON
            return JsonConvert.SerializeObject(rows, Formatting.Indented);
        }
    }

}
