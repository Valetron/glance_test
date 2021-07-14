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
    }
}
