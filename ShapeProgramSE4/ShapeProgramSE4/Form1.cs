using System;
using System.Collections;
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
        String totalText = "square"; //concept idea, not permanent

        ArrayList shapes = new ArrayList(); //creating an array list
        public Form1()
        {
            InitializeComponent();
            this.Width = 1100;
            this.Height = 760;

            ShapeFactory factory = new ShapeFactory(); //creating new instance of shapeFactory class

            try 
            {
                shapes.Add(factory.getShape("circle"));  //creating circle into array list
                shapes.Add(factory.getShape("square")); //creating square into array list 
                shapes.Add(factory.getShape("rectangle")); //creating rectangle into array list
                shapes.Add(factory.getShape("triangle")); //creating triangle into array list
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Invalid Shape" + e); //if shape doesnt exist exception is thrown
            }


            //can insert shapes here

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Debug.WriteLine("Paint method called"); //for logging
            Graphics g = e.Graphics; //creates graphics object

            setCommand(g, totalText); //example of what could happen
          // g.Dispose(); //closing down graphics
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

            if (totalText.Equals("square"))
            {
               Debug.WriteLine("Square typed"); //for logging
            }

        }

        public void setCommand(Graphics g, String shapeName)
        {
            Shape s;
            ShapeFactory factory1 = new ShapeFactory();

            if (shapeName.Equals("square"))
            {
                s = factory1.getShape("square");
                s.set(Color.Red, 100, 100, 200, 200);
                shapes.Add(s);
                s.draw(g);
            }
        }

        
    }
}
