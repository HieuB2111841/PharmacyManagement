using QLNhaThuoc.Models;
using System;
using System.Windows.Forms;

namespace QLNhaThuoc
{
    public partial class fMedicine : Form
    {
        private Medicine _medicine;

        public fMedicine()
        {
            InitializeComponent();
        }

        public void ToAddFrom()
        {
            gbMedicineInfo.Text = "Nhập thông tin thuốc";
            lMedicineID.Text = "Mã thuốc (Được tạo tự động)";
            lSupplierID.Text = "Mã Nhà cung cấp (VD: F0001)";
            lManufacturerID.Text = "Mã hãng sản xuất (VD: B0001)";
        }

        public void ToEditFrom(string medicineID)
        {
            _medicine = new Medicine(medicineID);

            txtMedicineID.Text = _medicine.ID;
            txtMedicineName.Text = _medicine.Name;
            txtlMedicineType.Text = _medicine.Type;
            txtSupplierID.Text = _medicine.SupplierID;
            txtManufacturerID.Text = _medicine.ManufacturerID;
            rtxtMedicineEffect.Text = _medicine.Effect;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn hủy thay đổi không?", "Hủy thay đổi", MessageBoxButtons.OKCancel) == DialogResult.OK)
                this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
