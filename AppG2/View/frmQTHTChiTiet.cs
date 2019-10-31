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
    public partial class frmQTHTChiTiet : Form
    {
        HistoryLearning history;
        string studentID;
        string pathHistoryFile;
        public frmQTHTChiTiet(string studentID, string pathHistoryFile, HistoryLearning history = null)
        {
            InitializeComponent();
            this.history = history;
            this.studentID = studentID;
            this.pathHistoryFile = pathHistoryFile;
            if (history != null)
            {
                //chỉnh sửa
                this.Text = "Chỉnh sửa QTHT"; //tiêu đề
                numTuNam.Value = history.YearFrom;
                numDenNam.Value = history.YearEnd;
                txtNoiHoc.Text = history.Address;
            } 
            else
            {
                //Thêm mới
                this.Text = "Thêm mới QTHT";
            }
        }

        private void BtnDongY_Click(object sender, EventArgs e)
        {
            var yearFrom = numTuNam.Value;
            var yearEnd = numDenNam.Value;
            var address = txtNoiHoc.Text;
            if (history != null)
            {
                //cập nhật               
                StudentService.editHistory((int)yearFrom, (int)yearEnd, address, studentID, pathHistoryFile, history.IDHistoryLearning);
            }
            else
            {
                //thêm mới
                StudentService.addHistory((int)yearFrom, (int)yearEnd, address, studentID, pathHistoryFile);
            }
            MessageBox.Show("Đã cập nhật thành công");
            DialogResult = DialogResult.OK; // đóng form
        }
    }
}
