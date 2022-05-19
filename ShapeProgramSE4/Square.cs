using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ShapeProgramSE4
{
    /// <summary>
    /// Square class extends rectangle as a square shares most of the same properties with a rectangle. 
    /// Only difference is that one size is applied to all sides of a square.
    /// </summary>
    public class Square : Rectangle 
    {
        private int size; // Only one size can be applied to all sides of a square

        public Square() :base() // Explicitly calling constructor rectangle
        { 

        }

        /// <summary>
        /// Constructor method for square class - passing in parameters of same value and extending base class (Shape)
        /// </summary>
        /// <param name="colour">Parameter for square colour</param>
        /// <param name="x">Parameter for x axis</param>
        /// <param name="y">Parameter for y axis</param>
        /// <param name="width">Parameter for width of square</param>
        /// <param name="height">Parameter for height of square</param>
        public Square(Color colour, int x, int y, int size) : base(colour, x, y, size, size)
        {
            this.size = size;
        }

        /// <summary>
        /// References draw method in Rectangle class 
        /// </summary>
        /// <param name="g">Grpahics</param>
        public override void Draw(Graphics g, String fillFlag)
        {
            base.Draw(g,fillFlag);
        }
    }
}
