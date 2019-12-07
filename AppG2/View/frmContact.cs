using AppG2.Controller;
using AppG2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppG2.View
{
    public partial class frmContact : Form
    {
        string pathFile;
        User user;
        public frmContact(User user)
        {
            this.user = user;
            InitializeComponent();
            pathFile = Application.StartupPath + @"\Data\Contacts.txt";

            bdsContact.DataSource = null;
            dtgContact.AutoGenerateColumns = false;

            updateData(user);
        }

        private void updateData(User user, string search=null)
        {
            List<Contacts> listContacts = ContactService.GetContactDB(user, search);
            if (listContacts == null)
            {
                throw new Exception("Không tồn tại mảng");
            }
            else
            {
                bdsContact.DataSource = listContacts;
            }
            dtgContact.DataSource = bdsContact;
            addNewLabel();
        }

        public void addNewLabel()
        {
            flowLayoutPanel1.Controls.Clear();
            List<string> listLabelDuplicate = new List<string>();
            var listContact = ContactService.GetContactDB(user);

            //Them vao mang cac chu cai dau tien cua name
            foreach (var item in listContact)
            {
                listLabelDuplicate.Add(item.Character);
            }

            //Loai bo cac phan tu trung nhau
            List<string> labels = listLabelDuplicate.Distinct().ToList();

            //Tao label moi 
            for (int i = 0; i < labels.Count; i++)
            {
                Label lbl = new Label();
                lbl.Text = labels[i];
                lbl.Click += new System.EventHandler(this.label_Click); //EventHandler là gọi đến sự kiện gì đó
                flowLayoutPanel1.Controls.Add(lbl);
            }
        }

        private void label_Click(object sender, EventArgs e)
        {
            var labelName = ((Label)sender).Text; //sender đại diện cho đối tượng của event, ở đây là label
            var listContact = ContactService.GetContactInAlphabetic(user, labelName);
            bdsContact.DataSource = listContact;
            bdsContact.ResetBindings(true);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var contact = bdsContact.Current as Contacts;
            if (contact != null)
            {
                var f = new frmContactCT(user, contact);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    updateData(user);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var f = new frmContactCT(user);
            if (f.ShowDialog() == DialogResult.OK)
            {
                updateData(user);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Có chắc chắn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                var contact = bdsContact.Current as Contacts;
                ContactService.DeleteContactDB(contact.IDContact);
                updateData(user);
                MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            updateData(user, txtSearch.Text);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            List<Contacts> listContacts = ContactService.GetContactDB(user);
            ContactService.Import(user, listContacts);
            updateData(user);
        }
    }
}
