using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Canvas class handles the programs drawing system. It will control the Pen's properties and its positions 
    /// depending on user input. It will also contain all the methods to allow the user to draw a shape. 
    /// </summary>
    class Canvas
    {
        //Declaring properties of canvas
        Graphics g;
        Pen Pen;
        Pen turtlePen;
        int xPos, yPos, xOldPos, yOldPos;
        SolidBrush brush = new SolidBrush(Color.Black);
        ShapeFactory factory = null; //trying to set factory

        /// <summary>
        /// Canvas constructor, setting properties of canvas.
        /// Expects something to draw on, therefore requires graphics class to be passed in.
        /// </summary>
        /// <param name="g">Object that creates canvas for user to draw on.</param>
        public Canvas(Graphics g)
        {
            this.g = g;
            Pen = new Pen(Color.Black, 2);
            //position of pen point
            xPos = 10;
            yPos = 10;
            xOldPos = 10;
            yOldPos = 10;
            
        }

        /// <summary>
        /// Method to draw line to canvas using values of x axis and y axis
        /// </summary>
        /// <param name="toX">x axis position to draw to</param>
        /// <param name="toY">y axis position to draw to</param>
        public void drawLine(int toX, int toY)
        {
            g.DrawLine(Pen, xPos, yPos, toX, toY); //draws line between points 

            //updating pens position so pen draws from last position    
            xPos = toX;
            yPos = toY;
        }

        public void drawSquare(int height, int width)
        {
           // g.DrawRectangle(Pen, xPos, yPos, height, width); needs work!
            Square square = new Square();
            Color coll = Color.Black;
            square.set(coll, xPos, yPos, height, width);
            square.draw(g);
        }

        /// <summary>
        /// Method that moves the pen point to what the user inputs e.g. moveto 230,150
        /// The pen will draw from that point onwards.
        /// </summary>
        /// <param name="xPos">x axis position</param>
        /// <param name="yPos">y axis position</param>
        public void moveTo(int xPos, int yPos)
        {
            this.xPos = xPos;
            this.yPos = yPos;
        }


        public void moveTurtle(int xOldPos, int yOldPos, int xPos, int yPos)
        {
            turtlePen = new Pen(Color.Red, 2);
            g.FillRectangle(brush, xOldPos, yOldPos, 3, 3);
            g.DrawRectangle(Pen, xPos, yPos, 3, 3);
        }

        public void Clear()
        {
            g.Clear(Color.DeepPink);
        }

        /*public void drawCircle(int radius)//needs work!!!
        {
            //factory = new ShapeFactory();
            Color coll = Color.Black;

            Circle circle = new Circle();
            circle.set(coll, radius);
            circle.draw(g);
        }*/

        public void drawTriangle(int width, int height)
        {
            //could get the user to input the width and height of the triangle and then get a box
            //to pop up for the user to input values inside such as idk
            Color coll = Color.Black;

            Triangle triangle = new Triangle();

            triangle.set(coll, width, height);
            triangle.draw(g);
        }
            

    }
}
