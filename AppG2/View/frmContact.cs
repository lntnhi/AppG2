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
    public partial class frmContact : Form
    {
        string pathFile;
        public frmContact()
        {
            InitializeComponent();
            pathFile = Application.StartupPath + @"\Data\Contacts.txt";

            bdsContact.DataSource = null;
            dtgContact.AutoGenerateColumns = false;

            updateData();
        }

        private void updateData(string search=null)
        {
            List<Contacts> listContacts = ContactService.GetContactDB(search);
            if (listContacts == null)
            {
                throw new Exception("Không tồn tại mảng");
            }
            else
            {
                bdsContact.DataSource = listContacts;
            }
            dtgContact.DataSource = bdsContact;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var contact = bdsContact.Current as Contacts;
            if (contact != null)
            {
                var f = new frmContactCT(contact);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    updateData();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var f = new frmContactCT();
            if (f.ShowDialog() == DialogResult.OK)
            {
                updateData();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Có chắc chắn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                var contact = bdsContact.Current as Contacts;
                ContactService.DeleteContactDB(contact.IDContact);
                updateData();
                MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            updateData(txtSearch.Text);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
