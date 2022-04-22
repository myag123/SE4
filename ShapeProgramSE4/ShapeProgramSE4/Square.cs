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
    class Square : Rectangle 
    {
        private int size; //only one size can be applied to all sides of a square

        public Square() :base() //explicitly calling constructor rectangle
        { 

        }

        public Square(Color colour, int x, int y, int size) : base(colour, x, y, size, size)
        {
            this.size = size;
        }

        /// <summary>
        /// References draw method in Rectangle class 
        /// </summary>
        /// <param name="g"></param>
        public override void draw(Graphics g)
        {
            base.draw(g);
        }
    }
}
