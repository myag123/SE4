using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShapeProgramSE4
{
    class Rectangle : Shape //extends shape
    {
        int width, height; //declaring dimension variables 

        //Constructor, passing in many paramaters including width and height, extending base class shape
        public Rectangle(Color colour, int x, int y, int width, int height) : base(colour, x, y)
        {
            //setting extra width and height properties
            this.width = width; 
            this.height = height;
        }


        //Overriding draw method with code for specific Rectangle shape
        public override void draw(Graphics g)
        {
            Pen p = new Pen(Color.Black, 2); //creates pen object, pen draws border around a shape
            Brush b = new SolidBrush(colour);  //brush paints the interior of a shape

            g.FillRectangle(b, x, y, width, height);  //fills rectangle with brush
            g.DrawRectangle(p, x, y, width, height);   //draws rectangle
        }
    }
}
