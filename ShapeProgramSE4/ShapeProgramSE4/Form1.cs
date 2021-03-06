using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

        //declaring constant keyword variables
        const string syntDrwTo = "drawto";
        const string syntDrwSqu = "square";
        const string syntDrwRec = "rectangle";
        const string syntDrwTri = "triangle";
        const string syntDrwCir = "circle";
        const string syntMoveTo = "moveto";
        const string syntClr = "clear";
        const string syntRst = "reset";
        const string syntPen = "pen";
        const string syntFil = "fill";
        const string syntOn = "on";
        const string syntOff = "off";
        const string syntVar = "var";
        const string syntWhl = "while";
        const string syntLp = "loop";
        const string syntRn = "run";

        Bitmap OutputBitmap = new Bitmap(mapWidth, mapHeight); // Creating bitmap to draw on inside canvas box
        Canvas MyCanvas; 
        Parser MyParser; 

        String title = "Graphical Programming Language Application c7207442";
        String command = "";

        OpenFileDialog openFileDialog = new OpenFileDialog();
        ColorDialog colorDialog = new ColorDialog();
        Keywords keywords = new Keywords(); 

        /// <summary>
        /// Constructor for form, contains main entry point of code.
        /// Forms dimensions are set here. 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            MyCanvas = new Canvas(Graphics.FromImage(OutputBitmap)); // Assigning canvas to bitmap so it can be drawn on
            MyParser = new Parser(MyCanvas);

            // Setting screen size values
            this.Width = screenWidth;
            this.Height = screenHeight;
        }

        /// <summary>
        /// This method is the Paint method. When the form has been resized it will draw image back in box.
        /// </summary>
        /// <param name="sender">Contains object that raised event.</param>
        /// <param name="e">Passes in paint event data.</param>
        private void canvasBox_Paint(object sender, PaintEventArgs e)
        {
            Debug.WriteLine("Paint method called"); 
            Graphics g = e.Graphics; 
            g.DrawImageUnscaled(OutputBitmap, 0, 0); // Drawing bitmap onto canvasBox
        }

        /// <summary>
        /// Method for when user clicks on the Help button in tool bar of application.
        /// </summary>
        /// <param name="sender">Contains object that raised event.</param>
        /// <param name="e">Passes in pressed key event.</param>
        private void toolStripHelpBtn_Click(object sender, EventArgs e)
        {
            String message = "This application is where you can learn how to program. \n" + "Type in commands from the command list e.g. (DrawTo 200,230) to tell the program to draw a line."
            + "\nClick on the list icon in the tool bar to view the list of commands you can use.";
           
            MessageBox.Show(message, title); // Displaying messagebox to user
        }

        /// <summary>
        /// Method for when user clicks on the List button in tool bar of application.
        /// </summary>
        /// <param name="sender">Contains object that raised event.</param>
        /// <param name="e">Passes in pressed key event.</param>
        private void toolStripListBtn_Click(object sender, EventArgs e)
        {
            String message = "Valid syntax examples" + "\n MoveTo 150,300 - Moves axis to new coordinates \n DrawTo 120,230 - Draws line between given coordinates \n Rectangle 150,200 - Draws rectangle to specified size" +
                " \n Triangle 150,200 - Draws triangle to specified size \n Square 150,150 - Draws square to specified size \n Circle 100 - Draws circle to specified size \n Reset - Moves pen back to top left corner of screen "
                + "\n Clear - Clears canvas \n Pen green - changes pen colour to green \n var apple - creates a variable called apple \n var apple = 40 + 2 - creates a variable called apple with a value of 42 ";
            MessageBox.Show(message, title); // Displaying messagebox to user
        }

        /// <summary>
        /// Text changed method to set keywords to specified colours to let user know that they have typed
        /// a keyword.
        /// </summary>
        /// <param name="sender">Contains object that raised event.</param>
        /// <param name="e">Passes in button click event data</param>
        private void richTxtCmdLine_TextChanged(object sender, EventArgs e)
        {
            keywords.chkKeyword(richTxtCmdLine, syntDrwTo, Color.Green, 0);
            keywords.chkKeyword(richTxtCmdLine, syntDrwSqu, Color.Green, 0);
            keywords.chkKeyword(richTxtCmdLine, syntDrwRec, Color.Green, 0);
            keywords.chkKeyword(richTxtCmdLine, syntDrwTri, Color.Green, 0);
            keywords.chkKeyword(richTxtCmdLine, syntDrwCir, Color.Green, 0);
            keywords.chkKeyword(richTxtCmdLine, syntMoveTo, Color.Green, 0);
            keywords.chkKeyword(richTxtCmdLine, syntRst, Color.Red, 0);
            keywords.chkKeyword(richTxtCmdLine, syntClr, Color.Red, 0);
            keywords.chkKeyword(richTxtCmdLine, syntWhl, Color.Blue, 0);
            keywords.chkKeyword(richTxtCmdLine, syntLp, Color.Blue, 0);
            keywords.chkKeyword(richTxtCmdLine, syntPen, Color.Purple, 0);
            keywords.chkKeyword(richTxtCmdLine, syntFil, Color.Green, 0);
            keywords.chkKeyword(richTxtCmdLine, syntOn, Color.Blue, 0);
            keywords.chkKeyword(richTxtCmdLine, syntOff, Color.Blue, 0);
            keywords.chkKeyword(richTxtCmdLine, syntVar, Color.Brown, 0);
            keywords.chkKeyword(richTxtCmdLine, syntRn, Color.DarkGreen, 0);
        }

        /// <summary>
        /// Text changed method to set keywords to specified colours to let user know that they have typed
        /// a keyword.
        /// </summary>
        /// <param name="sender">Contains object that raised event.</param>
        /// <param name="e">Passes in button click event data</param>
        private void richTxtCmdBox_TextChanged(object sender, EventArgs e)
        {
            keywords.chkKeyword(richTxtCmdBox, syntDrwTo, Color.Green, 0);
            keywords.chkKeyword(richTxtCmdBox, syntDrwSqu, Color.Green, 0);
            keywords.chkKeyword(richTxtCmdBox, syntDrwRec, Color.Green, 0);
            keywords.chkKeyword(richTxtCmdBox, syntDrwTri, Color.Green, 0);
            keywords.chkKeyword(richTxtCmdBox, syntDrwCir, Color.Green, 0);
            keywords.chkKeyword(richTxtCmdLine, syntMoveTo, Color.Green, 0);
            keywords.chkKeyword(richTxtCmdBox, syntRst, Color.Red, 0);
            keywords.chkKeyword(richTxtCmdBox, syntClr, Color.Red, 0);
            keywords.chkKeyword(richTxtCmdBox, syntWhl, Color.Blue, 0);
            keywords.chkKeyword(richTxtCmdBox, syntLp, Color.Blue, 0);
            keywords.chkKeyword(richTxtCmdBox, syntPen, Color.Purple, 0);
            keywords.chkKeyword(richTxtCmdBox, syntFil, Color.Green, 0);
            keywords.chkKeyword(richTxtCmdBox, syntOn, Color.Blue, 0);
            keywords.chkKeyword(richTxtCmdBox, syntOff, Color.Blue, 0);
            keywords.chkKeyword(richTxtCmdLine, syntVar, Color.Brown, 0);
            keywords.chkKeyword(richTxtCmdBox, syntRn, Color.DarkGreen,0);
        }

        /// <summary>
        /// Method to check syntax as user is typing it into cmd box.
        /// If syntax is invalid a message will appear.
        /// </summary>
        /// <param name="sender">Contains object that raised event.</param>
        /// <param name="e">Passes in button click event data</param>
        private void richTxtCmdBox_KeyDown(object sender, KeyEventArgs e)
        {

            String[] allLines = richTxtCmdBox.Text.ToString().ToLower().Trim().Split("\n"); //  Creating string array - split by newline

            if (e.KeyCode == Keys.Enter) // If user hits enter key then input is passed to parser class line by line
            {
                for (int i = 0; i < allLines.Length; i++)
                {
                    MyParser.ParseCommand(allLines[i], false);
                }

                Refresh(); // Update bitmap
            }
        }

        /// <summary>
        /// Method passes user input in cmdLine to parser when user hits enter.
        /// Method will take command and change case to lowercase and remove spaces before it is passed to parser class.
        /// </summary>
        /// <param name="sender">Contains object that raised event.</param>
        /// <param name="e">Passes in button click event data</param>
        private void richTxtCmdLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // If user clicks enter key on command line
            {
                Debug.WriteLine("User hit enter on command line."); 
                e.SuppressKeyPress = true; // Stop windows noise

                command = richTxtCmdLine.Text.ToString().ToLower().Trim(); // Setting string to equal input of user in lowercase
                Debug.WriteLine("command: " + command);

                MyParser.ParseCommand(command, true); //passing command to parser class
                richTxtCmdLine.Text = ""; //clearing down the command line
                Refresh(); //display needs updating
             
            }
        }

        /// <summary>
        /// Method for when user clicks btnRun
        /// Method passes user input to parser class when user clicks run button, it will pass each line in at a time to the Pa
        /// </summary>
        /// <param name="sender">Contains object that raised event.</param>
        /// <param name="e">Passes in button click event data</param>
        private void btnRun_Click(object sender, EventArgs e)
        {
            String[] allLines = richTxtCmdBox.Text.ToString().ToLower().Trim().Split("\n"); // Create string array - split by newline

            for (int i = 0; i < allLines.Length; i++) // Passing user input to parser class line by line
            {
                MyParser.ParseCommand(allLines[i], true);
            }

            Refresh(); // Update display
        }

        /// <summary>
        /// Method to allow user to set canvas pen colour by selecting colour from ColorDialog.
        /// </summary>
        /// <param name="sender">Contains object that raised event.</param>
        /// <param name="e">Passes in button click event data</param>
        private void btnPenCol_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK) // If user clicks OK then color is changed
            {
                MyCanvas.myCol = colorDialog.Color;
                MyCanvas.PenColour(colorDialog.Color);
                Debug.WriteLine("User changed color by colour dialog.");
            }
        }

        /// <summary>
        /// Method for when user clicks save button.
        /// Bitmap image is saved to computer.
        /// </summary>
        /// <param name="sender">Contains object that raised event.</param>
        /// <param name="e">Passes in button click event data</param>
        private void toolStripSaveBtn_Click(object sender, EventArgs e)
        {
            File.WriteAllText(@"C:\Users\myasa\source\repos\ShapeProgramSE4\ShapeProgramSE4\SavedPictures\ProgramCode.txt", richTxtCmdBox.Text); // Using filewritealltext method from file class to create a new file and save to computer
            MessageBox.Show("File saved successfully!", "Saved"); // Inform user that file has been saved
            Debug.WriteLine("User saved file");
        }

        /// <summary>
        /// Method for when user clicks load button.
        /// User will be able to load program code to command text box in application.
        /// </summary>
        /// <param name="sender">Contains object that raised event.</param>
        /// <param name="e">Passes in button click event data</param>
        private void toolStripLoadBtn_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog(); // Displaying dialog box for user to select file
            richTxtCmdBox.Text = File.ReadAllText(openFileDialog.FileName); // Setting cmdBox to text file so user can run code
        }

        /// <summary>
        /// Method for when user clicks Save Canvas button.
        /// Bitmap image is saved to desktop.
        /// </summary>
        /// <param name="sender">Contains object that raised event.</param>
        /// <param name="e">Passes in button click event data</param>
        private void btnSaveCan_Click(object sender, EventArgs e)
        {
            OutputBitmap.Save(@"C:\Users\myasa\source\repos\ShapeProgramSE4\ShapeProgramSE4\SavedPictures\ShapeProgramSave.png", ImageFormat.Png); // Image is saved to desktop
            MessageBox.Show("File saved successfully!", "Saved"); // Inform user that file has been saved
            Debug.WriteLine("User saved file");
        }

        /// <summary>
        /// Method for when user clicks Load Canvas button.
        /// User will be able to load previously saved png image to project.
        /// </summary>
        /// <param name="sender">Contains object that raised event.</param>
        /// <param name="e">Passes in button click event data</param>
        private void btnLoadCan_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            OutputBitmap = new Bitmap(openFileDialog.FileName);
            MyCanvas = new Canvas(Graphics.FromImage(OutputBitmap)); //needs work
            //new Form1();
            // MyCanvas = new Canvas(Graphics.FromImage(OutputBitmap)); //needs work!!!
            Refresh();
        }
    }
}
