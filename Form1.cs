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
            images = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.png");
        }

        private void pictureBox1_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MessageBox.Show("R");
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Load(openFileDialog1.FileName);
                    MessageBox.Show("W " + pictureBox1.Image.Width + " H " + pictureBox1.Image.Height);
                    this.Text = "Glance at " + Path.GetFullPath(openFileDialog1.FileName);
                }
                catch
                {
                    MessageBox.Show("Неверный формат файла.");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // MessageBox.Show(Application.ExecutablePath); <- photo
            if (!Application.ExecutablePath.EndsWith("exe"))
            {
                pictureBox1.Load(Application.ExecutablePath);
            }
        }
    }
}
