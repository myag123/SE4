using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeProgramSE4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Width = 1100;
            this.Height = 600;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Console.WriteLine("Paint method called"); //for logging
            Graphics g = e.Graphics; //creates graphics object
        }
    }
}
