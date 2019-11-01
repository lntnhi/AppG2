using AppG2.Controller;
using AppG2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppG2.View
{
    public partial class frmContactCT : Form
    {
        string pathFile;
        Contacts contact;
        public frmContactCT(string pathFile, Contacts contact = null)
        {
            InitializeComponent();
            this.pathFile = pathFile;
            this.contact = contact;
            if (contact != null)
            {
                //chỉnh sửa
                this.Text = "Chỉnh sửa";
                txtName.Text = contact.Name;
                txtPhone.Text = contact.Phone;
                txtEmail.Text = contact.Email;
            }
            else
            {
                //Thêm mới
                this.Text = "Thêm mới";
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            var name = txtName.Text;
            var phone = txtPhone.Text;
            var email = txtEmail.Text;
            if (contact != null)
            {
                //cập nhật 
                ContactService.edit(name, phone, email, pathFile,contact.IDContact);
            }
            else
            {
                //thêm mới
                ContactService.add(name, phone, email, pathFile);
            }
            MessageBox.Show("Đã cập nhật thành công");
            DialogResult = DialogResult.OK; // đóng form
        }
    }
}
