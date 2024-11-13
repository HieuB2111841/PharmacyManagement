using QLNhaThuoc.Models;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLNhaThuoc
{
    public partial class fMedicineInfo : Form
    {
        private Medicine _medicine;
        private bool _isEditMode = false;

        public fMedicineInfo()
        {
            InitializeComponent();
        }

        public void ToAddFrom()
        {
            this.Text = "Nhập thông tin thuốc";
            lSupplierID.Text = "Mã Nhà cung cấp (VD: F0001)";
            lManufacturerID.Text = "Mã hãng sản xuất (VD: B0001)";
            pMedicineID.Visible = false;

            this.Size = new Size(this.Size.Width, this.Size.Height - pMedicineID.Height);

            _isEditMode = false;
        }

        public void ToEditFrom(string medicineID)
        {
            _medicine = new Medicine(medicineID);

            txtMedicineID.Text = _medicine.ID;
            txtMedicineName.Text = _medicine.Name;
            txtlMedicineTypeID.Text = _medicine.Type;
            txtSupplierID.Text = _medicine.SupplierID;
            txtManufacturerID.Text = _medicine.ManufacturerID;
            rtxtMedicineEffect.Text = _medicine.Effect;

            _isEditMode = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (IsEdited())
            {
                if(MessageBox.Show("Bạn có muốn hủy thay đổi không?", "Hủy thay đổi", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                // Edit Mode
                if(_isEditMode)
                {
                    this.EditMedicine();
                }
                // Add Mode
                else
                {
                    this.AddMedicine();
                }
            }
        }


        private void EditMedicine()
        {
            DataTable data = MyPublics.Instance.CallProcedure("EditThuoc",
                    out string message,
                    ("@p_MaThuoc", txtMedicineID.Text),
                    ("@p_TenThuoc", txtMedicineName.Text),
                    ("@p_MaHangSX", txtManufacturerID.Text),
                    ("@p_MaNhaCungCap", txtSupplierID.Text),
                    ("@p_CongDung", rtxtMedicineEffect.Text),
                    ("@p_MaLoai", txtlMedicineTypeID.Text));

            if (message.Equals(MyPublics.SUCCESS_MESSAGE))
            {
                MessageBox.Show("Sửa thông tin thuốc thành công", "Thông báo", MessageBoxButtons.OK);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(message, "Lỗi trong quá trình sửa thông tin thuốc", MessageBoxButtons.OK);
            }
        }

        private void AddMedicine()
        {
            DataTable data = MyPublics.Instance.CallProcedure("AddThuoc",
                    out string message,
                    ("@p_TenThuoc", txtMedicineName.Text),
                    ("@p_MaHangSX", txtManufacturerID.Text),
                    ("@p_MaNhaCungCap", txtSupplierID.Text),
                    ("@p_CongDung", rtxtMedicineEffect.Text),
                    ("@p_MaLoai", txtlMedicineTypeID.Text));

            if (message.Equals(MyPublics.SUCCESS_MESSAGE))
            {
                MessageBox.Show("Thêm thuốc thành công", "Thông báo", MessageBoxButtons.OK);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(message, "Lỗi trong quá trình thêm thuốc", MessageBoxButtons.OK);
            }
        }


        private bool IsValidate()
        {
            // Medicine name
            if (string.IsNullOrEmpty(txtMedicineName.Text))
            {
                MessageBox.Show("Tên thuốc không được trống", "Lỗi", MessageBoxButtons.OK);
                return false;
            }

            // Medicine Type
            if (txtlMedicineTypeID.Text.Length != 5)
            {
                MessageBox.Show("Mã loại thuốc chứa 5 ký tự", "Lỗi", MessageBoxButtons.OK);
                return false;
            }
            if (txtlMedicineTypeID.Text[0] != 'T')
            {
                MessageBox.Show("Mã loại thuốc bắt đầu bằng ký tự 'T'", "Lỗi", MessageBoxButtons.OK);
                return false;
            }

            // Medicine Supplier
            if (txtSupplierID.Text.Length != 5)
            {
                MessageBox.Show("Mã nhà cung cấp chứa 5 ký tự", "Lỗi", MessageBoxButtons.OK);
                return false;
            }
            if (txtSupplierID.Text[0] != 'F')
            {
                MessageBox.Show("Mã nhà cung cấp bắt đầu bằng ký tự 'F'", "Lỗi", MessageBoxButtons.OK);
                return false;
            }

            // Medicine Manufacturer
            if (txtManufacturerID.Text.Length != 5)
            {
                MessageBox.Show("Mã hãng sản xuất chứa 5 ký tự", "Lỗi", MessageBoxButtons.OK);
                return false;
            }
            if (txtManufacturerID.Text[0] != 'B')
            {
                MessageBox.Show("Mã hãng sản xuất bắt đầu bằng ký tự 'B'", "Lỗi", MessageBoxButtons.OK);
                return false;
            }

            // Medicine Effect
            if (string.IsNullOrEmpty(rtxtMedicineEffect.Text))
            {
                MessageBox.Show("Công dụng không được để trống", "Lỗi", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private bool IsEdited()
        {
            if(_isEditMode)
            {
                if (txtMedicineID.Text != _medicine.ID) return true;
                if (txtMedicineName.Text != _medicine.Name) return true;
                if (txtlMedicineTypeID.Text != _medicine.Type) return true;
                if (txtSupplierID.Text != _medicine.SupplierID) return true;
                if (txtManufacturerID.Text != _medicine.ManufacturerID) return true;
                if (rtxtMedicineEffect.Text != _medicine.Effect) return true;

                return false;
            }
            else
            {
                if (txtMedicineID.Text != string.Empty) return true;
                if (txtMedicineName.Text != string.Empty) return true;
                if (txtlMedicineTypeID.Text != string.Empty) return true;
                if (txtSupplierID.Text != string.Empty) return true;
                if (txtManufacturerID.Text != string.Empty) return true;
                if (rtxtMedicineEffect.Text != string.Empty) return true;

                return false;
            }
        }
    }
}
