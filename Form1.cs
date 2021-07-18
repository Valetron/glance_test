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
        private string[] images;

        public Form1()
        {
            InitializeComponent();
            //images = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.png");
        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            this.Text = e.Delta.ToString();
            if (pictureBox1.Image != null)
            {
                //pictureBox1.Load(pictureBox1.Image.Size / e.Delta);
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
                }
            }
        }
    }
}
