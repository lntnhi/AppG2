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
    public partial class frmThongTinSV : Form
    {
        Student student;
        #region Variables for process Image Avt 
        Image img;
        string pathDirecImg;
        string pathAvtImg;
        #endregion

        #region Path Data File
        string pathStudentDataFile;
        string pathHistoryDataFile;
        #endregion
        public frmThongTinSV(string maSV)
        {
            InitializeComponent();
            pathDirecImg = Application.StartupPath + "/Img";
            pathAvtImg = pathDirecImg + "/avatar.png";
            picAvatar.AllowDrop = true;

            pathStudentDataFile = Application.StartupPath + @"\Data\Student.txt";
            pathHistoryDataFile = Application.StartupPath + @"\Data\LearningHistory.txt";

            if (File.Exists(pathAvtImg)) //mở avt
            {
                FileStream fileStream = new FileStream(pathAvtImg, FileMode.Open, FileAccess.Read);
                picAvatar.Image = Image.FromStream(fileStream);
                fileStream.Close();
            }

            bdsQTHT.DataSource = null;
            dtgQTHT.AutoGenerateColumns = false;

            student = StudentService.GetStudent(pathHistoryDataFile, pathStudentDataFile, maSV);
            if (student == null)
            {
                throw new Exception("Không tồn tại sinh viên này");
            }
            else
            {
                txtMaSV.Text = student.IDStudent;
                txtHo.Text = student.LastName;
                txtTen.Text = student.FirstName;
                txtQueQuan.Text = student.POB;
                dtpNgaySinh.Value = student.DOB;
                chkGioiTinh.Checked = student.Gender == GENDER.Male;
                if (student.ListHistoryLearning != null)
                {
                    bdsQTHT.DataSource = student.ListHistoryLearning;
                }
            }
            dtgQTHT.DataSource = bdsQTHT; 
            //sau đó qua bên form click chuột phải vô cái bảng -> Edit Col -> sửa Data Prop thành tên trường của mình đặt
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //nhớ chọn sizemode là StretchImage bên Properties
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn ảnh đại diện";
            ofd.Filter = "File ảnh(*.png,*.jpg)|*.png;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                img = Image.FromFile(ofd.FileName);
                picAvatar.Image = img;
            }
        }

        private void BtnCapNhat_Click(object sender, EventArgs e)
        {
            #region Cập nhật hình đại diện
            bool imgSave = false;
            if (img != null)
            {
                if (!Directory.Exists(pathDirecImg)) //là thư mục chứa ứng dụng của mình hihihi, ktra thư mục của mình có mục Img chưa
                {
                    Directory.CreateDirectory(pathDirecImg);
                }
                img.Save(pathAvtImg);
                imgSave = true;
            }
            #endregion
            if (imgSave)
            {
                MessageBox.Show("Đã cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PicAvatar_DragDrop(object sender, DragEventArgs e) //kéo thả ảnh vào avt
        {
            var rs = (string[])e.Data.GetData(DataFormats.FileDrop); //lấy dữ liệu trên file mình thả vào
            var filePath = rs.FirstOrDefault();
            img = Image.FromFile(filePath);
            picAvatar.Image = img;
        }

        private void PicAvatar_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void MniXoaAvt_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Xóa ảnh đại diện");
            picAvatar.Image = Properties.Resources.a;
            File.Delete(pathAvtImg);
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            var historyLearning = bdsQTHT.Current as HistoryLearning; //as là ép kiểu
            StudentService.deleteHistoryLearning(pathHistoryDataFile, historyLearning.IDHistoryLearning); //xóa trên file
            dtgQTHT.Rows.RemoveAt(dtgQTHT.SelectedRows[0].Index); //xóa trên bảng hiển thị, SelectedRows là những hàng mình chọn trong trường hợp mình chọn nhiều hàng thì nó lấy hàng đầu tiên thôi, Index thì kệ nó
            MessageBox.Show("Đã xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var f= new frmQTHTChiTiet(student.IDStudent, pathHistoryDataFile);
            if (f.ShowDialog() == DialogResult.OK)
            {
                //tiến hành nạp lại DL lên lưới
                student = StudentService.GetStudent(pathHistoryDataFile, pathStudentDataFile, student.IDStudent);
                if (student == null)
                {
                    throw new Exception("Không tồn tại sinh viên này");
                }
                else
                {
                    txtMaSV.Text = student.IDStudent;
                    txtHo.Text = student.LastName;
                    txtTen.Text = student.FirstName;
                    txtQueQuan.Text = student.POB;
                    dtpNgaySinh.Value = student.DOB;
                    chkGioiTinh.Checked = student.Gender == GENDER.Male;
                    if (student.ListHistoryLearning != null)
                    {
                        bdsQTHT.DataSource = student.ListHistoryLearning;
                    }
                }
                dtgQTHT.DataSource = bdsQTHT;
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            var history = bdsQTHT.Current as HistoryLearning; 
            if (history!=null)
            {
                var f = new frmQTHTChiTiet(student.IDStudent, pathHistoryDataFile, history);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    //tiến hành nạp lại DL lên lưới
                    student = StudentService.GetStudent(pathHistoryDataFile, pathStudentDataFile, student.IDStudent);
                    if (student == null)
                    {
                        throw new Exception("Không tồn tại sinh viên này");
                    }
                    else
                    {
                        txtMaSV.Text = student.IDStudent;
                        txtHo.Text = student.LastName;
                        txtTen.Text = student.FirstName;
                        txtQueQuan.Text = student.POB;
                        dtpNgaySinh.Value = student.DOB;
                        chkGioiTinh.Checked = student.Gender == GENDER.Male;
                        if (student.ListHistoryLearning != null)
                        {
                            bdsQTHT.DataSource = student.ListHistoryLearning;
                        }
                    }
                    dtgQTHT.DataSource = bdsQTHT;
                }
            }           
        }
    }
}
