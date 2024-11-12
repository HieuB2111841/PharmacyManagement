﻿namespace QLNhaThuoc
{
    partial class fBillInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fBillInfo));
            this.gbBillInfo = new System.Windows.Forms.GroupBox();
            this.flpBillInfo = new System.Windows.Forms.FlowLayoutPanel();
            this.pEmployee = new System.Windows.Forms.Panel();
            this.lEmployee = new System.Windows.Forms.Label();
            this.tlpEmployee = new System.Windows.Forms.TableLayoutPanel();
            this.txtEmployeeID = new System.Windows.Forms.TextBox();
            this.txtEmployeeName = new System.Windows.Forms.TextBox();
            this.pCustomerID = new System.Windows.Forms.Panel();
            this.lCustomerID = new System.Windows.Forms.Label();
            this.txtCustomerID = new System.Windows.Forms.TextBox();
            this.pBillDetails = new System.Windows.Forms.Panel();
            this.dgvBillDetails = new System.Windows.Forms.DataGridView();
            this.MaThuoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lBillDetails = new System.Windows.Forms.Label();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbBillInfo.SuspendLayout();
            this.flpBillInfo.SuspendLayout();
            this.pEmployee.SuspendLayout();
            this.tlpEmployee.SuspendLayout();
            this.pCustomerID.SuspendLayout();
            this.pBillDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillDetails)).BeginInit();
            this.tlpButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbBillInfo
            // 
            this.gbBillInfo.BackColor = System.Drawing.Color.Aquamarine;
            this.gbBillInfo.Controls.Add(this.flpBillInfo);
            this.gbBillInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbBillInfo.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.gbBillInfo.Location = new System.Drawing.Point(0, 0);
            this.gbBillInfo.Name = "gbBillInfo";
            this.gbBillInfo.Padding = new System.Windows.Forms.Padding(15);
            this.gbBillInfo.Size = new System.Drawing.Size(396, 570);
            this.gbBillInfo.TabIndex = 2;
            this.gbBillInfo.TabStop = false;
            this.gbBillInfo.Text = "Thông tin phiếu nhập";
            // 
            // flpBillInfo
            // 
            this.flpBillInfo.BackColor = System.Drawing.Color.White;
            this.flpBillInfo.Controls.Add(this.pEmployee);
            this.flpBillInfo.Controls.Add(this.pCustomerID);
            this.flpBillInfo.Controls.Add(this.pBillDetails);
            this.flpBillInfo.Controls.Add(this.tlpButtons);
            this.flpBillInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpBillInfo.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpBillInfo.Location = new System.Drawing.Point(15, 46);
            this.flpBillInfo.Name = "flpBillInfo";
            this.flpBillInfo.Padding = new System.Windows.Forms.Padding(15);
            this.flpBillInfo.Size = new System.Drawing.Size(366, 509);
            this.flpBillInfo.TabIndex = 0;
            this.flpBillInfo.WrapContents = false;
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
            // pCustomerID
            // 
            this.pCustomerID.Controls.Add(this.lCustomerID);
            this.pCustomerID.Controls.Add(this.txtCustomerID);
            this.pCustomerID.Location = new System.Drawing.Point(18, 84);
            this.pCustomerID.Name = "pCustomerID";
            this.pCustomerID.Size = new System.Drawing.Size(330, 60);
            this.pCustomerID.TabIndex = 3;
            // 
            // lCustomerID
            // 
            this.lCustomerID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lCustomerID.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lCustomerID.Location = new System.Drawing.Point(0, 0);
            this.lCustomerID.Name = "lCustomerID";
            this.lCustomerID.Size = new System.Drawing.Size(330, 30);
            this.lCustomerID.TabIndex = 2;
            this.lCustomerID.Text = "Mã khách hàng";
            this.lCustomerID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomerID.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtCustomerID.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtCustomerID.Location = new System.Drawing.Point(0, 30);
            this.txtCustomerID.MaxLength = 10;
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.Size = new System.Drawing.Size(330, 30);
            this.txtCustomerID.TabIndex = 0;
            // 
            // pBillDetails
            // 
            this.pBillDetails.Controls.Add(this.dgvBillDetails);
            this.pBillDetails.Controls.Add(this.lBillDetails);
            this.pBillDetails.Location = new System.Drawing.Point(18, 150);
            this.pBillDetails.Name = "pBillDetails";
            this.pBillDetails.Size = new System.Drawing.Size(330, 295);
            this.pBillDetails.TabIndex = 4;
            // 
            // dgvBillDetails
            // 
            this.dgvBillDetails.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Aquamarine;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBillDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBillDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBillDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaThuoc,
            this.SoLuong,
            this.DonGia});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Aquamarine;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBillDetails.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBillDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBillDetails.Location = new System.Drawing.Point(0, 30);
            this.dgvBillDetails.MultiSelect = false;
            this.dgvBillDetails.Name = "dgvBillDetails";
            this.dgvBillDetails.RowHeadersVisible = false;
            this.dgvBillDetails.RowHeadersWidth = 51;
            this.dgvBillDetails.RowTemplate.Height = 24;
            this.dgvBillDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvBillDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBillDetails.Size = new System.Drawing.Size(330, 265);
            this.dgvBillDetails.TabIndex = 9;
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
            this.SoLuong.Width = 135;
            // 
            // DonGia
            // 
            this.DonGia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.DonGia.HeaderText = "Đơn giá";
            this.DonGia.MaxInputLength = 20;
            this.DonGia.MinimumWidth = 6;
            this.DonGia.Name = "DonGia";
            this.DonGia.ToolTipText = "Giá của thuốc";
            this.DonGia.Width = 114;
            // 
            // lBillDetails
            // 
            this.lBillDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lBillDetails.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lBillDetails.Location = new System.Drawing.Point(0, 0);
            this.lBillDetails.Name = "lBillDetails";
            this.lBillDetails.Size = new System.Drawing.Size(330, 30);
            this.lBillDetails.TabIndex = 8;
            this.lBillDetails.Text = "Chi tiết";
            this.lBillDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // fBillInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 570);
            this.Controls.Add(this.gbBillInfo);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "fBillInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nhà thuốc số 8";
            this.Load += new System.EventHandler(this.fBillInfo_Load);
            this.gbBillInfo.ResumeLayout(false);
            this.flpBillInfo.ResumeLayout(false);
            this.pEmployee.ResumeLayout(false);
            this.tlpEmployee.ResumeLayout(false);
            this.tlpEmployee.PerformLayout();
            this.pCustomerID.ResumeLayout(false);
            this.pCustomerID.PerformLayout();
            this.pBillDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillDetails)).EndInit();
            this.tlpButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbBillInfo;
        private System.Windows.Forms.FlowLayoutPanel flpBillInfo;
        private System.Windows.Forms.Panel pEmployee;
        private System.Windows.Forms.Panel pCustomerID;
        private System.Windows.Forms.Label lCustomerID;
        private System.Windows.Forms.TextBox txtCustomerID;
        private System.Windows.Forms.Panel pBillDetails;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lEmployee;
        private System.Windows.Forms.TableLayoutPanel tlpEmployee;
        private System.Windows.Forms.TextBox txtEmployeeName;
        private System.Windows.Forms.TextBox txtEmployeeID;
        private System.Windows.Forms.Label lBillDetails;
        private System.Windows.Forms.DataGridView dgvBillDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaThuoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn DonGia;
    }
}