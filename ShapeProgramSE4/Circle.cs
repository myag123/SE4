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
    public class Circle : Shape
    {
        int radius;
        Color colour;

        /// <summary>
        /// Method explicitly calling constructor of shape.
        /// </summary>
        public Circle() : base() 
        { 

        }

        /// <summary>
        /// Constructor method for Circle class - passing in many parameters and extending base class (Shape)
        /// </summary>
        /// <param name="colour">Colour of circle</param>
        /// <param name="x">x axis position</param>
        /// <param name="y">y axis position</param>
        /// <param name="radius">Radius value of circle</param>
        public Circle(Color colour, int x, int y, int radius) : base(colour, x, y)
        {
            this.radius = radius; // Setting extra property of circle
        }

        /// <summary>
        /// Set method to set properties of circle.
        /// Method expects a colour and int to be passed in.
        /// </summary>
        /// <param name="colour">Colour of circle</param>
        /// <param name="list">List of numbers</param>
        public override void Set(Color colour, params int[] list)
        {
            base.Set(colour, list[0], list[1]); // Calling shape class setting colour, x, y, radius passing it through list
            this.radius = list[2];
            this.colour = colour;
        }

        /// <summary>
        /// Draw method to draw circle.
        /// Draw method requires graphics object and a string (Y/N) for fillFlag, to be passed in.
        /// </summary>
        /// <param name="g">Object that creates canvas for user to draw on.</param>
        public override void Draw(Graphics g, String fillFlag)
        {
            Pen p = new Pen(colour, 2); // Creates pen object, pen draws border around a shape
            Brush b = new SolidBrush(colour);  // Brush paints the interior of a shape

            if (fillFlag == "Y") { g.FillEllipse(b, x, y, radius * 2, radius * 2); } // If fillFlag is equal to Y then a solid circle will be drawn
            if (fillFlag == "N") { g.DrawEllipse(p, x, y, radius * 2, radius * 2); } // If fillFlag is equal to N then an outline of a circle will be drawn
        }

        /// <summary>
        /// Method to calculate area of circle.
        /// </summary>
        /// <returns>Returns value of area of circle.</returns>
        public override double CalcArea()
        {
            return Math.PI * (radius ^ 2); 
        }

        /// <summary>
        /// Method to calculate circles perimeter.
        /// </summary>
        /// <returns>Returns value of perimeter of circle.</returns>
        public override double CalcPerimeter()
        {
            return 2 * Math.PI * radius; 
        }

        /// <summary>
        /// Overriding ToString method.
        /// </summary>
        /// <returns>Returns name of class and value of radius.</returns>
        public override string ToString()
        {
            return base.ToString() + " " + this.radius;
        }

        public override void Draw(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}
