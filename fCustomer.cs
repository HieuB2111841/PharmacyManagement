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
        }

        #region From 
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
                dtpProfileBirthday.Text = MyUser.Instance.Birthday.ToShortDateString();
            }
            
        }
        #endregion

        #region History Tab
        private void tabHistory_Enter(object sender, EventArgs e)
        {
            if (dgvHistory.DataSource != null) return;

            DataTable historyData = MyPublics.Instance.CallProcedure("usp_hienThiLichSuMuaThuoc", ("@in_maKhachHang", MyUser.Instance.ID));
            if (historyData.Rows.Count > 0)
            {
                dgvHistory.DataSource = historyData;
            }
        }

        private void dgvHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // cột tổng tiền
            if(e.ColumnIndex == 3)
            {
                e.Value = StringUtils.FormatMoneyNumber(e.Value.ToString());
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
                    dgvInfoBillDetails.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
            }
        }

        #endregion

    }
}
