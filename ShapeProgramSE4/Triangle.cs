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
    class Triangle : Shape
    {
        int width, height;
        Point[] pts = new Point[3]; //creating points for triangle shape

        public Triangle() :base() //explicitly calling shape constructor
        {

        }

        public Triangle(Color colour, int x, int y, int width, int height) : base(colour, x, y)
        {
            this.width = width;
            this.height = height;

            //Setting points to form a triangle
            pts[0].X = width / 2;
            pts[0].Y = height / 2 - 50;
            pts[1].X = width / 2 + 50;
            pts[1].Y = height / 2 + 50;
            pts[2].X = width / 2 - 50;
            pts[2].Y = height / 2 + 50;
        }

        public override void set(Color colour, params int[] list)
        {
            base.set(colour, list[0], list[1]); //calling shape class setting colour, x, y, radius passing it through list
            this.width = list[2];
            this.height = list[3];
        }

        /// <summary>
        /// Method overriding draw method, with code to draw specific Polygon triangle shape to graphics class.
        /// </summary>
        /// <param name="g"></param>
        public override void draw(Graphics g)
        {
            Pen p = new Pen(Color.Black, 2); //creates pen object, pen draws border around a shape
            Brush b = new SolidBrush(colour); //brush paints the interior of a shape

            g.FillPolygon(b, pts); //filling in colour of triangle
            g.DrawPolygon(p, pts); //drawing triangle passing in points of triangle from array
            
        }

        public override double calcArea()
        {
           return height * width / 2;
        }

        public override double calcPerimeter()
        {
            return 0; //fill in later
        }

        public override string ToString()
        {
            return base.ToString() + " " + this.width + "  " + this.height;
        }
    }
}
