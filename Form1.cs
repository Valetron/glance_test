using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Gla
{
    public partial class Form1 : Form
    {
        private string path;
        private int curIndexImage = 0;
        private string[] files;
        private static string[] extensions = { "jpg", "jpeg", "png", "gif", "bmp" };
        private static int scale = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private Image Zoom()
        {
            Bitmap newImg = new Bitmap(pictureBox1.Image, pictureBox1.Image.Width * 10, pictureBox1.Image.Height * 10);
            Graphics gr = Graphics.FromImage(pictureBox1.Image);
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //pictureBox1.Image = newImg;
            return newImg;
        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Shift && e.Delta == 120) // zoom in
            {
                //MessageBox.Show("Zoom in");
                pictureBox1.Width += 5;
                pictureBox1.Height += 5;
            }
            else if (ModifierKeys == Keys.Shift && e.Delta == -120) // zoom out
            {
                pictureBox1.Width-=5;
                pictureBox1.Height-=5;
            }
            else if (ModifierKeys == Keys.Control && e.Delta == 120 && pictureBox1.Image != null) // show next
            {
                if (curIndexImage == files.Length - 1) { curIndexImage = 0; }
                else { curIndexImage++; }
                pictureBox1.Load(files[curIndexImage]);
                this.Text = "Glance at " + files[curIndexImage];
            }
            else if (ModifierKeys == Keys.Control && e.Delta == -120 && pictureBox1.Image != null) // show prev
            {
                if (curIndexImage == 0) { curIndexImage = files.Length - 1; }
                else { curIndexImage--; }
                pictureBox1.Load(files[curIndexImage]);
                this.Text = "Glance at " + files[curIndexImage];
            }  
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Load(openFileDialog1.FileName);
                    this.Text = "Glance at " + Path.GetFullPath(openFileDialog1.FileName);
                }
                catch
                {
                    pictureBox1.Image = null;
                    this.Text = "Glance";
                    MessageBox.Show("Неверный формат файла.");
                }
            }
        }

        private static String[] GetFiles(String folder)
        {
            List<String> images = new List<String>();
            foreach (var filter in extensions)
            {
                images.AddRange(Directory.GetFiles(folder, String.Format("*.{0}", filter)));
            }
            return images.ToArray();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
            {
                var img = args[args.Length - 1];
                if (File.Exists(img))
                {
                    pictureBox1.Load(img);
                    this.Text = "Glance at " + Path.GetFullPath(img);

                    files = GetFiles(Path.GetDirectoryName(img));
                    foreach (var i in files)
                    {
                        if (i != Path.GetFullPath(img)) { curIndexImage++; }
                    }
                }
            }
            else { files = GetFiles(Directory.GetCurrentDirectory()); }
        }
    }
}
