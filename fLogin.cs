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
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void fLogin_Load(object sender, EventArgs e)
        {
            if(MyPublics.Instance.ConnectDatabase(out string error))
            {
                
            }
            else
            {
                MessageBox.Show(error, "Error");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //string account = txtAccount.Text;
            //string password = txtPassword.Text;

            //if(account == string.Empty && password == string.Empty)
            //    MessageBox.Show("Hãy nhập SDT và mật khẩu", "Error");
            //else if (account == string.Empty) MessageBox.Show("Hãy nhập SDT", "Error");
            //else if (password == string.Empty) MessageBox.Show("Hãy nhập mật khẩu", "Error");
            //else
            //{
                fMain formMain = new fMain();
                this.Hide();
                formMain.ShowDialog();
                this.ResetForm();
                this.Show();
            //}

        }

        private void ResetForm()
        {
            txtAccount.Text = string.Empty; 
            txtPassword.Text = string.Empty;
        }
    }
}
