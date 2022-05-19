using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Pie class extends shape.
    /// Contains all methods appropriate to draw, calculate area, calculate perimeter and set properties of a Pie.
    /// </summary>
    public class Pie : Shape
    {
        int width, height, startAngle = 40, sweepAngle = 90;
        Color colour;

        /// <summary>
        /// Constructor method for Pie class - passing in many parameters and extending base class (Shape)
        /// </summary>
        /// <param name="colour">Colour of pen</param>
        /// <param name="x">X coordinates</param>
        /// <param name="y">Y coordinates</param>
        /// <param name="width">Width of pie</param>
        /// <param name="height">Height of pie</param>
        public Pie(Color colour, int x, int y, int width, int height) : base(colour, x, y)
        {
            this.width = width;
            this.height = height;
            this.colour = colour;
        }

        /// <summary>
        /// Constructor for pie
        /// </summary>
        public Pie()
        {

        }

        /// <summary>
        /// Set method for Pie class.
        /// Sets all parameters to equal user input.
        /// </summary>
        /// <param name="colour">Parameter for colour of Pie</param>
        /// <param name="list">Parameter for integer array list for properties of Pie</param>
        public override void Set(Color colour, params int[] list)
        {
            base.Set(colour, list[0], list[1]); // Calling shape class setting colour, width and height through list
            this.width = list[2];
            this.height = list[3];
            this.colour = colour;
        }

        /// <summary>
        /// Overriding draw method with code to draw specific Pie shape to graphics class
        /// </summary>
        /// <param name="g">Object that creates canvas for user to draw on.</param>
        public override void Draw(Graphics g, String fillFlag)
        {
            Pen p = new Pen(colour, 2); // Creates pen object, pen draws border around a shape
            Brush b = new SolidBrush(colour); // Brrush paints the interior of a shape

            if (fillFlag == "N")
            {
                g.DrawPie(p, 50, 12, width, height, startAngle, sweepAngle); // Drawing triangle passing in points of triangle from array
            }
            if (fillFlag == "Y")
            {
                g.FillPie(b, 50, 12, width, height, startAngle, sweepAngle); // Filling in colour of Pie
            }
        }

        public override double CalcArea()
        {
            throw new NotImplementedException();
        }

        public override double CalcPerimeter()
        {
            throw new NotImplementedException();
        }

        public override void Draw(Graphics g)
        {
            throw new NotImplementedException();
        }

    }
}
