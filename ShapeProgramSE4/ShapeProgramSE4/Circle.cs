using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// The Circle class extends its base class Shape and implements all of its abstract methods
    /// which are overriden and further developed.
    /// </summary>
    class Circle : Shape
    {
        int radius;

        /// <summary>
        /// Method explicitly calling constructor of shape.
        /// </summary>
        public Circle() : base() 
        { 

        }

        public Circle(Color colour, int x, int y, int radius) : base(colour, x, y)
        {
            this.radius = radius; //setting extra property of circle
        }

        public override void set(Color colour, params int[] list)
        {
            base.set(colour, list[0], list[1]); //calling shape class setting colour, x, y, radius passing it through list
            this.radius = list[2];
        }

        /// <summary>
        /// Draw method to draw shape.
        /// </summary>
        /// <param name="g"></param>
        public override void draw(Graphics g)
        {
            Pen p = new Pen(Color.Black, 2); //creates pen object, pen draws border around a shape
            Brush b = new SolidBrush(colour);  //brush paints the interior of a shape
            g.FillEllipse(b, x, y, radius * 2, radius * 2);
            g.DrawEllipse(p, x, y, radius * 2, radius * 2);
        }

        /// <summary>
        /// Method to calculate area of circle.
        /// </summary>
        /// <returns></returns>
        public override double calcArea()
        {
            return Math.PI * (radius ^ 2); //calculating area
        }

        /// <summary>
        /// Method to calculate circles perimeter.
        /// </summary>
        /// <returns></returns>
        public override double calcPerimeter()
        {
            return 2 * Math.PI * radius; 
        }

        public override string ToString()
        {
            return base.ToString() + " " + this.radius;
        }
    }
}
