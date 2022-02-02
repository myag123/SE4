using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeProgramSE4
{
    public partial class Form1 : Form
    {
        String totalText = null;
        public Form1()
        {
            InitializeComponent();
            this.Width = 1100;
            this.Height = 760;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Debug.WriteLine("Paint method called"); //for logging
            Graphics g = e.Graphics; //creates graphics object

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("Form loaded");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("User typing commands");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("User clicked Run"); //for logging
           
            totalText = textBox1.Text.ToString(); //setting text from user to equal string total text
            Debug.WriteLine(totalText);
        }
    }
}
