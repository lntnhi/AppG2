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
            addNewLabel();
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

        public void addNewLabel()
        {
            flowLayoutPanel1.Controls.Clear();
            List<string> listLabelDuplicate = new List<string>();
            var listContactNoSort = ContactService.GetContactDB();

            //Them vao mang cac chu cai dau tien cua name
            foreach (var item in listContactNoSort)
            {
                listLabelDuplicate.Add(item.Character);
            }

            //Loai bo cac phan tu trung nhau
            List<String> labels = listLabelDuplicate.Distinct().ToList();

            //Sap xep lai mang
            labels.Sort();

            //Tao label moi 
            for (int i = 0; i < labels.Count; i++)
            {
                Label lbl = new Label();
                lbl.Text = labels[i];
                lbl.Click += new System.EventHandler(this.label_Click);
                flowLayoutPanel1.Controls.Add(lbl);
            }
        }

        private void label_Click(object sender, EventArgs e)
        {
            var labelName = ((Label)sender).Text;
            var listContactNoSort = ContactService.GetContactInAlphabetic(labelName);
            var newContactList = listContactNoSort.OrderBy(x => x.Name).ToList();
            bdsContact.DataSource = newContactList;
            bdsContact.ResetBindings(true);
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
