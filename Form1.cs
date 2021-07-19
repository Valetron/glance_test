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

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Shift && e.Delta == 120) // zoom in
            {
                MessageBox.Show("Zoom in");
            }
            else if (ModifierKeys == Keys.Shift && e.Delta == -120) // zoom out
            {
                MessageBox.Show("Zoom out");
            }
            else if (ModifierKeys == Keys.Control && e.Delta == 120) // show next
            {
                if (curIndexImage == files.Length - 1) { curIndexImage = 0; }
                else { curIndexImage++; }
                pictureBox1.Load(files[curIndexImage]);
                this.Text = "Glance at " + files[curIndexImage];
            }
            else if (ModifierKeys == Keys.Control && e.Delta == -120) // show prev
            {
                if (curIndexImage == 0) { curIndexImage = files.Length - 1; }
                else { curIndexImage--; }
                pictureBox1.Load(files[curIndexImage]);
                this.Text = "Glance at " + files[curIndexImage];
            }
            /*this.Text = e.Delta.ToString();
            if (pictureBox1.Image != null)
            {
                //pictureBox1.Load(pictureBox1.Image.Size / e.Delta);
            }*/

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
            else
            {
                files = GetFiles(Directory.GetCurrentDirectory());
            }
        }
    }
}
