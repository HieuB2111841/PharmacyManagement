namespace QLNhaThuoc
{
    partial class fImportInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fImportInfo));
            this.pImportInfo = new System.Windows.Forms.Panel();
            this.flpImportInfo = new System.Windows.Forms.FlowLayoutPanel();
            this.pEmployee = new System.Windows.Forms.Panel();
            this.lEmployee = new System.Windows.Forms.Label();
            this.tlpEmployee = new System.Windows.Forms.TableLayoutPanel();
            this.txtEmployeeID = new System.Windows.Forms.TextBox();
            this.txtEmployeeName = new System.Windows.Forms.TextBox();
            this.pSupplierID = new System.Windows.Forms.Panel();
            this.lSupplier = new System.Windows.Forms.Label();
            this.tlpSupplier = new System.Windows.Forms.TableLayoutPanel();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.txtSupplierID = new System.Windows.Forms.TextBox();
            this.pImportDetails = new System.Windows.Forms.Panel();
            this.dgvImportDetails = new System.Windows.Forms.DataGridView();
            this.MaThuoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lImportDetails = new System.Windows.Forms.Label();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pImportInfo.SuspendLayout();
            this.flpImportInfo.SuspendLayout();
            this.pEmployee.SuspendLayout();
            this.tlpEmployee.SuspendLayout();
            this.pSupplierID.SuspendLayout();
            this.tlpSupplier.SuspendLayout();
            this.pImportDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImportDetails)).BeginInit();
            this.tlpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pImportInfo
            // 
            this.pImportInfo.BackColor = System.Drawing.Color.Aquamarine;
            this.pImportInfo.Controls.Add(this.flpImportInfo);
            this.pImportInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pImportInfo.Location = new System.Drawing.Point(0, 0);
            this.pImportInfo.Name = "pImportInfo";
            this.pImportInfo.Padding = new System.Windows.Forms.Padding(15);
            this.pImportInfo.Size = new System.Drawing.Size(396, 533);
            this.pImportInfo.TabIndex = 0;
            // 
            // flpImportInfo
            // 
            this.flpImportInfo.BackColor = System.Drawing.Color.White;
            this.flpImportInfo.Controls.Add(this.pEmployee);
            this.flpImportInfo.Controls.Add(this.pSupplierID);
            this.flpImportInfo.Controls.Add(this.pImportDetails);
            this.flpImportInfo.Controls.Add(this.tlpButtons);
            this.flpImportInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpImportInfo.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpImportInfo.Location = new System.Drawing.Point(15, 15);
            this.flpImportInfo.Name = "flpImportInfo";
            this.flpImportInfo.Padding = new System.Windows.Forms.Padding(15);
            this.flpImportInfo.Size = new System.Drawing.Size(366, 503);
            this.flpImportInfo.TabIndex = 1;
            this.flpImportInfo.WrapContents = false;
            // 
            // pEmployee
            // 
            this.pEmployee.Controls.Add(this.lEmployee);
            this.pEmployee.Controls.Add(this.tlpEmployee);
            this.pEmployee.Location = new System.Drawing.Point(18, 18);
            this.pEmployee.Name = "pEmployee";
            this.pEmployee.Size = new System.Drawing.Size(330, 60);
            this.pEmployee.TabIndex = 1;
            // 
            // lEmployee
            // 
            this.lEmployee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lEmployee.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lEmployee.Location = new System.Drawing.Point(0, 0);
            this.lEmployee.Name = "lEmployee";
            this.lEmployee.Size = new System.Drawing.Size(330, 30);
            this.lEmployee.TabIndex = 4;
            this.lEmployee.Text = "Nhân viên";
            this.lEmployee.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpEmployee
            // 
            this.tlpEmployee.ColumnCount = 2;
            this.tlpEmployee.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tlpEmployee.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66F));
            this.tlpEmployee.Controls.Add(this.txtEmployeeID, 0, 0);
            this.tlpEmployee.Controls.Add(this.txtEmployeeName, 1, 0);
            this.tlpEmployee.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlpEmployee.Location = new System.Drawing.Point(0, 30);
            this.tlpEmployee.Margin = new System.Windows.Forms.Padding(0);
            this.tlpEmployee.Name = "tlpEmployee";
            this.tlpEmployee.RowCount = 1;
            this.tlpEmployee.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpEmployee.Size = new System.Drawing.Size(330, 30);
            this.tlpEmployee.TabIndex = 3;
            // 
            // txtEmployeeID
            // 
            this.txtEmployeeID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmployeeID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEmployeeID.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtEmployeeID.Location = new System.Drawing.Point(0, 0);
            this.txtEmployeeID.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.txtEmployeeID.MaxLength = 128;
            this.txtEmployeeID.Name = "txtEmployeeID";
            this.txtEmployeeID.ReadOnly = true;
            this.txtEmployeeID.Size = new System.Drawing.Size(109, 30);
            this.txtEmployeeID.TabIndex = 0;
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmployeeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEmployeeName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtEmployeeName.Location = new System.Drawing.Point(115, 0);
            this.txtEmployeeName.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.txtEmployeeName.MaxLength = 128;
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.ReadOnly = true;
            this.txtEmployeeName.Size = new System.Drawing.Size(215, 30);
            this.txtEmployeeName.TabIndex = 1;
            // 
            // pSupplierID
            // 
            this.pSupplierID.Controls.Add(this.lSupplier);
            this.pSupplierID.Controls.Add(this.tlpSupplier);
            this.pSupplierID.Location = new System.Drawing.Point(18, 84);
            this.pSupplierID.Name = "pSupplierID";
            this.pSupplierID.Size = new System.Drawing.Size(330, 60);
            this.pSupplierID.TabIndex = 3;
            // 
            // lSupplier
            // 
            this.lSupplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lSupplier.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lSupplier.Location = new System.Drawing.Point(0, 0);
            this.lSupplier.Name = "lSupplier";
            this.lSupplier.Size = new System.Drawing.Size(330, 30);
            this.lSupplier.TabIndex = 4;
            this.lSupplier.Text = "Nhà cung cấp";
            this.lSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpSupplier
            // 
            this.tlpSupplier.ColumnCount = 2;
            this.tlpSupplier.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.93939F));
            this.tlpSupplier.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.06061F));
            this.tlpSupplier.Controls.Add(this.txtSupplierName, 0, 0);
            this.tlpSupplier.Controls.Add(this.txtSupplierID, 0, 0);
            this.tlpSupplier.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlpSupplier.Location = new System.Drawing.Point(0, 30);
            this.tlpSupplier.Name = "tlpSupplier";
            this.tlpSupplier.RowCount = 1;
            this.tlpSupplier.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSupplier.Size = new System.Drawing.Size(330, 30);
            this.tlpSupplier.TabIndex = 3;
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSupplierName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSupplierName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtSupplierName.Location = new System.Drawing.Point(114, 0);
            this.txtSupplierName.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.txtSupplierName.MaxLength = 10;
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Size = new System.Drawing.Size(216, 30);
            this.txtSupplierName.TabIndex = 2;
            // 
            // txtSupplierID
            // 
            this.txtSupplierID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSupplierID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSupplierID.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtSupplierID.Location = new System.Drawing.Point(0, 0);
            this.txtSupplierID.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.txtSupplierID.MaxLength = 10;
            this.txtSupplierID.Name = "txtSupplierID";
            this.txtSupplierID.Size = new System.Drawing.Size(108, 30);
            this.txtSupplierID.TabIndex = 1;
            // 
            // pImportDetails
            // 
            this.pImportDetails.Controls.Add(this.dgvImportDetails);
            this.pImportDetails.Controls.Add(this.lImportDetails);
            this.pImportDetails.Location = new System.Drawing.Point(18, 150);
            this.pImportDetails.Name = "pImportDetails";
            this.pImportDetails.Size = new System.Drawing.Size(330, 295);
            this.pImportDetails.TabIndex = 4;
            // 
            // dgvImportDetails
            // 
            this.dgvImportDetails.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Aquamarine;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvImportDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvImportDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvImportDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaThuoc,
            this.SoLuong,
            this.DonGia});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Aquamarine;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvImportDetails.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvImportDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvImportDetails.Location = new System.Drawing.Point(0, 30);
            this.dgvImportDetails.MultiSelect = false;
            this.dgvImportDetails.Name = "dgvImportDetails";
            this.dgvImportDetails.RowHeadersVisible = false;
            this.dgvImportDetails.RowHeadersWidth = 51;
            this.dgvImportDetails.RowTemplate.Height = 24;
            this.dgvImportDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvImportDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvImportDetails.Size = new System.Drawing.Size(330, 265);
            this.dgvImportDetails.TabIndex = 9;
            // 
            // MaThuoc
            // 
            this.MaThuoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MaThuoc.HeaderText = "Mã thuốc";
            this.MaThuoc.MaxInputLength = 5;
            this.MaThuoc.MinimumWidth = 6;
            this.MaThuoc.Name = "MaThuoc";
            this.MaThuoc.ToolTipText = "Mã của thuốc (VD: M0001)";
            // 
            // SoLuong
            // 
            this.SoLuong.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.SoLuong.HeaderText = "Số lượng";
            this.SoLuong.MaxInputLength = 10;
            this.SoLuong.MinimumWidth = 6;
            this.SoLuong.Name = "SoLuong";
            this.SoLuong.ToolTipText = "Số lượng thuốc";
            this.SoLuong.Width = 107;
            // 
            // DonGia
            // 
            this.DonGia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.DonGia.HeaderText = "Đơn giá";
            this.DonGia.MaxInputLength = 20;
            this.DonGia.MinimumWidth = 6;
            this.DonGia.Name = "DonGia";
            this.DonGia.ToolTipText = "Giá của thuốc";
            this.DonGia.Width = 99;
            // 
            // lImportDetails
            // 
            this.lImportDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lImportDetails.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lImportDetails.Location = new System.Drawing.Point(0, 0);
            this.lImportDetails.Name = "lImportDetails";
            this.lImportDetails.Size = new System.Drawing.Size(330, 30);
            this.lImportDetails.TabIndex = 8;
            this.lImportDetails.Text = "Chi tiết";
            this.lImportDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpButtons
            // 
            this.tlpButtons.ColumnCount = 2;
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpButtons.Controls.Add(this.btnCancel, 0, 0);
            this.tlpButtons.Controls.Add(this.btnSave, 1, 0);
            this.tlpButtons.Location = new System.Drawing.Point(18, 451);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.RowCount = 1;
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpButtons.Size = new System.Drawing.Size(330, 41);
            this.tlpButtons.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(3, 4);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(159, 33);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(168, 4);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(159, 33);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // fImportInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 533);
            this.Controls.Add(this.pImportInfo);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "fImportInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thông tin phiếu nhập";
            this.Load += new System.EventHandler(this.fImportInfo_Load);
            this.pImportInfo.ResumeLayout(false);
            this.flpImportInfo.ResumeLayout(false);
            this.pEmployee.ResumeLayout(false);
            this.tlpEmployee.ResumeLayout(false);
            this.tlpEmployee.PerformLayout();
            this.pSupplierID.ResumeLayout(false);
            this.tlpSupplier.ResumeLayout(false);
            this.tlpSupplier.PerformLayout();
            this.pImportDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvImportDetails)).EndInit();
            this.tlpButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pImportInfo;
        private System.Windows.Forms.FlowLayoutPanel flpImportInfo;
        private System.Windows.Forms.Panel pEmployee;
        private System.Windows.Forms.Label lEmployee;
        private System.Windows.Forms.TableLayoutPanel tlpEmployee;
        private System.Windows.Forms.TextBox txtEmployeeID;
        private System.Windows.Forms.TextBox txtEmployeeName;
        private System.Windows.Forms.Panel pSupplierID;
        private System.Windows.Forms.Label lSupplier;
        private System.Windows.Forms.TableLayoutPanel tlpSupplier;
        private System.Windows.Forms.TextBox txtSupplierName;
        private System.Windows.Forms.TextBox txtSupplierID;
        private System.Windows.Forms.Panel pImportDetails;
        private System.Windows.Forms.DataGridView dgvImportDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaThuoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn DonGia;
        private System.Windows.Forms.Label lImportDetails;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}