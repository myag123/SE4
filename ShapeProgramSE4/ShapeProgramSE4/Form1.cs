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
    /// <summary>
    /// This class loads the main form for the user to use.
    /// The form class contains a range of methods with events that trigger when an event occurs.
    /// </summary>
    public partial class Form1 : Form
    {
        //declaring constant variables
        const int mapWidth = 500;
        const int mapHeight = 448;
        const int screenWidth = 1110;
        const int screenHeight = 760;

        Bitmap OutputBitmap = new Bitmap(mapWidth, mapHeight); //creating bitmap to draw on inside canvas box
        Canvas MyCanvas; //creating object of canvas
        Parser MyParser; //creating object of parser
        //ProcessCommand MyCommand; //creating object of process command

        //String totalText = "square"; //concept idea, not permanent
        ArrayList shapes = new ArrayList(); //creating an array list

        /// <summary>
        /// Constructor for form, contains main entry point of code.
        /// Forms dimensions are set here. 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            MyCanvas = new Canvas(Graphics.FromImage(OutputBitmap)); //assigning canvas to bitmap so it can be drawn on
            //setting screen size values
            this.Width = screenWidth;
            this.Height = screenHeight;

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

        /// <summary>
        /// This method is the Paint method, its used to draw...
        /// </summary>
        /// <param name="sender">Contains object that raised event.</param>
        /// <param name="e">Passes in paint event data.</param>
        private void canvasBox_Paint(object sender, PaintEventArgs e)
        {
            Debug.WriteLine("Paint method called"); //for logging

            Graphics g = e.Graphics; //creates graphics object
            g.DrawImageUnscaled(OutputBitmap, 0, 0); //drawing bitmap onto canvasBox

            // setCommand(g, totalText); //example of what could happen
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
        /// <summary>
        /// Method for button1_click.
        /// When the user clicks 
        /// </summary>
        /// <param name="sender">Contains object that raised event.</param>
        /// <param name="e">Passes in button click event data</param>
        private void button1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("User clicked Run"); //for logging

            // totalText = txtCmdBox.Text.ToString(); //setting text from user to equal string total text
            // Debug.WriteLine(totalText);

            /* if (totalText.Equals("square"))
             {
                Debug.WriteLine("Square typed"); //for logging
             }*/

        }

        public void setCommand(Graphics g, String shapeName)
        {
            /*Shape s;
            ShapeFactory factory1 = new ShapeFactory();

            if (shapeName.Equals("square"))
            {
                s = factory1.getShape("square");
                s.set(Color.Red, 100, 100, 200, 200);
                shapes.Add(s);
                s.draw(g);
            }*/
        }

        private void txtCmdLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //If user clicks enter key on command line
            {
                Debug.WriteLine("User hit enter on command line.");
                String command;
                command = txtCmdLine.Text.ToString().ToLower().Trim(); //setting string to equal input of user in lowercase
                Debug.WriteLine("command" + command);
                MyParser = new Parser(command); //passing typed input into parser class for processing

                txtCmdLine.Text = ""; //clearing down the command line

                //Refresh();//display needs updating
            }
        }

        /// <summary>
        /// Method passes user input to parser class when user hits enter after typing multiple lines in the command box.
        /// </summary>
        /// <param name="sender">Contains object that raised event.</param>
        /// <param name="e">Passes in pressed key event.</param>
        private void txtCmdBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //If user presses enter key
            {
                Debug.WriteLine("User hit enter on command box.");
                String[] allLines = txtCmdBox.Text.ToString().ToLower().Trim().Split("\n"); //create string array, split by newline

                for (int i = 0; i < allLines.Length;) ////passing user input to parser class line by line
                {
                    MyParser = new Parser(allLines[i]);
                    Debug.WriteLine(allLines[i]);
                    i++;
                }

                txtCmdBox.Text = ""; //clearing down the command line

                //Refresh();//display needs updating
            }
        }
    }
}
