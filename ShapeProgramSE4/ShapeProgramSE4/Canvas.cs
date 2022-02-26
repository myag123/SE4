using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Canvas class handles the programs drawing system. It will control the Pen's properties and its positions 
    /// depending on user input.
    /// </summary>
    class Canvas
    {
        //Declaring properties of canvas
        Graphics g;
        Pen Pen;
        int xPos = 0; //position of pen point
        int yPos = 0;
        

        /// <summary>
        /// Canvas constructor, setting properties of canvas.
        /// Expects something to draw on, therefore requires graphics class to be passed in.
        /// </summary>
        /// <param name="g">Object that creates canvas for user to draw on.</param>
        public Canvas(Graphics g)
        {
            this.g = g;
            xPos = 0;
            yPos = 0;
            Pen = new Pen(Color.Black, 2); //apply constant later
        }

        /// <summary>
        /// Method to draw line to canvas using values of x axis and y axis
        /// </summary>
        /// <param name="toX">x axis position to draw to</param>
        /// <param name="toY">y axis position to draw to</param>
        public void DrawLine(int toX, int toY)
        {
            g.DrawLine(Pen, xPos, yPos, toX, toY); //draws line between points 
            //updating pens position so pen draws from last position    
            xPos = toX;
            yPos = toY;
        }

        public void DrawSquare(int width)
        {
          //  g.DrawRectangle();  
        }
            

    }
}
