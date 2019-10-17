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

namespace AppG2
{
    public partial class Slide_Show_Image___ThuyNhi : Form
    {
        #region Variables for process Image Avt 
        Image img;
        string pathDirecImg;
        string pathAvtImg;
        string[] filePaths;
        int counter = 0;
        Timer T;
        #endregion
        public Slide_Show_Image___ThuyNhi()
        {
            InitializeComponent();
            pathDirecImg = Application.StartupPath + "/Img";
            pathAvtImg = pathDirecImg + "/avatar.png";
            picAnh.AllowDrop = true;
            if (File.Exists(pathAvtImg)) //mở avt
            {
                FileStream fileStream = new FileStream(pathAvtImg, FileMode.Open, FileAccess.Read);
                picAnh.Image = Image.FromStream(fileStream);
                fileStream.Close();
            }
        }

        private void LnkPic_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) //chọn 1 folder ảnh
        {
            //nhớ chọn sizemode là StretchImage bên Properties
            OpenFileDialog ofd = new OpenFileDialog();
            FolderBrowserDialog path = new FolderBrowserDialog();
            DialogResult res = path.ShowDialog();
            filePaths = Directory.GetFiles(path.SelectedPath);
            foreach (var filename in filePaths)
            {
                Bitmap bmp = null;
                bmp = new Bitmap(filename);
            }

            T = new Timer();
            T.Tick += new EventHandler(PlayTime);
            T.Start();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
        }

        private void Slide_Show_Image___ThuyNhi_Load(object sender, EventArgs e)
        {
                
        }
        void PlayTime(object sender, EventArgs e)
        {
            T.Interval = 3000;
            picAnh.Image = Image.FromFile(filePaths[counter]);
            counter++;
            if (counter >= filePaths.Length) counter = 0;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
