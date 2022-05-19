using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Triangle class extends shape.
    /// Contains all methods appropriate to draw, calculate area, calculate perimeter and set properties of a Triangle.
    /// </summary>
    public class Triangle : Shape
    {
        int width, height;
        Point[] pts = new Point[3]; //creating points for triangle shape
        Color colour;


        /// <summary>
        /// Constructor method for Triangle class - passing in many parameters and extending base class (Shape)
        /// </summary>
        /// <param name="colour">Colour of pen</param>
        /// <param name="x">X coordinates</param>
        /// <param name="y">Y coordinates</param>
        /// <param name="width">Width of triangle</param>
        /// <param name="height">Height of triangle</param>
        public Triangle(Color colour, int x, int y, int width, int height) : base(colour, x, y)
        {
            this.width = width;
            this.height = height;
            this.colour = colour;
        }

        /// <summary>
        /// Constructor for triangle
        /// </summary>
        public Triangle()
        {

        }

        public override void Set(Color colour, params int[] list)
        {
            base.Set(colour, list[0], list[1]); // Calling shape class setting colour, x, y, radius passing it through list
            this.width = list[2];
            this.height = list[3];
            this.colour = colour;
        }

        /// <summary>
        /// Method overriding draw method, with code to draw specific Polygon triangle shape to graphics class.
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g, String fillFlag)
        {
            Pen p = new Pen(colour, 2); // Creates pen object, pen draws border around a shape
            Brush b = new SolidBrush(colour); // Brush paints the interior of a shape

            // Setting points to form a triangle
            pts[0].X = width / 2;
            pts[0].Y = height / 2 - 50;
            pts[1].X = width / 2 + 50;
            pts[1].Y = height / 2 + 50;
            pts[2].X = width / 2 - 50;
            pts[2].Y = height / 2 + 50;

            if (fillFlag == "N")
            {
                g.DrawPolygon(p, pts); // Drawing triangle passing in points of triangle from array
            }
            if (fillFlag == "Y")
            {
                g.FillPolygon(b, pts); // Filling in colour of triangle
            }
        }

        /// <summary>
        /// Method to calculate area of triangle.
        /// </summary>
        /// <returns>Area value</returns>
        public override double CalcArea()
        {
           return height * width / 2;
        }

        /// <summary>
        /// Method to calculate perimeter of triangle.
        /// </summary>
        /// <returns>Returns perimeter value.</returns>
        public override double CalcPerimeter()
        {
            return 0; 
        }

        /// <summary>
        /// Overriding ToString method.
        /// </summary>
        /// <returns>Returns name of class and value of width and height.</returns>
        public override string ToString()
        {
            return base.ToString() + " " + this.width + "  " + this.height;
        }

        public override void Draw(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}
