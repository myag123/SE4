using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Canvas class handles the programs drawing system. It will control the Pens properties and its positions 
    /// depending on user input. It will also contain all the methods to allow the user to draw a shape. 
    /// </summary>
    public class Canvas
    {
        // Declaring class variables
        Graphics g;
        int xPos, yPos;
        Color bckCol = Color.DeepPink;
        public Color myCol;
        public Pen pen = new Pen(Color.Black, 2);
        public String fillFlag = "";
        SolidBrush b = new SolidBrush(Color.Black);

        const int origPos = 10; // Constant variable to store original position of pen point
        
        /* public Canvas()
         { 

         }*/

        /// <summary>
        /// Canvas constructor, setting properties of canvas.
        /// Expects something to draw on, therefore requires graphics class to be passed in.
        /// </summary>
        /// <param name="g">Object that creates canvas for user to draw on.</param>
        public Canvas(Graphics g)
        {
            this.g = g;
            myCol = Color.Black;
            pen.Color = myCol;

            // Setting default position of pen point
            xPos = origPos;
            yPos = origPos;

            fillFlag = "N"; // Setting default fillFlag set to off
        }

        /// <summary>
        /// Method to draw line to canvas using values of x axis and y axis
        /// </summary>
        /// <param name="toX">x axis position to draw to</param>
        /// <param name="toY">y axis position to draw to</param>
        public void DrawTo(int toX, int toY)
        {
            g.DrawLine(pen, xPos, yPos, toX, toY); // Draws line between points given

            // Updating pens position so pen draws from last position    
            xPos = toX;
            yPos = toY;
        }

        public void DrawString(String msg)
        {
            g.Clear(bckCol);
            g.DrawString(msg, new Font("Arial", 10), b, origPos, origPos);
        }

        /// <summary>
        /// Method to draw square to canvas.
        /// Calls squares set method passing in the current pen color, current x and y position and 
        /// users given height and width.
        /// </summary>
        /// <param name="height">Height value inputted by user</param>
        /// <param name="width">Width value inputted by user</param>
        public void DrawSquare(int height, int width)
        {
            Square square = new Square();
            square.Set(myCol, xPos, yPos, height, width);
            square.Draw(g,fillFlag);
        }

        /// <summary>
        /// Method to draw rectangle to canvas.
        /// Calls rectangles set method passing in the current pen color, current x and y position and 
        /// users given height and width.
        /// </summary>
        /// <param name="height">Height value inputted by user</param>
        /// <param name="width">Width value inputted by user</param>
        public void DrawRectangle(int height, int width)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Set(myCol, xPos, yPos, height, width);
            rectangle.Draw(g,fillFlag);
        }

        /// <summary>
        /// Method that moves the pen point to what the user inputs e.g. moveto 230,150
        /// This allows the pen to draw from that point onwards.
        /// </summary>
        /// <param name="xPos">x axis position</param>
        /// <param name="yPos">y axis position</param>
        public void MoveTo(int xPos, int yPos)
        {
            this.xPos = xPos;
            this.yPos = yPos;
        }

        /// <summary>
        /// Method to set canvas back to original colour - to clear canvas.
        /// </summary>
        public void Clear()
        {
            g.Clear(bckCol);
            //DrawString(" ");
        }

        /// <summary>
        /// Method to reset pen back to its original points. (top left of canvas)
        /// </summary>
        public void Reset()
        {
            xPos = origPos;
            yPos = origPos;
        }

        /// <summary>
        /// Method to change Pen Colour.
        /// </summary>
        /// <param name="color">Color of pen</param>
        public void PenColour(Color color)
        {
            myCol = color;
            this.pen.Color = myCol;
            Debug.WriteLine("Color: " + myCol);
        }

        /// <summary>
        /// Method to draw circle to canvas.
        /// Calls circles set method passing in the current pen color, current x and y position and 
        /// users given radius.
        /// </summary>
        /// <param name="radius">Radius value of circle.</param>
        public void DrawCircle(int radius)
        {
            Circle circle = new Circle();
            circle.Set(myCol, xPos, yPos, radius);
            circle.Draw(g, fillFlag);
        }

        /// <summary>
        /// Method to draw triangle to canvas.
        /// Calls triangles set method passing in the current pen color, current x and y position and 
        /// users given height and width.
        /// </summary>
        /// <param name="width">Width value inputted by user</param>
        /// <param name="height">Height value inputted by user</param>
        public void DrawTriangle(int width, int height)
        {
            //could get the user to input the width and height of the triangle and then get a box
            //to pop up for the user to input values inside such as idk

            Triangle triangle = new Triangle();

            triangle.Set(myCol, xPos, yPos, width, height);
            triangle.Draw(g, fillFlag);
        }
    }
}
