using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Rectangle class extends Shape.
    /// Contains all methods appropriate to draw, calculate area, calculate perimeter and set properties of a Rectangle.
    /// </summary>
    class Rectangle : Shape 
    {
        int width, height;

        /// <summary>
        /// Constructor method for Rectangle class - passing in many parameters and extending base class (Shape)
        /// </summary>
        /// <param name="colour">Parameter for rectangle colour</param>
        /// <param name="x">Parameter for x axis</param>
        /// <param name="y">Parameter for y axis</param>
        /// <param name="width">Parameter for width of Rectangle</param>
        /// <param name="height">Parameter for height of Rectangle</param>
        public Rectangle(Color colour, int x, int y, int width, int height) : base(colour, x, y)
        {
            this.width = width; 
            this.height = height;
        }

        /// <summary>
        /// Constructor class for Rectangle.
        /// </summary>
        public Rectangle()
        {
        }

        /// <summary>
        /// Overriding set method for Rectangle.
        /// </summary>
        /// <param name="colour">Parameter for colour of Rectangle</param>
        /// <param name="list">Parameter for integer array list for properties of Rectangle</param>
        public override void set(Color colour, params int[] list)
        {
            base.set(colour, list[0], list[1]); //calling shape class setting colour, width and height through list
            this.width = list[2];
            this.height = list[3];
        }

        /// <summary>
        /// Overriding draw method with code to draw specific Rectangle shape to graphics class
        /// </summary>
        /// <param name="g"></param>
        public override void draw(Graphics g)
        {
            Pen p = new Pen(Color.Black, 2); //creates pen object, pen draws border around a shape
            Brush b = new SolidBrush(colour);  //brush paints the interior of a shape
            //g.FillRectangle(b, x, y, width, height);  //fills rectangle with brush
            g.DrawRectangle(p, x, y, width, height);   //draws rectangle
        }

        /// <summary>
        /// Method for calculating area of Rectangle. Width times Height.
        /// </summary>
        /// <returns></returns>
        public override double calcArea()
        {
            return width * height; 
        }

        /// <summary>
        /// Method for calculating perimeter of Rectangle. Width plus Height.
        /// </summary>
        /// <returns></returns>
        public override double calcPerimeter()
        {
            return width + height; 
        }

        public override string ToString()
        {
            return base.ToString() + "Rectangle" + " " + this.width + "  " + this.height;
        }
    }
}
